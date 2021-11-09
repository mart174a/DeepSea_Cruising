using System;
using System.Collections.Generic;
using System.IO;

namespace DeepSea_Cruising
{
    class Crew : Person
    {
        public enum Role {Captain,Sailor,Chef,Waiter,Engineer};

        //Private Variables
        Role role;
        DateTime workStartTime;
        DateTime workEndTime;

        public static List<Crew> list = new List<Crew>();

        //Construct from file
        public Crew(string filePath) : base(filePath)
        {
            var dataLines = FileManager.GetDataStringListFromFile(filePath);

            this.role = (Role)Enum.Parse(typeof(Role), dataLines[6]);
            this.workStartTime = DateTime.Parse(dataLines[7]);
            this.workEndTime = DateTime.Parse(dataLines[8]);
        }

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
            Console.WriteLine("Crew member Info");
            Console.WriteLine("--------------");
            Console.WriteLine("Id : " + Id);
            Console.WriteLine("Name : " + Name);
            Console.WriteLine("Email : " + Email);
            Console.WriteLine("Phone number: " + PhoneNumber);
            Console.WriteLine("Date of birth : " + DateOfBirth.ToString("dd/MM/yyyy"));
            Console.WriteLine("Gender : " + GenderIdentity);
            Console.WriteLine("Role : " + role);
            Console.WriteLine("Work start : " + workStartTime.ToString("HH:mm"));
            Console.WriteLine("Work end : " + workEndTime.ToString("HH:mm"));
            Console.WriteLine();
        }

        public string ListString()
        {
            return $"{role} | {Name} | {GenderIdentity} | {DateOfBirth.ToString("dd/MM/yyyy")} | {PhoneNumber} | {Email}";
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
            string newRole;
            string newWorkStartTime;
            string newWorkEndTime;

            #endregion

            #region Instantiating Variables
            Console.Clear();
            Console.WriteLine("Crew Member Info (Write Exit to exit)");
            Console.WriteLine("--------------------------------------");

            newId = FileManager.GetValidtNewID("./Crew").ToString();

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
            if (!Validater.AskForValidtInputLoop("Role (Captain,Sailor,Chef,Waiter,Engineer) : ", Validater.IsCrewRole, out newRole))
                return false;
            if (!Validater.AskForValidtInputLoop("Work Start Time (hh:mm) : ", Validater.IsDate, out newWorkStartTime))
                return false;
            if (!Validater.AskForValidtInputLoop("Work End Time (hh:mm) : ", Validater.IsDate, out newWorkEndTime))
                return false;
            Console.WriteLine();

            #endregion

            #region Add string variables to list

            stringList.Add(newId);
            stringList.Add(newName);
            stringList.Add(newEmail);
            stringList.Add(newPhoneNumber);
            stringList.Add(newDateOfBirth);
            stringList.Add(newGenderIdentity);
            stringList.Add(newRole);
            stringList.Add(newWorkStartTime);
            stringList.Add(newWorkEndTime);

            #endregion

            //Save it to file and add til list
            string fileName = "Crew/" + newId + ".txt";
            FileManager.StoreDataListInFile(fileName, stringList);
            list.Add(new Crew(fileName));
            return true;
        }

        #region Find costumer
        public static Crew Find(string searchSting)
        {
            Crew crewMemberToReturn;

            if (Validater.IsInt(searchSting))
            {
                crewMemberToReturn = FindById(searchSting);
                if (crewMemberToReturn != null)
                    return crewMemberToReturn;
            }

            if (Validater.IsName(searchSting))
            {
                crewMemberToReturn = FindByName(searchSting);
                if (crewMemberToReturn != null)
                    return crewMemberToReturn;
            }

            if (Validater.IsPhoneNumber(searchSting))
            {
                crewMemberToReturn = FindByPhoneNumber(searchSting);
                if (crewMemberToReturn != null)
                    return crewMemberToReturn;
            }

            if (Validater.None(searchSting))
            {
                crewMemberToReturn = FindByEmail(searchSting);
                if (crewMemberToReturn != null)
                    return crewMemberToReturn;
            }

            return null;
        }
        static Crew FindById(string id)
        {
            try
            {
                foreach (var crew in list)
                {
                    if (crew.Id == int.Parse(id))
                        return crew;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        static Crew FindByPhoneNumber(string phoneNumber)
        {
            foreach (var crew in list)
            {
                if (crew.PhoneNumber == int.Parse(phoneNumber))
                    return crew;
            }

            return null;
        }
        static Crew FindByEmail(string email)
        {
            foreach (var crew in list)
            {
                if (crew.Email.ToUpper() == email.ToUpper())
                    return crew;
            }

            return null;
        }
        static Crew FindByName(string name)
        {
            foreach (var crew in list)
            {
                if (crew.Name.ToUpper() == name.ToUpper())
                    return crew;
                if (crew.Name.ToUpper().Contains(name.ToUpper()))
                    return crew;
            }

            return null;
        }

        #endregion
    }
}
