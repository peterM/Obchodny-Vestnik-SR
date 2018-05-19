﻿// Copyright (c) 20016-2018 Peter M.
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

namespace MalikP.BusinessJournal.Models.V1.Exekucie.UpovedomeniaExekutorov
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.gov.sk/form/00166073.MSSR_OV_Upovedomenie_o_exekucii/1.x")]
    [XmlRoot(Namespace = "http://schemas.gov.sk/form/00166073.MSSR_OV_Upovedomenie_o_exekucii/1.x", IsNullable = false)]
    public class MSSR_OV_Upovedomenie_o_exekucii
    {
        public MSSR_OV_Upovedomenie_o_exekuciiExecutor Executor { get; set; }

        [XmlElement("Obligatory")]
        public MSSR_OV_Upovedomenie_o_exekuciiObligatory[] Obligatory { get; set; }

        public MSSR_OV_Upovedomenie_o_exekuciiOther Other { get; set; }
    }
}