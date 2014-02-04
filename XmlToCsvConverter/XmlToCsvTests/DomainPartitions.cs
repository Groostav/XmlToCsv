using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace XmlToCsvTests
{

    /// <summary>
    /// Currently only used as documentation for what domain-partitions each test is testing.
    /// </summary>
    public class DomainPartition : Attribute {}

    /// <summary>
    /// Valid | Invalid
    /// </summary>
    public class FilePathValidity : DomainPartition
    {
        public string validity { get; private set; }

        public FilePathValidity(string validity)
        {
            this.validity = validity;
        }
    }

    /// <summary>
    /// Simple Document Set | Contains Nested Documents | Contains Nested dynamic-length-list
    /// </summary>
    public class XmlContent : DomainPartition
    {
        public IEnumerable<string> formatting { get; private set; }

        public XmlContent(params string[] xmlFormatting)
        {
            this.formatting = xmlFormatting;
        }
    }

    /// <summary>
    /// valid | Invalid
    /// </summary>
    public class XmlValidity : DomainPartition
    {
        public string validity { get; private set; }

        public XmlValidity(string xmlIsValid)
        {
            validity = xmlIsValid;
        }
    }

    /// <summary>
    /// None | One | Many
    /// </summary>
    public class Tables : DomainPartition
    {
        public string TableCount { get; private set; }

        public Tables(string tableCount)
        {
            TableCount = tableCount;
        }
    }

    /// <summary>
    /// Exist | Do Not Exist
    /// </summary>
    public class XmlAttributes : DomainPartition
    {
        public string Attributes { get; private set; }

        public XmlAttributes(string attributes)
        {
            this.Attributes = attributes;
        }
    }

    /// <summary>
    /// Exists | Does Not Exist
    /// </summary>
    public class XmlFormattingWhitespace : DomainPartition
    {
        public string Whitespace { get; private set; }

        public XmlFormattingWhitespace(string whitespace)
        {
            this.Whitespace = whitespace;
        }
    }

    /// <summary>
    /// Exist | Do Not Exist
    /// </summary>
    public class XmlNamespaceSpecificElements : DomainPartition
    {
        public string NamespacedElements { get; private set; }

        public XmlNamespaceSpecificElements(string namespacedElements)
        {
            this.NamespacedElements = namespacedElements;
        }
    }
}