using System;
using System.Collections.Generic;

namespace XmlToCsvTests
{
    public class XMLContentAttribute : Attribute
    {
        public IEnumerable<string> formatting { get; private set; }

        public XMLContentAttribute(params string[] xmlFormatting)
        {
            this.formatting = xmlFormatting;
        }
    }

    public class XMLValidityAttribute : Attribute
    {
        public string validity { get; private set; }

        public XMLValidityAttribute(string xmlIsValid)
        {
            validity = xmlIsValid;
        }
    }

    public class DuplicateColumnsAttribute : Attribute
    {
        public string DuplicateColumnCount { get; private set; }

        public DuplicateColumnsAttribute(string numberOfDuplicateColumns)
        {
            DuplicateColumnCount = numberOfDuplicateColumns;
        }
    }

    public class TablesAttribute : Attribute
    {
        public String TableCount { get; private set; }

        public TablesAttribute(string tableCount)
        {
            TableCount = tableCount;
        }
    }
}