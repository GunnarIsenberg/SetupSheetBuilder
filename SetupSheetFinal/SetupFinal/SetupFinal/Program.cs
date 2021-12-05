using System;
using System.Collections;
using System.Linq;

namespace SetupSheetBuilder
{
    class start
    {
        static void Main(string[] args)
        { 
            LocalMenus mainmenu = new LocalMenus();
            mainmenu.MainMenu();
        }
    }

    class LocalMenus
    {
        /*
         * Init all objexts, calls menu to selection functionality in loop, break not implimented
         */
        
        public void MainMenu()
        {
            string[] positions = { "Shift Leader", "Breaker", "Prep", "Secondary", "Fries", "Primary 1", "Breader", "Machines",
                "Primary 2", "Utilities", "Buns", "Secondary 2", "Trainee", "Trainer", "Team Leader" };

            string[] functionalityOptions = { "Manage Team Members", "Create A SetupSheet", "Exit The Program" };

            string dataPath = @"C:\Users\Gunna\OneDrive\Desktop\SetupSheetFinal\Data";

            SetupSheet todaysSetup = new SetupSheet();
            PersonelManager teamManager = new PersonelManager();

            Console.WriteLine("Please Select which fucntionality you would like to use right now!");
            Console.WriteLine("1) Create a setupsheet for today");
            Console.WriteLine("2) Manage active Team Members");
            Console.WriteLine("3) Manage SetupSheet Settings *NOT CURRENTLY ACTIVE");
            string currentFunctionality = GetCorrectInput("desired functionality", " '1', '2', or '3' ");

            if (currentFunctionality == "1")
            {
                string todaysNames = GetCorrectInput("Names", "Mike_Wazowski Donald_Roberts Gary_Squarepants");
                todaysSetup.InitShiftSetup(dataPath, todaysNames);
            }
            else if (currentFunctionality == "2")
            {
                string[] options2 = { "Add a tm", "Delete a tm", "Update a tm", "Go Back"};
                string manageFunction = ChoseOption(options2);
                if (manageFunction == "1")
                {
                    string tmToAdd = GetCorrectInput("Name", "Mike_Wazowski");
                    teamManager.AddTeamMembers(tmToAdd, dataPath);
                }
                else if (manageFunction == "2")
                {
                    string tmToDel = GetCorrectInput("Name", "Mike_Wazowski");
                    teamManager.DeleteTeamMember(dataPath, tmToDel);
                }
                else if (manageFunction == "3")
                {
                    string tmToManage = GetCorrectInput("Name", "Mike_Wazowski");
                    teamManager.SetSkillsForTeamMember(tmToManage);
                }
                else
                {
                    Console.WriteLine("Returning!");
                }
            }
            else
            {
                Console.WriteLine("That is currently not an accepted action");
            }
        }

        /*
         * continues a loop until the user confirms that the string input was correct
         */

        public string GetCorrectInput(string desiredOutput, string desiredFormat)
        {
            do
            {
                Console.WriteLine("Please enter the " + desiredOutput);
                Console.WriteLine("Like the following: " + desiredFormat);

                string formatedOutput = Console.ReadLine();

                Console.WriteLine("You typed the following: " + formatedOutput + "\n  Is that Correct?");
                Console.WriteLine("Type 'y' for yes & 'n' for no.");
                string checkInput = Console.ReadLine();

                if (checkInput == "y")
                {
                    return formatedOutput;
                }
                else
                {
                    continue;
                }
            } while (true);

        }

        /*
         * Creates a list of options for the user to select from an array of string
         */

        public string ChoseOption(string[] options)
        {
            int currentOption = 1;
            foreach (string option in options)
            {
                Console.WriteLine(currentOption + option);
                currentOption++;
            }
            string currentFunction = GetCorrectInput("desired option", " '1' ");
            return currentFunction;
        }

    }

