using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Text;

class TestClass
{

    public class MinStack
    {
        private List<int> stack;
        private List<int> trackMinPosition;

        public MinStack()
        {
            stack = new List<int>();
            trackMinPosition = new List<int>();
        }

        public void Push(int val)
        {
            stack.Add(val);
            if (stack.Count() - 1 == 0)
            {
                trackMinPosition.Add(stack.Count() - 1);
            }
            else
            {
                if (val < stack[trackMinPosition.Last()])
                {
                    trackMinPosition.Add(stack.Count() - 1);
                }
            }
        }

        public void Pop()
        {
            if (stack.Count() - 1 == trackMinPosition.Last())
            {
                trackMinPosition.RemoveAt(trackMinPosition.Count() - 1);
            }
            stack.RemoveAt(stack.Count() - 1);
        }

        public int Top()
        {
            return stack.Last();
        }

        public int GetMin()
        {
            return stack[trackMinPosition.Last()];
        }
    }


    static void Main(string[] args)
    {
        MinStack minStack = new MinStack();

        // Perform operations based on the input [[],[-2],[0],[-3],[],[],[],[]]
        minStack.Push(-2);
        minStack.Push(0);
        minStack.Push(-3);
        Console.WriteLine("GetMin(): " + minStack.GetMin()); // Expected output: -3
        minStack.Pop();
        Console.WriteLine("Top(): " + minStack.Top());       // Expected output: 0
        Console.WriteLine("GetMin(): " + minStack.GetMin()); // Expected output: -2
        minStack.Pop();
        Console.WriteLine("Top(): " + minStack.Top());       
        Console.WriteLine("GetMin(): " + minStack.GetMin());
    }
}