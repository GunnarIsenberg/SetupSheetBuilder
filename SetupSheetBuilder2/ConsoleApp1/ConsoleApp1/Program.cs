using System.Collections;
using System;
using System.Linq;

namespace SetupBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalMenus currentMenu = new LocalMenus();
            currentMenu.MainMenu();
        }
    }
    public class LocalMenus
    {

        /*
         * The initailization of all objects and menu to allow the user to 
         */
        public void MainMenu()
        {
            string[] positions = { "Shift Leader", "Breaker", "Prep", "Secondary", "Fries", "Primary 1", "Breader", "Machines",
                "Primary 2", "Utilities", "Buns", "Secondary 2", "Trainee", "Trainer", "Team Leader" };
            
            string[] functionalityOptions = { "Manage Team Members", "Create A SetupSheet", "Exit The Program" };

            SetupSheet todaysSetup = new SetupSheet();
            PersonelManagement teamManager = new PersonelManagement();
            
            Console.WriteLine("Please Select which fucntionality you would like to use right now!");
            Console.WriteLine("1) Create a setupsheet for today");
            Console.WriteLine("2) Manage active Team Members");
            Console.WriteLine("3) Manage SetupSheet Settings *NOT CURRENTLY ACTIVE");
            string currentFunctionality = GetCorrectInput("desired functionality"," '1', '2', or '3' ");

            if (currentFunctionality == "1")
            {
                todaysSetup.InitalizeShiftSetup();
            }
            else if (currentFunctionality == "2")
            {
                Console.WriteLine("test");
            }
            else
            {
                Console.WriteLine("That is currently not an accepted action");
            }
        }

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
         *  Designed to be a multiple choice type question. 
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
        /*
         * returns a dictionary where the key is a name and the value is a positions taken from an array
         */

        public Dictionary<string, string> createTodaysSetup(string[] positionArray)
        {
            string[] teamMemberNames = GetSetupNames();
            int currentIndex = 0;
            Dictionary<string, string> assignedPositions = new Dictionary<string, string>();
            foreach (string teamMember in teamMemberNames)
            {
                assignedPositions.Add(teamMember, positionArray[currentIndex]);
                Console.WriteLine(teamMember, positionArray[currentIndex]);
                currentIndex++;
            }
            return assignedPositions;

        }


        /*
         * relies upon the menu class to get a list of names for the setupsheet. 
         */

        public Dictionary<string, string> BuildSetupSheet(Stack<string> positions, Queue<string> tms, int currentIndex, Dictionary<string, string> passedDictionary)
        {
            string currentTeamMember = (string)tms.Dequeue();
            string[] currentarray = currentTeamMember.Split(" ");

            List<string> tmString = new List<string>();
            tmString = currentarray.ToList();

            string tmName = tmString[0];
            tmString.Remove(tmName);

            List<int> tmAtributes = new List<int>();
            foreach (string atrbite in tmString)
            {
                int tmAtribute = int.Parse(atrbite);
                tmAtributes.Add(tmAtribute);
            }

            while (tms.Count > 0)
            {
                string currentPosition = (string)positions.Peek();

                if (tmAtributes[0] >= 4 && passedDictionary.Count() == 0)
                {
                    passedDictionary.Add(tmName, currentPosition);
                    currentIndex--;
                    BuildSetupSheet(positions, tms, currentIndex, passedDictionary);
                }
                else if (tmAtributes[currentIndex] <= 2)
                {
                    passedDictionary.Add(tmName, currentPosition);
                    currentIndex--;
                    BuildSetupSheet(positions, tms, currentIndex, passedDictionary);
                }
                else
                {
                    tms.Enqueue(currentTeamMember);
                    positions.Push(currentPosition);
                    return passedDictionary;
                }
            }
            return passedDictionary;
        }


        public Dictionary<string, string> InitalizeShiftSetup()
        {
            Stack<string> positions = new Stack<string>();
            positions.Push("Shift Leader");
            positions.Push("Breaker");
            positions.Push("Prep");
            positions.Push("Secondary");
            positions.Push("Fries");
            positions.Push("Primary");
            positions.Push("Breader");
            positions.Push("Machines");
            positions.Push("Utilities");
            positions.Push("Buns");
            positions.Push("Training");

            int indexOfStack = 1;
            int indexOfQue = 1;
            int startingIndex = 11;
            string[] todaysTeam = GetSetupNames();
            Queue<string> teamQue = new Queue<string>();
            Dictionary<string, string> assignedPositions = new Dictionary<string, string>();

            foreach (string tm in todaysTeam)
            { 
                teamQue.Enqueue(tm);
            }

            return BuildSetupSheet(positions, teamQue, startingIndex, assignedPositions);
        }

        private string[] GetSetupNames()
        {
            LocalMenus setupMenu = new LocalMenus();
            string allNamesString = setupMenu.GetCorrectInput("team members names", "Greg_Anderson Bob_Michaels Steven_DuPont Daniel_Lynn Robert_DeNiro Mike_Wazowski");
            string[] allNamesList = allNamesString.Split(" ");
            return allNamesList;
        }

       

    }

    public class PersonelManagement
    {

        /*
         * instance variables that help define properties in their relation to the restraunt.
         */

        string couldNotFindTm = "Error: Could not locate That Team member";

        Dictionary<int, string> indexPositionPair = new Dictionary<int, string>
        {
            {01 , "Leader Ship Level" },
            {02 , "Prep Level" },
            {03, "Secondary Level" },
            {04, "Fries Level"},
            {05, "Primary Level"},
            {06, "Breader Level"},
            {07, "Machines Level" },
            {08, "Utilities Level" },
            {09, "Buns Level" },
            {10, "Delete The Team Member" }
        };

        

        
        
        string[] definitionOfLevel = { "0: Cannot perform the job duties", "1: Can perform the duties with help", "2: Can Perform the duties without help", 
            "3: Can solve issues or perform the job on a weekend alone", "4: Can Perform the job duties to Chick-Fil-A standard alone", "5: May teach the Job to others"};

        private List<string> GetCurrentTeamMembers(string path)
        {
            List<string> currentTeamMembers = File.ReadAllLines(path).ToList();
            foreach(string tm in currentTeamMembers)
            {
                Console.WriteLine(tm);
            }
            return currentTeamMembers;
        }

        private void AddTeamMember(string path, string name)
        {
            List<string> currentTeamMembers = GetCurrentTeamMembers(path);
            currentTeamMembers.Add(name);
        }

        private List<string> RemoveTeamMember(List<string> currenTeamMembers, string targetTeamMember)
        {
            if (currenTeamMembers.Contains(targetTeamMember))
            {
                currenTeamMembers.Remove(targetTeamMember);
                return currenTeamMembers;
            }
            else
            {
                Console.WriteLine(couldNotFindTm);
                return currenTeamMembers;
            }
        }

        private List<string> GetTeamMember(List<string> currentTeamMembers, string targetTeamMember)
        {
            if (currentTeamMembers.Contains(targetTeamMember))
            {
                int indexOfTarget = currentTeamMembers.IndexOf(targetTeamMember);
                currentTeamMembers.RemoveAt(indexOfTarget);
                string teamMember = currentTeamMembers[indexOfTarget];
                List<string> teamMemberAtributes = teamMember.Split(" ").ToList();
                return teamMemberAtributes;
            }
            else 
            {
                Console.WriteLine(couldNotFindTm);
                return currentTeamMembers;
            } 
                
        }

        private string ManageTeamMember(List<string> targetTeamMember, Dictionary<int, string> indexPosition, string[] ratings)
        {
            
            /*
             * Prints out the scale upon with a team member is graded
             */

            Console.WriteLine("Please remember the scale upon which a Team Member is graded");
            foreach (string rating in ratings)
            {
                Console.WriteLine(rating);
            }

            Console.WriteLine("Please type the following number to update the corresponding skill of the team member.");
            foreach (KeyValuePair<int, string> keyValuePair in indexPosition)
            {
                Console.WriteLine (keyValuePair.Key + keyValuePair.Value);
            }

            string userInput = Console.ReadLine();

            try
            {
                int optionSelected = int.Parse(userInput);
                Console.WriteLine("Please Type what you would like to replace the Skill level with:");
                string updatedSkillLevel = Console.ReadLine();
                targetTeamMember[optionSelected].Replace(targetTeamMember[optionSelected], updatedSkillLevel);
                return targetTeamMember.ToString();
            }
            catch
            {
                Console.Write("failed");
                return targetTeamMember.ToString();
            }
        }


    }    
}