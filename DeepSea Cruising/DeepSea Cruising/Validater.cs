using System;
using System.Linq;

namespace DeepSea_Cruising
{
    public static class Validater
    {
        public delegate bool ValidationBool(string stringToValidate);

        public static bool AskForValidtInputLoop(string askString, Validater.ValidationBool validationBool, out string validString)
        {
            string stringToReturn;
            bool isInputValidt;
            while (true)
            {
                Console.Write(askString);
                stringToReturn = Console.ReadLine();
                isInputValidt = validationBool(stringToReturn);

                if (stringToReturn.ToUpper() == "EXIT")
                {
                    validString = null;
                    return false;
                }

                else if (isInputValidt)
                {
                    validString = stringToReturn;
                    return true;
                }

                else
                {
                    //error
                    WriteCode.WriteError("Invalidt input");
                    Console.ReadKey();
                    WriteCode.ClearMultipleLines(2);
                }
            }
        }

        //Always true
        public static bool None(string stringToValidate)
        {
            return true;
        }
        //Is int
        public static bool IsInt(string inputToValidate)
        {

            if (!int.TryParse(inputToValidate, out int x))
                return false;
            else
                return true;
        }

        //vallidt number
        public static bool IsPhoneNumber(string inputToValidate)
        {
            ////cant start with 0
            if (inputToValidate.StartsWith("0"))
                return false;

            ////Needs 8 digits
            if (inputToValidate.Length != 8)
                return false;

            //Must be a number
            if (int.TryParse(inputToValidate, out int number) == false)
            {
                return false;
            }

            //It passed all criteria
            return true;
        }

        //vallidt gender
        public static bool IsGender(string inputToValidate)
        {
            if (!Enum.TryParse<Person.Gender>(inputToValidate, out Person.Gender t))
                return false;

            return true;
        }

        //Validt ticket type
        public static bool IsTicketType(string inputToValidate)
        {
           if(!Enum.TryParse<Ticket.Type>(inputToValidate, out Ticket.Type t))
                return false;

            return true;
        }

        //Validt Port type
        public static bool IsPort(string inputToValidate)
        {
            if (!Enum.TryParse<Ticket.Port>(inputToValidate, out Ticket.Port p))
                return false;

            return true;
        }

        //Validt ticket type
        public static bool IsCrewRole(string inputToValidate)
        {
            if (!Enum.TryParse<Crew.Role>(inputToValidate, out Crew.Role t))
                return false;

            return true;
        }

        //Validt dateTime
        public static bool IsDate(string inputToValidate)
        {
            if (!DateTime.TryParse(inputToValidate, out DateTime d))
                return false;

            return true;
        }

        //Is name/only contains letters and have min 2 letters
        public static bool IsName(string inputToValidate)
        {
            //Must be atleast 2 chars long
            if (inputToValidate.Length < 2)
                return false;

            //if its not a letter or a white space
            if (inputToValidate.Any(ch => !Char.IsLetter(ch) && !Char.IsWhiteSpace(ch)))
                return false;

            return true;
        }

    }
}
