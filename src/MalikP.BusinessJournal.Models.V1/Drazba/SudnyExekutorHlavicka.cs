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

namespace MalikP.BusinessJournal.Models.V1.Drazba
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class SudnyExekutorHlavicka
    {
        public int formular_id { get; set; }

        public string database_id { get; set; }

        public string cislo_ex_konania { get; set; }

        public int typ_drazobnej_vyhlasky { get; set; }

        public int poradie_drazby { get; set; }

        public string drazba_ov_datum { get; set; }

        public string drazba_cislo_ov { get; set; }

        public SudnyExekutorHlavickaExekutor exekutor { get; set; }

        [XmlElement("zastupujuci_exekutor")]
        public SudnyExekutorHlavickaZastupujuci_exekutor[] zastupujuci_exekutor { get; set; }

        public string drazba_datum { get; set; }

        public string drazba_cas { get; set; }

        public SudnyExekutorHlavickaDrazba_miesto drazba_miesto { get; set; }

        [XmlElement("vlastnik")]
        public SudnyExekutorHlavickaVlastnik[] vlastnik { get; set; }

        public string datum_novy { get; set; }

        public string vyhlasenie_datum { get; set; }

        public string znalecka_hodnota { get; set; }

        public string realna_hodnota { get; set; }

        public string vyska_zabezpeky { get; set; }

        public int cena_typ { get; set; }

        public string sposob_zaplatenia { get; set; }

        public string cislo_bu { get; set; }

        public string najnizsie_podanie { get; set; }

        public string sposob_zaplatenia_np { get; set; }

        public string cislo_bu_np { get; set; }

        public string zavady_na_prevzatie { get; set; }

        public string zavady_uzitky { get; set; }

        public string odovzdanie { get; set; }

        [XmlElement("ohliadka")]
        public SudnyExekutorHlavickaOhliadka[] ohliadka { get; set; }

        public string ohliadka_miesto { get; set; }

        public string dalsie_info { get; set; }
    }
}