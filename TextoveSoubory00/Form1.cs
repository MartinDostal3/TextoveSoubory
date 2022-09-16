using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Soubory
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        //Pro práci se soubory v CSharp vždy potřebujeme nějaký stream.

        //Textový soubor je speciální typ souboru, kde jsou znaky uspořádány do řádků (pomocí řídících znaků CR a LF)
        //Textové soubory můžeme zpracovávat obecně, jako kterýkoliv jiný soubor, ale pokud chceme
        //zpracovávat text, práci nám usnadní použití speciálních streamů určených právě jen pro textové soubory: 
        //          StreamWriter 
        //          StreamReader
        
        //Textové soubory lze zpracovávat jedině sekvenčně - od začátku do konce, při zpracování se nelze v souboru vracet.
        //Proto můžeme do textového souboru buďto zapisovat nebo z něj číst. Nelze obojí současně.
        private void button1_Click(object sender, EventArgs e)
        {
            //Pro zápis do textového souboru použijeme třídu StreamWriter
            //- slouží pro otevření konkrétního textového souboru (pokud neexistuje - vytvoří se)
            //  a má metody pro zápis do textového souboru (Write a WriteLine) 

            StreamWriter streamWriter = new StreamWriter("Text.txt");
            streamWriter.WriteLine("První");
            streamWriter.WriteLine("Druhý");
            streamWriter.WriteLine("Bohdan je rasista");
            streamWriter.Write('A');
            streamWriter.Write("***");
            streamWriter.Write('X');
            streamWriter.WriteLine();
            streamWriter.Write("Bohdan je kořeň");
            streamWriter.Close(); //toto musim !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        }

        private void button2_Click(object sender, EventArgs e)
        {

            StreamWriter streamWriter = new StreamWriter("Text.txt", true);
            streamWriter.WriteLine("LOL");
            streamWriter.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Přečte textový soubor po řádcích 
            //a každyý přečtený řádek zobrazí v listBox1
            listBox1.Items.Clear();
            StreamReader streamReader = new StreamReader("Text.txt");
            /*while(!streamReader.EndOfStream)
            {
                string s = streamReader.ReadLine();
                listBox1.Items.Add(s);
            }*/ //radek
            while (!streamReader.EndOfStream)
            {
                char c = (char)streamReader.Read();
                listBox1.Items.Add(c);
            } //pismena
            streamReader.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Přečtené řádky budeme zobrazovat v listbox
           

            listBox1.Items.Clear();

            StreamReader streamReader = new StreamReader("Text.txt");
            while (!streamReader.EndOfStream)
            {
                string s = streamReader.ReadLine();
                listBox1.Items.Add(s);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Na rozdíl od řetězce nemůžeme číst znak na konci souboru
            //Metoda Peek - vrací kód znaku, který je na řadě pro další čtení - zeptáme se, zda je ještě něco ke čtení,
            //pokud žádný další znak není (konec souboru) - vrátí Peek kód -1

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //(Vytvoříme prázdné textové soubory)


            //Různé způsoby zápisu cesty: (Nelze jednoduše psát opačné lomítko)
            //Při programování aplikace používejte skoro vždy relativní cesty!!!
            StreamWriter sw = new StreamWriter("text1.txt"); // v aktualni slozce                   
            sw.Close();

            sw = new StreamWriter("..\\..\\text2.txt");
            sw.Close();
            sw = new StreamWriter("../../text3.txt");
            sw.Close();
            sw = new StreamWriter(@"..\..\text4.txt");
            sw.Close();
            sw = new StreamWriter(@"..\..\soubory\text5.txt");
            sw.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Zobrazí vybraný soubor
            //Soubor vybereme pomocí komponenty OpenFileDialog nebo SaveFileDialog
            //Tyto dialogy nic neotevírají ani neukládají, jen nám umožní vybrat soubor, ostatní musíme naprogramovat sami
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox3.Items.Clear();
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                while (!streamReader.EndOfStream)
                {
                   
                    string s = streamReader.ReadLine();
                    listBox3.Items.Add(s);
                }
            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný soubor, ty nulo!");
                //listBox3.Items.Add("nebyl vybrán soubor");
            }


            //save file dialog
            listBox3.Items.Clear();
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.Close();

            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný soubor, ty nulo!");
            }
          
            //OpenFileDialog ofd = new OpenFileDialog();



            //Vyzkoušet jiný způsob obsloužení dialogu!!!!

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //V textovém souboru vybraném pomocí OpenFileDialogu zapíšeme
            //na konec každého řádku *

            //Textový soubor opravíme tak, že jej celý čteme, opravené řádky nebo znaky zapisujeme
            //do pomocného textového souboru.
            //Oba streamy zavřeme, původní soubor smažeme 
            //a pomocný soubor přejmenujeme na jméno původního souboru (včetně umístění)
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                StreamReader streamReader = new StreamReader(openFileDialog1.FileName);
                StreamWriter sw = new StreamWriter("pomocny.txt");
                
                while (!streamReader.EndOfStream)
                {
                    
                    string line = streamReader.ReadLine();
                    listBox4.Items.Add(line);
                    line += "*";
                    sw.WriteLine(line);
                   
                }
                streamReader.Close();
                sw.Close();

                File.Delete(openFileDialog1.FileName);
                File.Move("pomocny.txt", openFileDialog1.FileName);

                //zobrazeni opraveneho souboru
                streamReader = new StreamReader(openFileDialog1.FileName);
                while (!streamReader.EndOfStream)
                {
                    string s = streamReader.ReadLine();
                    listBox5.Items.Add(s);
                }
                streamReader.Close();


             

            }
       
            else
            {
                MessageBox.Show("Nebyl vybrán žádný soubor");
                
            }






        }

        private void button10_Click(object sender, EventArgs e)
        {
            StreamWriter swU = new StreamWriter("kodovaniNeurceno.txt");
            swU.WriteLine("vizaz");
            swU.WriteLine("presivany");
            swU.Close();

            StreamWriter sw1250 = new StreamWriter("w1250.txt", false, Encoding.GetEncoding("Windows-1250"));
            sw1250.WriteLine("vizáž");
            sw1250.WriteLine("přešívaný");
            sw1250.Close();


            StreamWriter swDef = new StreamWriter("Default.txt", false, Encoding.Default);
            swDef.WriteLine("vizáž");
            swDef.WriteLine("přešívaný");
            swDef.Close();

            StreamReader sr = new StreamReader("KodovaniNeurceno.txt");
            textBox1.Text = "Kódování jsme neurcili" + "\r\n";
            textBox1.Text += sr.ReadToEnd();
            sr.Close();

           sr = new StreamReader("w1250.txt");
            textBox1.Text += "\r\nSoubor Windows1250\r\n";
            textBox1.Text += sr.ReadToEnd();
            sr.Close();

            sr = new StreamReader("Default.txt");
            textBox1.Text += "\r\nSoubor defaultní kódování\r\n";
            textBox1.Text += sr.ReadToEnd();
            sr.Close();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
