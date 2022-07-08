#nullable enable

using System;

namespace CSVTransformer.Codebase
{
    public class CellPosition
    {
        private ushort Number { get; set; }
        public ushort AsArrayIndex => (ushort)(Number - 1U);

        public CellPosition(ushort number)
        {
            if (number < 1U)
            {
                throw new ArgumentOutOfRangeException();
            }

            Number = number;
        }
    }
}