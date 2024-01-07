//Folder

var folder = @"C:\Image Reader\Main\";

//File names
var fileKeyWords = "KeyWords.txt";
var fileKeyWordsLog = "KeyWordsHistory.txt";
var fileLastRunErrorLog = "ErrorLogBit.txt";

//File paths
var pathToFileKeyWords = folder + fileKeyWords;
var pathToFileKeyWordsLog = folder + fileKeyWordsLog;
var pathToFileLastRunErrorLog = folder + fileLastRunErrorLog;

string readText = null;
var errorForFileKeyWords = false;
var errorForFileKeyWordsHistory = false;
var errorGettingFilePreviousRun = false;

//If there was an error during the previous iteration then do not continue, create an error file with a 1 bit for true.
//Dev must fix error state cause, and delete ErrorLogBit to protect loop error continued.
if (FileExists(pathToFileLastRunErrorLog, out errorGettingFilePreviousRun) && errorGettingFilePreviousRun == false)
{
    return;
}

if (errorGettingFilePreviousRun)
{
    //Assume error since error file bit should always be used to prevent a continued loop during error state true.
    return;
}

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
    if (errorForFileKeyWords)
    {
        CreateFileErrorStateBit(pathToFileLastRunErrorLog);
        return;
    }

    //No file to read in for keywords so end here.
    return;
}

//Create the history file if it does not exist.
var strFirstLine = "Log file created at: " + DateTime.Now;
CreateFileIfNotExists(pathToFileKeyWordsLog, strFirstLine, out errorForFileKeyWordsHistory);
if (errorForFileKeyWordsHistory)
{
    CreateFileErrorStateBit(pathToFileLastRunErrorLog);
    return;
}

WriteToFileIfExists(pathToFileKeyWordsLog, "", out errorForFileKeyWordsHistory);
if (errorForFileKeyWordsHistory == false)
{
    var strLogDateTime = "New entry added at: " + DateTime.Now;
    WriteToFileIfExists(pathToFileKeyWordsLog, strLogDateTime, out errorForFileKeyWordsHistory);
}

if (errorForFileKeyWordsHistory == false)
{
    WriteToFileIfExists(pathToFileKeyWordsLog, readText, out errorForFileKeyWordsHistory);
}

if (errorForFileKeyWordsHistory)
{
    CreateFileErrorStateBit(pathToFileLastRunErrorLog);
    return;
}

if (string.IsNullOrEmpty(readText) == false)
{
    Console.Write(readText);
    //Console.ReadKey();
}

//File.Delete(fullPathIncomingFile);


static bool CreateFileErrorStateBit(string pathToFileLastRunErrorLog)
{
    var errorWritingErrorBit = false;
    var success = CreateFileIfNotExists(pathToFileLastRunErrorLog, "Error State: 1, at: " + DateTime.Now,
        out errorWritingErrorBit);
    if (errorWritingErrorBit)
    {
        return false;
    }

    return success;
}

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