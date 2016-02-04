﻿using System.Xml;
using System.Xml.Serialization;
using ZuoraMagic.ORM.BaseRequestTemplates;

namespace ZuoraMagic.Http
{
    internal static class XmlRequestGenerator
    {
        internal static string GenerateRequest(XmlBody body)
        {
            return GenerateRequest(body, null);
        }

        internal static string GenerateRequest(XmlHeader header)
        {
            return GenerateRequest(null, header);
        }

        internal static string GenerateRequest(XmlBody body, XmlHeader header)
        {
            XmlDocument document = SerializeToDocument(new XmlRequest
            {
                Body = body,
                Header = header
            });

            return document.OuterXml;
        }

        private static XmlDocument SerializeToDocument(XmlRequest request)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("soapenv", ZuoraNamespaces.Envelope);
            namespaces.Add("api", ZuoraNamespaces.Request);
            namespaces.Add("xsi", ZuoraNamespaces.Type);

            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(request.GetType()).Serialize(writer, request, namespaces);
            }

            return doc;
        }
    }
}