using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        public static readonly string[][] txtBotoes = {
                                                        new string[] { "(", ")", "^", "C" },
                                                        new string[] { "7", "8", "9", "/" },
                                                        new string[] { "4", "5", "6", "*" },
                                                        new string[] { "1", "2", "3", "-" },
                                                        new string[] { "0", ".", "=", "+" }
                                                      };

        private Button[][] botoes;

        public Form1()
        {
            InitializeComponent();

            botoes = new Button[txtBotoes.Length][];
            for (int n = 0; n < botoes.Length; n++)
            {
                botoes[n] = new Button[txtBotoes[0].Length];
                for (int i = 0; i < botoes[n].Length; i++)
                {
                    botoes[n][i] = new Button();
                    botoes[n][i].Text = txtBotoes[n][i];
                    botoes[n][i].Size = new Size(75, 75);
                    botoes[n][i].Margin = new Padding(3, 3, 3, 3);
                    botoes[n][i].Click += btnClick;
                    this.flowLayoutPanel1.Controls.Add(botoes[n][i]);
                }
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            textBox1.Text += ((Button)sender).Text;
        }
    }
}
