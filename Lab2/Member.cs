using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Member
    {

        private string UserName;
        private string Password;

        private List<Product> CartList;
        
        

        public Member(string userName, string password) 
        {
            
            UserName = userName;
            Password = password;
            CartList = new List<Product>();

        }

        public string GetPassword() 
        {
            return Password;
        }
        public string GetUserName()
        {
            return UserName;
        }

        public void AddToCart(Product product)
        {
            CartList.Add(product);
            
            Console.WriteLine($"{product.Name} tillagd i kundvagnen.");

        }

        public List<Product> GetCart()
        {
            return CartList;
        }

        public override string ToString()
        {
            StringBuilder memberInfo = new StringBuilder();
            memberInfo.AppendLine($"Kund: {UserName}");
            memberInfo.AppendLine($"Password: {Password}");
            memberInfo.AppendLine("Kundvagn");

            double totalPrice = 0;


            for (int i = 0; i < CartList.Count; i++)
            {


                for (int j = i + 1; j < CartList.Count; j++)
                {
                    if (CartList[i].Name == CartList[j].Name)
                    {
                        CartList[j].Stack++;
                        CartList.RemoveAt(j);
                        j--;

                    }
                }

            }
            for (int i = 0; i < CartList.Count; i++)
            {
                memberInfo.AppendLine($"{CartList[i].Name} x {CartList[i].Stack} : {CartList[i].Price * CartList[i].Stack} kr");
            }

            for (int i = 0; i < CartList.Count; i++)
            {
                totalPrice += CartList[i].Price * CartList[i].Stack;
            }
            memberInfo.AppendLine($"Belopp att betala är {totalPrice} kr");


            return memberInfo.ToString();
        }
        public bool VerifyPassword(string inputPassword)
        {

            return inputPassword == Password;
        }

    }

}

