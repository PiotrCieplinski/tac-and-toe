using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _14052022
{
    public partial class Form1 : Form
    {
        string ostatniZwyciezca = "O";
        int liczbaWygranychPodrząd = 0;

        public Form1()
        {
            InitializeComponent();
            cbx_ustawieniaAutomatu.SelectedItem = "Komputer nie gra";
            cbx_ustawieniaAutomatu.DropDownStyle = ComboBoxStyle.DropDownList;

            cbx_poziomTrudnosci.SelectedItem = "Łatwy";
            cbx_poziomTrudnosci.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btn_00_Click(object sender, EventArgs e)
        {
            btn_Click(btn_00);
        }

        private void btn_01_Click(object sender, EventArgs e)
        {
            btn_Click(btn_01);
        }

        private void btn_Click(Button button)
        {
            cbx_czyZaczynaKolko.Enabled = false;
            cbx_ustawieniaAutomatu.Enabled = false;
            btn_restart.Visible = true;
            button.Text = lbl_gracz.Text;

            if(button.Text == "O")
            {
                button.BackColor = Color.Green;
            } else
            {
                button.BackColor = Color.Red;
            }

            string nazwaBezBtn = button.Name.Replace("btn_", "");
            char y = nazwaBezBtn[0];
            char x = nazwaBezBtn[1];
            lbx_historia.Items.Add("Gracz " + lbl_gracz.Text + " zaznaczył pole (" + x + "," + y + ")");

            if (CzyKoniecGry())
            {
                if(lbl_gracz.Text == "O")
                {
                    lbl_liczbaWygranychO.Text = (Convert.ToInt32(lbl_liczbaWygranychO.Text) + 1).ToString();
                } else
                {
                    lbl_liczbaWygranychGraczaX.Text = (Convert.ToInt32(lbl_liczbaWygranychGraczaX.Text) + 1).ToString();
                }

                DialogResult dialogResult = MessageBox.Show("Gracz " + lbl_gracz.Text + " wygrał grę! Czy wyzerować liczby wygranych gier?",
                    "Wynik gry", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    lbl_liczbaWygranychGraczaX.Text = 0.ToString();
                    lbl_liczbaWygranychO.Text = 0.ToString();
                    liczbaWygranychPodrząd = 0;
                }

                if(lbl_gracz.Text == ostatniZwyciezca)
                {
                    liczbaWygranychPodrząd++;
                } else
                {
                    ostatniZwyciezca = lbl_gracz.Text;
                    liczbaWygranychPodrząd = 1;
                }

                if(liczbaWygranychPodrząd > 2)
                {
                    string gryLubGier = liczbaWygranychPodrząd > 4 ? "gier" : "gry";
                    lbl_pasmoZwycieztw.Text = "Gracz " + lbl_gracz.Text + " wygrał " + liczbaWygranychPodrząd + " " + gryLubGier + " pod rząd!";
                    lbl_pasmoZwycieztw.Visible = true;
                } else
                {
                    lbl_pasmoZwycieztw.Visible = false;
                }

                Restart();
            } else if(lbx_historia.Items.Count == 9)
            {
                liczbaWygranychPodrząd = 0;
                lbl_pasmoZwycieztw.Visible = false;
                MessageBox.Show("Gra zakończyła się remisem!", "Wynik gry", MessageBoxButtons.OK);
                Restart();
            }
            else
            {
                if (lbl_gracz.Text == "O")
                {
                    lbl_gracz.Text = "X";
                }
                else
                {
                    lbl_gracz.Text = "O";
                }

                button.Enabled = false;

                SprawdźIWykonajRuch();
            }
        }

        private void btn_02_Click(object sender, EventArgs e)
        {
            btn_Click(btn_02);
        }

        private void btn_10_Click(object sender, EventArgs e)
        {
            btn_Click(btn_10);
        }

        private void btn_11_Click(object sender, EventArgs e)
        {
            btn_Click(btn_11);
        }

        private void btn_12_Click(object sender, EventArgs e)
        {
            btn_Click(btn_12);
        }

        private void btn_20_Click(object sender, EventArgs e)
        {
            btn_Click(btn_20);
        }

        private void btn_21_Click(object sender, EventArgs e)
        {
            btn_Click(btn_21);
        }

        private void btn_22_Click(object sender, EventArgs e)
        {
            btn_Click(btn_22);
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Restart()
        {
            if (cbx_czyZaczynaKolko.Checked)
            {
                lbl_gracz.Text = "O";
            }

            Button[] buttons = new Button[]
            {
                btn_00, btn_01, btn_02, btn_10, btn_11, btn_12, btn_20, btn_21, btn_22
            };

            foreach (Button button in buttons)
            {
                button.Enabled = true;
                button.Text = "";
                button.BackColor = Color.White;
            }

            lbx_historia.Items.Clear();
            cbx_czyZaczynaKolko.Enabled = true;
            btn_restart.Visible = false;
            cbx_ustawieniaAutomatu.Enabled = true;
        }

        private bool CzyKoniecGry()
        {
            if (lbx_historia.Items.Count > 4)
            {
                //Pierwszy rząd
                if (btn_00.Text != "" && btn_00.Text == btn_01.Text && btn_00.Text == btn_02.Text)
                {
                    return true;
                }
                //drugi rząd
                else if (btn_10.Text != "" && btn_10.Text == btn_11.Text && btn_10.Text == btn_12.Text)
                {
                    return true;
                }
                //trzeci rząd
                else if (btn_20.Text != "" && btn_20.Text == btn_21.Text && btn_20.Text == btn_22.Text)
                {
                    return true;
                }
                //Pierwsza kolumna
                else if (btn_00.Text != "" && btn_00.Text == btn_10.Text && btn_00.Text == btn_20.Text)
                {
                    return true;
                }
                //druga kolumna
                else if (btn_01.Text != "" && btn_01.Text == btn_11.Text && btn_01.Text == btn_21.Text)
                {
                    return true;
                }
                //trzecia kolumna
                else if (btn_02.Text != "" && btn_02.Text == btn_12.Text && btn_02.Text == btn_22.Text)
                {
                    return true;
                }
                //Skos 1
                else if (btn_00.Text != "" && btn_00.Text == btn_11.Text && btn_00.Text == btn_22.Text)
                {
                    return true;
                }
                //Skos 2
                else if (btn_02.Text != "" && btn_02.Text == btn_11.Text && btn_02.Text == btn_20.Text)
                {
                    return true;
                }
            }

            return false;
        }

        private void cbx_czyZaczynaKolko_CheckedChanged(object sender, EventArgs e)
        {
            if(cbx_czyZaczynaKolko.Checked)
            {
                lbl_gracz.Text = "O";
            } else
            {
                lbl_gracz.Text = "X";
            }
        }

        private void WykonajRuch()
        {
            Button[] buttons = new Button[] { btn_00, btn_01, btn_02, btn_10, btn_11, btn_12, btn_20, btn_21, btn_22 };
            Button button = null;
            if ((string)cbx_poziomTrudnosci.SelectedItem == "Trudny" && lbx_historia.Items.Count > 2)
            {
                Kombinacja[] kombinacje = new Kombinacja[]
                {
                    new Kombinacja(btn_00, btn_01, btn_02),
                    new Kombinacja(btn_10, btn_11, btn_12),
                    new Kombinacja(btn_20, btn_21, btn_22),
                    new Kombinacja(btn_00, btn_10, btn_20),
                    new Kombinacja(btn_01, btn_11, btn_21),
                    new Kombinacja(btn_02, btn_12, btn_22),
                    new Kombinacja(btn_00, btn_11, btn_22),
                    new Kombinacja(btn_02, btn_11, btn_20)
                };
                int index = 0;

                do
                {
                    button = SprawdzKombinacje(kombinacje[index++], lbl_gracz.Text);
                } while (index < kombinacje.Length && button == null);

                if(button == null)
                {
                    string znak = lbl_gracz.Text == "O" ? "X" : "O";
                    index = 0;

                    do
                    {
                        button = SprawdzKombinacje(kombinacje[index++], znak);
                    } while (index < kombinacje.Length && button == null);
                }
            }

            if (button == null)
            {
                Button[] enabledButtons = buttons.Where(x => x.Enabled).ToArray();
                Random random = new Random();
                button = enabledButtons[random.Next(0, enabledButtons.Length - 1)];
            }
            //while(!button.Enabled)
            //{
            //    int wylosowanaLiczba = random.Next(0, buttons.Length - 1);
            //    button = buttons[wylosowanaLiczba];
            //}

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(stopwatch.ElapsedMilliseconds < 0.5 * 1000)
            {

            }

            stopwatch.Stop();
            stopwatch.Reset();

            btn_Click(button);
        }

        private Button SprawdzKombinacje(Kombinacja kombinacja, string znak)
        {
            Button[] rzad1 = new Button[] { kombinacja.button1, kombinacja.button2, kombinacja.button3 }.Where(x => !x.Enabled).ToArray();
            if (rzad1.Count() == 2 && rzad1[0].Text == znak && rzad1[1].Text == znak)
            {
                return new Button[] { kombinacja.button1, kombinacja.button2, kombinacja.button3 }.Where(x => x.Enabled).ToArray()[0];
            }

            return null;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            SprawdźIWykonajRuch();
        }

        private void SprawdźIWykonajRuch()
        {
            if ((string)cbx_ustawieniaAutomatu.SelectedItem == "Komputer gra " + lbl_gracz.Text ||
                    (string)cbx_ustawieniaAutomatu.SelectedItem == "Komputer vs. Komputer")
            {
                WykonajRuch();
            }
        }

        private void cbx_ustawieniaAutomatu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbx_poziomTrudnosci.Visible = (string)cbx_ustawieniaAutomatu.SelectedItem == "Komputer vs. Komputer" ||
                (string)cbx_ustawieniaAutomatu.SelectedItem == "Komputer gra O" ||
                (string)cbx_ustawieniaAutomatu.SelectedItem == "Komputer gra X";
        }
    }
}
