using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDeweyDecimal
{
    public class MatchingColumnsTwo
    {
        //data type resposible for random selection
        Random randomise = new Random();


        // List to store descriptions
        public List<string> descriptionsList = new List<string>();

        public string numberAndDesc;
        public List<string> thenumberAndDesc = new List<string>();

        //string type list, storing call numbers
        private List<string> theDeweyNumbers;

        // Keep track of used random descriptions and Dewey numbers
        private List<string> usedRandomDescriptions = new List<string>();
        public List<string> usedDeweyNumbers = new List<string>();


        private List<string> theDescriptions = new List<string>
        {
            "General Knowledge",
            "Philosophy & Psychology",
            "Religion",
            "Social Sciences",
            "Languages",
            "Physical Sciences",
            "Technology",
            "Arts & Culture",
            "Literature",
            "History & Geography"



        };


        Dictionary<string, (int startRange, int endRange)> descriptionOfRange;


        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public MatchingColumnsTwo()
        {

            //refresh data
            randomise = new Random();
            theDeweyNumbers = new List<string>();
            descriptionOfRange = DescriptionOfRangeDictionary();

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        private Dictionary<string, (int, int)> DescriptionOfRangeDictionary()
        {

            Dictionary<string, (int, int)> descriptionOfRange = new Dictionary<string, (int, int)>();

            descriptionOfRange["General Knowledge"] = (000, 99);
            descriptionOfRange["Philosophy & Psychology"] = (100, 199);
            descriptionOfRange["Religion"] = (200, 299);
            descriptionOfRange["Social Sciences"] = (300, 399);
            descriptionOfRange["Languages"] = (400, 499);
            descriptionOfRange["Physical Sciences"] = (500, 599);
            descriptionOfRange["Technology"] = (600, 699);
            descriptionOfRange["Arts & Culture"] = (700, 799);
            descriptionOfRange["Literature"] = (800, 899);
            descriptionOfRange["History & Geography"] = (900, 999);


            return descriptionOfRange;
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public string FindMatch(string deweyNumber)
        {
            // Split the Dewey Decimal number into its components
            string[] parts = deweyNumber.Split('.');

            if (parts.Length == 2)
            {
                // Parse the start range from the first part
                if (int.TryParse(parts[0], out int callNumber))
                {
                    foreach (var entry in descriptionOfRange)
                    {
                        if (callNumber >= entry.Value.startRange && callNumber <= entry.Value.endRange)
                        {
                            return entry.Key;
                        }
                    }
                }
            }
            return null;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        //generates random numbers
        public String DeweyDecimalGenerate()
        {
            //ensure that the number is generate within the range of 000-1000
            int beginCallNumber = randomise.Next(000, 1000);

            //ensure that the number is generate two digit numbers
            int midCallNumber = randomise.Next(100);

            // Define the characters that can be used in the notation
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Generate a random length (1 to 3 characters)
            int length = randomise.Next(3, 3);



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

        //-----------------------------------------------------New---------------------------------------------------------------------------------------------//
        public void GenerateRandomDescriptionsAndNumbers()
        {
            usedRandomDescriptions.Clear(); // Clear the list of used descriptions
            usedDeweyNumbers.Clear(); // Clear the list of used Dewey numbers

            // Generate 4 random descriptions with matching Dewey numbers
            for (int i = 0; i < 4; i++)
            {
                string deweyNumber;
                string description;
                do
                {
                    deweyNumber = DeweyDecimalGenerate();
                    description = FindMatch(deweyNumber);
                } while (usedRandomDescriptions.Contains(description) || usedDeweyNumbers.Contains(deweyNumber));

                usedRandomDescriptions.Add(description); // Track used description
                usedDeweyNumbers.Add(deweyNumber); // Track used Dewey number

                numberAndDesc = $"Dewey Number: {deweyNumber}, Description: {description}";

                thenumberAndDesc.Add(numberAndDesc);

                Console.WriteLine($"Dewey Number: {deweyNumber}, Description: {description}");
                descriptionsList.Add($"{description}");
            }

            // Generate 3 random descriptions that do not match any Dewey numbers
            usedRandomDescriptions.Clear(); // Clear the list of used descriptions
            for (int i = 0; i < 3; i++)
            {
                string randomDeweyNumber;
                do
                {
                    randomDeweyNumber = DeweyDecimalGenerate();
                } while (usedDeweyNumbers.Contains(randomDeweyNumber));

                usedDeweyNumbers.Add(randomDeweyNumber); // Track used Dewey number

                Console.WriteLine($"Random Dewey Number: {randomDeweyNumber}");
            }
        }

        public void ShuffleDeweyNumbers()
        {
            Random rand = new Random();

            int n = theDeweyNumbers.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);

                // Swap theDeweyNumbers[i] and theDeweyNumbers[j]
                string temp = theDeweyNumbers[i];
                theDeweyNumbers[i] = theDeweyNumbers[j];
                theDeweyNumbers[j] = temp;
            }
        }


    }
}

//--------------------------------------------------------------------------------------------------------------------------------------------------//



