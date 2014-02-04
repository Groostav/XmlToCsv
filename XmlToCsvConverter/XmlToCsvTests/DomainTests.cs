using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moor.XmlConversionLibrary;

namespace XmlToCsvTests
{
    [TestClass]
    public class DomainTests
    {

        [TestMethod]
        [FilePathValidity("Valid")]
        [XmlContent("Simple Document Set")]
        [XmlValidity("Valid")]
        [Tables("One")]
        [XmlAttributes("Do Not Exist")]
        [XmlNamespaceSpecificElements("Do Not Exist")]
        [XmlFormattingWhitespace("Exists")]
        [SpecialCharacters("Do Not Exist")]
        public void when_converting_hand_written_simple_XML()
        {
            const string path = @"BusStops.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"BusStop.csv";
            const String expected = @"BusStops.expected.csv";

            TestHelper.AssertContentsAreEqual(actual, expected);   

            File.Delete(actual);
        }

        [TestMethod]
        [FilePathValidity("Valid")]
        [XmlContent("Contains ")]
        [XmlValidity("Valid")]
        [Tables("One")]
        [XmlAttributes("Do Not Exist")]
        [XmlNamespaceSpecificElements("Do Not Exist")]
        [XmlFormattingWhitespace("Exist")]
        [SpecialCharacters("Do Not Exist")]
        public void when_converting_rss_xml_object()
        {
            const string path = @"engadget.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            foreach (var tuple in new []{new []{"item.csv", "engadget.item.expected.csv"}, 
                                         new []{"channel.csv", "engadget.channel.expected.csv"},
                                         new []{"rss.csv", "engadget.rss.expected.csv"},
                                         new []{"guid.csv", "engadget.guid.expected.csv"},
                                         new []{"image.csv", "engadget.image.expected.csv"}})
            {
                var actual = tuple[0];
                var expected = tuple[1];

                TestHelper.AssertContentsAreEqual(actual, expected);

                File.Delete(actual);
            }

        }

        [TestMethod]
        [FilePathValidity("Valid")]
        [XmlContent("Simple Document Set")]
        [XmlValidity("Valid")]
        [Tables("One")]
        [XmlAttributes("Exist")]
        [XmlNamespaceSpecificElements("Do Not Exist")]
        [XmlFormattingWhitespace("Exists")]
        [SpecialCharacters("Do Not Exist")]
        public void when_converting_document_with_attributes()
        {
            const string path = @"DataWithDifferentAttributes.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"DataWithDifferentAttributes.csv";
            const String expected = @"DataWithDifferentAttributes.expected.csv";

            TestHelper.AssertContentsAreEqual(actual, expected);

            File.Delete(actual);
        }

        [TestMethod]
        [FilePathValidity("Valid")]
        [XmlContent("Contains Nested dynamic-length-list")]
        [XmlValidity("Valid")]
        [Tables("One")]
        [XmlAttributes("Does Not Exist")]
        [XmlNamespaceSpecificElements("Do Not Exist")]
        [XmlFormattingWhitespace("Exists")]
        [SpecialCharacters("Do Not Exist")]
        public void when_converting_xml_with_nested_lists()
        {
            const string path = @"TreeLike.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const string actual = @"Node.csv";
            const string expected = @"TreeLike.expected.csv";

            TestHelper.AssertContentsAreEqual(actual, expected);

            File.Delete(actual);
        }

        [TestMethod]
        [XmlValidity("Invalid")]
        public void when_converting_invalid_xml()
        {
            const string path = @"malformed.xml";

            TestHelper.Throws<XmlException>(
                () => XMLtoCsvConverter.ConvertTables(path, "."), 
                "Unexpected end tag. Line 4, position 11.");
        }

        [TestMethod]
        [FilePathValidity("Invalid")]
        public void when_asked_to_convert_non_existent_file()
        {
            const string path = "DNE.xml";

            TestHelper.Throws<FileNotFoundException>(() => XMLtoCsvConverter.ConvertTables(path, "."));
        }

        [TestMethod]
        [FilePathValidity("Valid")]
        [XmlContent("Simple Document Set")]
        [XmlValidity("Valid")]
        [Tables("One")]
        [XmlAttributes("Does Not Exist")]
        [XmlNamespaceSpecificElements("Do Not Exist")]
        [XmlFormattingWhitespace("Exists")]
        [SpecialCharacters("Exist")]
        public void when_transcoding_utf16_xml_with_special_characters()
        {
            const string path = @"SpecialCharacters.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const string actual = @"SpecialCharacter.csv";
            const string expected = @"SpecialCharacters.expected.csv";

            TestHelper.AssertContentsAreEqual(actual, expected);

            File.Delete(actual);
        }
    }
}