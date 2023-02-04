// See https://aka.ms/new-console-template for more information
using FluentValidation.Results;
using Onboarding;
using Onboarding.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Signup_App
{
    class Program
    {
        static void Main(string[] args)
        {

            User user = new User();



            SqlConnection con = new SqlConnection("Server=DESKTOP-7H20OK2;Database=Signup;Trusted_Connection=True;MultipleActiveResultSets=true;");

            con.Open();



            Console.Title = "My App";
            Console.ForegroundColor = ConsoleColor.Yellow;

            

            Console.Write("Please choose one; 1. Signup 2. login: ");
            string option = Console.ReadLine();



            if (option == Convert.ToString(1))
            {
                Console.WriteLine("Input your First name:");
                user.firstName = Console.ReadLine();

                Console.WriteLine("Input your Last name:");
                user.lastName = Console.ReadLine();

                Console.WriteLine("Input your email address:");
                user.email = Console.ReadLine();

                Console.WriteLine("Input your phone number:");
                user.phoneNumber = Console.ReadLine();

                Console.WriteLine("Input your password:");
                user.password = Console.ReadLine();


                string query = string.Format("insert into register values('{0}', '{1}', '{2}', '{3}', '{4}')", user.firstName, user.lastName, user.email, user.phoneNumber, user.password);
                SqlCommand cmd = new SqlCommand(query, con);
                int n = cmd.ExecuteNonQuery();
                Console.WriteLine("{0} row(s) inserted successfully!...", n);
  




                // validate my data
                UserValidator validator = new UserValidator();
                var results = validator.Validate(user);

                if (results.IsValid == false)
                {
                    // Console.WriteLine("Error!!!");
                    foreach (ValidationFailure failure in results.Errors)
                    {
                        Console.WriteLine(failure.ErrorMessage);
                    }
                }


            }
            else if (option == Convert.ToString(2))
            {
                Console.WriteLine("Input your email address:");
                string loginEmail = Console.ReadLine();

                Console.WriteLine("Input your Password:");
                string loginPassword = Console.ReadLine();

                SqlCommand accountLogin = con.CreateCommand();
                string accountLoginString =
                    "SELECT email, password FROM dbo.register WHERE email = @userLoginEmail AND password = @userLoginPassword";
                accountLogin.Parameters.AddWithValue("@userLoginEmail", loginEmail);
                accountLogin.Parameters.AddWithValue("@userLoginPassword", loginPassword);
                accountLogin.CommandText = accountLoginString;
                accountLogin.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = accountLogin.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    string verifyEmailLogin = reader[0].ToString();
                    string verifyPasswordLogin = reader[1].ToString();

                    if (verifyEmailLogin == loginEmail & verifyPasswordLogin == loginPassword)
                    {
                        Console.WriteLine("Successful login!.");
                        Environment.Exit(0);
                    }
                    else
                    {
        
                    }
                    
                }
                else
                {
                    Console.WriteLine("Incorrect Login Details");
                }

            }
            else
            {
                Console.WriteLine("Input 1 or 2!");
            }




            //SqlConnection con = new SqlConnection("Data Source = DESKTOP - 7H20OK2; Initial Catalog = Signup; Integrated Security = True");
            

            con.Close();
             

        }
    }
}
