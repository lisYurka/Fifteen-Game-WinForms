using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GIIS_4
{
    public static class Serializator
    {
        private static BinaryFormatter bin = new BinaryFormatter();
        public static void Serialize(object obj, string file)
        {
            using (Stream s = File.Open(file, FileMode.OpenOrCreate))
            {
                try
                {
                    bin.Serialize(s, obj);
                }
                catch (SerializationException exc)
                {
                    //$"Reason: {exc.Message}"
                    throw;
                }
            }
        }
        public static T Deserialize<T>(string file)
        {
            T item;
            using (Stream s = File.Open(file,FileMode.Open))
            {
                try
                {
                    item = (T)bin.Deserialize(s);
                }
                catch(SerializationException exc)
                {
                    //$"Reason: {exc.Message}"
                    throw;
                }
            }
            return item;
        }
    }
}
