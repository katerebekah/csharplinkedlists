using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {
        private SinglyLinkedListNode first_node;
        public SinglyLinkedList()
        {
            // NOTE: This constructor isn't necessary, once you've implemented the constructor below.
        }

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {

            //if they try and pass in an empty list, throw an error
            //an empty contructor will still work, but this avoids trying to assign a node the value of null
            if (values.Count() == 0)
            {
                throw new ArgumentException();
            }

            //for each of the values being passed in, add it to the list. (Use AddLast to guarantee the correct order)
            for (var i = 0; i < values.Count(); i++)
            {
                //this has to be made into a string because it is passed in as an array of objects
                this.AddLast(values[i].ToString());
            }
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]//list[i] = value;
        {
            //uses the ElementAt function to get value (saves you from duplicating your code)
            get { return this.ElementAt(i); }
            set
            {
                //using a placeholder list helps save the values in order without having to do voodoo with the pointers
                var placeholderList = new SinglyLinkedList();
                for (var q = 0; q < this.Count(); q++)
                {
                    //if this is the place where you need to exchange the value, insert it here
                    if (q == i)
                    {
                        placeholderList.AddLast(value);
                    }
                    //otherwise, insert the value that was preexisting in the list
                    else
                    {
                        placeholderList.AddLast(this.ElementAt(q));
                    }
                }
                 
                //now swap out the values in the placeholder list into the real list.
                //first swap the first value to clear out the old list
                first_node = new SinglyLinkedListNode(placeholderList.First());
                //then loop through the placeholder list and add the values to the real list in order
                for (var w = 1; w < placeholderList.Count(); w++)
                {
                    this.AddLast(placeholderList.ElementAt(w));
                }
            }
        }

        public void AddAfter(string existingValue, string value)
       { 
            //Create an int to store the place in line of the newValue
            int testForValue = -1;

            //find the existing value in the linked list and assign the place in testForValue
            for (var i = 0; i < this.Count(); i++)
            {
                if (this.ElementAt(i) == existingValue)
                {
                    testForValue = i;
                    break;
                }
            }

            //if the existing value isn't in the linked list (if it remains a -1 after the if statement), throw an error 
            if (testForValue < 0)
            {
                throw new ArgumentException();
            }

            //use a placeholder linked list to store the nodes in the correct order
            var placeholderList = new SinglyLinkedList();

            //loop through the current linked list and assign each of the values to the placeholder list
            //and insert the new value in its proper place
            for (var q = 0; q < this.Count(); q++)
            {
                //add each value to the placeholder list
                placeholderList.AddLast(this.ElementAt(q));
                //if this spot is the spot where you need to add the new value, add the new value here
                if (q == testForValue)
                {
                    placeholderList.AddLast(value);
                }
            }
            //now reassign the values to the current linked list in order (this.First() etc)
            first_node = new SinglyLinkedListNode(placeholderList.First());
            for (var w = 1; w < placeholderList.Count(); w++)
            {
                this.AddLast(placeholderList.ElementAt(w));
            }
        }

        public void AddFirst(string value)
        {
            //if this is the first node, this is just an assignment
            if (this.First() == null)
            {
                first_node = new SinglyLinkedListNode(value);
            }
            else
            {
                //create a placeholder list to store the values in order.
                var placeholderList = new SinglyLinkedList();
                placeholderList.AddFirst(value);
                for (var i = 0; i < this.Count(); i++)
                {
                    placeholderList.AddLast(this.ElementAt(i));
                }
                //now reassign the values to the current linked list in order (this.First() etc)
                first_node = new SinglyLinkedListNode(placeholderList.First());
                for (var q = 1; q < placeholderList.Count(); q++ )
                {
                    this.AddLast(placeholderList.ElementAt(q));
                }
            }
        }

        public void AddLast(string value)
        {
            if (this.First() == null)
            {
                first_node = new SinglyLinkedListNode(value);
            }
            else
            {
                var node = this.first_node;
                while (!node.IsLast()) // What's another way to do this????
                {
                    node = node.Next;
                }
                node.Next = new SinglyLinkedListNode(value);
            }
        }

        // NOTE: There is more than one way to accomplish this.  One is O(n).  The other is O(1).
        public int Count()
        {
            // If the list is empty
            // this.Count() == 0
            if (this.First() == null)
            {
                return 0;
            }
            else
            {
                int length = 1;
                var node = this.first_node;
                // Complexity is O(n)
                while (node.Next != null)
                {
                    length++;
                    node = node.Next;
                }
                return length;
            }


            // Provide a second implementation
        }

        public string ElementAt(int index)
        {
            if (this.First() == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {

                var node = this.first_node;

                for (var i = 0; i <= index; i++)
                {
                    if (i == index)
                    {
                        break;
                    }
                    node = node.Next;
                }
                return node.Value;

            }
        }

        public string First()
        {
            if (this.first_node == null)
            {
                return null;
            }
            else
            {
                return this.first_node.Value;
            }

            // return this.first_node ? null : this.first_node.Value;

        }

        public int IndexOf(string value)
        {
            int testForValue = -1;
            for (var i = 0; i < this.Count(); i++)
            {
                if (this.ElementAt(i) == value)
                {
                    testForValue = i;
                    break;
                }
            }
            return testForValue;
        }

        public bool IsSorted()
        {
            if (this.First() != null)
            {
                for (var i = 0; i < this.Count()-1; i++)
                {
                    if (String.Compare(this.ElementAt(i), this.ElementAt(i + 1), StringComparison.CurrentCulture) == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
            
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            var node = this.first_node;
            if (node == null)
            {
                return null;
            }
            else
            {
                // Step 1: Do I need to loop??????
                // Step 2: IF yes, Do I already have an example of how??? ***
                // Step 3: How can I modify the previous examples?
                // Step 4: Write what I think works.
                // Step 5: Rebuild/Re-run tests
                // Step 6: Rinse and repeat
                while (!node.IsLast())
                {
                    node = node.Next;
                }
                return node.Value;

            }
        }

        public void Remove(string value)
        {
            int number = this.IndexOf(value);
            var placeholderList = new SinglyLinkedList();
            for (var i = 0; i < this.Count(); i++)
            {
                if (i != number)
                {
                    placeholderList.AddLast(this.ElementAt(i));
                }
            }
            first_node = new SinglyLinkedListNode(placeholderList.First());
            for (var q = 1; q < placeholderList.Count(); q++)
            {
                this.AddLast(placeholderList.ElementAt(q));
            }
        }

        public void Sort()
        {
            if (this.Count() < 2)
            {
                return;
            }
            else
            {
                while (!this.IsSorted())
                {
                    var node = first_node;
                    var node2 = node.Next;
                    for (var i = 1; i < this.Count(); i++)
                    {
                        if (node.Value.CompareTo(node.Next.Value) > 0)
                        {
                            var temp = node.Next.Value;
                            node2.Value = node.Value;
                            node.Value = temp;
                        }
                        node = node.Next;
                        node2 = node2.Next;
                    }
                }
                
            }
        }

        public string[] ToArray()
        {
            string[] Arr = new string[this.Count()];
            for (int i = 0; i < this.Count(); i++)
            {
               Arr[i] = this.ElementAt(i);
            }
            return Arr;
        }

        public override string ToString()
        {
            string str = "{ ";
            var node = first_node;
            if (this.first_node != null)
            {
                while (!node.IsLast())
                {
                    str += "\"" + node.Value + "\", ";
                    node = node.Next;
                }
                str += "\"" + this.Last() + "\" ";
            }
            return str + "}";
        }

    }
}

/*public override string ToString()
        {
            var opening = "{";
            var ending = "}";
            var items = " ";
            var space = " ";
            var output = "";
            var quote = "\"";
            var comma = "," + space;

            if (this.first_node == null)
            {
                return opening + items + ending;
            }
            else if (this.Count() == 1)
            {
                output += opening;
                output += space;
                var node = this.first_node;
                output += quote + node.Value + quote;
            }
            else
            {
                output += opening;
                output += space;
                var node = this.first_node;
                while(!node.IsLast())
                {
                    output += quote + node.Value + quote + comma;
                    node = node.Next;

                }
            }
            output += space;
            output += ending;
            return output;
        }*/
