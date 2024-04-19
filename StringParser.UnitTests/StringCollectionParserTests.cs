using NUnit.Framework;
using Moq;
using Shouldly;
using System.Collections.Generic;
using StringParser.Abstractions;
using StringParser.Services;

namespace StringParser.UnitTests
{
    [TestFixture]
    public class StringCollectionParserTests
    {
        private StringCollectionParser _parser;

        [SetUp]
        public void Setup()
        {
            var stringParserMock = new Mock<IStringParser>();
            stringParserMock.Setup(parser => parser.Parse(It.IsAny<string>())).Returns((string input) => input);
            _parser = new StringCollectionParser(stringParserMock.Object);
        }

        /// <summary>
        /// Verifies that the processed string collection is not null.
        /// </summary>

        [Test]
        public void Verify_Input_Not_Null()
        {
            var inputCollection = new List<string> { "AAAc91%cWwWkLq$1ci3_848v3d__K" };

            // Process the input collection
            var result = _parser.Parse(inputCollection);

            // Assert that the processed strings collection is not null
            result.ShouldNotBeNull();
        }
    }
 }

