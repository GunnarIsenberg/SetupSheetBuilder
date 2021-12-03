namespace SetupBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] positions = { "Shift Leader", "Breaker", "Prep", "Secondary", "Fries", "Primary 1", "Breader", "Machines",
                "Primary 2", "Utilities", "Buns", "Secondary 2", "Trainee", "Trainer", "Team Leader" };
            string tmPath = @"C:\Users\Gunna\OneDrive\Desktop\Projects\SetupSheetBuilder2\Data\teammembers.txt";


            SetupSheet TodaysSetup = new SetupSheet();
            TodaysSetup.createTodaysSetup(positions);
        }
    }
    public class LocalMenus
    {

        /*
         * this will take in noun and a format and display them, then confirm the user input is correct returnin the value is desired.
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
         *  Designed to be a multiple choice type question. 
         */



        public void ChoseFunctionality(string[] options)
        {
            int currentOption = 1;
            Console.WriteLine("Please type a number without formatting to select what you would like to do!");
            foreach (string option in options)
            {
                Console.WriteLine(currentOption + "option");
            }

            do
            {

            } while (true);
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
        string couldNotFindTm = "Error: Could not locate That Team member";


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

        public string GetTeamMember(List<string> currentTeamMembers, string targetTeamMember)
        {
            if (currentTeamMembers.Contains(targetTeamMember))
            {
                int currentLocation = currentTeamMembers.IndexOf(targetTeamMember);
                string teamMember = currentTeamMembers[currentLocation];
                teamMember.Split(" ");
                Console.WriteLine("Which atribute would you like to edit?");
                foreach(string atribute in teamMember)
                {
                    Console.WriteLine(atribute);
                }
            }
            else
            { 
                Console.WriteLine(couldNotFindTm);
                return targetTeamMember;
            }
        }


    }   




    }
}