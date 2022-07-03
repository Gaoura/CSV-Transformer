#nullable enable

using System.IO;
using System.Text;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public sealed class FileLoaderTest
    {
        private StreamReader ConvertToFileStream(string lines)
        {
            var lines_as_byte_array = Encoding.UTF8.GetBytes(lines);
            var file = new MemoryStream(lines_as_byte_array);
            return new StreamReader(file);
        }

        [Test]
        public void Load_ShouldNotThrow_WhenFileIsFake()
        {
            var csv_lines = "field00,field01\n" +
                            "field10,field11\n" +
                            "field20,field21\n" +
                            "field30,field31";
            var file_stream = ConvertToFileStream(csv_lines);

            var loader = new FileLoader();

            Assert.That
            (
                () => loader.Load(file_stream),
                Throws.Nothing
            );
        }

        [Test]
        public void Load_ShouldReturnTheRightNumberOfLines()
        {
            var csv_lines = "field00,field01\n" +
                            "field10,field11\n" +
                            "field20,field21\n" +
                            "field30,field31";
            var file_stream = ConvertToFileStream(csv_lines);

            var loader = new FileLoader();
            var lines = loader.Load(file_stream);

            Assert.That
            (
                lines.Count,
                Is.EqualTo(4)
            );
        }

        [Test]
        public void Load_ShouldReturnTheRightLinesContent()
        {
            var csv_lines = "field00,field01\n" +
                            "field10,field11\n" +
                            "field20,field21\n" +
                            "field30,field31";
            var file_stream = ConvertToFileStream(csv_lines);

            var loader = new FileLoader();
            var lines = loader.Load(file_stream);

            for (int i = 0; i < lines.Count; ++i)
            {
                Assert.That
                (
                    lines[i],
                    Is.EqualTo($"field{i}0,field{i}1")
                );
            }
        }
    }
}