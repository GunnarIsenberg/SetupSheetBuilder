namespace SetupBuilder
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
             * initalizing instance variables, These should probably be placed elsewhere
             */

            string[] positions = { "Shift Leader", "Breaker", "Prep", "Secondary", "Fries", "Primary 1", "Breader", "Machines",
                "Primary 2", "Utilities", "Buns", "Secondary 2", "Trainee", "Trainer", "Team Leader" };
            string tmPath = @"C:\Users\Gunna\OneDrive\Desktop\Projects\SetupSheetBuilder2\Data\teammembers.txt";
            string[] functionalityOptions = { "Manage Team Members", "Create A SetupSheet", "Exit The Program"};


            /*
             * Initalizing All nessecary objects and calling function to chose what the user would like to do.
             */

            do 
            {

                LocalMenus MainMenu = new LocalMenus();
                string currentOption = MainMenu.ChoseOption(functionalityOptions);
                if (currentOption == "3")
                {
                    break;
                }
                else if (currentOption == "2")
                {
                    SetupSheet TodaysSetup = new SetupSheet();
                    TodaysSetup.createTodaysSetup(positions);
                }
                else if (currentOption == "1")
                { 
                    PersonelManagement TeamManager = new PersonelManagement();
                    List<string> currentTeamMembers = TeamManager.GetCurrentTeamMembers(tmPath);
                }
            }while (true);
        }
    }
    public class LocalMenus
    {

        /*
         * this will take in noun and a format and display them, then confirm the user input is correct returnin the value is desired.
         */
        public void MainMenu()
        {
            Console.WriteLine("Please Select which fucntionality you would like to use right now!");
            Console.WriteLine("1) Create a setupsheet for today");
            Console.WriteLine("2) Manage active Team Members");
            Console.WriteLine("3) Manage SetupSheet Settings *NOT CURRENTLY ACTIVE");
            string currentFunctionality = GetCorrectInput("desired functionality"," '1', '2', or '3' ");
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

        private string[] GetSetupNames()
        {
            LocalMenus setupMenu = new LocalMenus();
            string allNamesString = setupMenu.GetCorrectInput("team members names", "Greg Bob Steven Daniel Robert Mike");
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






        public List<string> GetCurrentTeamMembers(string path)
        {
            List<string> currentTeamMembers = File.ReadAllLines(path).ToList();
            return currentTeamMembers;
        }

        public void AddTeamMember(string path, string name)
        {
            List<string> currentTeamMembers = GetCurrentTeamMembers(path);
            currentTeamMembers.Add(name);
        }

        public List<string> RemoveTeamMember(List<string> currenTeamMembers, string targetTeamMember)
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

        public List<string> GetTeamMember(List<string> currentTeamMembers, string targetTeamMember)
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

        public string ManageTeamMember(List<string> targetTeamMember, Dictionary<int, string> indexPosition, string[] ratings)
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