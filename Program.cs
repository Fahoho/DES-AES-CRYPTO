using System;
using System.Security.Cryptography; // Şifreleme için gerekli
using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Şifrelenecek metni giriniz: ");
        string input = Console.ReadLine();

        // Girişi şifreleme için bayt dizisine dönüştürmek
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        // DES şifreleme nesnesi oluşturmak
        using (DES des = DES.Create())
        {
            // DES için rastgele anahtar ve başlatma vektörü (IV) oluşturmak
            des.GenerateKey();
            des.GenerateIV();

            // Şifreleme nesnesi oluşturmak ve girişi şifrelemek
            ICryptoTransform desEncryptor = des.CreateEncryptor();
            byte[] desEncryptedBytes = desEncryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            // DES şifreli metni ters ve ikili eşdeğeri olarak yazdırmak
            Console.WriteLine("\nDES Şifrelenmiş metin (tersten):");
            for (int i = desEncryptedBytes.Length - 1; i >= 0; i--)
            {
                Console.Write(desEncryptedBytes[i].ToString("X2") + " ");
            }
            Console.WriteLine("\nBinary Karşılığı:");
            foreach (byte b in desEncryptedBytes)
            {
                Console.Write(Convert.ToString(b, 2).PadLeft(8, '0') + " ");
            }
            Console.WriteLine();
        }

        // AES şifreleme nesnesi oluşturmak
        using (Aes aes = Aes.Create())
        {
            // AES için rastgele anahtar ve IV oluşturmak
            aes.GenerateKey();
            aes.GenerateIV();

            // Şifreleme nesnesi oluşturmak ve girişi şifrelemek
            ICryptoTransform aesEncryptor = aes.CreateEncryptor();
            byte[] aesEncryptedBytes = aesEncryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

            // AES şifreli metni tersten ve ikili eşdeğerini yazdırmak
            Console.WriteLine("\nAES Şifrelenmiş Metin (Tersten):");
            for (int i = aesEncryptedBytes.Length - 1; i >= 0; i--)
            {
                Console.Write(aesEncryptedBytes[i].ToString("X2") + " ");
            }
            Console.WriteLine("\nBinary Karşılığı:");
            foreach (byte b in aesEncryptedBytes)
            {
                Console.Write(Convert.ToString(b, 2).PadLeft(8, '0') + " ");
            }
            Console.WriteLine();
        }

        // Girişteki toplam karakter sayısını yazdırmak
        Console.WriteLine($"\nToplam karakter sayısı: {input.Length}");
        Console.ReadKey();
    }
}