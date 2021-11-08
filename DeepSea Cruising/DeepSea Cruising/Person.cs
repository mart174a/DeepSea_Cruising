using System;

namespace DeepSea_Cruising
{
    class Person
    {
        //Gender Enum
        public enum Gender { Male, Female, NonBinary };

        //Private Variables
        private int id;
        private string name;
        private string email;
        private int phoneNumber;
        private DateTime dateOfBirth;
        private Gender genderIdentity;

        //Public Properties
        public int Id { get => id; }
        public string Name { get => name;}
        public string Email { get => email;}
        public int PhoneNumber { get => phoneNumber;}
        public DateTime DateOfBirth { get => dateOfBirth;}
        internal Gender GenderIdentity { get => genderIdentity;}
        public int Age 
        { 
            get 
            {
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(dateOfBirth.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                return age; 
            } 
        }

        //Contruct from file
        public Person(string filePath)
        {
            var dataLines = FileManager.GetDataStringListFromFile(filePath);

            this.id = int.Parse(dataLines[0]);
            this.name = dataLines[1];
            this.email = dataLines[2];
            this.phoneNumber = int.Parse(dataLines[3]);
            this.dateOfBirth = DateTime.Parse(dataLines[4]);
            this.genderIdentity = (Gender)Enum.Parse(typeof(Gender), dataLines[5]);
        }
    }
}
