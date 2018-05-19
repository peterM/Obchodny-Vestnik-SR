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
using System.Xml.Serialization;

namespace MalikP.BusinessJournal.Models.V1.Zmluva
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class OVZmluvaDodavatel
    {
        public string ObchodneMenoNazov { get; set; }

        public string AdresaUlica { get; set; }

        public string AdresaMesto { get; set; }

        public string AdresaStat { get; set; }

        public OVZmluvaDodavatelPravnaForma PravnaForma { get; set; }

        public string PravnaFormaIne { get; set; }

        public OVZmluvaDodavatelSudRegistracie SudRegistracie { get; set; }

        public string ICO { get; set; }

        public string DRC { get; set; }

        public string CisloUctu { get; set; }

        public string DoplnInformacia { get; set; }

        public string OsobaMeno { get; set; }

        public string OsobaPriezvisko { get; set; }

        public string OsobaFunkcia { get; set; }
    }
}