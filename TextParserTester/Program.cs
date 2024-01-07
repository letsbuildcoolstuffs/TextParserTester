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
var strFirstLine = "Log file created at: " + DateTime.Now;
CreateFileIfNotExists(pathToFileKeyWordsLog, strFirstLine, out errorForFileKeyWordsHistory);

if (errorForFileKeyWordsHistory == false)
{
    WriteToFileIfExists(pathToFileKeyWordsLog, "", out errorForFileKeyWords);
    if (errorForFileKeyWords == false)
    {
        var strLogDateTime = "New entry added at: " + DateTime.Now;
        WriteToFileIfExists(pathToFileKeyWordsLog, strLogDateTime, out errorForFileKeyWords);
    }

    if (errorForFileKeyWords == false)
    {
        WriteToFileIfExists(pathToFileKeyWordsLog, readText, out errorForFileKeyWords);
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

static bool CreateFileIfNotExists(string pathFull, string text, out bool error)
{
    error = false;

    if (FileExists(pathFull, out error) == false && error == false)
    {
        try
        {
            File.Create(pathFull).Dispose();
            using var sw = new StreamWriter(pathFull);
            sw.WriteLine(text);
        }
        catch (Exception)
        {
            error = true;
            return false;
        }

        return true;
    }

    return false;
}

static bool WriteToFileIfExists(string pathFull, string text, out bool error)
{
    error = false;

    try
    {
        using var sw = new StreamWriter(pathFull, true);
        sw.WriteLine(text);
    }
    catch (Exception)
    {
        error = true;
        return false;
    }

    return true;
}

/*
Notes:
Pseudo code logic:
Idea will be to read in screenshots, parse, and prep a sales list to skip alt tab process
*/