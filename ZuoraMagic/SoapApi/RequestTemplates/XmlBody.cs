﻿using System.Xml.Serialization;
using ZuoraMagic.SoapApi.RequestTemplates;

namespace ZuoraMagic.ORM.BaseRequestTemplates
{
    public partial class XmlBody
    {
        [XmlElement("login", Namespace = ZuoraNamespaces.Request)]
        public LoginRequestTemplate LoginTemplate { get; set; }

        [XmlElement("query", Namespace = ZuoraNamespaces.Request)]
        public QueryTemplate QueryTemplate { get; set; }

        [XmlElement("queryMore", Namespace = ZuoraNamespaces.Request)]
        public QueryMoreTemplate QueryMoreTemplate { get; set; }

        [XmlElement("create", Namespace = ZuoraNamespaces.Request)]
        public BasicCrudTemplate InsertTemplate { get; set; }

        [XmlElement("update", Namespace = ZuoraNamespaces.Request)]
        public BasicCrudTemplate UpdateTemplate { get; set; }

        [XmlElement("delete", Namespace = ZuoraNamespaces.Request)]
        public DeleteTemplate DeleteTemplate { get; set; }
    }
}