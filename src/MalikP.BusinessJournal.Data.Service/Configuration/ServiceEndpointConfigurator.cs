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
using System.ServiceModel;
using System.Xml;

namespace MalikP.BusinessJournal.Data.Service.Configuration
{
    public class ServiceEndpointConfigurator
    {
        public static EndpointAddress Address { get; private set; }
        public static HttpBindingBase Binding { get; private set; }

        public static void SetConfigValuesMax(string address, string endpointName)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Bad config  file value => endpoint address");
            if (string.IsNullOrEmpty(endpointName)) throw new ArgumentException("Bad config  file value => endpoint name");

            var time = new TimeSpan(0, 1, 0);

            if (Address == null) Address = new EndpointAddress(address);
            if (Binding == null)
                Binding = new BasicHttpsBinding
                {
                    TransferMode = TransferMode.Streamed,
                    Security = new BasicHttpsSecurity
                    {
                        Mode = BasicHttpsSecurityMode.Transport,
                        Transport = new HttpTransportSecurity
                        {
                            ClientCredentialType = HttpClientCredentialType.None,
                        }
                    },

                    Name = endpointName,
                    MaxReceivedMessageSize = 2147483647,
                    AllowCookies = true,
                    MaxBufferSize = 2147483647,
                    MaxBufferPoolSize = 524288,
                    ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxArrayLength = 2147483647,
                        MaxDepth = 32,
                        MaxStringContentLength = 2147483647,
                        MaxBytesPerRead = 2147483647,
                        MaxNameTableCharCount = 2147483647
                    },
                    CloseTimeout = time,
                    OpenTimeout = time,
                    ReceiveTimeout = time,
                    SendTimeout = time
                };
        }
    }
}
