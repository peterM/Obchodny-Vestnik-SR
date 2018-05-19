// Copyright (c) 20016-2018 Peter M.
// 
// File: ZipCompression.cs 
// Company: MalikP.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace MalikP.BusinessJournal.Core.Compression
{
    public class ZipCompression : ICompression
    {
        protected Encoding DefaultEncoding { get; private set; }
        protected ZipCompressionSettings Settings { get; set; }

        public ZipCompression()
        {
            DefaultEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
        }

        public void Decompress()
        {
            TryCreateDirectory();

            using (var fs = new FileStream(Settings.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var zipArchive = new ZipArchive(fs, ZipArchiveMode.Read, false, DefaultEncoding))
            {
                var entries = zipArchive.Entries;
                foreach (var entry in entries)
                {
                    var entryDestinationPath = GetEntryDestinationPath(entry);
                    entryDestinationPath = CheckAndUpdatePath(entryDestinationPath);

                    entry.ExtractToFile(entryDestinationPath);
                }
            }
        }

        protected virtual string CheckAndUpdatePath(string entryDestinationPath)
        {
            if (File.Exists(entryDestinationPath))
            {
                var directoryPath = Path.GetDirectoryName(entryDestinationPath);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(entryDestinationPath);
                var extension = Path.GetExtension(entryDestinationPath);

                entryDestinationPath = CheckAndUpdatePath(Path.Combine(directoryPath, $"{fileNameWithoutExtension}_{extension}"));
            }

            return entryDestinationPath;
        }

        protected virtual string GetEntryDestinationPath(ZipArchiveEntry entry)
        {
            return Path.Combine(Settings.DestinationFolder, entry.Name);
        }

        protected void TryCreateDirectory()
        {
            if (Settings.CreateDictionary)
            {
                var directoryName = Settings.DestinationFolder;

                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                else
                {
                    Directory.Delete(directoryName, true);
                    TryCreateDirectory();
                }
            }
        }

        public void Compress()
        {
            ZipFile.CreateFromDirectory(Settings.DestinationFolder, Settings.SourceFile);
        }

        public IAlsoInitializable Initialize<T>(T initializeContext)
        {
            Settings = initializeContext as ZipCompressionSettings;
            return this;
        }

        public void Reset()
        {
            (Settings as IDisposable)?.Dispose();
            Settings = null;
        }

        public IAlsoInitializable AlsoWith<T>(T initializeContext)
        {
            return Initialize(initializeContext);
        }
    }
}
