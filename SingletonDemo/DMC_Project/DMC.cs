using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMC_Project
{
    public sealed class DMC
    {
        //Datastructure that will hold the lists of objects. 
        //The key is the type and the value is the list of all the objects of that type that we have found so far.
        public static Dictionary<Type, List<CustomObject>> dictionary = new Dictionary<Type, List<CustomObject>>();
        private static readonly object obj = new object();


        private static int counter = 0;
        private static DMC instance = null;

        public static DMC GetInstance
        {
            get
            {
                if(instance == null)//
                {
                    lock (obj)
                    {
                        if(instance == null)
                        {
                            instance = new DMC();
                        }
                    }
                }
                return instance;
            }
        }

        private DMC()
        {
            counter++;
            Console.WriteLine("Counter to verify number of instances: " + counter); //DMC behaves in a singleton way, so there will be only 1 instance.
        }

        public void PrintDictionaryContents()
        {
            lock (obj)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Dump of dictionary: ");
                foreach (KeyValuePair<Type, List<CustomObject>> pair in dictionary)
                {
                    Type type = pair.Key;
                    Console.WriteLine(" - Type: " + type.ToString());

                    foreach (CustomObject obj in pair.Value)
                    {
                        Console.WriteLine("    * " + obj.PrintObject());
                    }
                }
                Console.WriteLine("---------------------");
            }
        }
    
        /// <summary>
        /// This function will take an object, and verify its type:
        /// 1. If it already exists in the dictionary, then it proceeds to search (binary search) in the list and see if there is any object matching (by the IComparable CompareTo function) to that object. If there is, updates, if not, inserts in the index where it is supposed to so the list keeps sorted.
        /// 2. If the type does not exists, creates a new entry in the dictionary with the new type as the key and a new list as value, with the list containing the new object.
        /// </summary>
        /// <param name="newObject"></param>
        public void InsertUpdateObject(CustomObject newObject)
        {
            lock (obj)
            {
                Type newObjectType = newObject.GetType();

                if (dictionary.ContainsKey(newObjectType))
                {
                    //We have faced an object of this type before. We access that list and perform an insert or update.
                    //We will perform a binary search, reducing the time to O(log n).
                    int indexOfObject = dictionary[newObjectType].BinarySearch(newObject);  //BinarySearch uses the IComparable comparation.
                    if (indexOfObject < 0)
                    {
                        //This is a new object. We insert it in the list taking care of the order.
                        dictionary[newObjectType].Insert(~indexOfObject, newObject);
                    }
                    else
                    {
                        //The object already exists in the list. We update to the new value.
                        dictionary[newObjectType][indexOfObject] = newObject;
                    }

                }
                else
                {
                    //This is a new type of object. We create a new list of this type of object and add the object.

                    List<CustomObject> list = new List<CustomObject> { newObject };
                    dictionary.Add(newObjectType, list);
                }
            }
        }

    }

    public abstract class CustomObject : IComparable
    {
        public abstract int CompareTo(object other);
        public abstract string PrintObject();

    }



}
