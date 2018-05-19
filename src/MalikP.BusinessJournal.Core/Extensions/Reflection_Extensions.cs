// Copyright (c) 20016-2018 Peter M.
// 
// File: Reflection_Extensions.cs 
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
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Linq;

namespace MalikP.BusinessJournal.Core.Extensions
{
    public static class Reflection_Extension
    {
        public static string GetOriginalName(this Type type)
        {
            var typeName = type.FullName
                               .Replace(type.Namespace + ".", "");

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var reference = new CodeTypeReference(typeName);

            return provider.GetTypeOutput(reference);
        }

        public static string GetPrettyTypeName(this Type item)
        {
            var originalTypeName = item.GetOriginalName();

            if (originalTypeName.Contains('<') && originalTypeName.Contains('>'))
            {
                var data = originalTypeName.Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                if (data != null && data.Length == 2)
                {
                    var names = data[1].Split(',');
                    foreach (var name in names)
                    {
                        var finalNname = name.Split('.').Last();
                        originalTypeName = originalTypeName.Replace(name, $"{finalNname}");
                    }
                }
            }

            return originalTypeName;
        }
    }
}
