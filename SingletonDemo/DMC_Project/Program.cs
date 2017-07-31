using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This in an example of a DMC designed using the Singleton dessign pattern.");

            Parallel.Invoke( // We will create two threads that will access the same instance of the DMC (where each thread creates their own instantiation of the object).
                () => FirstMethod(),
                () => SecondMethod());

            Console.ReadLine();
        }

        private static void FirstMethod()
        {
            DMC dmc1 = DMC.GetInstance;
            //We insert (or update, we don't know) the objects. 
            dmc1.InsertUpdateObject(new Object1(1, "Steve"));
            dmc1.PrintDictionaryContents();
            dmc1.InsertUpdateObject(new Object1(4, "John"));
            dmc1.PrintDictionaryContents();
            dmc1.InsertUpdateObject(new Object1(2, "Sam"));
            dmc1.PrintDictionaryContents();
            dmc1.InsertUpdateObject(new Object1(3, "Peter"));
            dmc1.PrintDictionaryContents();

            //By this moment we have 4 objects of type Object1.
            dmc1.InsertUpdateObject(new Object1(3, "Carl")); //Here, because there is already an object with ID=3, then is updated. Note that the comparison is made in the object implementation (using IComparable) and not in the DMC.
            dmc1.PrintDictionaryContents();
        }

        private static void SecondMethod()
        {
            //We create a second instance of the DMC. Because the DMC behaves as a singleton, there is only one instance running, so we will access the same dictionary.
            DMC dmc2 = DMC.GetInstance;
            dmc2.InsertUpdateObject(new Object2("id1", "passwordABC")); //This is the first time an object of type Object2 is found. We proceed to add a new entry in the dictionary, and create a new list with this object as its only element.
            dmc2.PrintDictionaryContents();
            dmc2.InsertUpdateObject(new Object2("id2", "password123"));
            dmc2.PrintDictionaryContents();

            //By this moment we have two entries in the dictionary (keys type: Object1 and Object2 and values the list of objects). Because there is already an existing Object2 with id = "id2", we proceed to update (Again, the comparison is made in the Object2 by IComparable and not in the DMC).
            dmc2.InsertUpdateObject(new Object2("id2", "password987"));
            dmc2.PrintDictionaryContents();
        }

        public class Object1 : CustomObject
        {
            public int ID;
            public string Name;

            public Object1(int id, string name)
            {
                this.ID = id;
                this.Name = name;
            }

            public void SetName(string newName)
            {
                this.Name = newName;
            }

            public override int CompareTo(object other)
            {
                Object1 obj_pivot = (Object1)other;
                return this.ID.CompareTo(obj_pivot.ID);
            }

            public override string PrintObject()
            {
                return "[" + this.ID + "][" + this.Name + "]";
            }
        }

        public class Object2 : CustomObject
        {
            public string ID_string;
            public string Password;

            public Object2(string id, string password)
            {
                this.ID_string = id;
                this.Password = password;
            }

            public override int CompareTo(object other)
            {
                Object2 obj_pivot = (Object2)other;
                return this.ID_string.CompareTo(obj_pivot.ID_string);
            }

            public override string PrintObject()
            {
                return "[" + this.ID_string + "][" + this.Password + "]";
            }
        }

    }
}
