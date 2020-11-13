using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DLTP_Phase2_Code_Refactoring
{
    /* CLASS: Person
     * PURPOSE: Defines what data each new object should have. Will ask for name, adress, phonenumber & email for every object
     * If user request to add a new entry to List<Person> Dict, the class Person will request these arguments.
     */
    class Person
    {
        public string name, adress, phone, email;

        /* METHOD: Person Constructor (static)
         * PURPOSE: Constructor for Person. Will require for four entries (name, adress, phone & email)
         * PARAMETERS: name: Full name of new contact, adress: Adress of new contact, phone: Phonenumber of new contact, email: Emailadress of new contact
         * RETURN VALUE: none
         */
        public Person(string N, string A, string P, string E)
        {
            name = N; adress = A; phone = P; email = E;
        }
        /* METHOD: Person (static)
        * PURPOSE: When called, will ask user for four entries (name, adress, phone & email) to be created and added into the list
        * PARAMETERS: none
        * RETURN VALUE: Values for Person object
        */
        public Person()
        {
            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            adress = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }
        /* METHOD: EditValue (static)
         * PURPOSE: When called for, will take values and arguments stored in valueToEdit & newValue and change stored value in list for that specific contact (index)
         * PARAMETERS: valueToEdit: What category to change (name, adress, phone or email) newValue: What new value that selected category should have from now on.
         * RETURN VALUE: none
         */
        public void EditValue(string valueToEdit, string newValue, List<Person> Dict)
        {
            switch (valueToEdit)
            {
                case "namn": name = newValue; break;
                case "adress": adress = newValue; break;
                case "telefon": phone = newValue; break;
                case "email": email = newValue; break;
                default: break;
            }
        }
        /* METHOD: Print (static)   
         * PURPOSE: To print out a specific object in form of contact information stored in List<Person> Dict   
         * PARAMETERS: List<Person> Dict: List that contains Person objects. Ask for name, adress, phone & email for that specific index in Dict.
         * RETURN VALUE: returvärdets innebörd   
         */
        public void Print()
        {
            Console.WriteLine($"{name}, {adress}, {phone}, {email}");
        }
    }
    class Program
    {
        private static List<Person> Dict = new List<Person>();
        static void Main(string[] args)
        {
            LoadAdressFile();
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                switch (command)
                {
                    case "sluta": Console.WriteLine("Hej då!"); break;
                    case "ny": AddPerson(); break;
                    case "ta bort": RemovePerson(); break;
                    case "visa": PrintList(); break;
                    case "ändra": EditPersonValue(); break;
                    default: Console.WriteLine("Okänt kommando: {0}", command); break;
                }
            } while (command != "sluta");
        }
        /* METHOD: AddPerson (static)
         * PURPOSE: Uses the constructor in Person to ask user about what values to be added into the list
         * PARAMETERS: List<Person> Dict: List that contains Person objects
         * RETURN VALUE: None
         */
        static void AddPerson()
        {
            Console.WriteLine("Lägger till ny person");
            Dict.Add(new Person());
        }
        /* METHOD: EditPersonValue (static)
         * PURPOSE: Gives the option for user to change a value set on a specific person in the List<Person> Dict. 
         * Takes two arguments from user and pass it through into the Person.EditValue + List<Person> Dict.
         * PARAMETERS: List<Person> Dict: List that contains Person objects. valueToEdit: What argument user wants to change. newValue: What value user wants to change on that argument. 
         * RETURN VALUE: None
         */
        static void EditPersonValue()
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string nameOfPersonToEdit = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == nameOfPersonToEdit) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", nameOfPersonToEdit);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string valueToEdit = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", valueToEdit, nameOfPersonToEdit);
                string newValue = Console.ReadLine();
                Person P = Dict[found];
                P.EditValue(valueToEdit, newValue, Dict);

            }
        }
        /* METHOD: RemovePerson (static)
         * PURPOSE: Gives user the option to remove a entry in the list by asking for the name and remove that specific index from list
         * PARAMETERS: List<Person> Dict: List that contains Person objects
         * RETURN VALUE: None
         */
        static void RemovePerson()
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string nameToRemove = Console.ReadLine();
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == nameToRemove) found = i;
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", nameToRemove);
            }
            else
            {
                Dict.RemoveAt(found);
            }
        }
        /* METHOD: PrintList (static)
         * PURPOSE: Print out name, adress, phone & email for all objects stored in list
         * PARAMETERS: List<Person> Dict: List that contains Person objects
         * RETURN VALUE: None
         */
        static void PrintList()
        {
            for (int i = 0; i < Dict.Count(); i++)
            {
                Dict[i].Print();
            }
        }
        /* METHOD: LoadAdressFile (static)
         * PURPOSE: To locate and read a specific file. Will read each line of formated text one by one and split by #. Adds each splitted row into a new object in List<Person> Dict
         * PARAMETERS: List<Person> Dict to add new created objects
         * RETURN VALUE: None
         */
        static void LoadAdressFile()
        {
            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    string[] word = line.Split('#');
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }
    }
}