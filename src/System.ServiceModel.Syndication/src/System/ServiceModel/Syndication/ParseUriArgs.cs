// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml;

namespace System.ServiceModel.Syndication
{
    public struct ParseUriArgs
    {
        public string UriString { get; set; }
        public UriKind UriKind { get; set; }
        public XmlQualifiedName ElemntQualifiedName { get; set; }
    }
}