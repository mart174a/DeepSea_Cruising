using System;
using System.Collections.Generic;
using System.IO;

namespace DeepSea_Cruising
{
    class Costumer : Person
    {
        public static List<Costumer> list = new List<Costumer>();

        //Private Variables
        string notes;
        DateTime timeOfCreation;
        Ticket ticket;

        public string Notes { get => notes; }
        public DateTime TimeOfCreation { get => timeOfCreation; }
        internal Ticket Ticket { get => ticket;}


        //Construct from file
        public Costumer(string filePath) : base(filePath)
        {
            var dataLines = FileManager.GetDataStringListFromFile(filePath);

            this.notes = dataLines[6];
            this.timeOfCreation = DateTime.Parse(dataLines[7]);
            this.ticket = new Ticket("Ticket/" + dataLines[8] + ".txt");
        }

        public void Save()
        {

        }

        public void Delete()
        {
            string filePath = "Costumer/" + Id + ".txt";
            File.Delete(filePath);

            list.Remove(this);

            this.ticket.Delete();
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
            return $"{Name} | {GenderIdentity} | {Age} years | {PhoneNumber} | {Email} | {ticket.TicketType.ToString()} | {ticket.FromDestination} to {ticket.ToDestination}";
        }

        public static void SetupList()
        {
            list = new List<Costumer>();

            foreach (var filePath in Directory.GetFiles("./Costumer"))
            {
                list.Add(new Costumer(filePath));
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
