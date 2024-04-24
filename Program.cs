using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Net.Http.Headers;

namespace dbtest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbCtx = new StudentDbContext();
            int menu_input = 0;
            do
            {
                DisplayMenu();
                try
                {
                    menu_input = Convert.ToInt32(Console.ReadLine());
                }

                catch (FormatException)
                {
                    Console.WriteLine("Ange ett korrekt nummer");
                    continue;
                }
                MenuSwitch(dbCtx, menu_input);

            } while (menu_input != 4);
            static void DisplayMenu()
            {
                Console.Clear();
                Console.WriteLine("*** Student Databas ***");
                Console.WriteLine("***-----------------***");
                Console.WriteLine("**        Meny       **");
                Console.WriteLine("*---------------------*");
                Console.WriteLine("* 1: Ny Student       *");
                Console.WriteLine("* 2: Ändra Student    *");
                Console.WriteLine("* 3: Lista Studenter  *");
                Console.WriteLine("* 4: Avsluta          *");
                Console.WriteLine("*---------------------*");
                Console.Write("> ");

            }
            static void MenuSwitch(StudentDbContext dbCtx, int menu_input)
            {
                switch (menu_input)
                {
                    case 1:
                        dbCtx.Add(AddStudent());
                        dbCtx.SaveChanges();
                        break;

                    case 2:
                        Console.Clear();
                        ListStudents(dbCtx);
                        ChangeStudent(dbCtx);
                        break;
                    case 3:
                        ListStudents(dbCtx);
                        Console.ReadLine();
                        break;
                    case 4:
                        //Exit
                        break;
                }



            }
            static Student AddStudent()
            {
                string? fName, lName, city;

                Console.Clear();
                Console.WriteLine("*** Registrera ny student ***");
                Console.Write("Förnamn: ");
                fName = Console.ReadLine();
                Console.Write("Efternamn: ");
                lName = Console.ReadLine();
                Console.Write("Stad: ");
                city = Console.ReadLine(); ;
                var newStudent = new Student()
                {
                    FirstName = fName,
                    LastName = lName,
                    City = city
                };

                Console.Clear();
                Console.WriteLine($"* Ny Student Registrerad * \n" +
                    $"Förnamn: {newStudent.FirstName} \n" +
                    $"Efternamn: {newStudent.LastName}  \n" +
                    $"Stad: {newStudent.City}");

                return newStudent;
            }
            static void ListStudents(StudentDbContext dbCtx)
            {
                Console.Clear();
                foreach (var i in dbCtx.Students)
                {
                    Console.WriteLine($"{i.StudentId} {i.FirstName} {i.LastName}\t\t{i.City}");
                }
                
            }
            static void ChangeStudent(StudentDbContext dbCtx)
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Ange Index för student som du vill ändra.");
                Console.WriteLine("-----------------------------------------");
                Console.Write("> ");
                int stdId = Convert.ToInt32(Console.ReadLine());
                var std = dbCtx.Students.Where(s => s.StudentId==stdId).FirstOrDefault<Student>();

                if (std != null)
                {
                    Console.Clear();
                    Console.WriteLine("Vad vill du ändra?");
                    Console.WriteLine("1: Förnamn\n2: Efternamn\n3: Stad");
                    Console.Write("> ");
                    int change = 0;
                    try
                    {
                        change = Convert.ToInt32(Console.ReadLine());
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine("Ange ett korrekt alternativ");
                        Console.ReadLine(); 
                    }
                    
                    switch (change)
                    {
                        case 1:
                            std.FirstName = Console.ReadLine();
                            break;

                        case 2:
                            std.LastName = Console.ReadLine();
                            break;

                        case 3:
                            std.City = Console.ReadLine();
                            break;

                    }

                    Console.Clear();
                    Console.WriteLine("Ändring genomförd.");
                    Console.WriteLine("------------------");
                    Console.WriteLine($"{std.FirstName} {std.LastName}\t\t{std.City}");
                    Console.ReadLine();
                    dbCtx.SaveChanges();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ange ett korrekt index");
                    Console.ReadLine();
                }
            }
        }
    }
}