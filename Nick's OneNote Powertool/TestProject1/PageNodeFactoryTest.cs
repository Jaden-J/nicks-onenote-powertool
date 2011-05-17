using NicksPowerTool.ONReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace XmLTest
{
    
    
    /// <summary>
    ///This is a test class for PageNodeFactoryTest and is intended
    ///to contain all PageNodeFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PageNodeFactoryTest
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

        /*
        /// <summary>
        ///A test for PageNodeFactory Constructor
        ///</summary>
        [TestMethod()]
        public void PageNodeFactoryConstructorTest()
        {
            PageNodeFactory target = new PageNodeFactory();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for GenerateNode
        ///</summary>
        [TestMethod()]
        public void GenerateNodeTest()
        {
            XmlNode node = null; // TODO: Initialize to an appropriate value
            PageNode expected = null; // TODO: Initialize to an appropriate value
            PageNode actual;
            actual = PageNodeFactory.GenerateNode(node);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }*/

        /// <summary>
        ///A test for GeneratePageNodeDictionary
        ///</summary>
        [TestMethod()]
        public void GeneratePageNodeDictionaryTest()
        {
            System.Console.WriteLine("PageNodes: ");

            foreach (KeyValuePair<String, Type> kv in PageNodeFactory.PageElementDictionary)
            {
                System.Console.WriteLine("\tNodeName: " + kv.Key + ". Type: " + kv.Value.Name);
            }

            System.Console.WriteLine("PageNodeProperties: ");

            foreach (KeyValuePair<String, Type> kv in PageNodeFactory.PagePropertyDictionary)
            {
                System.Console.WriteLine("\tPropertyName: " + kv.Key + ". Type: " + kv.Value.Name);
            }
            Assert.IsTrue(true);
        }
        /*
        /// <summary>
        ///A test for getName
        ///</summary>
        [TestMethod()]
        public void getNameTest()
        {
            XmlNode node = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = PageNodeFactory.getName(node);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isElement
        ///</summary>
        [TestMethod()]
        public void isElementTest()
        {
            XmlNode node = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = PageNodeFactory.isElement(node);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isProperty
        ///</summary>
        [TestMethod()]
        public void isPropertyTest()
        {
            XmlNode node = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = PageNodeFactory.isProperty(node);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }*/
    }
}
