using RandomNumber;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDeweyDecimalFinalApp
{
    public partial class ReplacingBooks : Form
    {
        private List<string> sortedDeweyNumbers; // Declare sortedDeweyNumbers at the class level

        // Declare a variable to hold a reference to a thread object
        Thread theThread;

        private GenerateRandom randomGenerator;

        private List<string> correctOrder;

        public ReplacingBooks()
        {
            InitializeComponent();
            randomGenerator = new GenerateRandom();
            correctOrder = new List<string>();

            button2.Enabled = false;


            // Populate listView2 with randomly generated items
            for (int i = 0; i < 10; i++)
            {
                string deweyNumber = randomGenerator.DeweyDecimalGenerate();
                correctOrder.Add(deweyNumber);

          


                ListViewItem item = new ListViewItem(deweyNumber);
                listView2.Items.Add(item);
            }

            // Set up event handlers for listView1
            listView1.AllowDrop = true;
            listView1.ItemDrag += ListView_ItemDrag;
            listView1.DragEnter += ListView_DragEnter;
            listView1.DragDrop += ListView_DragDrop;

            // Set up event handlers for listView2
            listView2.AllowDrop = true;
            listView2.ItemDrag += ListView_ItemDrag;
            listView2.DragEnter += ListView_DragEnter;
            listView2.DragDrop += ListView_DragDrop;


            // Using LINQ to sort the Dewey Decimal numbers.
            // The numbers will be sorted first then the letters.
            sortedDeweyNumbers = correctOrder.OrderBy(number => number).ThenBy(number => number.Substring(7)).ToList();

          

        }

        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Start dragging the item
            ListView listView = sender as ListView;
            if (listView != null)
            {
                listView.DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            // Allow dropping on the list view
            e.Effect = DragDropEffects.Move;
        }

        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            // Handle item drop on the list view
            ListView listView = sender as ListView;
            if (listView != null)
            {
                ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                if (draggedItem != null)
                {
                    // Add the dragged item to the target list view
                    listView.Items.Add((ListViewItem)draggedItem.Clone());

                    // Remove the dragged item from the source list view
                    RemoveItemFromSourceListView(draggedItem);

                    // Increment progress bar
                    IncrementProgressBar();


                }
            }
        }



        private void RemoveItemFromSourceListView(ListViewItem item)
        {
            ListView sourceListView = item.ListView;
            if (sourceListView != null)
            {
                sourceListView.Items.Remove(item);
            }
        }

        private void IncrementProgressBar()
        {
            progressBar1.Value += 10;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("All items have been moved!");

                // Enable button2 when the progress bar is full
                button2.Enabled = true;

                // Reset the progress bar if needed
                progressBar1.Value = 0;
            }
        }

        private void CheckOrder()
        {

            List<string> currentOrder = new List<string>();


            // Update the correctOrder list with the sorted values
            correctOrder.Clear();
            correctOrder.AddRange(currentOrder);

            // Extract dewey numbers from listView1
            foreach (ListViewItem item in listView1.Items)
            {
                currentOrder.Add(item.Text);
            }

            if (currentOrder.SequenceEqual(sortedDeweyNumbers)) // Use sortedDeweyNumbers instead of currentOrder
            {
                MessageBox.Show("Correct Order!");
            }
            else
            {
                MessageBox.Show("Incorrect Order!");
            }

            StringBuilder message = new StringBuilder("Correct Order:\n");

            foreach (string item in sortedDeweyNumbers)
            {
                message.AppendLine(item);
            }

            MessageBox.Show(message.ToString(), "Order Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            theThread = new Thread(OpenReplaceBooks_ClickForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check the order
            CheckOrder();

           

        }
    }
}
