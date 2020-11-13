﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DLTP_Phase2_Code_Refactoring
{
    class Person
    {
        public string name, adress, phone, email;

        public Person(string N, string A, string P, string E)
        {
            name = N; adress = A; phone = P; email = E;
        }
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
        public void EditValue(string valueToEdit, string newValue, List<Person> Dict)
        {
            switch (valueToEdit)
            {
                case "namn": name = newValue; break;
                case "adress":adress = newValue; break;
                case "telefon": phone = newValue; break;
                case "email": email = newValue; break;
                default: break;
            }
        }
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
        static void AddPerson()
        {
            Console.WriteLine("Lägger till ny person");
            Dict.Add(new Person());
        }
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
        static void PrintList()
        {
            for (int i = 0; i < Dict.Count(); i++)
            {
                Dict[i].Print();
            }
        }
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