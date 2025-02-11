using System.Globalization;
using System.Text.RegularExpressions;


namespace ExpenseTracker
{
    public class Driver
    {
        static void Main(string[] args)
        {
            StreamWriter inWriter;
            StreamWriter exWriter;
            StreamReader inReader;
            StreamReader exReader;
            string directory = @"C:\Expenses\ExpenseTracker";

            List<Income> totalIncome = new();
            List<Expense> totalExpense = new();

            double balance = 0;
            bool released = false;
            bool exited = false;
            int choice = -1;
            string choice2 = "";
            
            string desc = "";
            bool recur = false;
            bool necces = false;
            double hours = 0;
            double amount = 0.0;
            double rate = 0.0;

            //Creates a directory for the income and expenses if one doesn't exist already
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }


            //Greets the user before asking what they want to do. If they don't provide a valid input, it loops until they do.
            Console.WriteLine("Welcome to the Expense Tracker!");

            while (!exited)
            {
                Console.Write("What would you like to do?\n\n1. Check balance\n2. Add a transaction\n" +
                    "3. View current expenses\n4. View current income\n5. Clear all transactions\n6. Exit" +
                    "\nYour response: ");

                released = false;
                while (!released)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                        released = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\nOnly enter one of the numbers on the menu. Try again.\n");

                        Console.WriteLine("What would you like to do?\n\n1. Check balance\n2. Add a transaction\n" +
                        "3. View current expenses\n4. View current income\n" +
                        "5. Clear all transactions\n6. Exit\nYour response: ");

                    }
                }

                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        if (totalExpense.Count == 0 && totalIncome.Count == 0)
                        {
                            Console.WriteLine($"\nYour current balance is: ${balance}");
                        }
                        else
                        {
                            for (int i = 0; i < totalExpense.Count(); i++)
                            {
                                try
                                {
                                    balance += totalExpense[i].getAmount();
                                }
                                catch (ArgumentNullException)
                                {
                                    balance += 0;
                                }

                            }

                            for (int i = 0; i < totalIncome.Count(); i++)
                            {
                                try
                                {
                                    balance += totalIncome[i].getAmount();
                                }
                                catch (ArgumentNullException)
                                {
                                    balance += 0;
                                }

                            }

                            Console.WriteLine($"\nYour current balance is: ${balance}");
                        }
                        
                        break;

                    case 2:

                        //Asks the user if they're adding an expense or income then asks them further questions accordingly.
                        //Loops if the user enters an invalid input
                        Console.Write("\nWhat are you adding?\n1. Income\n2. Expense\nYour response: ");

