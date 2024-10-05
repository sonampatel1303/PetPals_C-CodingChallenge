using Petpals.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.Model
{
    public class FileHandler
    {
        public void CheckFile(string filePath)
        {
            filePath= @"C:\Users\SonamPatel\OneDrive\Desktop\summary";
            if (!File.Exists(filePath))
            {
                // Throwing the custom exception if the file is not found
                throw new FileHandlingException($"The file at '{filePath}' was not found.");
            }

            Console.WriteLine("File found..");
           
        }
    }

}