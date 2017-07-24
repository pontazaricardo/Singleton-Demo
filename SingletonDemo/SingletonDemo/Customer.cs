using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonDemo
{
    public class Customer:IComparable<Customer>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int CompareTo(Customer obj)
        {
            return this.Salary.CompareTo(obj.Salary);
        }
    }
}