    public class SetupSheet
    {
        public Dictionary<string, string> InitShiftSetup(string path, string names)
        {
            Stack<string> positions = new Stack<string>();
            positions.Push("Training");
            positions.Push("Buns");
            positions.Push("Utilities");
            positions.Push("Machines");
            positions.Push("Breader");
            positions.Push("Primary");
            positions.Push("Fries");
            positions.Push("Shift Leader");
            positions.Push("Secondary");
            positions.Push("Prep");
            positions.Push("Breaker");

            int startingIndex = 0;
            List<string> todaysTeam = GetSetupNames(path, names);
            Queue<string> teamQue = new Queue<string>();

            Dictionary<string, string> assignedPositions = new Dictionary<string, string>();
            foreach (string tm in todaysTeam)
            {
                teamQue.Enqueue(tm);
            }

            return SetupSheetBuilder(positions, teamQue, startingIndex, assignedPositions);
        }

        private List<string> GetSetupNames(string path, string names)
        {
            string[] splitNames = names.Split(" ");
            string[] namesOnFile = File.ReadAllLines(path);
            List<string> verifiedTeamMembers = new List<string>();

            foreach (string name in namesOnFile)
            {
                foreach (string input in splitNames)
                {
                    if (namesOnFile.Contains(input))
                    {
                        verifiedTeamMembers.Add(input);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine(input + "Could not be found in the system");
                    }

                }

            }
            return verifiedTeamMembers;
        }


        private Dictionary<string, string> SetupSheetBuilder(Stack<string> positions, Queue<string> tms, int currentIndex, Dictionary<string, string> passedDictionary)
        {


            string currentTeamMember = (string)tms.Dequeue();
            string[] currentarray = currentTeamMember.Split(" ");
            List<string> teamMemberString = currentarray.ToList();

            string tmName = teamMemberString[0];
            teamMemberString.Remove(tmName);

            List<int> tmAtributes = new List<int>();
            foreach (string atribute in teamMemberString)
            {
                int tmAtribute = int.Parse(atribute);
                tmAtributes.Add(tmAtribute);
            }

            if (tms.Count > 0)
            {
                string currentPosition = (string)positions.Pop();

                if (tmAtributes[0] >= 4 && passedDictionary is null)
                {
                    passedDictionary.Add(tmName, currentPosition);
                    currentIndex++;
                    return SetupSheetBuilder(positions, tms, currentIndex, passedDictionary);
                }
                else if (tmAtributes[currentIndex] >= 2)
                {
                    passedDictionary.Add(tmName, currentPosition);
                    currentIndex++;
                    return SetupSheetBuilder(positions, tms, currentIndex, passedDictionary);
                }
                else
                {
                    positions.Push(currentPosition);
                    tms.Enqueue(tmName);
                    return SetupSheetBuilder(positions, tms, currentIndex, passedDictionary);
                }

            }
            else
            {
                return passedDictionary;
            }


        }
    }

        class PersonelManager
        {
        /*
         * Allows the user to add, remove, and edit existing team members
         */

            public List<string> GetTeamMembers(string path)
            { 
                List<string> currentTeamMembers = File.ReadAllLines(path).ToList();
                return currentTeamMembers;
            }

            public int FetchTeamMember(string path, string targetTeamMember)
            {
            List<string> currentTeamMembers = GetTeamMembers(path);
                int index = 0;
                foreach (string teamMember in currentTeamMembers)
                {
                    index++;
                    List<string> teamMemberBrokenDown = teamMember.Split(" ").ToList();
                    if (index > currentTeamMembers.Count() && teamMemberBrokenDown.Contains(targetTeamMember))
                    {
                        return currentTeamMembers.IndexOf(teamMember);
                    }
                    else
                    {
                        continue;
                    }
                }
                return -1;
            }

            public void DeleteTeamMember(string path, string targetTeamMember)
            {
                List<string> currentTms = GetTeamMembers(path);
                int indexOfTarget = FetchTeamMember(path, targetTeamMember);

                currentTms.RemoveAt(indexOfTarget);
                File.WriteAllLines(path, currentTms);

            }

            public void AddTeamMembers(string userToAdd, string path)
            {
                List<string> currentTeamMembers = GetTeamMembers(path);
                string atributes = SetSkillsForTeamMember(userToAdd);
                string writeToFile = userToAdd + " " + atributes;
                currentTeamMembers.Add(writeToFile);
                File.WriteAllLines(path, currentTeamMembers);
            }

            public string SetSkillsForTeamMember(string name)
            {
                Console.WriteLine("Please type a skill level for each of the skills listed below like so: \n 5 5 5 5 5 5 5 5");
                string updatedAtributes = Console.ReadLine();
                return updatedAtributes;
            }
        
        }
    
}