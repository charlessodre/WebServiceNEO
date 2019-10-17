//////////////////////////////////////////////////////////////////////////////
// This source code and all associated files and resources are copyrighted by
// the author(s). This source code and all associated files and resources may
// be used as long as they are used according to the terms and conditions set
// forth in The Code Project Open License (CPOL), which may be viewed at
// http://www.blackbeltcoder.com/Legal/Licenses/CPOL.
//
// Copyright (c) 2011 Jonathan Wood
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
	public enum FtpDirectoryFormat
	{
		Unix,
		Windows,
		Unknown
	}

	public class FtpDirectoryEntry
	{
		public string Name;
		public DateTime CreateTime;
		public bool IsDirectory;
		public Int64 Size;
		public string Group;	// UNIX only
		public string Owner;
		public string Flags;
	}

	/// <summary>
	/// General, easy-to-use FTP class.
	/// </summary>
	public class FtpClient
	{
		protected FtpDirectory _host;

		/// <summary>
		/// Gets or sets the current FTP domain and optional directory
		/// </summary>
		public string Host
		{
			set { _host.SetUrl(value); }
			get { return _host.GetUrl(); }
		}

		/// <summary>
		/// Gets or sets the login username.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the login password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Indicates if the current directory is the
		/// root directory.
		/// </summary>
		public bool IsRootDirectory
		{
			get { return _host.IsRootDirectory; }
		}

		// Construction
		public FtpClient()
		{
			_host = new FtpDirectory();
		}

		/// <summary>
		/// Returns a directory listing of the current working directory.
		/// </summary>
		public List<FtpDirectoryEntry> ListDirectory()
		{
			FtpWebRequest request = GetRequest();
			request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

			string listing;
			using (FtpWebResponse response = request.GetResponse() as FtpWebResponse)
			{
				StreamReader sr = new StreamReader(response.GetResponseStream(),
					System.Text.Encoding.ASCII);
				listing = sr.ReadToEnd();
				response.Close();
			}
			return ParseDirectoryListing(listing);
		}

		/// <summary>
		/// Changes the current working directory. If directory starts with "/" then it
		/// is relative to the root directory. If directory is ".." then it refers to
		/// the parent directory.</param>
		/// </summary>
		/// <param name="directory">Directory to make active.</param>
		public void ChangeDirectory(string directory)
		{
			_host.CurrentDirectory = directory;
		}

		/// <summary>
		/// Indicates if the specified directory exists. This function returns false
		/// if a filename existing with the given name.
		/// </summary>
		/// <param name="directory">Directory to test. May be relative or absolute.</param>
		/// <returns></returns>
		public bool DirectoryExists(string directory)
		{
			try
			{
				FtpWebRequest request = GetRequest(directory);
				request.Method = WebRequestMethods.Ftp.ListDirectory;

				using (FtpWebResponse response = request.GetResponse() as FtpWebResponse)
				{
					StreamReader sr = new StreamReader(response.GetResponseStream(),
						System.Text.Encoding.ASCII);
					sr.ReadToEnd();
					sr.Close();
					response.Close();
				}
				return true;
			}
			catch { }
			return false;
		}

		/// <summary>
		/// Creates the specified directory. This method will create multiple levels of
		/// subdirectories as needed.
		/// </summary>
		/// <param name="directory">Directory to create. May be relative or absolute.</param>
		public void CreateDirectory(string directory)
		{
			// Get absolute directory
			directory = _host.ApplyDirectory(directory);

			// Split into path components
			string[] steps = directory.Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);

			// Build list of full paths to each path component
			List<string> paths = new List<string>();
			for (int i = 1; i <= steps.Length; i++)
				paths.Add(FtpDirectory.ForwardSlash + String.Join(FtpDirectory.ForwardSlash, steps, 0, i));

			// Find first path component that needs creating
			int createIndex;
			for (createIndex = paths.Count; createIndex > 0; createIndex--)
			{
				if (DirectoryExists(paths[createIndex - 1]))
						break;
			}

			// Created needed paths
			for ( ; createIndex < paths.Count; createIndex++)
			{
				FtpWebRequest request = GetRequest(paths[createIndex]);
				request.Method = WebRequestMethods.Ftp.MakeDirectory;
				FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				response.Close();
			}
		}

		/// <summary>
		/// Uploads the given list of files to the current working directory.
		/// </summary>
		/// <param name="paths">List of local files to upload</param>
		public void UploadFiles(params string[] paths)
		{
			foreach (string path in paths)
			{
				FtpWebRequest request = GetRequest(Path.GetFileName(path));
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.UseBinary = true;

				FileInfo info = new FileInfo(path);
				request.ContentLength = info.Length;

				// Create buffer for file contents
				int buffLength = 16384;
				byte[] buff = new byte[buffLength];

				// Upload this file
				using (FileStream instream = info.OpenRead())
				{
					using (Stream outstream = request.GetRequestStream())
					{
						int bytesRead = instream.Read(buff, 0, buffLength);
						while (bytesRead > 0)
						{
							outstream.Write(buff, 0, bytesRead);
							bytesRead = instream.Read(buff, 0, buffLength);
						}
						outstream.Close();
					}
					instream.Close();
				}

				FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				response.Close();
			}
		}

		/// <summary>
		/// Downloads the given list of files to the specified local target path
		/// </summary>
		/// <param name="path">Location where downloaded files will be saved</param>
		/// <param name="files">Names of files to download from current FTP directory</param>
		public void DownloadFiles(string path, params string[] files)
		{
			foreach (string file in files)
			{
				FtpWebRequest request = GetRequest(file);
				request.Method = WebRequestMethods.Ftp.DownloadFile;
				request.UseBinary = true;

				using (FileStream outstream = new FileStream(Path.Combine(path, file), FileMode.Create))
				{
					FtpWebResponse response = (FtpWebResponse)request.GetResponse();
					using (Stream instream = response.GetResponseStream())
					{
						int buffLength = 16384;
						byte[] buffer = new byte[buffLength];

						int bytesRead = instream.Read(buffer, 0, buffLength);
						while (bytesRead > 0)
						{
							outstream.Write(buffer, 0, bytesRead);
							bytesRead = instream.Read(buffer, 0, buffLength);
						}
						instream.Close();
					}
					outstream.Close();
					response.Close();
				}
			}
		}

		/// <summary>
		/// Deletes the given list of files from the current working directory.
		/// </summary>
		/// <param name="files">List of files to delete.</param>
		public void DeleteFiles(params string[] files)
		{
			foreach (string file in files)
			{
				FtpWebRequest request = GetRequest(file);
				request.Method = WebRequestMethods.Ftp.DeleteFile;
				FtpWebResponse response = (FtpWebResponse)request.GetResponse();
				response.Close();
			}
		}

		/// <summary>
		/// Deletes the specified directory. The directory should be empty.
		/// </summary>
		/// <param name="files">Directory to delete.</param>
		public void DeleteDirectory(string directory)
		{
			FtpWebRequest request = GetRequest(directory);
			request.Method = WebRequestMethods.Ftp.RemoveDirectory;
			FtpWebResponse response = (FtpWebResponse)request.GetResponse();
			response.Close();
		}

		#region Protected helper methods

		// Constructs an FTP web request
		protected FtpWebRequest GetRequest()
		{
			return GetRequest("");
		}

		// Constructs an FTP web request with the given filename
		protected FtpWebRequest GetRequest(string filename)
		{
			string url = _host.GetUrl(filename);
			FtpWebRequest request = WebRequest.Create(url) as FtpWebRequest;
			request.Credentials = new NetworkCredential(Username, Password);
			request.Proxy = null;
			request.KeepAlive = false;
			return request;
		}

		delegate FtpDirectoryEntry ParseLine(string lines);

		// Converts a directory listing to a list of FtpDirectoryEntrys
		protected List<FtpDirectoryEntry> ParseDirectoryListing(string listing)
		{
			ParseLine parseFunction = null;
			List<FtpDirectoryEntry> entries = new List<FtpDirectoryEntry>();
			string[] lines = listing.Split('\n');
			FtpDirectoryFormat format = GuessDirectoryFormat(lines);

			if (format == FtpDirectoryFormat.Windows)
				parseFunction = ParseWindowsDirectoryListing;
			else if (format == FtpDirectoryFormat.Unix)
				parseFunction = ParseUnixDirectoryListing;

			if (parseFunction != null)
			{
				foreach (string line in lines)
				{
					if (line.Length > 0)
					{
						FtpDirectoryEntry entry = parseFunction(line);
						if (entry.Name != "." && entry.Name != "..")
							entries.Add(entry);
					}
				}
			}
			return entries; ;
		}

		// Attempts to determine the directory format.
		protected FtpDirectoryFormat GuessDirectoryFormat(string[] lines)
		{
			foreach (string s in lines)
			{
				if (s.Length > 10 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
				{
					return FtpDirectoryFormat.Unix;
				}
				else if (s.Length > 8 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
				{
					return FtpDirectoryFormat.Windows;
				}
			}
			return FtpDirectoryFormat.Unknown;
		}

		// Parses a line from a Windows-format listing
		// 
		// Assumes listing style as:
		// 02-03-04  07:46PM       <DIR>          Append
		protected FtpDirectoryEntry ParseWindowsDirectoryListing(string text)
		{
			FtpDirectoryEntry entry = new FtpDirectoryEntry();

			text = text.Trim();
			string dateStr = text.Substring(0, 8);
			text = text.Substring(8).Trim();
			string timeStr = text.Substring(0, 7);
			text = text.Substring(7).Trim();
			entry.CreateTime = DateTime.Parse(String.Format("{0} {1}", dateStr, timeStr));
			if (text.Substring(0, 5) == "<DIR>")
			{
				entry.IsDirectory = true;
				text = text.Substring(5).Trim();
			}
			else
			{
				entry.IsDirectory = false;
				int pos = text.IndexOf(' ');
				entry.Size = Int64.Parse(text.Substring(0, pos));
				text = text.Substring(pos).Trim();
			}
			entry.Name = text;  // Rest is name

			return entry;
		}

		// Parses a line from a UNIX-format listing
		// 
		// Assumes listing style as:
		// dr-xr-xr-x   1 owner    group               0 Nov 25  2002 bussys
		protected FtpDirectoryEntry ParseUnixDirectoryListing(string text)
		{
			// Assuming record style as
			// dr-xr-xr-x   1 owner    group               0 Nov 25  2002 bussys
			FtpDirectoryEntry entry = new FtpDirectoryEntry();
			string processstr = text.Trim();
			entry.Flags = processstr.Substring(0, 9);
			entry.IsDirectory = (entry.Flags[0] == 'd');
			processstr = (processstr.Substring(11)).Trim();
			CutSubstringWithTrim(ref processstr, ' ', 0);   //skip one part
			entry.Owner = CutSubstringWithTrim(ref processstr, ' ', 0);
			entry.Group = CutSubstringWithTrim(ref processstr, ' ', 0);
			CutSubstringWithTrim(ref processstr, ' ', 0);   //skip one part
			entry.CreateTime = DateTime.Parse(CutSubstringWithTrim(ref processstr, ' ', 8));
			entry.Name = processstr;   //Rest of the part is name
			return entry;
		}

		// Removes the token ending in the specified character
		protected string CutSubstringWithTrim(ref string s, char c, int startIndex)
		{
			int pos = s.IndexOf(c, startIndex);
			if (pos < 0) pos = s.Length;
			string retString = s.Substring(0, pos);
			s = (s.Substring(pos)).Trim();
			return retString;
		}

		#endregion

	}

	/// <summary>
	/// Helper class for managing current FTP directory.
	/// </summary>
	public class FtpDirectory
	{
		// Static members
		protected static char[] _slashes = { '/', '\\' };
		public static string BackSlash = "\\";
		public static string ForwardSlash = "/";

		// Member variables
		protected string _domain;	// No trailing slash
		protected string _cwd;		// Leading, no trailing slash
		public string Domain { get { return _domain; } }

		// Construction
		public FtpDirectory()
		{
			_domain = String.Empty;
			_cwd = ForwardSlash;	// Root directory
		}

		/// <summary>
		/// Determines if the current directory is the root directory.
		/// </summary>
		public bool IsRootDirectory
		{
			get { return _cwd == ForwardSlash; }
		}

		/// <summary>
		/// Gets or sets the current FTP directory.
		/// </summary>
		public string CurrentDirectory
		{
			get { return _cwd; }
			set { _cwd = ApplyDirectory(value); }
		}

		/// <summary>
		/// Sets the domain and current directory from a URL.
		/// </summary>
		/// <param name="url">URL to set to</param>
		public void SetUrl(string url)
		{
			// Separate domain from directory
			int pos = url.IndexOf("://");
			pos = url.IndexOfAny(_slashes, (pos < 0) ? 0 : pos + 3);
			if (pos < 0)
			{
				_domain = url;
				_cwd = ForwardSlash;
			}
			else
			{
				_domain = url.Substring(0, pos);
				// Normalize directory string
				_cwd = ApplyDirectory(url.Substring(pos));
			}
		}

		/// <summary>
		/// Returns the domain and current directory as a URL.
		/// </summary>
		public string GetUrl()
		{
			return GetUrl(String.Empty);
		}

		/// <summary>
		/// Returns the domain and specified directory as a URL.
		/// </summary>
		/// <param name="directory">Partial directory or filename applied to the 
		/// current working directory.</param>
		public string GetUrl(string directory)
		{
			if (directory.Length == 0)
				return _domain + _cwd;
			return _domain + ApplyDirectory(directory);
		}

		/// <summary>
		/// Applies the given directory to the current directory and returns the
		/// result.
		/// 
		/// If directory starts with "/", it replaces all of the current directory.
		/// If directory is "..", the top-most subdirectory is removed from
		/// the current directory.
		/// </summary>
		/// <param name="directory">The directory to apply</param>
		public string ApplyDirectory(string directory)
		{
			// Normalize directory
			directory = directory.Trim();
			directory = directory.Replace(BackSlash, ForwardSlash);
			directory = directory.TrimEnd(_slashes);

			if (directory == "..")
			{
				int pos = _cwd.LastIndexOf(ForwardSlash);
				return (pos <= 0) ? ForwardSlash : _cwd.Substring(0, pos);
			}
			else if (directory.StartsWith(ForwardSlash))
			{
				// Specifies complete directory path
				return directory;
			}
			else
			{
				// Relative to current directory
				if (_cwd == ForwardSlash)
					return _cwd + directory;
				else
					return _cwd + ForwardSlash + directory;
			}
		}

		/// <summary>
		/// Returns the domain and current directory as a URL
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return GetUrl();
		}
	}
}
