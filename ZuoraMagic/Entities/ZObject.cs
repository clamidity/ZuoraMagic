using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FastMember;
using ZuoraMagic.Extensions;
using ZuoraMagic.ORM;
using ZuoraMagic.ORM.BaseRequestTemplates;

namespace ZuoraMagic.Entities
{
    public abstract class ZObject : IXmlSerializable
    {
        public string Id { get; set; }

        public XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader) { }

        public void WriteXml(XmlWriter writer)
        {
            // TODO: Implement robust serialization
            Type type = GetType();
            TypeAccessor accessor = ObjectHydrator.GetAccessor(type);
            writer.WriteAttributeString("type", ZuoraNamespaces.Type, "obj:" + type.GetName());

            foreach (PropertyInfo info in type.GetCachedProperties())
            {
                var value = accessor[this, info.Name];

                if (info.PropertyType == typeof(DateTime))
                {
                    value = Convert.ToDateTime(value).ToString("o");
                }

                if (info.PropertyType.IsGenericType)
                {
                    writer.WriteStartElement(info.Name, "http://api.zuora.com/");

                    foreach (var thing in value as IEnumerable)
                    {
                        var type2 = thing.GetType();
                        TypeAccessor accessor2 = ObjectHydrator.GetAccessor(type2);
                        writer.WriteStartElement(type2.GetName());
                        writer.WriteAttributeString("type", ZuoraNamespaces.Type, "obj:" + type2.GetName());

                        foreach (PropertyInfo info2 in type2.GetCachedProperties())
                        {
                            var value2 = accessor2[thing, info2.Name];

                            if (info2.PropertyType == typeof(DateTime))
                            {
                                value2 = Convert.ToDateTime(value2).ToString("o");
                            }

                            if (value2 != null) writer.WriteElementString(info2.GetName(), ZuoraNamespaces.ZObject, value2.ToString());
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    value = null;
                }
                
                if (value != null) writer.WriteElementString(info.GetName(), ZuoraNamespaces.ZObject, value.ToString());
            }
        }
    }
}