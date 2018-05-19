// Copyright (c) 20016-2018 Peter M.
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MalikP.BusinessJournal.Core.FileSets
{
    public class FileSetGetter : IGetter<List<FileSet>, List<string>>
    {
        public List<FileSet> Get(List<string> source)
        {
            var result = new List<FileSet>();

            var xmlFiles = source.Where(d => d.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase))
                                 .ToList();

            while (xmlFiles != null &&
                   xmlFiles.Count > 0)
            {
                var xmlFile = xmlFiles.First();
                xmlFiles.Remove(xmlFile);

                source.Remove(xmlFile);
                var filename = Path.GetFileName(xmlFile);
                var code = filename.Split(new[] { '-' }).First();
                var attachments = source.Where(item => item.Contains(code))
                                        .ToList();

                if (attachments != null && attachments.Count > 0)
                    attachments.ForEach(item => source.Remove(item));

                var fileSet = new FileSet
                {
                    Code = code,
                    DataFile = xmlFile,
                    Attachments = new List<string>(attachments)
                };

                result.Add(fileSet);
            }

            return result;
        }
    }
}
