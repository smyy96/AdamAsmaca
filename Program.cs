using static System.Net.Mime.MediaTypeNames;

namespace AdamAsmacaOyunu2._0
{
    internal class Program
    {
        public static string randomWord = "";

        static string guessedLetters = ""; // Tahmin edilen harflerin toplandığı değişken
        static string guessChar; //Tahmin edilen harf

        static int playerScore = 0;

        /*
         *          oyuna eklenecekler
         *  ✓ oyuncunun kullandığı harfleri ekrana yazdırma 
         *  ✓ puanını yazdırma  
         *  ✓ oyunu bitirince de ekranı renkli yapma.  
         *  ✓ çıkıs kısmında temizleme yap 0 -1 alınca hata vermesi
         *    oyun bitirme kısmı 0 a basmadan bitirdiniz yazmalı bence
         *  ✓ tahminde hatalı değer girildiğinde ekranı temizleme kısmı
         *  
         */


        static void Main(string[] args)
        {

            arrWords();

        }

        static void arrWords() // Kelimelerin oluşturulduğu metot
        {
            string[] Words = { "aura", "teveccüh", "müşkülpesent", "elma", "masa", "katip", "bilgisayar", "gökyüzü", "kalem", "gözlük", "güllaç", "kedi", "ropdöşambır", "şambrel", "kuruntu", "mistik", "eksantrik" };


            string[] usedWords = new string[Words.Length]; // oyunda aynı kelimelerin kullanılmaması için yeni bir dizi oluşturdum.

            Game(Words, usedWords);

        }

        static string[] RandomWord(string[] Words, string[] usedWords) // eklime havuzundan rastgele kelime secme ve aynı kelimelerin secilmemesi için kullanılan kelimelerin kontrollerinin yapılması
        {

            Random random = new Random();

            bool endGame = false;// oyunun sonunu kontrol etme

            while (!endGame)
            {
                int RandomNum = random.Next(0, Words.Length); //Rastgele sayı al
                randomWord = Words[RandomNum]; // word dizisindeki secilen kelime alınıyor.

                if (!usedWords.Contains(randomWord))
                {
                    for (int j = 0; j < usedWords.Length; j++)
                    {
                        if (usedWords[j] == null)
                        {
                            usedWords[j] = randomWord;
                            endGame = true;
                            break;
                        }
                    }
                    break;
                }

                else if (!usedWords.Contains(null))
                {
                    //endGame = false;

                    Console.SetBufferSize(100, 30);
                    Console.SetWindowSize(100, 30);

                    DateTime startTime = DateTime.Now;

                    while (true)
                    {
                        DateTime endTime = DateTime.Now;

                        if ((endTime - startTime).TotalSeconds > 30) //30sn sonra uygulamayı kapatma
                        {
                            //Console.Clear();
                            Environment.Exit(0);
                            break;
                        }

                        Console.ForegroundColor = (ConsoleColor)random.Next(1, 16);
                        Console.SetCursorPosition(random.Next(100), random.Next(30));
                        Console.Write(" ♦ Oyunu Bitirdiniz. ♦ ");
                        Console.Write(' ');
                        Thread.Sleep(100);
                    }

                }

                else { continue; }
            }

            if (endGame)
            {
                string[] Guess = new string[randomWord.Length]; //rastgele secilen kelimenin boyutu kadar bir dizi oluşturdum. Tahmin edilen harfler doğru ise bu diziye aktarılacak.


                for (int i = 0; i < randomWord.Length; i++) //tahminlerin alınacagı diziye kelimenin boyutu kadar _ ekleme
                {
                    Guess[i] = "_";
                }

                return Guess;
            }

            else return null;


        }

        static void PrintGame(string[] Guess) //oyun esnasında tahmin edilen kelimenin tahminler sonucunda ekrana yazdırılan görüntüsü
        {

            foreach (string word in Guess)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();

        }

