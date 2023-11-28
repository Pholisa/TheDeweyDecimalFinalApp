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
    public partial class ReverseIdentifyingAreas : Form
    {

        Thread theThread;

        MatchingColumns mc = new MatchingColumns();

        private RadioButton[] buttons;


        private RadioButton[] buttonDescription;

        public string saveValue;
        public String saveDescription;



        public ReverseIdentifyingAreas()
        {

            InitializeComponent();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;

            Assigning();
            AssigningDescription();

        }
        public void Assigning()
        {

            // All button added to the array
            buttons = new RadioButton[]
            {
                rbtnButtonOne,
                rbtnButtonTwo,
                rbtnButtonThree,
                rbtnButtonFour

            };

            mc.GenerateDeweyNumbers();

            List<string> deweyNumber = mc.usedDeweyNumbers;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = deweyNumber[i];
            }


        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        public void AssigningDescription()
        {


            // All button added to the array
            buttonDescription = new RadioButton[]
            {
                rbtnDescriptOne,
                rbtnDescriptTwo,
                rbtnDescriptThree,
                rbtnDescriptFour,
                rbtnDescriptFive,
                rbtnDescriptSix,
                rbtnDescriptSeven

            };


            mc.ShuffleMatchedDescriptions();

            List<string> descriptions = mc.descriptionsList;

            for (int i = 0; i < buttonDescription.Length; i++)
            {
                buttonDescription[i].Text = descriptions[i];
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnCapture_Click_1(object sender, EventArgs e)
        {
            if (rbtnButtonOne.Checked)
            {
                saveValue = rbtnButtonOne.Text;
                // Make the picture box visible
                pictureBox3.Visible = true;

                MessageBox.Show("Number " + saveValue);

            }
            else if (rbtnButtonTwo.Checked)
            {
                saveValue = rbtnButtonTwo.Text;
                // Make the picture box visible
                pictureBox4.Visible = true;

                MessageBox.Show("Number " + saveValue);
            }
            else if (rbtnButtonThree.Checked)
            {
                saveValue = rbtnButtonThree.Text;
                // Make the picture box visible
                pictureBox5.Visible = true;

                MessageBox.Show("Number " + saveValue);
            }
            else if (rbtnButtonFour.Checked)
            {
                saveValue = rbtnButtonFour.Text;
                // Make the picture box visible
                pictureBox6.Visible = true;

                MessageBox.Show("Number " + saveValue);
            }
            else
            {
                MessageBox.Show("Try again");
            }


        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//


        private void btnCaptureDesc_Click_1(object sender, EventArgs e)
        {
            mc.GenerateDeweyNumbers();


            RadioButton[] radioButtonArray = { rbtnDescriptOne, rbtnDescriptTwo, rbtnDescriptThree, rbtnDescriptFour };
            RadioButton[] buttonArray = { rbtnButtonOne, rbtnButtonTwo, rbtnButtonThree, rbtnButtonFour };

            if (rbtnDescriptFive.Checked || rbtnDescriptSix.Checked || rbtnDescriptSeven.Checked)
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

        //--------------------------------------------------------------------------------------------------------------------------------------------------//


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnIdentAreas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select one radio button under 'Call Number,' then click the 'Capture Call Number'" +
                 " button. After that, choose one radio button under 'Descriptions' and press the 'Capture Descriptions' button.");

            theThread = new Thread(IdentifyAreasForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();
        }

        //Calling the Reverse Form
        private void IdentifyAreasForm()
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

        private void btnNormalIdAreas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please select one radio button under 'Descriptions,' then click the " +
                "'Capture Descriptions' button. After that, select one radio button under 'Call Number' and click the 'Capture Call Number' button.");

            theThread = new Thread(OpenNormalID_ClickForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------//


    private void OpenNormalID_ClickForm()
    {
        Application.Run(new IdentifyingAreas());
    }

        private void btnNew_Click(object sender, EventArgs e)
        {
            theThread = new Thread(IdentifyAreasForm);
            theThread.SetApartmentState(ApartmentState.STA);
            theThread.Start();
            this.Close();

        }

        //--------------------


        //--------------------------------------------------------------------------------------------------------------------------------------------------//





    }
}


//-------------------------------------------------------------EndOfFile-------------------------------------------------------------------------------------//




