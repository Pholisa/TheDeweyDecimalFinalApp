using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumber
{
    public class GenerateRandom
    {
        //data type resposible for random selection
        private Random randomise;

        //string type list, storing call numbers
        private List<string> theDeweyNumbers;

        public GenerateRandom()
        {
            //refresh data
            randomise = new Random();
            theDeweyNumbers = new List<string>();

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        //generates random numbers
        public string DeweyDecimalGenerate()
        {
            //ensure that the number is generate within the range of 000-1000
            int beginCallNumber = randomise.Next(000, 1000);

            //ensure that the number is generate two digit numbers
            int midCallNumber = randomise.Next(100);

            // Define the characters that can be used in the notation
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Generate a random length (1 to 3 characters)
            int length = randomise.Next(3,3);

            string notation = new string(Enumerable.Repeat(characters, length)
                .Select(s => s[randomise.Next(s.Length)]).ToArray());

            //concates
            string theDeweyNumber = $"{beginCallNumber:D3}.{midCallNumber:D2} {notation:D3}";
            theDeweyNumbers.Add(theDeweyNumber);
            
            return theDeweyNumber;
             }

                //displayes the dewey numbers
            public List<string> GetAllDeweyNumbers()
            {
            return theDeweyNumbers;
                                          }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public void SortNumbers()
        {
            var sortedNumbers = theDeweyNumbers
         .OrderBy(number => int.Parse(number.Substring(0, 3))) // Sort by numeric part
         .ThenBy(number => number.Substring(7)); // Then sort by the letters





        }
    }
}


//-----------------------------------------------End of file-------------------------------------------------------------------------------------------------------//






