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
using log4net;
using MalikP.BusinessJournal.Core.Extensions;

namespace MalikP.BusinessJournal.Core.Uploaders
{
    public abstract class ResultUploaderBase<TResult, THelpData> : IResultUploader, IDisposable
        where TResult : class
        where THelpData : class
    {
        ILog _logger;

        protected ResultUploaderBase()
        {
            _logger = LogManager.GetLogger(name: GetType().GetPrettyTypeName());
        }

        ~ResultUploaderBase()
        {
            Dispose(false);
        }

        public ILog Logger
        {
            get
            {
                return _logger;
            }
        }
        protected THelpData HelpData { get; private set; }

        protected TResult Result { get; private set; }

        public IAlsoInitializable AlsoWith<T>(T initializeContext)
        {
            return Initialize(initializeContext);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IAlsoInitializable Initialize<T>(T initializeContext)
        {
            Logger?.Info($"Initializing: {initializeContext.GetType()}()");

            if (initializeContext is TResult)
            {
                Result = initializeContext as TResult;
            }
            else if (initializeContext is THelpData)
            {
                HelpData = initializeContext as THelpData;
            }

            return this;
        }

        public virtual void Reset()
        {
            Logger?.Info($"Nameof: {nameof(Reset)}()");
            Result = default(TResult);
        }

        public abstract bool Upload();

        public virtual bool UploadResults()
        {
            Logger?.Info($"Nameof: {nameof(UploadResults)}()");
            return Upload();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logger?.Debug("Disposing");
                (Result as IDisposable)?.Dispose();
                Result = default(TResult);
            }
        }

        protected abstract object GetMessage();
    }
}