// Copyright (c) 20016-2018 Peter M.
// 
// File: MultiResultUploader.cs 
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

using MalikP.BusinessJournal.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MalikP.BusinessJournal.Core.Uploaders
{
    public class MultiResultUploader<TResult, THelp> : ResultUploaderBase<TResult, THelp>, IDisposable
        where TResult : class
        where THelp : class
    {
        List<ResultUploaderBase<TResult, THelp>> ResultUploaders { get; set; } = new List<ResultUploaderBase<TResult, THelp>>();

        public MultiResultUploader()
        {
            Logger?.Debug($"Initializing: {GetType().GetPrettyTypeName()}");
            LoadAllResultUploaders();
        }

        protected virtual void LoadAllResultUploaders()
        {
            Logger?.Debug($"Getting all result uploaders");

            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                                       .Location);

            var files = Directory.GetFiles(folder, "*.*")
                                 .Where(d => d.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase) ||
                                             d.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
                                 .ToList();

            var searchedTypes = new List<Type>();
            var restrictedTypes = GetRestrictedTypes();
            foreach (var file in files)
            {
                Logger?.Debug($"Searching result uploaders in file: {Path.GetFileName(file)}");

                var assembly = Assembly.LoadFrom(file);
                var types = assembly.GetTypes()
                                    .Where(d => !d.IsAbstract &&
                                                !d.IsInterface &&
                                                typeof(ResultUploaderBase<TResult, THelp>).IsAssignableFrom(d) &&
                                                d != typeof(MultiResultUploader<TResult, THelp>))
                                    .ToList();

                if (types != null && types.Count > 0)
                {
                    types.ForEach(t =>
                    {
                        Logger?.Warn($"Found result uploader: {t.FullName}");
                        if (!restrictedTypes.Contains(t.FullName))
                        {
                            Logger?.Warn($"Creating result uploader: {t.FullName}");

                            var instance = Activator.CreateInstance(t) as ResultUploaderBase<TResult, THelp>;
                            if (instance != null)
                                ResultUploaders.Add(instance);
                        }
                        else
                        {
                            Logger?.Warn($"Restricted result uploader: {t.FullName}");
                        }
                    });
                }
            }
        }

        protected virtual List<string> GetRestrictedTypes()
        {
            Logger?.Debug($"Getting restricted list of result uploaders");
            var result = new List<string>();
            var restrictedString = ConfigurationManager.AppSettings[$"{typeof(MultiResultUploader<TResult, THelp>).FullName}.RestrictedTypes"];
            if (!string.IsNullOrEmpty(restrictedString))
            {
                result = restrictedString.Split(';')
                                         .ToList();
            }

            return result;
        }

        public override bool Upload()
        {
            var result = true;
            try
            {
                ResultUploaders.ForEach(d =>
                {
                    Logger?.Debug($"Processing uploader: {d.GetType().FullName}");
                    d.Reset();
                    d.Initialize(GetMessage())
                     .AlsoWith(HelpData);
                    d.UploadResults();
                });
            }
            catch (Exception ex)
            {
                Logger?.Error($"During result uploading error occured", ex);
                result = false;
            }

            return result;
        }

        protected override object GetMessage()
        {
            Logger?.Debug($"Getting Message");
            return Result;
        }

        ~MultiResultUploader()
        {
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logger?.Debug($"Disposing");
                if (ResultUploaders != null)
                {
                    ResultUploaders.ForEach(d => (d as IDisposable)?.Dispose());
                    ResultUploaders.Clear();
                }
            }
        }
    }
}