using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDeweyDecimal
{
    public class MatchingColumns
    {
        //data type resposible for random selection
        Random randomise = new Random();


        // List to store descriptions
        public List<string> descriptionsList = new List<string>();


        //string type list, storing call numbers
        private List<string> theDeweyNumbers;

        public string numberAndDesc;
        public List<string> thenumberAndDesc = new List<string>();



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

        public MatchingColumns()
        {

            //refresh data
            randomise = new Random();
            theDeweyNumbers = new List<string>();
            descriptionOfRange = DescriptionOfRangeDictionary();

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public Dictionary<string, (int, int)> DescriptionOfRangeDictionary()
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




        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public void GenerateDeweyNumbers()
        {
            usedRandomDescriptions.Clear(); // Clear the list of used descriptions
            usedDeweyNumbers.Clear(); // Clear the list of used Dewey numbers

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
                //descriptionsList.Add($"{description}");

            }
            ShuffleMatchedDescriptions();

            // Generate descriptions that do not match the random numbers
            usedRandomDescriptions.Clear(); // Clear the list of used descriptions
            for (int i = 0; i < 3; i++)
            {
                string randomDescription = GetRandomNonMatchingDescription();
                Console.WriteLine($"Random Description: {randomDescription}");
                descriptionsList.Add($"{randomDescription}");
                usedRandomDescriptions.Add(randomDescription); // Track used description

            }
        }



        // Get a random description that does not match the Dewey Decimal number
        private string GetRandomNonMatchingDescription()
        {
            string randomDescription;
            do
            {
                randomDescription = theDescriptions[randomise.Next(theDescriptions.Count)];
            } while (usedDeweyNumbers.Any(dewey => FindMatch(dewey) == randomDescription) || usedRandomDescriptions.Contains(randomDescription));
            return randomDescription;
        }

        public void ShuffleMatchedDescriptions()
        {
            Random rand = new Random();

            // Create a list to store the matched descriptions
            List<string> matchedDescriptions = new List<string>();

            // Find the matched descriptions and add them to the list
            for (int i = 0; i < 4; i++)
            {
                string deweyNumber = usedDeweyNumbers[i];
                string description = FindMatch(deweyNumber);
                matchedDescriptions.Add(description);
            }

            int n = matchedDescriptions.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);

                // Swap matchedDescriptions[i] and matchedDescriptions[j]
                string temp = matchedDescriptions[i];
                matchedDescriptions[i] = matchedDescriptions[j];
                matchedDescriptions[j] = temp;
            }

            // Add the shuffled matched descriptions to descriptionsList
            for (int i = 0; i < 4; i++)
            {
                descriptionsList.Add(matchedDescriptions[i]);
            }
        }



        public List<string> GetAllDescriptions()
        {
            return descriptionsList;
        }

        public void DisplayDescription()
        {
            Console.WriteLine("All Descriptions:");
            foreach (string description in descriptionsList)
            {
                Console.WriteLine(description);
            }



        }

    }



}




//-----------------------------------------------End of file-------------------------------------------------------------------------------------------------------//




