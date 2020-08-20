using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Saver.XmlSaver
{
    public static class Genarate
    {

        public static void Go<T>(T obj)
        {

            SaveFileDialog sld = new SaveFileDialog();
            sld.Filter = "xml files (*.xml)|*.xml";
            if (sld.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(sld.FileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    serializer.Serialize(sw, obj, ns);
                }
            }
        }

        public static void Go<T>(ObservableCollection<T> items)
        {

            SaveFileDialog sld = new SaveFileDialog();
            sld.Filter = "xml files (*.xml)|*.xml";
            if (sld.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(sld.FileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    serializer.Serialize(sw, items, ns);
                }
            }
        }

    }
}
