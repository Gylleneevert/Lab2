using System.Reflection.Metadata;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using System.Diagnostics.CodeAnalysis;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using System.Runtime;
using System.Reflection;

namespace Lab2
{

    internal class Program
    {
        

        static List<Member> members = new List<Member>();
        static List<Product> products = new List<Product>();
        static List<Product> cartlist = new List<Product>();






        

        static Product coke = new Product("CocaCola", 8);
        static Product fanta = new Product("Fanta", 8);
        static Product sprite = new Product("Sprite", 8);
        static Product noccoBjörnBär = new Product("Nocco Björn Bär", 15);
        static Product noccoHallon = new Product("Nocco Hallon", 15);
        static Product redBull = new Product("Redbull", 12);


        static void Main(string[] args)
        {
            LoadMembersFromFile();






            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);


            
            products.Add(redBull);
            products.Add(coke);
            products.Add(fanta);
            products.Add(sprite);
            products.Add(noccoHallon);
            products.Add(noccoBjörnBär);

            FirstMeny();

            static void FirstMeny()
            {
                Console.Clear();
                Console.WriteLine("1: Sign In");
                Console.WriteLine("2: SignUp");
                Console.WriteLine("3: Exit");


                string welcomeChoise = Console.ReadLine();
                Member logInMember = null;

                switch (welcomeChoise)
                {


                    case "1":
                        LogIn();
                        break;
                    case "2":
                        SignUp();
                        break;
                    case "3":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Unable Options, Press 1, 2 or 3");
                        FirstMeny();
                        break;
                }
            }
            static void LogIn()
            {
                Console.Clear();
                Console.WriteLine("Enter youre user name");
                string userName = Console.ReadLine();

                Console.WriteLine("Enter youre password");
                string passwordInput = Console.ReadLine();

                Member logInMember = members.Find(member => member.GetUserName() == userName && member.VerifyPassword(passwordInput));




                if (logInMember != null)
                {
                    Console.WriteLine($"Välkommen {logInMember.GetUserName()}");
                    MainMeny(logInMember);
                }
                else
                {
                    Console.WriteLine("Invalid User name and/or Password");
                    Console.ReadKey();
                    FirstMeny();
                }

            }

            static void SignUp()
            {

                Console.Clear();
                Console.WriteLine("Enter a user name");
                string userName = Console.ReadLine();

                Console.WriteLine("Enter a password");
                string password = Console.ReadLine();

                Member newMember = new Member(userName, password);
                members.Add(newMember);


                Console.WriteLine($"Välkommen : {newMember.GetUserName()}!");
                Console.ReadKey();

                FirstMeny();


            }
            static void MainMeny(Member currentUser)
            {
                Console.Clear();
                Console.WriteLine("****************MainMeny******************");
                Console.WriteLine("1: Shop");
                Console.WriteLine("2: Cart");
                Console.WriteLine("3: Checkout");
                Console.WriteLine("4: Log out");
                string menyChoise = Console.ReadLine();
                switch (menyChoise)
                {
                    case "1":
                        ShopMeny(currentUser);
                        break;
                    case "2":
                        CartMeny(currentUser);
                        break;
                    case "3":
                        ConfirmCheckOut(currentUser);
                        break;
                    case "4":
                        LogOut(ref currentUser);
                        break;
                    default:
                        MainMeny(currentUser);
                        break;



                }
            }
            static void ShopMeny(Member currentUser)
            {

                Console.Clear();
                Console.WriteLine("*********SHOP**********");
                Console.WriteLine($"1: Coca-Cola");
                Console.WriteLine($"2: Fanta");
                Console.WriteLine($"3: Sprite");
                Console.WriteLine($"4: Nocco BjörnBär");
                Console.WriteLine($"5: Nocco Hallon");
                Console.WriteLine($"6: Red Bull");
                Console.WriteLine("7: Cart");
                Console.WriteLine("8: Logga ut");

                while (true)
                {
                    string shopChoice = Console.ReadLine();

                    switch (shopChoice)
                    {
                        case "1":
                            Console.WriteLine();
                            currentUser.AddToCart(coke);
                            break;
                        case "2":
                            Console.WriteLine();
                            currentUser.AddToCart(fanta);
                            break;
                        case "3":
                            Console.WriteLine();
                            currentUser.AddToCart(sprite);
                            break;
                        case "4":
                            Console.WriteLine();
                            currentUser.AddToCart(noccoBjörnBär);
                            break;
                        case "5":
                            Console.WriteLine();
                            currentUser.AddToCart(noccoHallon);
                            break;
                        case "6":
                            Console.WriteLine();
                            currentUser.AddToCart(redBull);
                            break;
                        case "7":
                            CartMeny(currentUser);
                            break;
                        case "8":
                            LogOut(ref currentUser);
                            break;
                    }



                }

            }
            static void CartMeny(Member currentUser)
            {
                Console.Clear();
                Console.WriteLine("**********Cart**********");
                Console.WriteLine("1: Confim To Check Out");
                Console.WriteLine("2. Continue Shopping");
                Console.WriteLine("3:  Log Out");
                Console.WriteLine("*******************************");


                string memberInfo = currentUser.ToString();
                Console.WriteLine(memberInfo);

                string cartChoice = Console.ReadLine();
                switch (cartChoice)
                {
                    case "1":
                        ConfirmCheckOut(currentUser);
                        break;
                    case "2":
                        ShopMeny(currentUser);
                        break;
                    case "3":
                        LogOut(ref currentUser);
                        break;


                }
            }



            static void ConfirmCheckOut(Member currentUser)
            {
                Console.Clear();

                Console.WriteLine("Youre purchase is now done, have a nice day");

                Console.ReadKey();
                LogOut(ref currentUser);

            }
            static void LogOut(ref Member currentUser)
            {
                currentUser = null;
                Console.Clear();
                FirstMeny();

            }


        }



       
        static void SaveMembersToFile()
        {


            using (StreamWriter saveMembers = new StreamWriter("C:\\Users\\user\\Desktop\\ITHS C#\\Lab2\\Lab2\\members.txt"))
            {
                foreach (Member members in members)
                {
                    saveMembers.WriteLine($"{members.GetUserName()} : {members.GetPassword()}");
                }

            }



        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            SaveMembersToFile();
        }

        static void LoadMembersFromFile()
        {
            if (File.Exists("C:\\Users\\user\\Desktop\\ITHS C#\\Lab2\\Lab2\\members.txt"))
            {
                using (StreamReader loadMembers = new StreamReader("C:\\Users\\user\\Desktop\\ITHS C#\\Lab2\\Lab2\\members.txt"))
                {

                    string line;
                    while ((line = loadMembers.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string userName = parts[0].Trim();
                            string password = parts[1].Trim();
                            Member loadedMember = new Member(userName, password);
                            members.Add(loadedMember);
                        }
                    }
                }
            }
        }

     
    }
}

                

                
            

        
