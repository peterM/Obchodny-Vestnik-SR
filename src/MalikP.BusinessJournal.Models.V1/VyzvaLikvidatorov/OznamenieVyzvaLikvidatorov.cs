﻿// Copyright (c) 20016-2018 Peter M.
// 
// File: OznamenieVyzvaLikvidatorov.cs 
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

using MalikP.BusinessJournal.Models.Common;
using System;
using System.Xml.Serialization;

namespace MalikP.BusinessJournal.Models.V1.VyzvaLikvidatorov
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "/PortalJustice/XmlFormularObchodnyVestnik/")]
    [XmlRoot(Namespace = "/PortalJustice/XmlFormularObchodnyVestnik/", IsNullable = false)]
    public class OznamenieVyzvaLikvidatorov
    {
        public OznamenieVyzvaLikvidatorovLikvidatori Likvidatori { get; set; }

        public OznamenieVyzvaLikvidatorovPodnik Podnik { get; set; }

        public string RozhodnutieNaZaklade { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime RozhodnutieDatum { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime RozhodnutieDatumZaciatkuLikvidacie { get; set; }

        public XmlDropDownValue VeritelPravnaForma { get; set; }

        public string PohladavkyLehota { get; set; }

        public XmlDropDownValue PohladavkyAdresaTyp { get; set; }

        [XmlElement(IsNullable = true)]
        public OznamenieVyzvaLikvidatorovPohladavkyAdresaFO PohladavkyAdresaFO { get; set; }

        [XmlElement(IsNullable = true)]
        public OznamenieVyzvaLikvidatorovPohladavkyAdresaPO PohladavkyAdresaPO { get; set; }
    }
}