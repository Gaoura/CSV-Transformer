#nullable enable

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public sealed class FieldExtractorTest
    {
        [Test]
        public void Extract_ShouldReturnTheRightCountOfFields_WhenCSVLineIsProvided()
        {
            var extractor = new FieldExtractor();
            var fields = extractor.Extract("field0,field1,field2");
            
            Assert.That
            (
                fields.Count,
                Is.EqualTo(3)
            );
        }

        [Test]
        public void Extract_ShouldReturnTheRightFieldValues_WhenCSVLineIsProvided()
        {
            var extractor = new FieldExtractor();
            var fields = extractor.Extract("field0,field1,field2");

            byte i = 0;
            foreach (var field in fields)
            {
                Assert.That
                (
                    field,
                    Is.EqualTo($"field{i++}")
                );
            }
        }
    }
}