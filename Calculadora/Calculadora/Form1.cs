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
                                                        new string[] { "^", "/", "*", "-" },
                                                        new string[] { "7", "8", "9", "+" },
                                                        new string[] { "4", "5", "6", "," },
                                                        new string[] { "1", "2", "3", ")" },
                                                        new string[] { "0", "=", "C", "(" }
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
            Button b = ((Button) sender);
            switch (b.Text)
            {
                case "C":
                    edVisor.Text = "";
                    break;

                case "=":
                    if (!combinarParenteses(edVisor.Text))
                        MessageBox.Show("Parênteses não combinam");
                    else
                    {
                        Expressao ei = Expressao.Dicionarizar(edVisor.Text);
                        Expressao ep = Expressao.TraduzirParaPosfixa(ei);

                        edResultado.Text = Expressao.ResolverPosfixa(ep) + "";
                        lbSequencias.Text = ep.expressao;
                    }
                    break;

                default:
                    edVisor.Text += b.Text;
                    break;
            }
        }

        public bool combinarParenteses(string expressao)
        {
            Pilha<char> parenteses = new Pilha<char>();

            foreach (char c in expressao)
                if (c == '(')
                    parenteses.Empilhar(c);
                else if (c == ')')
                    try
                    {
                        parenteses.Desempilhar();
                    }
                    catch (Exception)
                    {
                        return false;
                    }

            if (!parenteses.EstaVazia())
                return false;

            return true;
        }
    }

    public class Expressao
    {
        public static readonly bool[,] precedenciasDeSinais =
                                        {
                                            { false, false, false, false, false, false, true  },
                                            { false, true,  true,  true,  true,  true,  true  },
                                            { false, false, true,  true,  true,  true,  true  },
                                            { false, false, true,  true,  true,  true,  true  },
                                            { false, false, false, false, true,  true,  true  },
                                            { false, false, false, false, true,  true,  true  },
                                            { false, false, false, false, false, false, false }
                                        };

        public static readonly char[] sinais = { '(', '^', '*', '/', '+', '-', ')' };

        private String _expressao;
        private Dictionary<char, double> _dicionario;

        public String expressao
        {
            get { return _expressao; }
        }

        public Dictionary<char, double> dicionario
        {
            get { return _dicionario; }
        }

        public Expressao(String expressao, Dictionary<char, double> dicionario)
        {
            this._expressao  = expressao;
            this._dicionario = dicionario;
        }

        public static Expressao Dicionarizar(string expressao)
        {
            Dictionary<char, double> d = new Dictionary<char, double>();
            StringBuilder novaExpressao = new StringBuilder(expressao);

            for (int i = 0; i < novaExpressao.Length; i++)
                if (!isOperador(novaExpressao[i]))
                {
                    //Se não é um operador, é um número, assim, pegamos seu início e vamos o percorrendo até achar o proximo operador, ou o final da string
                    //Após isso, sabemos seu tamanho, então fazemos um substring e o convertemos para double.
                    string numero = "";
                    int indexInicio = i;

                    while (indexInicio + numero.Length < novaExpressao.Length && !isOperador(novaExpressao[indexInicio + numero.Length]))
                        numero += novaExpressao[indexInicio + numero.Length];

                    double chave = Convert.ToDouble(numero);

                    novaExpressao.Remove( indexInicio, numero.Length);
                    d.Add(char.ConvertFromUtf32(65 + d.Count)[0], chave);

                    novaExpressao.Insert(indexInicio, char.ConvertFromUtf32(65 + d.Count - 1));

                    i = indexInicio;
                }

            return new Expressao(novaExpressao.ToString(), d);
        }

        public static Expressao TraduzirParaPosfixa(Expressao expressaoInfixa)
        {
            String infixa = expressaoInfixa.expressao;

            Pilha<char> p  = new Pilha<char>();
            String posfixa = "";

            for (int i = 0; i < infixa.Length; i++)
            {
                bool unario = false;

                if (isOperador(infixa[i]))
                {
                    if (infixa[i] == '-')
                        if (i == 0 || infixa[i - 1] == '(')
                        {
                            p.Empilhar('@');
                            unario = true;
                        }

                    if (!unario)
                    {
                        bool parar = false;

                        while (!parar && !p.EstaVazia() && Precedencia(p.OTopo(), infixa[i]))
                        {
                            char operadorComMaiorPrec = p.OTopo();
                            if (operadorComMaiorPrec == '(')
                                parar = true;
                            else
                            {
                                posfixa += operadorComMaiorPrec;
                                p.Desempilhar();
                            }
                        }

                        if (infixa[i] != ')')
                            p.Empilhar(infixa[i]);
                        else
                            p.Desempilhar();
                    }
                }
                else
                    posfixa += infixa[i];
            }

            while (!p.EstaVazia())
            {
                char operadorComMaiorPrec = p.Desempilhar();
                if (operadorComMaiorPrec != '(')
                    posfixa += operadorComMaiorPrec;
            }

            return new Expressao(posfixa, expressaoInfixa.dicionario);
        }

        public static double ResolverPosfixa(Expressao ExpPosfixa)
        {
            String posfixa = ExpPosfixa.expressao;

            Pilha<double> p = new Pilha<double>();
            for (int i = 0; i < posfixa.Length; i++)
            {
                if (!isOperador(posfixa[i]))
                    if (posfixa[i] == '@')
                        p.Empilhar(p.Desempilhar() * -1);
                    else
                        p.Empilhar(ExpPosfixa.dicionario[posfixa[i]]);
                else
                {
                    double operando2 = p.Desempilhar(),
                           operando1 = p.Desempilhar();

                    p.Empilhar(SubExpressao(operando1, operando2, posfixa[i]));
                }
            }

            return p.Desempilhar();
        }

        private static double SubExpressao(double operando1, double operando2, char sinal)
        {
            switch (sinal)
            {
                case '+': return operando1 + operando2;

                case '-': return operando1 - operando2;

                case '*': return operando1 * operando2;

                case '/': return operando1 / operando2;

                case '^': return Math.Pow(operando1, operando2);

                default : return 0; //Retorno padrão, apenas para satisfazer o compilador 
            }
        }

        private static bool isOperador(char c)
        {
            foreach (char sinal in sinais)
                if (c == sinal)
                    return true;

            return false;
        }

        private static bool Precedencia(char c1, char c2)
        {
            if (c1 == '@' || c2 == '@')
                return true;

            int indiceC1 = Array.FindIndex(sinais, x => x == c1),
                indiceC2 = Array.FindIndex(sinais, y => y == c2);

            if (indiceC1 < 0 || indiceC2 < 0)
                throw new ArgumentException("Sinal não existente");

            return precedenciasDeSinais[indiceC1, indiceC2];
        }
    }
}
