// Copyright (c) 20016-2018 Peter M.
// 
// File: UdajZRegistraRegistrovychUradov.cs 
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
using System.Xml.Serialization;

namespace MalikP.BusinessJournal.Models.V1.UdajeZRegistraRegUradov
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class UdajZRegistraRegistrovychUradov
    {
        public string URAD { get; set; }

        public string TYP_POHYBU { get; set; }

        public string NF_NO { get; set; }

        public string NAZOV_NF_NO { get; set; }

        public string NAZOV { get; set; }

        public string SIDLO { get; set; }

        public string ICO { get; set; }

        public string DATUM_VZNIKU { get; set; }

        public string DATUM_PLATNOSTI { get; set; }

        public string UCEL { get; set; }

        public string STATUTAR { get; set; }

        public string ZRIADOVATEL { get; set; }

        public string VKLAD_ZRIADOVATELOV_PENAZNY { get; set; }

        public string SPLATENY_VKLAD_PENAZNY { get; set; }

        public string VKLAD_ZRIADOVATELOV_NEPENAZNY { get; set; }

        public string PRIORITNY_MAJETOK { get; set; }

        public string DATUM_ZAPISU { get; set; }

        [XmlArrayItem("DRUH_ZMENY", IsNullable = false)]
        public string[] ZMENA { get; set; }
    }
}