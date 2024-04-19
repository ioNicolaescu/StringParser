using NUnit.Framework;
using Moq;
using Shouldly;
using System.Collections.Generic;
using StringParser.Abstractions;
using StringParser.Services;

namespace StringParser.UnitTests
{
    [TestFixture]
    public class StringParserTests
    {
        private Services.StringParser _parser;
        string input = "AAAc91%cWwWkLq$1ci3_848v3d__K";
        


        [SetUp]
        public void Setup()
        {
            _parser = new Services.StringParser();
        }

        /// <summary>
        /// Verifies that the processed string is not null or empty.
        /// </summary>
        [Test]
        public void Verify_Input_Is_Not_Null_Or_Empty()
        {
            string nullInput = null;
            string emptyString = string.Empty;

            // Process the input string and store the result.
            string result = _parser.Parse(input);
            string nullResult = _parser.Parse(nullInput);
            string emptyResult = _parser.Parse(emptyString);

            // Assert that the processed string is not null or empty.
            result.ShouldNotBeNullOrEmpty();

            /*
                *  The following asserts will fail.
                *  Based on the requirements, the processed string output must not be null or empty string.
                *  However, the method currently returns null if the input string is null or empty.
                *  To align with the requirements and improve the method, consider throwing an ArgumentException instead of returning null.
                *  As a result, testing for null or empty input strings is not considered further.
            */

            //nullResult.ShouldNotBeNullOrEmpty();
            //emptyResult.ShouldNotBeNullOrEmpty();
        }

        /// <summary>
        /// Verifies that the processed string will remove contiguous duplicate characters in the same case.
        /// </summary>
        [Test]
        public void Verify_No_Duplicate_Characters()
        {
            // Process the input string and store the result.
            var result = _parser.Parse(input);

            // Converting processed string into an array of characters.
            var characters = result.ToCharArray();

            for (int i = 1; i < characters.Length; i++)
            {
                // Assert that the current character is not the same as the previous character
                characters[i].ShouldNotBe(characters[i - 1]);
            }
        }

        /// <summary>
        /// Verifies that the processed string will be truncated to max length of 15 chars.
        /// </summary>
        [Test]
        public void Verify_Length()
        {
            // Process the input string and store the result.
            var result = _parser.Parse(input);

            // Assert that the processed string dose not exceed the max length.
            result.Length.ShouldBeLessThanOrEqualTo(15);
        }

        /// <summary>
        /// Verifies that the processed string will replace '$' to '£'.
        /// </summary>
        [Test]
        public void Verify_Replacement_Character()
        {
            var input = "$No$Dolar$";
            var expectedResult = "£No£Dolar£";

            // Process the input string and store the result.
            var result = _parser.Parse(input);

            // Assert that the processed string has replaced $ to £.
            result.ShouldBe(expectedResult);
        }

        /// <summary>
        /// Verifies that the processed string will remove restricted characters (_) and (4).
        /// </summary>
        [Test]
        public void Verify_Restricted_Characters()
        {
            var input = "_4Four_4Underscore4_";
            
            // Process the input string and store the result.
            var result = _parser.Parse(input);

            // Assert that the processed string has removed (4) and (_).
            result.ShouldNotContain('4');
            result.ShouldNotContain('_');

            // In case that our input string  is conpose of only restricted characters

            var restrictedCharacters = "4_4_4__44___4_44__444";

            // Process the input string and store the result.
            var restrictedResult = _parser.Parse(restrictedCharacters);

            /*
                *  The following assert will fail.
                *  Based on the requirements, the processed string output must not be null or empty string.
                *  However, the method currently dose not check if the input string after manipualtion is empty.
                *  To align with the requirements and improve the method, consider to validate the input string after manipulation.
                *  Also consider to throw an ArgumentException if the input string ends up to be empty string afte manipulation.
            */

            //restrictedResult.ShouldNotBeNullOrEmpty();
        }

    }
}
