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
    public partial class IdentifyingAreas : Form
    {
        Thread theThread;

        MatchingColumnsTwo mc = new MatchingColumnsTwo();

        private RadioButton[] buttons;

        private RadioButton[] buttonNumbers;

        public string saveValue;
        public String saveDescription;

        public IdentifyingAreas()
        {
            InitializeComponent();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;

            Assigning();
            AssigningNumbers();

        }
        public void Assigning()
        {

            // All button added to the array
            buttons = new RadioButton[]
            {
                rbtnDescOne,
                rbtnDescTwo,
                rbtnDescThree,
                rbtnDescFour

            };

            mc.GenerateRandomDescriptionsAndNumbers();
            List<string> theDescription = mc.descriptionsList;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = theDescription[i];
            }



        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public void AssigningNumbers()
        {


            // All button added to the array
            buttonNumbers = new RadioButton[]
            {
                rbtnNumberOne,
                rbtnNumberTwo,
                rbtnNumberThree,
                rbtnNumberFour,
                rbtnNumberFive,
                rbtnNumberSix,
                rbtnNumberSeven

            };

            mc.GenerateRandomDescriptionsAndNumbers();

            mc.ShuffleDeweyNumbers();

            List<string> theDescription = mc.GetAllDeweyNumbers();

            for (int i = 0; i < buttonNumbers.Length; i++)
            {
                buttonNumbers[i].Text = theDescription[i];
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void btnNew_Click(object sender, EventArgs e)
        {
            theThread = new Thread(OpenCallNumberMatch);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();
        }

        private void OpenCallNumberMatch()
        {
            Application.Run(new IdentifyingAreas());
        }

        private void btnCapture_Click_1(object sender, EventArgs e)
        {
            if (rbtnDescOne.Checked)
            {
                saveValue = rbtnDescOne.Text;
                // Make the picture box visible
                pictureBox3.Visible = true;

                MessageBox.Show("Description " + saveValue);

            }
            else if (rbtnDescTwo.Checked)
            {
                saveValue = rbtnDescTwo.Text;
                // Make the picture box visible
                pictureBox4.Visible = true;

                MessageBox.Show("Description " + saveValue);
            }
            else if (rbtnDescThree.Checked)
            {
                // Make the picture box visible
                pictureBox5.Visible = true;
                saveValue = rbtnDescThree.Text;

                MessageBox.Show("Description " + saveValue);
            }
            else if (rbtnDescFour.Checked)
            {
                saveValue = rbtnDescFour.Text;

                // Make the picture box visible
                pictureBox6.Visible = true;

                MessageBox.Show("Description " + saveValue);
            }
            else
            {
                MessageBox.Show("Try again");
            }
        }

        private void btnCaptureDescript_Click_1(object sender, EventArgs e)
        {

            mc.DeweyDecimalGenerate();


            RadioButton[] radioButtonArray = {  rbtnDescOne,
                rbtnDescTwo,
                rbtnDescThree,
                rbtnDescFour };
            RadioButton[] buttonArray = { rbtnNumberOne,
                rbtnNumberTwo,
                rbtnNumberThree,
                rbtnNumberFour, };

            if (rbtnNumberFive.Checked || rbtnNumberSix.Checked || rbtnNumberSeven.Checked)
            {
                saveDescription = "Incorrect!";
                MessageBox.Show(saveDescription);
                return;
            }
            else
            {

                for (int i = 0; i < radioButtonArray.Length; i++)
                {
                    if (radioButtonArray[i].Checked)
                    {
                        saveValue = buttonArray[i].Text;
                        saveDescription = radioButtonArray[i].Text;

                        MessageBox.Show("Call Number: " + saveValue + "  Description: " + saveDescription);
                        MessageBox.Show("Your Match:" + "\n" + "Call Number: " + saveValue + "  Description: " + saveDescription
                                            + "\n" + "\n" + "Correct Match:" + "\n" + mc.thenumberAndDesc[i]);
                        break; // Assuming you want to exit the loop after finding the checked radio button.
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            theThread = new Thread(IdentifyAreasReverseForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();
        }

        private void IdentifyAreasForm()
        {
            Application.Run(new IdentifyingAreas());
        }

        private void btnIdentAreas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select one radio button under 'Call Number,' then click the 'Capture Call Number'" +
                " button. After that, choose one radio button under 'Descriptions' and press the 'Capture Descriptions' button.");
            theThread = new Thread(IdentifyAreasReverseForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();
        }

        private void IdentifyAreasReverseForm()
        {
            Application.Run(new ReverseIdentifyingAreas());
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


    }
}

