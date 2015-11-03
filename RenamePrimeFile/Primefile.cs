/*
The MIT License(MIT)
Copyright(c) 2015 Freddy Juhel
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.IO;
namespace RenamePrimeFile
{
  public class Primefile
  {
    public string LongfileName { get; set; }
    public string ShortfileName { get; set; }
    private const string Keyword = "primes";
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public bool WrongSecondNumber { get; set; }
    public bool HasFirstNumber { get; set; }

    public Primefile(string longFileName)
    {
      LongfileName = longFileName;
      if (longFileName != string.Empty)
      {
        ProcessFile();
      }
    }

    public void ProcessFile()
    {
      ShortfileName = GetDirectoryFileNameAndExtension(LongfileName)[1];
      string firstNumberString = ShortfileName.Split('-')[0].Substring("primes".Length);
      string secondNumberString = ShortfileName.Split('-')[1];
      int firstNumber;
      int secondNumber;
      if (int.TryParse(secondNumberString, out secondNumber))
      {
        SecondNumber = secondNumber;
        WrongSecondNumber = false;
      }
      else
      {
        WrongSecondNumber = true;
      }

      if (!int.TryParse(firstNumberString, out firstNumber))
      {
        FirstNumber = -1;
        HasFirstNumber = false;
      }
      else
      {
        FirstNumber = firstNumber;
        HasFirstNumber = true;
      }
    }

    public string GetFirstNumber()
    {
      string result = string.Empty;
      try
      {
        var sr = new StreamReader(LongfileName);
        result = sr.ReadLine();
        sr.Close();
      }
      catch (Exception)
      {
        // ignored
      }
      return result;
    }

    private string CreateFileName()
    {
      string result = AddSlash(GetDirectoryFileNameAndExtension(LongfileName)[0]) +
               "primes" + FirstNumber + "-" + SecondNumber + ".txt";
      return result;
    }

    public bool AddFirstNumberToFileName()
    {
      bool result = false;
      if (GetFirstNumber() != string.Empty)
      {
        try
        {
          File.Replace(LongfileName, CreateFileName(), "", true);
        }
        catch (Exception)
        {
          // ignored
        }
      }
      return result;
    }

    private static string[] GetDirectoryFileNameAndExtension(string filePath)
    {
      string directory = Path.GetDirectoryName(filePath);
      string fileName = Path.GetFileNameWithoutExtension(filePath);
      string extension = Path.GetExtension(filePath);

      return new[] { directory, fileName, extension };
    }

    private static string AddSlash(string myString)
    {
      return myString.EndsWith("\\") ? myString : myString + "\\";
    }
  }
}