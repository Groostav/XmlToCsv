using System;
using System.Text;
using Moor.XmlConversionLibrary.XmlToCsvStrategy;

namespace Moor.XmlConversionLibrary
{
    public static class XmlToCsvConverter
    {
        public static void ConvertTables(String xmlInputFilePath,
                                         String outputDirectory)
        {
            var converter = new XmlToCsvUsingDataSet(xmlInputFilePath);
            var context = new XmlToCsvContext(converter);

            foreach (string xmlTableName in context.Strategy.TableNameCollection)
            {
                context.Execute(xmlTableName, outputDirectory + @"\" + xmlTableName + ".csv", Encoding.Unicode);
            }
        } 

        //could add a method that takes a factory:
        //public static void ConvertTables(String xmlInputFilePath, String outputDirectory, 
        //                                 Func<String, XmlToCsvStrategyBase> strategyFactory);
        //this would allow your consumers to pick the strategy they wish to use,
        //which adds complexity but also adds features (?)
    }
}