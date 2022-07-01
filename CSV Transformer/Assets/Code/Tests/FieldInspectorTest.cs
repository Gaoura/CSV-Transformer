#nullable enable

using NUnit.Framework;

using CSVTransformer.Codebase;

namespace CSVTransformer.Tests.Unit
{
    public class FieldInspectorTest
    {
        [Test]
        public void GetTypeOf_ShouldFindStringType_WhenFieldIsNotRecognized()
        {
            var inspector = new FieldInspector();

            var type = inspector.GetTypeOf("4b16df7944baaf276350");

            Assert.That
            (
                type,
                Is.EqualTo(FieldType.String)
            );
        }

        [Test]
        public void GetTypeOf_ShouldFindNumberType_WhenFieldIsNumberWithDecimal()
        {
            var inspector = new FieldInspector();

            var type = inspector.GetTypeOf("20.6");

            Assert.That
            (
                type,
                Is.EqualTo(FieldType.Number)
            );
        }

        [Test]
        public void GetTypeOf_ShouldFindNumberType_WhenFieldIsNumberWithNoDecimal()
        {
            var inspector = new FieldInspector();

            var type = inspector.GetTypeOf("20");

            Assert.That
            (
                type,
                Is.EqualTo(FieldType.Number)
            );
        }

        [Test]
        public void GetTypeOf_ShouldFindStringType_WhenFieldContainsNumberButIsNotOnlyNumber()
        {
            var inspector = new FieldInspector();

            var type = inspector.GetTypeOf("eazea20.5eaz");

            Assert.That
            (
                type,
                Is.EqualTo(FieldType.String)
            );
        }


        [Test]
        public void GetTypeOf_ShouldFindDateType_WhenFieldIsDateOnly()
        {
            var inspector = new FieldInspector();

            var type = inspector.GetTypeOf("2022-06-04");

            Assert.That
            (
                type,
                Is.EqualTo(FieldType.Date)
            );
        }

        [Test]
        public void GetTypeOf_ShouldFindDateType_WhenFieldIsDateAndTime()
        {
            var inspector = new FieldInspector();

            var type = inspector.GetTypeOf("2022-05-04T06:22:15+02:00");

            Assert.That
            (
                type,
                Is.EqualTo(FieldType.Date)
            );
        }

        [Test]
        public void GetTypeOf_ShouldFindStringType_WhenFieldContainsDateButIsNotOnlyDate()
        {
            var inspector = new FieldInspector();

            var type = inspector.GetTypeOf("eazea2022-06-04eaz");

            Assert.That
            (
                type,
                Is.EqualTo(FieldType.String)
            );
        }
    }
}