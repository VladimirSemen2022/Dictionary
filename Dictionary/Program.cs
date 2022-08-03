using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "Dictionary.json";
            string newStr, choice, word, str;
            List<Words> newDictionary = new List<Words>();
            Words newWord = new Words();

            do
            {
                if (File.Exists(fileName))
                {
                    newStr = File.ReadAllText(fileName);
                    newDictionary = JsonSerializer.Deserialize<List<Words>>(newStr);
                }
                Console.Clear();
                Console.WriteLine("-----------THE ENGLISH DICTIONARY-----------");
                Console.WriteLine("Input number of operation you want to do:");
                Console.WriteLine("0. Exit;");
                Console.WriteLine("1. Search and show the english word and his translate;");
                Console.WriteLine("2. Add the new word and his translate;");
                Console.WriteLine("3. Delete the word;");
                Console.WriteLine("4. Show all sorted dictionary`s word list .");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        if (newDictionary.Count>0)
                        {
                            Console.Write("Input english word you want to search - ");
                            word = Console.ReadLine();
                            if (newDictionary.Exists(x => x.English.ToLower() == word.ToLower()))
                                Console.WriteLine($"\n{newDictionary[newDictionary.FindIndex(x => x.English.ToLower() == word.ToLower())].ToString()}\n");
                            else
                                Console.WriteLine("\nThe inputed word didn`t find!\n");
                        }
                        else
                            Console.WriteLine("\nThe dictionary is empty yet!\n");
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Input new english word to add in the dictionary - ");
                        newWord.English = Console.ReadLine();
                        Console.Write("Input translate for english word - ");
                        newWord.Translate = Console.ReadLine();
                        if (newDictionary.Exists(x => x.English.ToLower() == newWord.English.ToLower()))
                        {
                            Console.WriteLine("\nThis english word already exsists in the dictionary!\n");
                            Console.WriteLine(newDictionary[newDictionary.FindIndex(x => x.English.ToLower() == newWord.English.ToLower())].ToString());
                            Console.Write("\nIf you want to change it translate press Y, else press N. Press key - ");
                            if (Console.ReadKey().Key == ConsoleKey.Y)
                            {
                                newDictionary[newDictionary.FindIndex(x => x.English.ToLower() == newWord.English.ToLower())].Translate = newWord.Translate;
                                Console.WriteLine($"\nWord {newWord.English.ToUpper()} was changed!");
                            }
                        }
                        else
                        {
                            newDictionary.Add(newWord);
                            Console.WriteLine($"\nWord {newWord.English.ToUpper()} was added!");
                        }
                        str = JsonSerializer.Serialize<List<Words>>(newDictionary);
                        File.WriteAllText(fileName, str);
                        Thread.Sleep(2000);
                        break;
                    case "3":
                        Console.Clear();
                        if (newDictionary.Count > 0)
                        {
                            Console.Write("Input an english word you want to delete from the dictionary - ");
                            word = Console.ReadLine();
                            if (newDictionary.FindIndex(x => x.English.ToLower() == word.ToLower()) != -1)
                            {
                                newDictionary.RemoveAt(newDictionary.FindIndex(x => x.English.ToLower() == word.ToLower()));
                                str = JsonSerializer.Serialize<List<Words>>(newDictionary);
                                File.WriteAllText(fileName, str);
                                Console.WriteLine($"\nWord {word.ToUpper()} was deleted!");
                            }
                            else
                                Console.WriteLine("\nThe word didn`t find!");
                        }
                        else
                            Console.WriteLine("\nThe dictionary is empty yet!\n");
                        Thread.Sleep(2000);
                        break;
                    case "4":
                        Console.Clear();
                        List<Words> sortList = new List<Words>();
                        sortList = newDictionary;
                        sortList.Sort(delegate (Words x, Words y)
                        {
                            if (x.English == null && y.English == null) return 0;
                            else if (x.English == null) return -1;
                            else if (y.English == null) return 1;
                            else return x.English.CompareTo(y.English);
                        });
                        foreach (Words item in sortList)
                            Console.WriteLine(item.ToString());
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        break;

                }
            } while (choice != "0");
        }
    }
}
