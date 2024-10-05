using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace Petpals.Exceptions
    {
        // Custom exception for Invalid Pet Age
        public class InvalidPetAgeException : Exception
        {
            public InvalidPetAgeException():base("Invalide age") { }
            public InvalidPetAgeException(string message) : base(message) { }

        }

        // Custom exception for Insufficient Funds
        public class InsufficientFundsException : Exception
        {
        public InsufficientFundsException() : base("Enter valid funds") { }
        public InsufficientFundsException(string message) : base(message) { }
        }

        // Custom exception for Null Reference
        public class NullReferencePetException : Exception
        {
        public NullReferencePetException() : base("Can't access values which are null") { }
        public NullReferencePetException(string message) : base(message) { }
    }

        // Custom exception for File Handling
        public class FileHandlingException : Exception
        {
            public FileHandlingException(string message) : base(message) { }
        }
    }


