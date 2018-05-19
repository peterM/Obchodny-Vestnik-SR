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

using MalikP.BusinessJournal.Data.Service.Configuration;
using MalikP.BusinessJournal.Data.Service.JusticeServiceReference;
using MalikP.BusinessJournal.Data.Service.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

namespace MalikP.BusinessJournal.Data.Service
{
    public class ServiceWrapper
    {
        protected static readonly object _locker = new object();

        protected JournalSettingsBase Settings { get; set; }

        public ServiceWrapper(JournalSettingsBase settings)
        {
            Settings = settings;

            RemoveSslVlidation(Settings.RemoveSslValidation);
        }

        public ServiceWrapper RemoveSslVlidation(bool removeSslValidation)
        {
            if (removeSslValidation)
            {
                ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            }
            else
            {
                ServicePointManager.ServerCertificateValidationCallback = null;
            }

            return this;
        }

        protected virtual int AllowedRetriesCount
        {
            get
            {
                var result = -1;
                var keyValue = ConfigurationManager.AppSettings[$"{typeof(ServiceWrapper).FullName}.{nameof(AllowedRetriesCount)}"];
                if (!string.IsNullOrEmpty(keyValue))
                {
                    result = int.Parse(keyValue);
                }

                return result;
            }
        }

        PodanieExportSoapClient _serviceClient;
        protected PodanieExportSoapClient ServiceClient
        {
            get
            {
                if (_serviceClient == null)
                {
                    lock (_locker)
                    {
                        if (_serviceClient == null)
                        {
                            ServiceEndpointConfigurator.SetConfigValuesMax(Settings.ServiceUrl, "PodanieExportSoap");
                            _serviceClient = new PodanieExportSoapClient(ServiceEndpointConfigurator.Binding, ServiceEndpointConfigurator.Address);
                        }
                    }
                }

                return _serviceClient;
            }
        }

        int _retryCounter;
        public virtual List<ObchodnyVestnikKapitola> CheckJournalChanges(ObchodnyVestnik journal, bool retry = true)
        {
            List<ObchodnyVestnikKapitola> result = null;

            try
            {
                result = ServiceClient.SkontrolovatZmenyOV(Settings.UserName, Settings.Password, journal.Cislo, journal.Rocnik)
                                      .ToList();
                _retryCounter = 0;
            }
            catch (Exception)
            {
                ServiceClient.Abort();
                _serviceClient = null;

                if (AllowedRetriesCount < 0)
                {
                    throw;
                }
                else if (AllowedRetriesCount == 0)
                {
                    result = CheckJournalChanges(journal, retry);
                }
                else
                {
                    _retryCounter++;
                    result = CheckJournalChanges(journal, retry);
                }
            }

            return result;
        }

        public string DownloadJournalBiddings(ObchodnyVestnik journal, bool retry = true)
        {
            var result = "";

            try
            {
                result = ServiceClient.StiahnutPodaniaOV(Settings.UserName, Settings.Password, journal.Cislo, journal.Rocnik);
                _retryCounter = 0;
            }
            catch (Exception)
            {
                ServiceClient.Abort();
                _serviceClient = null;

                if (AllowedRetriesCount < 0)
                {
                    throw;
                }
                else if (AllowedRetriesCount == 0)
                {
                    result = DownloadJournalBiddings(journal, retry);
                }
                else
                {
                    _retryCounter++;
                    result = DownloadJournalBiddings(journal, retry);
                }
            }

            return result;
        }

        public List<ObchodnyVestnikPodanie> DownloadJournalBiddingsByType(ArrayOfString codes, DateTime fromDate, DateTime toDate, bool retry = true)
        {
            List<ObchodnyVestnikPodanie> result = null;

            try
            {
                result = ServiceClient.StiahnutPodaniaOVPodlaTypu(Settings.UserName, Settings.Password, codes, fromDate, toDate)
                                      .ToList();
                _retryCounter = 0;
            }
            catch (Exception)
            {
                ServiceClient.Abort();
                _serviceClient = null;

                if (AllowedRetriesCount < 0)
                {
                    throw;
                }
                else if (AllowedRetriesCount == 0)
                {
                    result = DownloadJournalBiddingsByType(codes, fromDate, toDate, retry);
                }
                else
                {
                    _retryCounter++;
                    result = DownloadJournalBiddingsByType(codes, fromDate, toDate, retry);
                }
            }

            return result;
        }

        public virtual ObchodnyVestnik LastPublishedJournal(bool retry = true)
        {
            ObchodnyVestnik result = null;

            try
            {
                result = ServiceClient.ZiskatPoslednyVydanyOV(Settings.UserName, Settings.Password);
                _retryCounter = 0;
            }
            catch (Exception)
            {
                ServiceClient.Abort();
                _serviceClient = null;

                if (AllowedRetriesCount < 0)
                {
                    throw;
                }
                else if (AllowedRetriesCount == 0)
                {
                    result = LastPublishedJournal(retry);
                }
                else
                {
                    _retryCounter++;
                    result = LastPublishedJournal(retry);
                }
            }

            return result;
        }

        public virtual List<ObchodnyVestnik> PublishedJournals(bool retry = true)
        {
            List<ObchodnyVestnik> result = null;

            try
            {
                result = ServiceClient.ZiskatZoznamVydanychOV(Settings.UserName, Settings.Password)
                                      .ToList();
                _retryCounter = 0;
            }
            catch (Exception)
            {
                ServiceClient.Abort();
                _serviceClient = null;

                if (AllowedRetriesCount < 0)
                {
                    throw;
                }
                else if (AllowedRetriesCount == 0)
                {
                    result = PublishedJournals(retry);
                }
                else
                {
                    _retryCounter++;
                    result = PublishedJournals(retry);
                }
            }

            return result;
        }
    }
}
