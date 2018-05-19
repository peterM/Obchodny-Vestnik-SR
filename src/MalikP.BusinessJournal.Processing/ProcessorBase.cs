// Copyright (c) 20016-2018 Peter M.
// 
// File: ProcessorBase.cs 
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

using log4net;
using MalikP.BusinessJournal.Core;
using MalikP.BusinessJournal.Core.Extensions;

namespace MalikP.BusinessJournal.Processing
{
    public abstract class ProcessorBase<TOutput> : IProcessor, INaming
        where TOutput : class
    {
        protected readonly object _locker = new object();
        protected IDataCollector Collector { get; set; }
        protected IValidator Validator { get; set; }

        protected ILoggerFactory LoggerFactory { get; set; }

        ILog _logger;
        protected ILog Logger
        {
            get
            {
                if (_logger == null && LoggerFactory != null)
                {
                    lock (_locker)
                    {
                        if (_logger == null && LoggerFactory != null)
                        {
                            _logger = LoggerFactory?.CreateLogger(this);
                        }
                    }
                }

                return _logger;
            }
        }

        protected TOutput Context { get; private set; }

        public virtual string Name => GetType().GetPrettyTypeName();

        public virtual IAlsoInitializable Initialize<T>(T initializeContext)
        {
            if (initializeContext is TOutput)
            {
                Context = initializeContext as TOutput;
            }
            else if (initializeContext is IDataCollector)
            {
                Collector = (IDataCollector)initializeContext;
            }
            else if (initializeContext is IValidator)
            {
                Validator = (IValidator)initializeContext;
            }
            else if (initializeContext is ILoggerFactory)
            {
                LoggerFactory = (ILoggerFactory)initializeContext;
                _logger = null;
            }

            return this;
        }

        public virtual void Process()
        {
            Logger?.Debug($"OVERRIDE METHOD 'PROCESS()' IMPLEMENTATION FOR TYPE '{typeof(TOutput).GetPrettyTypeName()}'");
        }

        protected virtual bool Validate<T>(T initializationContext)
        {
            ResetValidator();
            Logger?.Debug($"Initializing validator {Validator?.GetType()?.Name}.");
            Validator?.Initialize(initializationContext);
            Validator?.Initialize(LoggerFactory);
            return Validator == null ? true : Validator.Validate();
        }

        protected virtual void ResetValidator()
        {
            Logger?.Debug($"Reseting validator.");
            Validator?.Reset();
        }

        public virtual void Reset()
        {
            Context = default(TOutput);
            Validator = null;
            Collector = null;
        }

        public IAlsoInitializable AlsoWith<T>(T initializeContext)
        {
            return Initialize(initializeContext);
        }
    }
}
