using System;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moor.XmlConversionLibrary;

namespace XmlToCsvTests
{
    [TestClass]
    public class DomainTests
    {

        [TestMethod]
        [Tables("one")]
        [FilePathValidity("Valid")]
        [XmlValidity("Valid")]
        [XmlContent("Simple Document")]
        [XmlAttributes("Do Not Exist")]
        [XmlFormattingWhitespace("Exists")]
        public void when_converting_hand_written_simple_XML()
        {
            const string path = @"BusStops.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"BusStop.csv";
            const String expected = @"BusStops.expected.csv";

            TestHelper.AssertContentsAreEqual(actual, expected);   
        }

        [TestMethod]
        [Tables("One")]
        [FilePathValidity("Valid")]
        [XmlValidity("Valid")]
        [XmlContent("Simple Document")]
        [XmlAttributes("Exist")]
        [XmlFormattingWhitespace("Does Not Exist")]
        public void when_converting_rss_xml_object()
        {
            const string path = @"engadget.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"engadget.item.converted.csv";
            const String expected = @"engadget.item.expected.csv";

            TestHelper.AssertContentsAreEqual(actual, expected);            
        }

        [TestMethod]
        [XmlValidity("Invalid")]
        public void when_converting_invalid_xml()
        {
            const string path = @"malformed.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            Assert.Fail();
        }

        [TestMethod]
        [Tables("One")]
        [XmlValidity("Valid")]
        [XmlContent("Exists")]
        public void when_converting_rss_one_table()
        {
            const string path = @"toms-one-table.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"rss.csv";
            const String expected = @"toms.onetable.rss.expected.csv";

            TestHelper.AssertContentsAreEqual(actual, expected);
        }
    }
}