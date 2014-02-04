using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlToCsvTests
{
    public static class TestHelper
    {
        public static void Throws<TException>(Action action, string message)
            where TException : Exception
        {
            try
            {
                action();

                failBecauseNoException<TException>();
            }
            catch (TException ex)
            {
                Assert.AreEqual(message, ex.Message);
            }
            catch (Exception ex)
            {
                failBecauseExceptionIsWrongType<TException>(ex);
            }
        }

        public static void Throws<TException>(Action action)
            where TException : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (TException ex)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                failBecauseExceptionIsWrongType<TException>(ex);
            }
        }

        private static void failBecauseExceptionIsWrongType<TException>(Exception ex) where TException : Exception
        {
            Assert.Fail("Exception of type {0} expected; got exception of type {1}", typeof(TException).Name, ex.GetType().Name);
        }

        private static void failBecauseNoException<TException>() where TException : Exception
        {
            Assert.Fail("Exception of type {0} expected; got none exception", typeof(TException).Name);
        }

        public static void AssertContentsAreEqual(string pathToActualResults, string pathToExpectedResults)
        {
            var actualContent = File.ReadAllText(pathToActualResults);
            var expectedContent = File.ReadAllText(pathToExpectedResults);

            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}
