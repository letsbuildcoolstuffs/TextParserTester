//Folder

var folder = @"C:\Image Reader\Main\";

//File names
var fileKeyWords = "KeyWords.txt";
var fileKeyWordsLog = "KeyWordsHistory.txt";

//File paths
var pathToFileKeyWords = folder + fileKeyWords;
var pathToFileKeyWordsLog = folder + fileKeyWordsLog;

string readText = null;
var errorForFileKeyWords = false;
var errorForFileKeyWordsHistory = false;

if (FileExists(pathToFileKeyWords, out errorForFileKeyWords) && errorForFileKeyWords == false)
{
    readText = File.ReadAllText(pathToFileKeyWords).Trim();
    if (string.IsNullOrEmpty(readText))
    {
        return;
    }
}
else
{
    return;
}

//Create the history file if it does not exist.
if (FileExists(pathToFileKeyWordsLog, out errorForFileKeyWordsHistory) == false && errorForFileKeyWordsHistory == false)
{
    try
    {
        File.Create(pathToFileKeyWordsLog).Dispose();
        using var tw = new StreamWriter(pathToFileKeyWordsLog);
        tw.WriteLine("Log file created at: " + DateTime.Now);
    }
    catch (Exception)
    {
        return;
    }
}

if (errorForFileKeyWordsHistory == false)
{
    try
    {
        using var sw = new StreamWriter(pathToFileKeyWordsLog, true);
        sw.WriteLine("");
        sw.WriteLine("New entry added at: " + DateTime.Now);
        sw.WriteLine(readText);
    }
    catch (Exception)
    {
        return;
    }
}

if (string.IsNullOrEmpty(readText) == false)
{
    Console.Write(readText);
    Console.ReadKey();
}

//File.Delete(fullPathIncomingFile);


static bool FileExists(string fullPath, out bool error)
{
    error = false;
    try
    {
        if (File.Exists(fullPath))
        {
            return true;
        }
    }
    catch (Exception)
    {
        error = true;
        return false;
    }

    return false;
}

/*
Notes:
Pseudo code logic:
Idea will be to read in screenshots, parse, and prep a sales list to skip alt tab process
*/