using System;
using System.Collections;

namespace QuestionSix
{
    public class Program
    {

        public interface IStack
        {
            ///
            /// Tests if this stack is empty.
            /// <returns>whether the stack is empty</returns>
            bool IsEmpty();
            /// Looks at the object at the top of this stack
            /// without removing it from the stack.
            /// <returns>the object at the top of the stack</returns>
            object Peek();
            /// Removes the object at the top of this stack and returns
            /// that object as the value of this function.
            /// <returns>object at the top of the stack</returns>
            object Pop();
            /// Pushes an item onto the top of this stack.
            /// <param name="item">the item to add to the stack</param>
            void Push(object item);
        }
        public class StackImplementation : IStack
        {
            Stack myStack;

            public StackImplementation()
            {
                myStack = new Stack();

            }

            public bool IsEmpty()
            {
                return myStack.Count == 0 ? true : false; 
            }
            public Object Peek()
            {
                return (Object)myStack.Peek();
            }
            public Object Pop()
            {
                return (Object)myStack.Pop();
            }
            public void Push(object ob)
            {
                   myStack.Push(ob);
            }
        }

        public class StackFactory : IstackFactory{


            public IStack CreateStack() => new StackImplementation(); 


            



        }

       
        public interface IstackFactory
        {
           public IStack CreateStack();
        }











        
    }
}
