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

namespace MayinTarlasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        Button[,] butonlar;
      
        int[,] indeks = new int[10, 10];
        
        int[,] sayilar = new int[10, 10];

        Random rndm = new Random();
        
        int yerlestirilenBombaSayisi = 0, toplamBombaSayisi = 10;
        int zaman = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            Button[,] btn = { 
                        {button1,button2,button3,button4,button5,button6,button7,button8,button9,button10}, 
                        {button11,button12,button13,button14,button15,button16,button17,button18,button19,button20 }, 
                        {button21,button22,button23,button24,button25,button26,button27,button28,button29,button30 }, 
                        {button31,button32,button33,button34,button35,button36,button37,button38,button39,button40 }, 
                        {button41,button42,button43,button44,button45,button46,button47,button48,button49,button50 }, 
                        {button51,button52,button53,button54,button55,button56,button57,button58,button59,button60 }, 
                        {button61,button62,button63,button64,button65,button66,button67,button68,button69,button70 }, 
                        {button71,button72,button73,button74,button75,button76,button77,button78,button79,button80 }, 
                        {button81,button82,button83,button84,button85,button86,button87,button88,button89,button90 }, 
                        {button91,button92,button93,button94,button95,button96,button97,button98,button99,button100 } };
            butonlar = btn;
            butonuGizle();
            timer1.Start();
            setGame();
         
            label1.Text = "0";
            timer2.Start();
            
            
        }
        private void Resimler()
        {
        
        if(Convert.ToInt32(label1.Text)<=4 && Convert.ToInt32(label1.Text)>0){

            pictureBox1.Image=Image.FromFile(@"C:\Users\Eko\Desktop\MayinTarlasi\MayinTarlasi\Resources\face-smile-64.png");
        }
        else if(Convert.ToInt32(label1.Text)>4 && Convert.ToInt32(label1.Text)<8){

            pictureBox1.Image=Image.FromFile(@"C:\Users\Eko\Desktop\MayinTarlasi\MayinTarlasi\Resources\face-smile-big-64.png");
        }
        else if(Convert.ToInt32(label1.Text)>=8)
        {
            pictureBox1.Image=Image.FromFile(@"C:\Users\Eko\Desktop\MayinTarlasi\MayinTarlasi\Resources\7.png");
        }
        
        else if(Convert.ToInt32(label1.Text)==0)
        {
            pictureBox1.Image=Image.FromFile(@"C:\Users\Eko\Desktop\\MayinTarlasi\MayinTarlasi\Resources\bad_smile.png");
        }

        }

        private void setGame()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // 1: Boşluk
                    // 0: Bomba
                    //-1: Sayılar
                    butonlar[i, j].Enabled = true;
                    butonlar[i, j].Text = "";
                    butonlar[i, j].ImageKey = "";
                    butonlar[i, j].ImageList = ımageList1;
                    indeks[i, j] = 1;
                    sayilar[i, j] = 0;
                }
            }
            panel1.Visible = true;
            bombalarıIndexle();
            sayilariYerlestir();
        }

        int acilanAlanSayisi = 0;

        private void oyunBittiKontrol()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (butonlar[i, j].Enabled == false)
                    {
                        acilanAlanSayisi++;
                    }
                }
            }

            if (acilanAlanSayisi == 90)
            {
                acilanAlanSayisi = 0;
                MessageBox.Show("Oyun Bitti, Kazandınız!!! Yeni Oyun Hazırlanıyor...");
                setGame();
            }
            else
            {
                acilanAlanSayisi = 0;
            }

        }

        private void bombaKontrol(int i, int j)
        {
            try
            {
                if (indeks[i, j] == 1)
                {
                    bosAc(i, j);

                }
                else if (indeks[i, j] == 0)
                {
                    butonlar[i, j].Image = pictureBox2.Image;
                    timer2.Stop();
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Eko\Desktop\\MayinTarlasi\MayinTarlasi\Resources\bad_smile.png");
                    MessageBox.Show("Oyunu Kaybettiniz!");
                   // panel1.Visible = false;
                    panel1.Enabled = false;
                    
                    pictureBox1.Image = Image.FromFile(@"C:\Users\Eko\Desktop\\MayinTarlasi\MayinTarlasi\Resources\bad_smile.png");
                    zaman = 0;
                    label1.Text = zaman.ToString();
                }
                else if (indeks[i, j] == -1)
                {
                    butonlar[i, j].Enabled = false;
                    butonlar[i, j].Text = sayilar[i, j].ToString();
                    indeks[i, j] = 4;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            oyunBittiKontrol();
        }
        
        
        private void bombalarıIndexle()
        {
            int x, y;
            for (int i = 0; i < toplamBombaSayisi; )
            {
                x = rndm.Next(1, 9);
                y = rndm.Next(1, 9);
                if (indeks[x, y] == 1)
                {
                    indeks[x, y] = 0;
                    i++;
                    yerlestirilenBombaSayisi++;
                }
            }
        }
        private void sayilariYerlestir()
        {
            for (int x = 1; x < 9; x++)
            {
                for (int y = 1; y < 9; y++)
                {
                    if (indeks[x, y] == 0)
                    {
                        // y + 1 , y - 1
                        if (indeks[x, y + 1] != 0 && y != 9)
                        {
                            sayilar[x, y + 1]++;
                            indeks[x, y + 1] = -1;
                        }
                        if (indeks[x, y - 1] != 0 && y != 0)
                        {
                            sayilar[x, y - 1]++;
                            indeks[x, y - 1] = -1;
                        }

                        // x + 1, x - 1
                        if (indeks[x + 1, y] != 0 && x != 9)
                        {
                            sayilar[x + 1, y]++;
                            indeks[x + 1, y] = -1;
                        }
                        if (indeks[x - 1, y] != 0 && x != 0)
                        {
                            sayilar[x - 1, y]++;
                            indeks[x - 1, y] = -1;
                        }

                        //y + 1 ---- x+1,x-1
                        if (x < 9 && y < 9)
                        {
                            if (indeks[x + 1, y + 1] != 0)
                            {
                                sayilar[x + 1, y + 1]++;
                                indeks[x + 1, y + 1] = -1;
                            }
                        }
                        if (x > 0 && y < 9)
                        {
                            if (indeks[x - 1, y + 1] != 0)
                            {
                                sayilar[x - 1, y + 1]++;
                                indeks[x - 1, y + 1] = -1;
                            }
                        }

                        // y - 1 ---- x+1,x-1
                        if (x < 9 && y > 0)
                        {
                            if (indeks[x + 1, y - 1] != 0)
                            {
                                sayilar[x + 1, y - 1]++;
                                indeks[x + 1, y - 1] = -1;
                            }
                        }
                        if (x > 0 && y > 0)
                        {
                            if (indeks[x - 1, y - 1] != 0)
                            {
                                sayilar[x - 1, y - 1]++;
                                indeks[x - 1, y - 1] = -1;
                            }
                        }
                    }
                }
            }
        }

        private void bosAc(int i, int j)
        {
            if (indeks[i, j] == 1)
            {
                butonlar[i, j].Enabled = false;
                indeks[i, j] = 4;

                //jjjjjjj
                if (j < 9)
                {
                    bosAc(i, j + 1);
                }
                if (j > 0)
                {
                    bosAc(i, j - 1);
                }
                //iiiiiiii
                if (i < 9)
                {
                    bosAc(i + 1, j);
                }
                if (i > 0)
                {
                    bosAc(i - 1, j);
                }
                //iiiiiii----jjjjjjjjjj
                if (i > 0 && j < 9)
                {
                    bosAc(i - 1, j + 1);
                }
                if (i < 9 && j < 9)
                {
                    bosAc(i + 1, j + 1);
                }
                if (i < 9 && j > 0)
                {
                    bosAc(i + 1, j - 1);
                }
                if (i > 0 && j > 0)
                {
                    bosAc(i - 1, j - 1);
                }

            }
            else if (indeks[i, j] == 0)
            {

            }
            else if (indeks[i, j] == -1)
            {
                butonlar[i, j].Enabled = false;
                butonlar[i, j].Text = sayilar[i, j].ToString();
                indeks[i, j] = 4;
            }
        }
        private void butonuGizle()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    butonlar[i,j].Visible = false;
                }
            }
        
        
        }

        int x = 0;
        int y = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {       
          
            butonlar[x, y].Visible = true;
            
            x++;
            if (x == 10)
            {
                x = 0;
                y++;
                if (y == 10) 
                {
                    timer1.Stop();
                }
            }
        }
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            x = 0;
            y = 0;
            butonuGizle();
            timer1.Start();
            setGame();
            panel1.Enabled = true;
            timer2.Start();
        }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            zaman=zaman+1;
            label1.Text = zaman.ToString();
            Resimler();
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 0);
            Console.Beep();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 1);
            Console.Beep();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 2);
            Console.Beep();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 3);
            Console.Beep();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 4);
            Console.Beep();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 5);
            Console.Beep();
        }

        private void button58_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 7);
            Console.Beep();
        }

        private void button99_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 8);
            Console.Beep();
        }

        private void button98_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 7);
            Console.Beep();
        }

        private void button97_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 6);
            Console.Beep();
        }

        private void button96_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 5);
            Console.Beep();
        }

        private void button95_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 4);
            Console.Beep();
        }

        private void button94_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 3);
            Console.Beep();
        }

        private void button93_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 2);
            Console.Beep();
        }

        private void button92_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 1);
            Console.Beep();
        }

        private void button91_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 0);
            Console.Beep();
        }

        private void button90_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 9); Console.Beep();
        }

        private void button89_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 8); Console.Beep();
        }

        private void button88_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 7); Console.Beep();
        }

        private void button87_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 6); Console.Beep();
        }

        private void button86_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 5); Console.Beep();
        }

        private void button85_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 4); Console.Beep();
        }

        private void button84_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 3); Console.Beep();
        }

        private void button83_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 2); Console.Beep();
        }

        private void button82_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 1); Console.Beep();
        }

        private void button81_Click(object sender, EventArgs e)
        {
            bombaKontrol(8, 0); Console.Beep();
        }

        private void button80_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 9); Console.Beep();
        }

        private void button79_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 8); Console.Beep();
        }

        private void button78_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 7); Console.Beep();
        }

        private void button77_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 6); Console.Beep();
        }

        private void button76_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 5); Console.Beep();
        }

        private void button75_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 4); Console.Beep();
        }

        private void button74_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 3); Console.Beep();
        }

        private void button73_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 2); Console.Beep();
        }

        private void button72_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 1); Console.Beep();
        }

        private void button71_Click(object sender, EventArgs e)
        {
            bombaKontrol(7, 0); Console.Beep();
        }

        private void button70_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 9); Console.Beep();
        }

        private void button69_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 8); Console.Beep();
        }

        private void button68_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 7); Console.Beep();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 6); Console.Beep();
        }

        private void button66_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 5); Console.Beep();
        }

        private void button65_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 4); Console.Beep();
        }

        private void button64_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 3); Console.Beep();
        }

        private void button63_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 2); Console.Beep();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 1); Console.Beep();
        }

        private void button61_Click(object sender, EventArgs e)
        {
            bombaKontrol(6, 0); Console.Beep();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 0); Console.Beep();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 1); Console.Beep();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 2); Console.Beep();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 3); Console.Beep();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 4); Console.Beep();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 5); Console.Beep();
        }

        private void button47_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 6); Console.Beep();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 7); Console.Beep();
        }

        private void button49_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 8); Console.Beep();
        }

        private void button50_Click(object sender, EventArgs e)
        {
            bombaKontrol(4, 9); Console.Beep();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 0); Console.Beep();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 1); Console.Beep();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 2); Console.Beep();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 3); Console.Beep();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 4); Console.Beep();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 5); Console.Beep();
        }

        private void button57_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 6); Console.Beep();
        }

        private void button100_Click(object sender, EventArgs e)
        {
            bombaKontrol(9, 9); Console.Beep();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 8); Console.Beep();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            bombaKontrol(5, 9); Console.Beep();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 0); Console.Beep();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 1); Console.Beep();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 2); Console.Beep();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 3); Console.Beep();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 4); Console.Beep();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 5); Console.Beep();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 6); Console.Beep();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 7); Console.Beep();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 8); Console.Beep();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            bombaKontrol(2, 9); Console.Beep();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 0); Console.Beep();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 1); Console.Beep();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 2); Console.Beep();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 3); Console.Beep();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 4); Console.Beep();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 5); Console.Beep();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 6); Console.Beep();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 7); Console.Beep();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 8); Console.Beep();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            bombaKontrol(3, 9); Console.Beep();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 0); Console.Beep();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 1); Console.Beep();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 2); Console.Beep();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 3); Console.Beep();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 4); Console.Beep();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 5); Console.Beep();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 6); Console.Beep();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 7); Console.Beep();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 8); Console.Beep();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bombaKontrol(1, 9); Console.Beep();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 9); Console.Beep();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 8); Console.Beep();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 7); Console.Beep();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            bombaKontrol(0, 6); Console.Beep();
        }

      
        
 

      

    }
}
