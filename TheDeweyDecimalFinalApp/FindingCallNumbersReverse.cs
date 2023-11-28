using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDeweyDecimalFinalApp
{
    public partial class FindingCallNumbersReverse : Form
    {

        Thread theThread;

        public DewyNode randomCategory;
        public DewyNode randomSubcategory;
        public DewyNode randomDescription;


        public List<DewyNode> wrongCategories = new List<DewyNode>();
        public List<DewyNode> wrongSubcategories = new List<DewyNode>();
        public List<DewyNode> wrongDescriptions = new List<DewyNode>();
        public FindingCallNumbersReverse()
        {
            InitializeComponent();
            DataInTree();
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            btnCheckTwo.Enabled = false;
            btnCheckThree.Enabled = false;
        }

        public void DataInTree()
        {
            DewyNode root = ReadDataFromFile("DewyDecData.txt");
            randomCategory = GetRandomCategory(root);
            randomSubcategory = GetRandomSubcategory(randomCategory);
            randomDescription = GetRandomDescription(randomSubcategory);

            // Get 3 wrong categories
            wrongCategories = GetWrongCategories(root, randomCategory, 3);
            wrongCategories.Add(randomCategory);

            // Get 3 wrong subcategories
            wrongSubcategories = GetWrongSubcategories(randomCategory, randomSubcategory, 3);
            wrongSubcategories.Add(randomSubcategory);

            // Get 3 wrong descriptions
            wrongDescriptions = GetWrongDescriptions(randomSubcategory, randomDescription, 3);
            wrongDescriptions.Add(randomDescription);


            label2.Text = $"What category falls under this number {randomDescription.DewyNum} ?";

            foreach (var item in wrongCategories)
            {
                OptionsCheckListBox.Items.Add(item.CatName);
            }
        }


        public DewyNode ReadDataFromFile(string filePath)
        {
            DewyNode root = new DewyNode("###", "Root");

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    DewyNode currentCategory = null;
                    DewyNode currentSubcategory = null;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string code = line.Substring(0, 3);
                        string name = line.Substring(4);

                        if (int.Parse(code) % 100 == 0)
                        {
                            // This is a category
                            currentCategory = new DewyNode(code, name);
                            root.AddChild(currentCategory);
                            currentSubcategory = null; // Reset currentSubcategory
                        }
                        else if (int.Parse(code) % 10 == 0)
                        {
                            // This is a subcategory
                            currentSubcategory = new DewyNode(code, name);
                            currentCategory?.AddChild(currentSubcategory);
                        }
                        else
                        {
                            // This is a description
                            currentSubcategory?.AddChild(new DewyNode(code, name));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }

            return root;
        }


        public DewyNode GetRandomCategory(DewyNode root)
        {
            Random random = new Random();
            List<DewyNode> categories = root.Children;
            int randomIndex = random.Next(categories.Count);
            return categories[randomIndex];
        }

        public DewyNode GetRandomSubcategory(DewyNode category)
        {
            Random random = new Random();
            List<DewyNode> subcategories = category.Children;
            int randomIndex = random.Next(subcategories.Count);
            return subcategories[randomIndex];
        }

        public DewyNode GetRandomDescription(DewyNode subcategory)
        {
            Random random = new Random();
            List<DewyNode> descriptions = subcategory.Children;
            int randomIndex = random.Next(descriptions.Count);
            return descriptions[randomIndex];
        }


        public List<DewyNode> GetWrongCategories(DewyNode root, DewyNode correctCategory, int count)
        {
            Random random = new Random();
            List<DewyNode> wrongCategories = new List<DewyNode>();

            while (wrongCategories.Count < count)
            {
                DewyNode randomCategory = GetRandomCategory(root);
                if (randomCategory != correctCategory && !wrongCategories.Contains(randomCategory))
                {
                    wrongCategories.Add(randomCategory);
                }
            }

            return wrongCategories;
        }

        public List<DewyNode> GetWrongSubcategories(DewyNode correctCategory, DewyNode correctSubcategory, int count)
        {
            Random random = new Random();
            List<DewyNode> wrongSubcategories = new List<DewyNode>();

            while (wrongSubcategories.Count < count)
            {
                DewyNode randomSubcategory = GetRandomSubcategory(correctCategory);
                if (randomSubcategory != correctSubcategory && !wrongSubcategories.Contains(randomSubcategory))
                {
                    wrongSubcategories.Add(randomSubcategory);
                }
            }

            return wrongSubcategories;
        }

        public List<DewyNode> GetWrongDescriptions(DewyNode correctSubcategory, DewyNode correctDescription, int count)
        {
            Random random = new Random();
            List<DewyNode> wrongDescriptions = new List<DewyNode>();

            while (wrongDescriptions.Count < count)
            {
                DewyNode randomDescription = GetRandomDescription(correctSubcategory);
                if (randomDescription != correctDescription && !wrongDescriptions.Contains(randomDescription))
                {
                    wrongDescriptions.Add(randomDescription);
                }
            }

            return wrongDescriptions;
        }

        public void DisplayNodeList(List<DewyNode> nodeList)
        {
            foreach (var node in nodeList)
            {
                Console.WriteLine($"Code: {node.CatName}, Name: {node.DewyNum}");
            }
        }


        //Redirecting to theReverse Form

        private void button3_Click(object sender, EventArgs e)
        {
            theThread = new Thread(ReverseForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();
        }


        //Calling the Reverse Form
        private void ReverseForm()
        {
            Application.Run(new FindingCallNumbers());
        }

        private void btnCheckThree_Click_1(object sender, EventArgs e)
        {
            var answer = OptionsCheckListBox.CheckedItems[0].ToString();
            if (answer == randomDescription.CatName)
            {
                MessageBox.Show("You have managed to finish finding call number successfully!!!!");

                // Make the picture box visible
                pictureBox3.Visible = true;

                OptionsCheckListBox.Items.Clear();

            }
            else
            {
                MessageBox.Show($"You have selected the wrong match! The correct answer is  {randomDescription.CatName}!");

            }
        }

        private void btnCheckTwo_Click_1(object sender, EventArgs e)
        {
            var answer = OptionsCheckListBox.CheckedItems[0].ToString();
            if (answer == randomSubcategory.CatName)
            {
                MessageBox.Show("Your answer is correct!!!");

                // Make the picture box visible
                pictureBox2.Visible = true;

                OptionsCheckListBox.Items.Clear();
                foreach (var item in wrongDescriptions)
                {
                    OptionsCheckListBox.Items.Add(item.CatName);
                }

                btnCheckTwo.Visible = false;
                btnCheckThree.Enabled = true;
            }
            else
            {
                MessageBox.Show($"You have selected the wrong match! The correct answer is  {randomSubcategory.CatName}!");

            }
        }

        private void btnCheckOne_Click_1(object sender, EventArgs e)
        {
            var answer = OptionsCheckListBox.CheckedItems[0].ToString();


            if (answer == randomCategory.CatName)
            {
                MessageBox.Show("Your answer is correct!!!");

                // Make the picture box visible
                pictureBox1.Visible = true;

                OptionsCheckListBox.Items.Clear();
                foreach (var item in wrongSubcategories)
                {
                    OptionsCheckListBox.Items.Add(item.CatName);
                }

                btnCheckOne.Visible = false;
                btnCheckTwo.Enabled = true;
            }
            else
            {
                MessageBox.Show($"You have selected the wrong match! The correct answer is  {randomCategory.CatName}!");

            }
        }

        private void btnReplaceBooks_Click(object sender, EventArgs e)
        {
            theThread = new Thread(OpenReplaceBooks_ClickForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//


        private void OpenReplaceBooks_ClickForm()
        {
            Application.Run(new ReplacingBooks());
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//



        private void btnFindCallNum_Click(object sender, EventArgs e)
        {
            theThread = new Thread(OpenFindCallNumForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//


        private void OpenFindCallNumForm()
        {
            Application.Run(new FindingCallNumbers());
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//


        private void btnIdentAreas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select one radio button under 'Call Number,' then click the 'Capture Call Number'" +
              " button. After that, choose one radio button under 'Descriptions' and press the 'Capture Descriptions' button.");

            theThread = new Thread(OpenIdentifyAreasForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();
        }

        private void OpenIdentifyAreasForm()
        {
            Application.Run(new ReverseIdentifyingAreas());
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//


        private void btnExit_Click(object sender, EventArgs e)
        {
            //close programme
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            theThread = new Thread(OpenReverseFindCallNumForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();

        }

        private void OpenReverseFindCallNumForm()
        {
            Application.Run(new FindingCallNumbersReverse());
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

    }
}
    
