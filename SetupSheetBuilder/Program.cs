namespace SetupSheetBuilder
{
    class program
{
    static void Main()
    {
        SetupSheet TodaysSetup = new SetupSheet();
        Console.WriteLine(TodaysSetup.GetSetupArray());
    }
}

public class SetupSheet
{
    public string GetSetupArray()
    {
        Console.WriteLine("Please enter the names of your Team Members today");
        string teamMemberNames = Console.ReadLine(); 
        Console.WriteLine(teamMemberNames);
        return teamMemberNames;
    }
}
}