        static void Game(string[] Words, string[] usedWords) // tahminlerin alınıp kontrol edildiği kısım
        {


            string guessWord = "";//kullanıcıdan aldığımız tahmin kelimesi



            string[] Guess = new string[Words.Length];


            int exit = 0;

            while (exit == 0)
            {

                Guess = RandomWord(Words, usedWords);
                if (Guess != null)
                {
                    GameTitle();
                    PrintGame(Guess);


                    //Game
                    int gameLimit = randomWord.Length + 3; //tahmin limiti

                    for (int j = 0; j < gameLimit; j++)
                    {
                        if (j == gameLimit - 2)
                        {

                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\nSon 2 hakkınız kaldı!!!!");
                            Console.ResetColor();
                        }

                        Console.Write("\nHarf giriniz: ");
                        guessChar = Console.ReadLine();

                        if (!guessChar.All(char.IsLetter) || guessChar.Length != 1)
                        {
                            Console.WriteLine("Yanlış değer girdiniz. Girdiğiniz değer sadece harf içermeli.");
                            ScreenClear();
                            PrintGame(Guess);
                            continue;
                        }

                        else
                        {
                            if (!guessedLetters.Contains(guessChar))
                                guessedLetters = guessedLetters + guessChar + " ";//Tahmin edilen harflerin toplandığı kısım

                            if (randomWord.Contains(guessChar))
                            {
                                for (int i = 0; i < Guess.Length; i++)
                                {
                                    if (randomWord[i].ToString() == guessChar)
                                    {
                                        Guess[i] = guessChar;
                                    }
                                }

                                ScreenClear();
                                PrintGame(Guess);
                            }

                            else
                            {
                                Console.WriteLine("\nHarf bulunmamaktadır.");

                                ScreenClear();
                                PrintGame(Guess);
                            }
                        }

                        exit = 1;

                        //Kontrol metodu, tahmin doğru ise 
                        if (string.Join("", Guess) == randomWord)
                        {
                            toWinGreen();
                            exit = GameExitEnter(exit);

                            Console.Clear();

                            break;

                        }

                    }

                    if (exit == -1)
                        break;
                    else if (exit == 0)
                        continue;
                    else
                    {
                        Console.Write("\nHakkınızı bitirdiniz. Tahmin ettiğiniz kelime nedir: ");
                        guessedLetters = "";
                        guessWord = Console.ReadLine();

                        if (guessWord.ToLower() == randomWord)
                        {
                            toWinGreen();
                            exit = GameExitEnter(exit);

                            if (exit == -1) break;

                            Console.Clear();
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nBilemediniz istenilen kelime: " + randomWord.ToUpper());
                            Console.ResetColor();

                            exit = GameExitEnter(exit);
                            Console.Clear();
                            if (exit == -1) break;
                            else continue;
                        }
                    }
                }

                Console.ReadKey();
            }
        }

        static int GameExitEnter(int exit) // oyuncunun kelime tahminlerinden sonra çıkmak isteyip istemediğini 
        {
            while (true)
            {
                try
                {
                    Console.Write("\nÇıkmak için -1 yazınız. Devam etmek için 0 yazınız. ");
                    exit = Convert.ToInt32(Console.ReadLine());

                    if (!(exit == 0 || exit == -1))
                    {
                        Console.WriteLine("0 veya -1 girmeliydiniz.");
                        Thread.Sleep(1200);
                        Console.Clear();
                        GameTitle();
                        continue;
                    }
                    else break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Karakter hatası. 0 veya -1 girmeliydiniz.");
                    Thread.Sleep(1200);
                    Console.Clear();
                    GameTitle();
                    continue;
                }
            }

            return exit;
        }

        static void toWinGreen() // kelime tahmini doğru ise çalışacak kısım skor puanı da burada veriliyor.
        {
            playerScore += randomWord.Length;

            Console.ForegroundColor = ConsoleColor.Green;
            guessedLetters = "";
            Console.WriteLine("\n\tDOĞRU BİLDİNİZ.");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nSkorunuz: " + playerScore);
            Console.ResetColor();
        }

        static void GameTitle() // her tahminde ekranı temizlediğimiz için her temizlemeden sonra başlığı yazdırma kısmı
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tADAM ASMACA   ".PadLeft(22));
            Console.WriteLine(new String('-', 25));
            Console.ResetColor();

            Console.WriteLine();
        }

        static void ScreenClear() //tahminlerden sonra ekranı temizleme ve başlık, tahmin harflerini yazdırma
        {
            Thread.Sleep(1000);
            Console.Clear();
            GameTitle();
            Console.WriteLine($"Kullandığınız harfler: {guessedLetters}\n");
        }

        static void LettersUsed() // oyun sırasında oyuncunun kullandığı harfleri ekrana yazdırma
        {
            Console.WriteLine("Kullandığınız harfler: " + LettersUsed);
        }
    }
}