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

using System.IO;
using System.Text;
using System.Xml.Linq;

namespace MalikP.BusinessJournal.Core.Serialization.Xml
{
    public sealed class XmlSerializer : ISerializer
    {
        public static TResult FromXml<TResult>(string xml, INamespaceReplacer namespaceReplacer = null)
        {
            xml = namespaceReplacer == null ? xml : namespaceReplacer.ReplaceNamespace<TResult>(xml);

            var result = default(TResult);
            var xmlSerializer = GetXmlSerializer<TResult>();
            var data = Encoding.UTF8.GetBytes(xml);

            using (var ms = new MemoryStream(data))
            {
                result = (TResult)xmlSerializer.Deserialize(ms);
            }

            return result;
        }

        public static string ToXml<TSource>(TSource source)
        {
            var result = string.Empty;
            var xmlSerializer = GetXmlSerializer<TSource>();

            using (var ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, source);
                ms.Position = 0;

                result = XDocument.Load(ms)
                                  .ToString();
            }

            return result;
        }

        public TResult Deserialize<TResult>(string xml)
        {
            return FromXml<TResult>(xml);
        }

        public string Serialize<TSource>(TSource source)
        {
            return ToXml<TSource>(source);
        }

        static System.Xml.Serialization.XmlSerializer GetXmlSerializer<TSource>()
        {
            return new System.Xml.Serialization.XmlSerializer(typeof(TSource));
        }
    }
}