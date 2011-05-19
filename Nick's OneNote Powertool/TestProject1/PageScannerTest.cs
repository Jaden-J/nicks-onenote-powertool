using NicksPowerTool.ONReader;
using NicksPowerTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml;
using NicksPowerTool.ONReader.PageNodes;
using Microsoft.Office.Interop.OneNote;

namespace XmLTest
{
    
    
    /// <summary>
    ///This is a test class for PageScannerTest and is intended
    ///to contain all PageScannerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PageScannerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        String sampleFilePath = "C:\\SamplePage.xml";

        /// <summary>
        ///A test for scan
        ///</summary>
        [TestMethod()]
        public void scanTest()
        {
            String xml = NicksPowerTool.Properties.Resources.LinkedNotes;
            //String xml = System.IO.File.ReadAllText(sampleFilePath);
            ONPage page = new ONPage(xml); // TODO: Initialize to an appropriate value
            PageScanner target = new PageScanner(page); // TODO: Initialize to an appropriate value
            target.scan();

            XmlNode temp;

            System.Console.WriteLine("Other: ");
            foreach (GenericPageNode node in target.collectedOther)
            {
                temp = node.Node;
                while ((temp = temp.ParentNode) != null) System.Console.Write("\t");
                System.Console.WriteLine("\tName: " + node.Node.LocalName);// + "\t\tType: " + node.Node.Value);
            }
            System.Console.WriteLine("");

            
            Assert.IsTrue(true);
        }

        /// <summary>
        ///A test for scan
        ///</summary>
        [TestMethod()]
        public void lookAtReaderOutput()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(sampleFilePath, settings);
            while (reader.Read())
            {
                System.Console.Write("Name: " + reader.Name + ". \t");
                System.Console.Write("XmlSpace: " + reader.XmlSpace + ". \t");
                System.Console.Write("Value: " + reader.Value + ". \t");
                System.Console.Write("Value type: " + reader.ValueType + ". \t");
                System.Console.WriteLine("NodeType: " + reader.NodeType + ". \t");
            }
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void lookAtXmlDocumentOutput()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(sampleFilePath, settings);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            XmlNode node = doc;
            int levels = 0;
            bool elevated = false;
            do
            {
                for (int i = 0; i < levels; i++) System.Console.Write("\t");
                System.Console.WriteLine(node.Name);

                if (node.NodeType == XmlNodeType.CDATA)
                {
                    System.Console.WriteLine(node.Value);
                }

                if (node.HasChildNodes && !elevated)
                {
                    node = node.FirstChild;
                    levels++;
                }
                else if (node.NextSibling != null)
                {
                    node = node.NextSibling;
                    elevated = false;
                }
                else
                {
                    node = node.ParentNode;
                    levels--;
                    elevated = true;
                }
            } while (node != null);
            Assert.IsTrue(true);
        }
        
        [TestMethod()]
        public void testAppInterface()
        {
            String xmlOut = new Debugg().getHierarchy();

            StreamWriter sw = new StreamWriter("C:\\Hierarchy.xml");
            sw.Write(xmlOut);
            sw.Close();
            Assert.IsTrue(true);
        }
    }
}
