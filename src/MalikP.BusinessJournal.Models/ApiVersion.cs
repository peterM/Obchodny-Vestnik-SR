// Copyright (c) 20016-2018 Peter M.
// 
// File: ApiVersion.cs 
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

namespace MalikP.BusinessJournal.Models
{
    public sealed class ApiVersion
    {
        static ApiVersion()
        {
            UNKNOWN = new ApiVersion(nameof(UNKNOWN), 0);
            V1 = new ApiVersion(nameof(V1), 1);
            V2 = new ApiVersion(nameof(V2), 2);
        }

        private ApiVersion(string version, int order)
        {
            Version = version;
            Order = order;
        }

        public static ApiVersion UNKNOWN { get; }
        public static ApiVersion V1 { get; }
        public static ApiVersion V2 { get; }
        public int Order { get; }
        public string Version { get; }

        public static ApiVersion FromInt(int version)
        {
            switch (version)
            {
                case 1:
                    {
                        return ApiVersion.V1;
                    }

                case 2:
                    {
                        return ApiVersion.V2;
                    }

                default:
                    {
                        return ApiVersion.UNKNOWN;
                    }
            }
        }

        public static ApiVersion FromString(string enumeration)
        {
            switch (enumeration)
            {
                case nameof(V1):
                case "v1":
                    {
                        return ApiVersion.V1;
                    }

                case nameof(V2):
                case "v2":
                    {
                        return ApiVersion.V2;
                    }

                default:
                    {
                        return ApiVersion.UNKNOWN;
                    }
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            ApiVersion ver = obj as ApiVersion;
            if (ver == null)
            {
                return false;
            }

            return Version == ver.Version;
        }

        public override int GetHashCode()
        {
            return (Version.GetHashCode() ^ 2) * 256;
        }
    }
}
