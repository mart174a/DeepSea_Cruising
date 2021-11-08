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
                    case '1': CostumerMenuLoop();
                        break;
                    //Crew
                    case '2':
                        break;
                    //Luk program
                    case '3': exitMain = true;
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

                    var x = Costumer.Find(input);
                    if (x == null)
                    {
                        Console.WriteLine("No Matches..");
                        Console.ReadKey();
                    }

                    else
                    {
                        x.DisplayAllInfo();

                        Console.WriteLine("\n-----------------------------------------------------------------------");
                        Console.WriteLine("Press Enter to search again or Write Delete if u wish to delete costumer");
                        if (Console.ReadLine().ToUpper() == "DELETE")
                        {
                            Console.WriteLine("\nAre u sure u wish to delete this costumer? write Yes");
                            if (Console.ReadLine().ToUpper() == "YES")
                            {
                                x.Delete();
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
                if(i == 0)
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
                            
                            break;
                        case '2':
                            
                            break;
                        case '3':
                            
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

                    var x = Costumer.Find(input);
                    if (x == null)
                    {
                        Console.WriteLine("No Matches..");
                        Console.ReadKey();
                    }
                    else
                    {
                        x.DisplayAllInfo();

                        Console.WriteLine("\n-----------------------------------------------------------------------");
                        Console.WriteLine("Press Enter to search again or Write Delete if u wish to delete crewmember");
                        if (Console.ReadLine().ToUpper() == "DELETE")
                        {
                            Console.WriteLine("\nAre u sure u wish to delete this crewmember? write Yes");
                            if (Console.ReadLine().ToUpper() == "YES")
                            {
                                x.Delete();
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
        }



    }

    class Test
    {
        public static List<Test> list = new List<Test>();

        int id;
        string name;

        public string Name { get => name;}

        public Test(string filePath)
        {
            var dataLines = FileManager.GetDataStringListFromFile(filePath);

            this.id = int.Parse(dataLines[0]);
            this.name = dataLines[1];
        } 

        public void Save()
        {
            
        }

        public void Delete()
        {
            string filePath = "Test/" + id + ".txt";
            File.Delete(filePath);

            list.Remove(this);
        }

        public static void SetupList()
        {
            list = new List<Test>();

            foreach (var filePath in Directory.GetFiles("./Test"))
            {
                list.Add(new Test(filePath));
            }
        }

        public static void CreateNew()
        {
            string newName;

            if(!Validater.AskForValidtInputLoop("Name : ",Validater.IsName,out newName))
                return;

            int newId = FileManager.GetValidtNewID("./Test");
            string fileName = "Test/" + newId + ".txt";
            FileManager.StoreDataListInFile(fileName, new List<string>() { newId.ToString(), newName});

            list.Add(new Test(fileName));
        }


    }

    class Crew : Person
    {
        public static List<Crew> list = new List<Crew>();

        //Private Variables
        public Crew(string filePath) : base(filePath)
        {
            var dataLines = FileManager.GetDataStringListFromFile(filePath);

            this.notes = dataLines[6];
            this.timeOfCreation = DateTime.Parse(dataLines[7]);
            this.ticket = new Ticket("Ticket/" + dataLines[8] + ".txt");
        }

        //Construct from file

        public void Save()
        {

        }

        public void Delete()
        {
            string filePath = "Crew/" + Id + ".txt";
            File.Delete(filePath);

            list.Remove(this);
        }
        public void DisplayAllInfo()
        {
            Console.WriteLine("Costumer Info");
            Console.WriteLine("--------------");
            Console.WriteLine("Id : " + Id);
            Console.WriteLine("Name : " + Name);
            Console.WriteLine("Email : " + Email);
            Console.WriteLine("Phone number: " + PhoneNumber);
            Console.WriteLine("Date of birth : " + DateOfBirth.ToString("dd/MM/yyyy"));
            Console.WriteLine("Gender : " + GenderIdentity);
            Console.WriteLine("Notes : " + Notes);
            Console.WriteLine("Time of creation : " + TimeOfCreation);
            Console.WriteLine();
            Ticket.DisplayAll();
            Console.WriteLine();
        }

        public string ListString()
        {
            return $"{Name} | {GenderIdentity} | {DateOfBirth.ToString("dd/MM/yyyy")} | {PhoneNumber} | {Email} | {ticket.TicketType.ToString()}";
        }

        public static void SetupList()
        {
            list = new List<Crew>();

            foreach (var filePath in Directory.GetFiles("./Crew"))
            {
                list.Add(new Crew(filePath));
            }
        }

        public static bool CreateNew()
        {
            #region Declaring variables
            List<string> stringList = new List<string>();

            string newId;
            string newName;
            string newEmail;
            string newPhoneNumber;
            string newDateOfBirth;
            string newGenderIdentity;
            string newNotes;
            string newTimeOfCreation;
            string newTicketId;

            #endregion

            #region Instantiating Variables
            Console.Clear();
            Console.WriteLine("Costumer Info (Write Exit to exit)");
            Console.WriteLine("--------------------------------------");

            newId = FileManager.GetValidtNewID("./Costumer").ToString();

            if (!Validater.AskForValidtInputLoop("Name : ", Validater.IsName, out newName))
                return false;
            if (!Validater.AskForValidtInputLoop("Email : ", Validater.None, out newEmail))
                return false;
            if (!Validater.AskForValidtInputLoop("Phone Number : ", Validater.IsPhoneNumber, out newPhoneNumber))
                return false;
            if (!Validater.AskForValidtInputLoop("Date of Birth (dd/mm/yyyy) : ", Validater.IsDate, out newDateOfBirth))
                return false;
            if (!Validater.AskForValidtInputLoop("Gender (Male,Female,NonBinary) : ", Validater.IsGender, out newGenderIdentity))
                return false;
            if (!Validater.AskForValidtInputLoop("Notes : ", Validater.None, out newNotes))
                return false;

            newTimeOfCreation = DateTime.Now.ToString();
            Console.WriteLine();

            //Get validt ide and create ticket
            newTicketId = FileManager.GetValidtNewID("./Ticket").ToString();
            if (!Ticket.CreateNew())
                return false;

            #endregion

            #region Add string variables to list

            stringList.Add(newId);
            stringList.Add(newName);
            stringList.Add(newEmail);
            stringList.Add(newPhoneNumber);
            stringList.Add(newDateOfBirth);
            stringList.Add(newGenderIdentity);
            stringList.Add(newNotes);
            stringList.Add(newTimeOfCreation);
            stringList.Add(newTicketId);

            #endregion

            //Save it to file and add til list
            string fileName = "Costumer/" + newId + ".txt";
            FileManager.StoreDataListInFile(fileName, stringList);
            list.Add(new Costumer(fileName));
            return true;
        }

        #region Find costumer
        public static Costumer Find(string searchSting)
        {
            Costumer costumerToReturn;

            if (Validater.IsInt(searchSting))
            {
                costumerToReturn = FindById(searchSting);
                if (costumerToReturn != null)
                    return costumerToReturn;
            }

            if (Validater.IsName(searchSting))
            {
                costumerToReturn = FindByName(searchSting);
                if (costumerToReturn != null)
                    return costumerToReturn;
            }

            if (Validater.IsPhoneNumber(searchSting))
            {
                costumerToReturn = FindByPhoneNumber(searchSting);
                if (costumerToReturn != null)
                    return costumerToReturn;
            }

            if (Validater.None(searchSting))
            {
                costumerToReturn = FindByEmail(searchSting);
                if (costumerToReturn != null)
                    return costumerToReturn;
            }

            return null;
        }
        static Costumer FindById(string id)
        {
            try
            {
                foreach (var costumer in list)
                {
                    if (costumer.Id == int.Parse(id))
                        return costumer;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        static Costumer FindByPhoneNumber(string phoneNumber)
        {
            foreach (var costumer in list)
            {
                if (costumer.PhoneNumber == int.Parse(phoneNumber))
                    return costumer;
            }

            return null;
        }
        static Costumer FindByEmail(string email)
        {
            foreach (var costumer in list)
            {
                if (costumer.Email.ToUpper() == email.ToUpper())
                    return costumer;
            }

            return null;
        }
        static Costumer FindByName(string name)
        {
            foreach (var costumer in list)
            {
                if (costumer.Name.ToUpper() == name.ToUpper())
                    return costumer;
                if (costumer.Name.ToUpper().Contains(name.ToUpper()))
                    return costumer;
            }

            return null;
        }

        #endregion
    }
}
