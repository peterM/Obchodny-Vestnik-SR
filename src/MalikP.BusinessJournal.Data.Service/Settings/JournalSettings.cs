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
using System.Configuration;
using System.Runtime.InteropServices;
using System.Security;

namespace MalikP.BusinessJournal.Data.Service.Settings
{
    public class JournalSettingsBase : IDisposable
    {
        protected static readonly object _locker = new object();
        public bool RemoveSslValidation { get; set; }

        public JournalSettingsBase()
        {
        }

        public string Password
        {
            get
            {
                return UnprotectPassword(SecurePassword);
            }
        }

        public SecureString SecurePassword { get; protected set; }

        public string UserName { get; protected set; }

        ~JournalSettingsBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (SecurePassword != null)
                {
                    SecurePassword.Dispose();
                    SecurePassword = null;
                }
            }
        }

        public virtual JournalSettingsBase LoadDefaults()
        {
            LoadUserName();
            LoadPassword();
            return this;
        }

        protected virtual void LoadPassword()
        {
            SecurePassword = new SecureString();
            foreach (var item in ConfigurationManager.AppSettings[$"{GetType().FullName}.Password"])
            {
                SecurePassword.AppendChar(item);
            }
        }

        protected virtual string UnprotectPassword(SecureString securePassword)
        {
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        protected virtual void LoadUserName()
        {
            UserName = ConfigurationManager.AppSettings[$"{GetType().FullName}.UserName"];
        }

        volatile string _serviceUrl;
        public string ServiceUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_serviceUrl))
                {
                    lock (_locker)
                    {
                        if (string.IsNullOrEmpty(_serviceUrl))
                        {
                            _serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
                        }
                    }
                }

                return _serviceUrl;
            }
            protected set
            {
                _serviceUrl = value;
            }
        }

        public JournalSettingsBase SetServiceUrl(string url)
        {
            ServiceUrl = url;
            return this;
        }
    }

}
