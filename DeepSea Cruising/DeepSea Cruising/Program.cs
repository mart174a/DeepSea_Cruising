using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DeepSea_Cruising
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup static lists
            Costumer.SetupList();
            Crew.SetupList();
            Ticket.SetupList();

            //Main Menu Loop
            bool exitMain = false;
            do
            {
                Console.Clear();
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Costumer Page[1] | Crew Page[2] | Luk Program[3]");
                switch (Console.ReadKey(true).KeyChar)
                {
                    //Costumers
                    case '1':
                        CostumerMenuLoop();
                        break;
                    //Crew
                    case '2':
                        CrewMenuLoop();
                        break;
                    //Luk program
                    case '3':
                        exitMain = true;
                        break;
                    default:
                        break;
                }
            } while (exitMain == false);

            //Costumer methods
            void CostumerMenuLoop()
            {
                bool exit = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine("COSTUMERS");
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("Create new[1] | Find[2] | Show all[3] | Main Menu[4]");
                    switch (Console.ReadKey(true).KeyChar)
                    {
                        case '1':
                            Costumer.CreateNew();
                            break;
                        case '2':
                            FindCostumerLoop();
                            break;
                        case '3':
                            DisplayCostumerList();
                            break;
                        case '4':
                            exit = true;
                            break;
                        default:
                            break;
                    }
                } while (exit == false);
            }
            void FindCostumerLoop()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Search for Costumer by Id/Name/PhoneNumber/Email or write exit to exit");

                    string input;
                    if (!Validater.AskForValidtInputLoop("Search : ", Validater.None, out input))
                        break;
                    Console.WriteLine();

                    var newCostumer = Costumer.Find(input);
                    if (newCostumer == null)
                    {
                        Console.WriteLine("No Matches..");
                        Console.ReadKey();
                    }

                    else
                    {
                        newCostumer.DisplayAllInfo();

                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("Press Enter to search again or Write Delete if u wish to delete costumer");
                        if (Console.ReadLine().ToUpper() == "DELETE")
                        {
                            Console.WriteLine("\nAre u sure u wish to delete this costumer? write Yes");
                            if (Console.ReadLine().ToUpper() == "YES")
                            {
                                newCostumer.Delete();
                                Console.WriteLine();
                                WriteCode.WriteError("Costumer has been deleted");
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
            void DisplayCostumerList()
            {
                Console.Clear();
                int i = 0;
                foreach (var item in Costumer.list)
                {
                    i++;
                    Console.WriteLine(item.ListString());
                    Console.WriteLine();
                }
                if (i == 0)
                    Console.WriteLine("List is empty");

                Console.ReadKey();
            }

            //Crew methods
            void CrewMenuLoop()
            {
                bool exit = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine("CREW");
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("Create new[1] | Find[2] | Show all[3] | Main Menu[4]");
                    switch (Console.ReadKey(true).KeyChar)
                    {
                        case '1':
                            Crew.CreateNew();
                            break;
                        case '2':
                            FindCrewLoop();
                            break;
                        case '3':
                            DisplayCrewList();
                            break;
                        case '4':
                            exit = true;
                            break;
                        default:
                            break;
                    }
                } while (exit == false);
            }
            void FindCrewLoop()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Search for Crew by Id/Name/PhoneNumber/Email or write exit to exit");

                    string input;
                    if (!Validater.AskForValidtInputLoop("Search : ", Validater.None, out input))
                        break;
                    Console.WriteLine();

                    var crewMember = Crew.Find(input);
                    if (crewMember == null)
                    {
                        Console.WriteLine("No Matches..");
                        Console.ReadKey();
                    }
                    else
                    {
                        crewMember.DisplayAllInfo();

                        Console.WriteLine("--------------------------------------------------------------------------");
                        Console.WriteLine("Press Enter to search again or Write Delete if u wish to delete crew member");
                        if (Console.ReadLine().ToUpper() == "DELETE")
                        {
                            Console.WriteLine("\nAre u sure u wish to delete this crew member? write Yes");
                            if (Console.ReadLine().ToUpper() == "YES")
                            {
                                crewMember.Delete();
                                Console.WriteLine();
                                WriteCode.WriteError("Crewmember has been deleted");
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
            void DisplayCrewList()
            {
                Console.Clear();
                int i = 0;
                foreach (var item in Crew.list)
                {
                    i++;
                    Console.WriteLine(item.ListString());
                    Console.WriteLine();
                }
                if (i == 0)
                    Console.WriteLine("List is empty");

                Console.ReadKey();
            }
        }
    }
}
