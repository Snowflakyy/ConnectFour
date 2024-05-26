using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
namespace CourseWork_OOP_Phase1_
{
    public interface IShapesOperations
    {
        public OrderedDictionary _operationUndo { get; }
        public OrderedDictionary _operationLastPlacedChips { get; }

        void _ChipsToBeUndone();
        void _ChipsToBeRedone();
    }
}
