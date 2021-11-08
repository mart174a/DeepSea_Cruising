using System;
using System.Collections.Generic;
using System.IO;

namespace DeepSea_Cruising
{
    class Ticket
    {
        public enum Type {FirstClass, SecondClass, Economy};

        public static List<Ticket> list = new List<Ticket>();

        #region Private variables

        int id;
        string fromDestination;
        string toDestination;
        DateTime dateOfDeparture;
        Type ticketType;
        int cabinNr;
        DateTime timeOfCreation;
        DateTime timeOfLastChange;

        #endregion

        #region Public Properties
        public int Id { get => id;}
        public string FromDestination { get => fromDestination; }
        public string ToDestination { get => toDestination; }
        public DateTime DateOfDeparture { get => dateOfDeparture;}
        public Type TicketType { get => ticketType;}
        public int CabinNr { get => cabinNr; }
        public DateTime TimeOfCreation { get => timeOfCreation; }
        public DateTime TimeOfLastChange { get => timeOfLastChange;  }

        #endregion

        //LoadTicket from file (constructor)
        public Ticket(string filePath)
        {
            var dataLines = FileManager.GetDataStringListFromFile(filePath);

            this.id = int.Parse(dataLines[0]);
            this.fromDestination = dataLines[1];
            this.toDestination = dataLines[2];
            this.dateOfDeparture =  DateTime.Parse(dataLines[3]);
            this.ticketType = (Type)Enum.Parse(typeof(Type), dataLines[4]);
            this.cabinNr = int.Parse(dataLines[5]);
            this.timeOfCreation = DateTime.Parse(dataLines[6]);
            this.timeOfLastChange = DateTime.Parse(dataLines[7]);
        }

        //Methods
        public void Save()
        {

        }
        public void Delete()
        {
            string filePath = "Ticket/" + id + ".txt";
            File.Delete(filePath);

            list.Remove(this);

        }

        public void DisplayAll()
        {
            Console.WriteLine("Ticket");
            Console.WriteLine("--------");
            Console.WriteLine("Id : " + Id);
            Console.WriteLine("From : " + FromDestination);
            Console.WriteLine("To : " + ToDestination);
            Console.WriteLine("Date of depature : " + DateOfDeparture.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("TicketType : " + TicketType.ToString());
            Console.WriteLine("Cabin number : " + CabinNr);
            Console.WriteLine("timeOfCreation : " + TimeOfCreation);
            Console.WriteLine("timeOfLastChange : " + TimeOfLastChange);
        }

        public string ListString()
        {
            return $"{fromDestination} to {toDestination} | {DateOfDeparture.ToString("HH:mm dd/MM/yyyy")} | {ticketType.ToString()}";
        }


        //Static Methods
        public static bool CreateNew()
        {
            #region Declaring variables
            List<string> stringList = new List<string>();

            string newId;
            string newFromDestination;
            string newToDestination;
            string newDayOfDeparture;
            string newTicketType;
            string newCabinNr;
            string newTimeOfCreation;
            string newTimeOfLastChange;

            #endregion

            #region Instantiating Variables
            Console.WriteLine("Ticket");
            Console.WriteLine("-------");

            newId = FileManager.GetValidtNewID("./Ticket").ToString();

            if (!Validater.AskForValidtInputLoop("From Destination : ", Validater.IsName, out newFromDestination))
                return false;
            if (!Validater.AskForValidtInputLoop("To Destination : ", Validater.IsName, out newToDestination))
                return false;
            if (!Validater.AskForValidtInputLoop("Date of Departure (dd/mm/yyyy hh:mm) : ", Validater.IsDate, out newDayOfDeparture))
                return false;
            if (!Validater.AskForValidtInputLoop("TicketType (FirstClass, SecondClass, Economy ) : ", Validater.IsTicketType, out newTicketType))
                return false;
            if (!Validater.AskForValidtInputLoop("Cabin number : ", Validater.IsInt, out newCabinNr))
                return false;

            newTimeOfCreation = DateTime.Now.ToString();

            newTimeOfLastChange = DateTime.Now.ToString();

            #endregion

            #region Add string variables to list

            stringList.Add(newId);
            stringList.Add(newFromDestination);
            stringList.Add(newToDestination);
            stringList.Add(newDayOfDeparture);
            stringList.Add(newTicketType);
            stringList.Add(newCabinNr);
            stringList.Add(newTimeOfCreation);
            stringList.Add(newTimeOfLastChange);

            #endregion

            //Save it to file and add to list
            string fileName = "Ticket/" + newId + ".txt";
            FileManager.StoreDataListInFile(fileName, stringList);
            list.Add(new Ticket(fileName));
            return true;
        }
        public static void SetupList()
        {
            list = new List<Ticket>();

            foreach (var filePath in Directory.GetFiles("./Ticket"))
            {
                list.Add(new Ticket(filePath));
            }

            list.Sort((a, b) => a.Id.CompareTo(b.Id));
        }
    }

}
