using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Globals
{
    public class Order
    {
        public bool Status { get; set; }


        //https://www.c-sharpcorner.com/blogs/serialize-and-deserialize-xml-file-into-c-sharp-object-and-vise-versa

        public static void Serialize(Order order, string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            try
            {
                string fileName = Path.Combine(directory, $"{DateTime.Now:dd-MM-yyyy_HH-mm-ss}");
                XmlSerializer serializer = new XmlSerializer(typeof(Order));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    serializer.Serialize(sw, order);
                }
                Console.WriteLine(fileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
        }

        public static T Deserialize<T>(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using(StreamReader sr = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(sr);
                }
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return default(T);
            }
        }

    }
}
