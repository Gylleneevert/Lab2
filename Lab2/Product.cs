using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    
        //class för produkter på hemsidan
   public class Product
   {
      public string Name { get; set; }

      public int Price { get; set; }

        public double StackPrice;
        public int Stack = 1;




            
       public Product(string name, int price)
       {
            Name = name;
            Price = price;
            
       }

        
        

        
     
    }
    
}