                        released = false;
                        while (!released)
                        {
                            try
                            {
                                choice = int.Parse(Console.ReadLine());
                                if (!(choice == 1 || choice == 2))
                                {
                                    Console.WriteLine("\nOnly enter '1' or '2' on the keyboard. Try again.\n");
                                    Console.Write("What are you adding?\n1. Income\n2. Expense\nYour response: ");
                                }
                                else
                                {
                                    released = true;
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("\nOnly enter '1' or '2' on the keyboard. Try again.\n");
                                Console.Write("What are you adding?\n1. Income\n2. Expense\nYour response: ");
                            }

                        }

                        //Happens for both expenses and income
                        Console.Write("\nIs it recurring? (Enter T or F for true or false): ");
                        {
                            released = false;
                            while (!released)
                            {
                                choice2 = Console.ReadLine();
                                if (choice2.Equals("T"))
                                {
                                    recur = true;
                                    released = true;
                                }
                                else if (choice2.Equals("F"))
                                {
                                    recur = false;
                                    released = true;
                                }
                                else
                                {
                                    Console.Write("\nOnly enter 'T' or 'F' on the keyboard. Try again: ");
                                }
                            }


                        }

                        //Income Path
                        if (choice == 1)
                        {
                            //Asks the user for the income's description before splitting down a rate or a flat sum
                            Console.Write("Where is the income from?: ");
                            desc = Console.ReadLine();
                            Console.Write("Is it time-based or a flat sum? (Enter 1 or 2 respectively): ");

                            choice = 0;
                            while (!(choice == 1 || choice == 2))
                            {
                                try
                                {
                                    choice = int.Parse(Console.ReadLine());
                                    if (!(choice == 1 || choice == 2))
                                    {
                                        Console.WriteLine("\nOnly enter '1' or '2' on the keyboard. Try again.\n");
                                    }

                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("\nOnly enter '1' or '2' on the keyboard. Try again.\n");
                                }
                                catch (ArgumentNullException)
                                {
                                    Console.WriteLine("\nOnly enter '1' or '2' on the keyboard. Try again.\n");
                                }
                            }

                            //Rate choice
                            if (choice == 1)
                            {
                                Console.Write("How many hours did you work?: ");

                                released = false;
                                while (!released)
                                {
                                    try
                                    {
                                        hours = double.Parse(Console.ReadLine());
                                        released = true;

                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                    }
                                    catch (ArgumentNullException)
                                    {
                                        Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                    }
                                }

                                Console.Write("How much do you make an hour?: ");

                                released = false;
                                while (!released)
                                {
                                    try
                                    {
                                        rate = double.Parse(Console.ReadLine());
                                        released = true;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                    }
                                    catch (ArgumentNullException)
                                    {
                                        Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                    }
                                }

                                Console.WriteLine("Adding new income...");
                                Income newIn = new(desc, recur, hours, rate);
                                totalIncome.Add(newIn);

                                inWriter = new StreamWriter(@"C:\Expenses\ExpenseTracker\Income.txt", true);
                                inWriter.WriteLine(newIn.getDesc() + ',' + newIn.getAmount() + ',' + newIn.getRec());
                                inWriter.Close();
                            }

                            //Raw amount choice
                            else if (choice == 2)
                            {
                                released = false;
                                while (!released)
                                {
                                    try
                                    {
                                        Console.Write("How much is it?: ");
                                        amount = double.Parse(Console.ReadLine());
                                        released = true;
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                    }
                                    catch (ArgumentNullException)
                                    {
                                        Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                    }

                                }

                                Console.WriteLine("Adding new income...");
                                Income newIn = new(desc, amount, recur);
                                totalIncome.Add(newIn);
                                inWriter = new StreamWriter(@"C:\Expenses\ExpenseTracker\Income.txt", true);
                                inWriter.WriteLine(newIn.getDesc() + ',' + newIn.getAmount() + ',' + newIn.getRec());
                                inWriter.Close();
                            }
                        }

                        //Expense Path
                        else if (choice == 2)
                        {
                            Console.Write("What is the expense for?: ");
                            desc = Console.ReadLine();

                            released = false;
                            while (!released)
                            {
                                try
                                {
                                    Console.Write("How much is it?: ");
                                    amount = double.Parse(Console.ReadLine());
                                    released = true;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                }
                                catch (ArgumentNullException)
                                {
                                    Console.WriteLine("\nOnly enter a numeral or decimal. Try again.\n");
                                }

                            }

                            Console.Write("Is this expense neccessary? (T for true, F for false): ");

                            choice2 = Console.ReadLine();

                            released = false;
                            while (!released)
                            {
                                if (choice2.Equals("T"))
                                {
                                    necces = true;
                                    released = true;
                                }
                                else if (choice2.Equals("F"))
                                {
                                    necces = false;
                                    released = true;
                                }
                                else
                                {
                                    Console.WriteLine("\nOnly enter 'T' or 'F' on the keyboard. Try again.");
                                }
                            }

                            //Adding expense to file
                            Expense newEx = new(desc, amount, recur, necces);
                            totalExpense.Add(newEx);

                            Console.WriteLine("\nAdding expense...");
                            exWriter = new StreamWriter(@"C:\Expenses\ExpenseTracker\Expenses.txt", true);
                            exWriter.WriteLine(newEx.getDesc() + ',' + newEx.getAmount() + ',' + newEx.getRec() + ',' + newEx.getNeces());
                            exWriter.Close();

                        }
                        break;

                    case 3:

                        
                        Console.WriteLine();
                        //Refreshes total expense before reading all expenses to ensure correct output

                        totalExpense.Clear();
                        exReader = new StreamReader(@"C:\Expenses\ExpenseTracker\Expenses.txt");
                        string[] rawEx = new string[4];
                        
                        while (!exReader.EndOfStream)
                        {
                            rawEx = exReader.ReadLine().Split(',');
                            if (rawEx.Length == 4)
                            {
                                Expense newE = new Expense(rawEx[0], double.Parse(rawEx[1]), bool.Parse(rawEx[2]), bool.Parse(rawEx[3]));
                                totalExpense.Add(newE);
                            }
                            
                        }
                        exReader.Close();

                        if (totalExpense.Count() == 0)
                        {
                            Console.WriteLine("No expenses available!");
                        }
                        else
                        {
                            //Displays all expenses to the user by their order in the file (will work on sorting by certain options later)
                            for (int i = 0; i < totalExpense.Count(); i++)
                            {
                                Console.Write(i + ".\n" + totalExpense[i].ToString() + "\n\n");
                            }
                        }
                        
                        break;

                    case 4:

                        Console.WriteLine();

                        //Refreshes total income before reading all credits to ensure correct output
                        inReader = new StreamReader(@"C:\Expenses\ExpenseTracker\Income.txt");
                        totalIncome.Clear();
                        string[] rawIn = new string[3];

                        while (!inReader.EndOfStream)
                        {
                            rawIn = inReader.ReadLine().Split(',');
                            if (rawIn.Length == 3)
                            {
                                Income newI = new Income(rawIn[0], double.Parse(rawIn[1]), bool.Parse(rawIn[2]));
                                totalIncome.Add(newI);
                            }
                            
                        }
                        inReader.Close();

                        if(totalIncome.Count == 0)
                        {
                            Console.WriteLine("No income available!");
                        }
                        else
                        {
                            //Displays all expenses to the user by their order in the file (will work on sorting by certain options later)
                            for (int i = 0; i < totalIncome.Count(); i++)
                            {
                                Console.Write(i + ".\n" + totalIncome[i].ToString() + "\n");
                            }
                        }
                        break;

                    case 5:
                        Console.WriteLine("\nClearing...");
                        inWriter = new StreamWriter(@"C:\Expenses\ExpenseTracker\Income.txt");
                        exWriter = new StreamWriter(@"C:\Expenses\ExpenseTracker\Expenses.txt");

                        inWriter.WriteLine();
                        exWriter.WriteLine();

                        inWriter.Close();
                        exWriter.Close();


                        Console.WriteLine("Cleared!");
                        break;
                    
                    //Closes the application
                    case 6:
                        Console.WriteLine("\nThank you for using the expense tracker. Goodbye!");
                        exited = true;
                        break;
                }

                Console.WriteLine("\n");

               


            }
        }
    }
}
