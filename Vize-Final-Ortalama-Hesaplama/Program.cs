using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Vize_Final_Ortalama_Hesaplama
{
    internal class Program
    {
        //Koleksiyonlarımı global olarak tanımlıyorum ki fonksiyonlarım içerisinden erişebileyim.
        static Dictionary<string, double> vizeNotlar = new Dictionary<string, double>();
        static Dictionary<string, double> finalNotlar = new Dictionary<string, double>();
        static Dictionary<string, double> hesaplanmisNotlar = new Dictionary<string, double>();

        static void Main(string[] args)
        {
            //Sınavların ağırlık ortalamasını kullanıcıdan alıyorum.
            agirlikOraniniAl(out int vizeAgirlik, out int finalAgirlik);

            //Kullanıcıdan ders adı ve vize-final sınav sonuclarını alıyorum.
            dersNotlariniAl();

            //Kullanıcıdan aldığım vize puanını, vize ağırlık oranına göre hesaplama işlemlerimi yapıyorum.
            vizeAgirlikHesaplama(vizeAgirlik);

            //Yine kullanıcıdan aldığım final puanını, final ağırlık oranına göre hesaplama işlemlerimi yapıyorum.
            finalAgirlikHesaplama(finalAgirlik);

            //Tüm hesaplama işlemlerim bittikten sonra kullanıcıya benden istediği verileri gösteriyorum.
            hesaplanmisNotlariGoster();

            Console.ReadKey();
        }

        //kullanıcıdan vize ve final sınavlarının ağırlık oranını istediğim fonksiyonum.
        static void agirlikOraniniAl(out int vizeAgirlik, out int finalAgirlik)
        {
            while (true)
            {
                Console.Write("Vize sınavınızın ağırlık oranını giriniz: -> ");
                int.TryParse(Console.ReadLine(), out vizeAgirlik);

                Console.Write("Final sınavınızın ağırlık oranını giriniz: -> ");
                int.TryParse(Console.ReadLine(), out finalAgirlik);

                int s = vizeAgirlik + finalAgirlik;
                if (s == 100)  //girilen oranlar 100e eşitse, döngüyü kıracağım
                {
                    break;
                }
                else // eğer yanlış bir oran girerse kullanıcıyı uyaracağım ve tekrar ağrılıkları girmesini önleyeceğim.
                {
                    Console.WriteLine("\nHata -> oranlar birbirini tamamlamıyor");
                }
            }
        }

        //kullanıcıdan ders adını, vize sınav notunu ve final sınavı notunu istediğim ve aldığım fonksiyonum
        static void dersNotlariniAl()
        {
            Console.WriteLine(" ");
            string dersAdi;
            while (true)
            {
                Console.WriteLine(" ");
                Console.Write("Lütfen ders adı girin (Sonuçları görmek için -1 değerini girin): -> ");
                dersAdi = Console.ReadLine().ToLower(); // kullanıcı ders ismi girdiğinde harfleri küçültmek için

                if (dersAdi == "-1")
                    break;

                while (true)
                {
                    Console.Write("Lütfen " + dersAdi + " vize ders notunu girin: -> ");
                    double.TryParse(Console.ReadLine(), out double vizeNotu);

                    if (vizeNotu >= 0 && vizeNotu <= 100)
                    {
                        vizeNotlar.Add(dersAdi, vizeNotu);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nHata -> geçersiz ders notu girdiniz (0 - 100 arası not girmelisiniz)");
                    }
                }

                while (true)
                {
                    Console.Write("Lütfen " + dersAdi + " final ders notunu girin: -> ");
                    double.TryParse(Console.ReadLine(), out double finalNotu);
                    

                    if (finalNotu >= 0 && finalNotu <= 100)
                    {
                        finalNotlar.Add(dersAdi, finalNotu);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nHata -> geçersiz ders notu girdiniz (0 - 100 arası not girmelisiniz)");
                    }
                }
            }
        }

        //kullanıcıdan aldığım vize ağırlık oranı ve vize sınav notunı işlediğim fonksiyonum
        static void vizeAgirlikHesaplama(int vizeAgirlik)
        {
            foreach (var vizeNot in vizeNotlar) //vize puanlarımı döngüye soktum ve,
            {
                double vizeSonuc = (vizeNot.Value / 100) * vizeAgirlik; //vize ağırlık işlemlerimi yaptım.
                hesaplanmisNotlar.Add(vizeNot.Key, vizeSonuc); //daha sonra yeni koleksiyonuma bu ağırlığı ekledim.
            }
        }
        //kullanıcıdan aldığım final ağırlık oranı ve final sınav notunı işlediğim fonksiyonum
        static void finalAgirlikHesaplama(int finalAgirlik)
        {
            foreach (var finalNot in finalNotlar) //bu sefer final puanlarımı döngüye soktum ve,
            {
                double finalSonuc = (finalNot.Value / 100) * finalAgirlik; //final ağırlık işlemlerimi yaptım.
                hesaplanmisNotlar[finalNot.Key] += finalSonuc; //daha sonra yeni koleksiyonuma bu ağırlığık puanlarını güncelledim.
            }
        }

        //kullanıcının aldığı puanın harf karşılığını gösteren minik fonksiyonum
        static void harfNotuKarsiliklari(double not, out string deger, out string sonuc)
        {
            
            if (not >= 90 && not <= 100) { deger = "AA"; sonuc = "geçtiniz"; }
            else if (not >= 80 && not <= 89.99) { deger = "BA"; sonuc = "geçtiniz"; }
            else if (not >= 70 && not <= 79.99) { deger = "BB"; sonuc = "geçtiniz"; }
            else if (not >= 65 && not <= 69.99) { deger = "CB"; sonuc = "geçtiniz"; }
            else if (not >= 60 && not <= 64.99) { deger = "CC"; sonuc = "koşullu geçtiniz"; }
            else if (not >= 55 && not <= 59.99) { deger = "DC"; sonuc = "kaldınız"; }
            else if (not >= 50 && not <= 54.99) { deger = "DD"; sonuc = "kaldınız"; }
            else if (not >= 40 && not <= 49.99) { deger = "FD"; sonuc = "kaldınız"; }
            else if (not >= 0 && not <= 39.99) { deger = "FF"; sonuc = "kaldınız"; }
            else { deger = "geçersiz giriş"; sonuc = "kaldınız"; }
        }

        // tüm işlemlerim bittikten sonra kullanıcıyı notları hakkında bilgilendirdiğim fonksiyonum
        static void hesaplanmisNotlariGoster()
        {
            Console.WriteLine("\n\n\n************************************************");
            Console.WriteLine("-> Sonuçlar:");
            foreach (var hesaplanmisNot in hesaplanmisNotlar) //hesaplanmisNotlar koleksiyonumdaki ağırlığı hesaplanmış puanları döngüye soktum, 
            {
                //hesaplanmisNot.Key ile ders adını,
                //hesaplanmisNot.Value ile ders notunu kullanıcıya gösterdim.
                string dersIsim = hesaplanmisNot.Key;
                double not = hesaplanmisNot.Value;
                harfNotuKarsiliklari(not, out string deger, out string sonuc);
                Console.Write(dersIsim + " dersini " + deger + " (" + not + ") ile " + sonuc + "\n");
            }
            Console.WriteLine("************************************************");
        }
    }
}
