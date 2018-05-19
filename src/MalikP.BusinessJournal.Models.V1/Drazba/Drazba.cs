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

using MalikP.BusinessJournal.Models.Common;
using System;
using System.Xml.Serialization;

namespace MalikP.BusinessJournal.Models.V1.Drazba
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "/PortalJustice/XmlFormularObchodnyVestnik/")]
    [XmlRoot(Namespace = "/PortalJustice/XmlFormularObchodnyVestnik/", IsNullable = false)]
    public class Drazba
    {
        public string Cislo { get; set; }

        [XmlElement(DataType = "date", IsNullable = true)]
        public DateTime? ZoDna { get; set; }

        public PravnickaOsoba Drazobnik { get; set; }

        [XmlArray(IsNullable = true)]
        [XmlArrayItem("Navrhovatel", IsNullable = false)]
        public DrazbaNavrhovatel[] Navrhovatelia { get; set; }

        [XmlArray(IsNullable = true)]
        [XmlArrayItem("Licitator", IsNullable = false)]
        public DrazbaLicitator[] Licitatori { get; set; }

        [XmlElement(IsNullable = true)]
        public DrazbaNotar Notar { get; set; }

        public string KonanieMiesto { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime KonanieDatum { get; set; }

        public string KonanieCas { get; set; }

        public string Kolo { get; set; }

        public string Predmet { get; set; }

        [XmlElement(IsNullable = true)]
        public string PredmetOpis { get; set; }

        [XmlElement(IsNullable = true)]
        public string PredmetStavOpis { get; set; }

        [XmlElement(IsNullable = true)]
        public string PredmetPravaZavazky { get; set; }

        [XmlElement(IsNullable = true)]
        public string PredmetCenaOdhad { get; set; }

        [XmlElement(IsNullable = true)]
        public string PredmetCenaVyska { get; set; }

        [XmlElement(IsNullable = true)]
        public string PredmetCenaSposobStanovenia { get; set; }

        [XmlElement(IsNullable = true)]
        public string PredmetPodmienkyOdovzdania { get; set; }

        [XmlElement(IsNullable = true)]
        public string NajnizsiePodanie { get; set; }

        [XmlElement(IsNullable = true)]
        public string NajvyssiePodanie { get; set; }

        [XmlElement(IsNullable = true)]
        public string MinimalnePrihodenie { get; set; }

        [XmlElement(IsNullable = true)]
        public DrazbaZabezpeka Zabezpeka { get; set; }

        [XmlElement(IsNullable = true)]
        public string SposobUhradyCeny { get; set; }

        [XmlElement(IsNullable = true)]
        public string ObhliadkaDatum { get; set; }

        [XmlElement(IsNullable = true)]
        public string ObhliadkaMiesto { get; set; }

        [XmlElement(IsNullable = true)]
        public string OrganizaceOpatrenia { get; set; }

        [XmlElement(IsNullable = true)]
        public string NadobudnutieVlastnickehoPrava { get; set; }

        [XmlElement(IsNullable = true)]
        public string DovodUpustenia { get; set; }

        [XmlElement(IsNullable = true)]
        public string DrazobnikOznamuje { get; set; }
    }
}