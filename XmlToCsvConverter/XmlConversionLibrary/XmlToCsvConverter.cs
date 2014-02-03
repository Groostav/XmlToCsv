using System;
using System.Text;
using Moor.XmlConversionLibrary.XmlToCsvStrategy;

namespace Moor.XmlConversionLibrary
{
    public class XMLtoCsvConverter
    {
        public static void ConvertTables(String xmlInputFilePath, String outputDirectory)
        {
            var converter = new XmlToCsvUsingDataSet(xmlInputFilePath);
            var context = new XmlToCsvContext(converter);

            foreach (string xmlTableName in context.Strategy.TableNameCollection)
            {
                context.Execute(xmlTableName, outputDirectory + "/" + xmlTableName + ".csv", Encoding.Default);
            }
        }
    }
}