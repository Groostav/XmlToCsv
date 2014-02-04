using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moor.XmlConversionLibrary;
using Moor.XmlConversionLibrary.XmlToCsvStrategy;

namespace XmlToCsvTests
{
    [TestClass]
    public class XmlToCsvTests
    {
        [TestMethod]
        public void DataSetImplementationTest()
        {
            const string path = @"data.xml";
            var converter = new XmlToCsvUsingDataSet(path);
            var context = new XmlToCsvContext(converter);
            Assert.AreEqual(1, context.Strategy.TableNameCollection.Count);
            foreach (string xmlTableName in context.Strategy.TableNameCollection)
            {
                context.Execute(xmlTableName, @"dataDataSet_" + xmlTableName + ".csv", Encoding.Default);
            }
        }

        [TestMethod]
        public void CanReadAttributeTest()
        {
            const string path = @"DataWithAttributes.xml";
            var converter = new XmlToCsvUsingDataSet(path);
            var context = new XmlToCsvContext(converter);
            Assert.AreEqual(2, context.Strategy.TableNameCollection.Count);
            foreach (string xmlTableName in context.Strategy.TableNameCollection)
            {
                context.Execute(xmlTableName, @"dataDataSet_" + xmlTableName + ".csv", Encoding.Default);
            }
        }

        [TestMethod]
        public void InvalidOperationNestedTableExceptionTest()
        {
            const string path = @"NestedDataError.xml";
            TestHelper.Throws<InvalidOperationException>(() => new XmlToCsvUsingDataSet(path), "Nested table 'Contact-Address' which inherits its namespace cannot have multiple parent tables in different namespaces.");
        }

        [TestMethod]
        public void LinqImplementationTest()
        {
            const string path = @"data.xml";
            var converter = new XmlToCsvUsingLinq(path);
            var context = new XmlToCsvContext(converter);
            Assert.AreEqual(1, context.Strategy.TableNameCollection.Count);
            foreach (string xmlTableName in context.Strategy.TableNameCollection)
            {
                context.Execute(xmlTableName, @"dataLinq_" + xmlTableName + ".csv", Encoding.Default);
            }
        }

        [TestMethod]
        public void DuplicateNameErrorTest()
        {
            const string path = @"ErrorDuplicateName.xml";
            TestHelper.Throws<DuplicateNameException>(() => new XmlToCsvUsingDataSet(path), "A column named 'Employees' already belongs to this DataTable: cannot set a nested table name to the same name.");
        }

        [TestMethod]
        public void DuplicateNameRenamedTest()
        {
            const string path = @"ErrorDuplicateName.xml";
            var converter = new XmlToCsvUsingDataSet(path, true);
            var context = new XmlToCsvContext(converter);
            Assert.AreEqual(2, context.Strategy.TableNameCollection.Count);

            foreach (string xmlTableName in context.Strategy.TableNameCollection)
            {
                context.Execute(xmlTableName, @"ErrorDuplicateName_" + xmlTableName + ".csv", Encoding.Default);
            }
        }


        [TestMethod]
        [Tables("One")]
        [DuplicateColumns("None")]
        [XMLValidity("Valid")]
        [XMLContent("List formatted")]
        public void when_converting_one_table_with_a_list_of_items()
        {
            const string path = @"BusStops.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"BusStops.csv";
            const String expected = @"BusStops.expected.csv";

            AssertContentsAreEqual(actual, expected);
        }

        [TestMethod]
        [Tables("Many")]
        [DuplicateColumns("None")]
        [XMLValidity("Valid")]
        [XMLContent("List formatted")]
        public void when_converting_blagh_1()
        {
            
        }

        [TestMethod]
        [Tables("One")]
        [DuplicateColumns("None")]
        [XMLValidity("Valid")]
        [XMLContent("List formatted")]
        public void when_converting_blagh_2()
        {
            
        }

        //Edwin's Code
        [TestMethod]
        [Tables("Many")]
        [DuplicateColumns("None")]
        [XMLValidity("Valid")]
        [XMLContent("RSS formatted")]
        // Since there are many tables, numOutput should equal numTables.
        public void when_converting_many_tables_duplicate_columns()
        {
            const string path = @"engadget.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            //Number of actual output CSV should equal number of tables.
            //int numTables;
            //int numOutput;
            const String actual = @"engadget.item.converted.csv";
            const String expected = @"engadget.item.expected.csv";

            AssertContentsAreEqual(actual, expected);
            //Assert(numTables == numOutput);
        }

        [TestMethod]
        [Tables("One")]
        [DuplicateColumns("No")]
        [XMLValidity("Invalid")]
        [XMLContent("List formatted")]
        
        public void when_converting_invalid_xml()
        {
            const string path = @"malformed.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            Assert.Fail();
        }

        [TestMethod]
        [Tables("One")]
        [DuplicateColumns("Yes")]
        [XMLValidity("Invalid")]
        [XMLContent("List formatted")]

        public void when_converting_invalid_xml_duplicate_columns()
        {
            const string path = @"duplicate-malformed.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            Assert.Fail();
        }

        [TestMethod]
        [Tables("One")]
        [DuplicateColumns("No")]
        [XMLValidity("Valid")]
        [XMLContent("RSS formatted")]

        public void when_converting_rss_one_table()
        {
            const string path = @"toms-one-table.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"rss.csv";
            const String expected = @"toms.onetable.rss.csv";

            AssertContentsAreEqual(actual, expected);
        }

        [TestMethod]
        [Tables("One")]
        [DuplicateColumns("Yes")]
        [XMLValidity("Valid")]
        [XMLContent("List formatted")]

        public void when_converting_many_tables_and_duplicate_columns()
        {
            const string path = @"duplicate-single-table.xml";

            XMLtoCsvConverter.ConvertTables(path, ".");

            const String actual = @"DUPE.csv";
            const String expected = @"DUPE.expected.csv";

            AssertContentsAreEqual(actual, expected);
        }


        private void AssertContentsAreEqual(string pathToActualResults, string pathToExpectedResults)
        {
            throw new NotImplementedException();
        }
    }
}
