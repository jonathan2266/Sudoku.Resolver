using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public struct Cell : IEquatable<Cell>
    {
        public Cell()
        {
            IsFixed = false;
            _value = null;
        }

        public Cell(int value)
        {
            IsFixed = true;
            _value = value;
        }

        public bool IsFixed { get; init; }

        private int? _value;
        public int? Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (IsFixed)
                {
                    throw new InvalidOperationException("Cell is fixed, and cannot be changed.");
                }

                _value = value;
            }
        }

        public bool Equals(Cell other)
        {
            return Value == other.Value;
        }

        public static bool operator ==(Cell lCell, Cell rCell)
        {
            return lCell.Equals(rCell);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Cell cellStruct)
            {
                return false;
            }

            return Equals(cellStruct);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, IsFixed);
        }
    }
}
