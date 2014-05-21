using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTextSummarizer;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace IntellidateLib
{
   public static class TextToWords
    {
       public static string[] ConvertTextToWords(string InputText)
       {
           try
           {
               string dicPath =Path.GetDirectoryName(new UriBuilder(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Path);
               dicPath = Directory.GetParent(dicPath).FullName;
               SummarizerArguments sumargs = new SummarizerArguments
               {
                   DictionaryLanguage = "en",
                   DisplayLines = 2,
                   DisplayPercent = 0,
                   InputFile = Constants.emptyString,
                   InputString = InputText
               };
               SummarizedDocument doc = Summarizer.Summarize(sumargs, dicPath);
               var concepts = doc.Concepts.ToArray();
               return concepts;
           }
           catch (Exception ex)
           {
               new Error().LogError(ex, Constants.textToWordsClass, Constants.convertTextToWordsMethod);
               return null;
           }
       }
    }
}
