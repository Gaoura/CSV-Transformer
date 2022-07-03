#nullable enable

using System.Collections.Generic;
using System.IO;

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    internal class FileSaverTest
    {
        [Test]
        public void Save_ShouldNotThrow_WhenFileIsFake()
        {
            var factory = new SheetFactory();
            var csv_lines = new List<string>()
            {
                "field00,field01",
                "field10,field11",
                "field20,field21",
                "field30,field31"
            };
            var sheet = factory.Build(csv_lines);
            var saver = new FileSaver();
            var file_stream = new StreamWriter(new MemoryStream());    

            Assert.That
            (
                () => saver.Save(sheet, file_stream),
                Throws.Nothing
            );
        }

        [Test]
        public void Save_ShouldReturnFileWithTheRightContent()
        {
            var factory = new SheetFactory();
            var csv_lines = new List<string>()
            {
                "field00,field01",
                "field10,field11",
                "field20,field21",
                "field30,field31"
            };
            var sheet = factory.Build(csv_lines);
            var saver = new FileSaver();

            var saved_csv_lines = new List<string>();

            using (var file = new MemoryStream())
            {
                var file_stream = new StreamWriter(file);
                saver.Save(sheet, file_stream);

                saved_csv_lines = ReadLines(new MemoryStream(file.ToArray()));
            }

            for (int i = 0; i < csv_lines.Count; ++i)
            {
                Assert.That
                (
                    saved_csv_lines[i],
                    Is.EqualTo(csv_lines[i])
                );
            }

            static List<string> ReadLines(MemoryStream file)
            {
                file.Position = 0;
                var loaded_lines = new List<string>();
                using (var file_stream = new StreamReader(file))
                {
                    string line = file_stream.ReadLine();

                    while (line != null)
                    {
                        loaded_lines.Add(line);
                        line = file_stream.ReadLine();
                    }
                }

                return loaded_lines;
            }
        }
    }
}