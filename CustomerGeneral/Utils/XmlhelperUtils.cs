using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace CustomerGeneral.Utils
{
    public static class XmlHelperUtils
    {
        public static string ConvertToXml<T>(this T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer))
                {
                    serializer.Serialize(xmlWriter, obj);
                    return writer.ToString();
                }
            }
        }
    }
}
