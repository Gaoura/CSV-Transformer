#nullable enable

using System;

namespace CSVTransformer.Codebase
{
    public struct CellPosition
    {
        private ushort Number { get; set; }
        public ushort AsArrayIndex => (ushort)(Number - 1U);

        public CellPosition(ushort number)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            Number = number;
        }
    }
}