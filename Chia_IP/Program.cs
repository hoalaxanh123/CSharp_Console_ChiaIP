using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chia_IP
{
    class Program
    {

        static void Main(string[] args)
        {

            //Dong dau ban quyen :p
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(" Chia IP ( VLSM ) Code By 1510289  Nguyen Van Vuong".PadLeft(60));
            Console.WriteLine("-------------------------------------------------------------------------");

            string IP;
            int IP_1, IP_2, IP_3, IP_4;

  
            Console.Write("Nhap vao dia chi IP ( VD: 192.168.0.0 ), \nIP = ");
            
            IP = Console.ReadLine();
           
            Console.WriteLine();


            //Chat IP thanh tung so
            string[] IP_Split = IP.Split('.');
            IP_1 = int.Parse(IP_Split[0]);
            IP_2 = int.Parse(IP_Split[1]);
            IP_3 = int.Parse(IP_Split[2]);
            IP_4 = int.Parse(IP_Split[3]);
            Console.Clear();


            //Xuat thong bao
            Console.WriteLine("Dia chi IP ban dau: " + IP);
            Console.WriteLine("Dia chi tren thuoc lop : " + XacDinhLop(IP_1));


            //Debug thu xem dung hay chua
            //Console.WriteLine("Phan dia chi dau: " + IP_1);

            ////IP = IP.Substring(vt1 + 1);


            //Console.WriteLine("Phan dia chi thu hai : " + IP_2);



            //Console.WriteLine("Phan dia chi thu ba : " + IP_3);


            //Console.WriteLine("Phan dia chi thu tu: " + IP_4);










            //  Lay thong tin host va subnet
            Console.Write("\nNhap vao so subnet : ");
            int subNet = int.Parse(Console.ReadLine());

            int[] a = new int[100];

            for (int i = 0; i < subNet; i++)
            {
                Console.Write("Nhap so host cho subnet thu {0} : ", i + 1);
                a[i] = int.Parse(Console.ReadLine());
            }

            Console.Clear();


            //Xuat thong bao lan cuoi
            SapXep(ref a,subNet);

            Console.WriteLine("Cac thong tin ban da cung cap la: ");
            Console.WriteLine("Dia chi IP ban dau: " + IP);
            Console.WriteLine("So sub net:"+subNet);
            Console.WriteLine("Danh sach cac host : ");
            for (int i = 0; i < subNet; i++)
                Console.Write(a[i] + "\t");
            Console.ReadKey();
            Console.Clear();


            //Xu ly
            Console.WriteLine("Dia chi IP ban dau: " + IP);
            //int[] a = { 700, 300, 200, 100,20,20,16,2,2 };
            for (int i = 0; i < subNet; i++)
            {
                Console.WriteLine("------------------------------------------------------");
                
                KiemTraTran(ref IP_1, ref IP_2, ref IP_3, ref IP_4);
                Console.WriteLine("{0} host : ", a[i]);
                Console.Write(" From : ");

                Console.WriteLine(Tostring(IP_1, IP_2, IP_3, IP_4+1));
                TEST(ref IP_1, ref IP_2, ref IP_3, ref IP_4, TimMAX_GanNhat(a[i]));
                Console.WriteLine();
                Console.Write(" To : ");
               
                if (IP_4 % 2 == 0)
                    Console.WriteLine(Tostring(IP_1, IP_2, IP_3, IP_4-2));
                else
                    Console.WriteLine(Tostring(IP_1, IP_2, IP_3, IP_4-1));

                Console.WriteLine("------------------------------------------------------");

            }
            Console.ReadKey();

        }
        //Xac dinh lop cua IP
        static char XacDinhLop(int Part_IP)
        {
            if (Part_IP >= 1 && Part_IP <= 127)
                return 'A';
            if (Part_IP >= 128 && Part_IP <= 191)
                return 'B';
            if (Part_IP >= 192 && Part_IP <= 223)
                return 'C';
            if (Part_IP >= 224 && Part_IP <= 225)
                return 'D';
            return '0';
        }
        //Chong tran khi qua 256
        static void KiemTraTran(ref int IP_1,ref int IP_2,ref int IP_3,ref int IP_4)
        {
            if (IP_4 == 255)
            {
                IP_4 = 0;
                IP_3 = IP_3 + 1;
            }
            if (IP_3 == 255)
            {
                IP_3 = 0;
                IP_2 = IP_2 + 1;
            }

            if (IP_2 == 255)
            {
                IP_2 = 0;
                IP_1 = IP_1 + 1;
            }
        }
        //Sap xep lai so host theo thu tu tang dan
        static void SapXep(ref int[] a,int subNet)
        {
            for (int i = 0; i < subNet; i++)
                for (int j = i + 1; j < subNet; j++)
                    if (a[i] < a[j])
                    {
                        int tam = a[i];
                        a[i] = a[j];
                        a[j] = tam;
                    }
        }
        //Tim chi so >= 2^n+2
        static double TimMAX_GanNhat(int host)
        {
            for (int i = 1; i < 20; i++)
                if (Math.Pow(2, i) >= host + 2)
                    return Math.Pow(2, i);
            return 0;
        }
        //To string dang bua loi`
        static string Tostring(int IP1, int IP2, int IP3, int IP4)
        {
            return string.Format("{0}.{1}.{2}.{3}", IP1, IP2, IP3, IP4);
        }
        //Tien hanh xu ly chia IP
        static void TEST(ref int IP_1, ref int IP_2, ref int IP_3, ref int IP_4, double MAx_GanNhat)
        {
            //Xet truong hop =256
            if(MAx_GanNhat==256)
            {
                IP_4 = 255;
                //IP_3 = IP_3 + 1;
            }
            //Xet truong hop <256
            if (MAx_GanNhat < 256)
            {
               IP_4 = (IP_4 + (int)MAx_GanNhat);
            }
            else
                //Xet truong hop >256
                while (MAx_GanNhat > 256)
                {
                    IP_4 = IP_4 + (int)MAx_GanNhat;
                    //Xu ly Ip 4
                    if (IP_4 > 256)
                    {
                        IP_3 = IP_3 + 1;
                        MAx_GanNhat = MAx_GanNhat - 256;
                        IP_4 = 0;
                        if (MAx_GanNhat == 256)
                            IP_4 = 255;
                    }
                    //Xu ly IP 3
                    if (IP_3 > 256)
                    {
                        IP_2 = IP_2 + 1;
                        MAx_GanNhat = MAx_GanNhat - 256;
                        IP_3 = 0;
                        if (MAx_GanNhat == 256)
                            IP_3 = 255;
                    }
                    //Xu ly IP 2
                    if (IP_2 > 256)
                    {
                        IP_1 = IP_1 + 1;
                        MAx_GanNhat = MAx_GanNhat - 256;
                        IP_2 = 0;
                        if (MAx_GanNhat == 256)
                            IP_2 = 255;
                    }
                }
        }
    }
}
