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
using System.ComponentModel;
using System.Xml.Serialization;

namespace MalikP.BusinessJournal.Models.V1.UctovnaZavierka
{
    [Serializable]
    public class UctovnaZavierkaNepodnikateliaRiadokPU
    {
        public UctovnaZavierkaNepodnikateliaRiadokPU()
        {
            IsData1Enabled = true;
            IsData2Enabled = true;
            IsData3Enabled = true;
            IsData4Enabled = true;
        }

        [XmlElement(DataType = "integer", IsNullable = true)]
        public string CisloUctu { get; set; }

        public string Text { get; set; }

        public int Cislo { get; set; }

        [XmlElement(IsNullable = true)]
        public decimal? Data1 { get; set; }

        [XmlElement(IsNullable = true)]
        public decimal? Data2 { get; set; }

        [XmlElement(IsNullable = true)]
        public decimal? Data3 { get; set; }

        [XmlElement(IsNullable = true)]
        public decimal? Data4 { get; set; }

        [XmlAttribute]
        public bool Bold { get; set; }

        [XmlAttribute]
        [DefaultValue(true)]
        public bool IsData1Enabled { get; set; }

        [XmlAttribute]
        [DefaultValue(true)]
        public bool IsData2Enabled { get; set; }

        [XmlAttribute]
        [DefaultValue(true)]
        public bool IsData3Enabled { get; set; }

        [XmlAttribute]
        [DefaultValue(true)]
        public bool IsData4Enabled { get; set; }
    }
}