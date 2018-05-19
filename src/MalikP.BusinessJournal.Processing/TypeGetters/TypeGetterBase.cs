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
using System.Linq;
using System.IO;
using System.Reflection;
using MalikP.BusinessJournal.Core.TypeGetters;
using MalikP.BusinessJournal.Models;
using System.Collections.Generic;

namespace MalikP.BusinessJournal.Processing.TypeGetters
{
    public abstract class TypeGetterBase : ITypeGetter
    {
        protected TypeGetterBase(string apiModelSource, string apiProcessingSource, ApiVersion apiVersion)
        {
            ApiModelSource = apiModelSource;
            ApiProcessingSource = apiProcessingSource;
            ApiVersion = apiVersion;

            ApiModelTypes = LoadAssemblyTypes(ApiModelSource);
            ApiProcessingTypes = LoadAssemblyTypes(ApiProcessingSource);
        }

        public string ApiModelSource { get; }

        public string ApiProcessingSource { get; }

        public IReadOnlyList<Type> ApiModelTypes { get; }
        public IReadOnlyList<Type> ApiProcessingTypes { get; }

        public ApiVersion ApiVersion { get; }

        public string RootPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                      .Location);

        public Type GetProcessor(string processorName)
        {
            return FindSpecifiedType(ApiProcessingTypes, processorName, true);
        }

        public Type GetModel(string modelName)
        {
            return FindSpecifiedType(ApiModelTypes, modelName, false);
        }

        protected virtual Type FindSpecifiedType(IReadOnlyList<Type> typeSource, string typeName, bool isProcessor)
        {
            Type result = null;

            if (isProcessor)
            {
                typeName = ConstructTypeName(typeName, ApiVersion);
            }

            return typeSource.SingleOrDefault(d => string.Equals(d.Name, typeName, StringComparison.InvariantCultureIgnoreCase));
        }

        protected IReadOnlyList<Type> LoadAssemblyTypes(string typeSource)
        {
            var assembly = Assembly.LoadFrom(Path.Combine(RootPath, typeSource));
            return assembly.GetTypes()
                           .ToList();
        }

        protected virtual string ConstructTypeName(string typeName, ApiVersion apiVersion)
        {
            return $"{typeName}_{apiVersion.Version}_Processor";
        }

        public TypeGetterResult GetResult(string name)
        {
            return new TypeGetterResult
            {
                ModelType = GetModel(name),
                ProcessorType = GetProcessor(name)
            };
        }
    }
}