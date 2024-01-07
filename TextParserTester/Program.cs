var folder = @"C:\Image Reader\Main";

//Filename
var fileName = "KeyWords.txt";
var fullPath = folder + fileName;
string? readText = null;

try
{
    if (File.Exists(fullPath))
    {
        //Open the file to read from.
        readText = File.ReadAllText(fullPath);
    }
}
catch (Exception)
{
    //File could not be opened.
    readText = null;
}


if (string.IsNullOrEmpty(readText))
{
}


/*
Notes:
Pseudo code logic:
Idea will be to read in screenshots, parse, and prep a sales list to skip alt tab process
*/