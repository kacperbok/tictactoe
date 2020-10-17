using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KolkoKrzyzyk
{
    class Game
    {

        private bool correctChoiceInput;

        public bool CorrectChoiceInput
        {
            get { return correctChoiceInput; }
            set { correctChoiceInput = value; }
        }


        private bool correctAgreementInput;

        public bool CorrectAgreementInput
        {
            get { return correctAgreementInput; }
            set { correctAgreementInput = value; }
        }


        private int turn;
        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        private int fieldChoice;
        public int FieldChoice
        {
            get { return fieldChoice; }
            set { fieldChoice = value; }
        }

        private int menuChoice;
        public int MenuChoice
        {
            get { return menuChoice; }
            set { menuChoice = value; }
        }

        private string player1;
        public string Player1
        {
            get { return player1; }
            set { player1 = value; }
        }

        private string player2;
        public string Player2
        {
            get { return player2; }
            set { player2 = value; }
        }

        private bool gameIsLive = true;

        private bool GameIsLive
        {
            get { return gameIsLive; }
            set { gameIsLive = value; }
        }


        private bool winFlag;
        public bool WinFlag
        {
            get { return winFlag; }
            set { winFlag = value; }
        }

        private int player1Points = 0;
        private int Player1Points
        {
            get { return player1Points; }
            set { player1Points = value; }
        }

        private int player2Points = 0;

        private int Player2Points
        {
            get { return player2Points; }
            set { player2Points = value; }
        }

        private string pounts;



        private bool correctFieldInput;

        public bool CorrectFieldInput
        {
            get { return correctFieldInput; }
            set { correctFieldInput = value; }
        }


        public int Turns { get; set; }

        static char[] choiceEx = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public void BoardReset()
        {
            choiceEx[0] = '1';
            choiceEx[1] = '2';
            choiceEx[2] = '3';
            choiceEx[3] = '4';
            choiceEx[4] = '5';
            choiceEx[5] = '6';
            choiceEx[6] = '7';
            choiceEx[7] = '8';
            choiceEx[8] = '9';

        }
        public void ShowMenu()
        {
            Console.WriteLine("a. Nowa gra");
            Console.WriteLine("b. Statystki graczy");
            Console.WriteLine("c. Wyjście");
        }

        public void GetChoice()
        {
            while (correctChoiceInput == false)
            {
                MenuChoice = Console.ReadKey().KeyChar;

                if (MenuChoice == 'a' || MenuChoice == 'b' || MenuChoice == 'c')
                {
                    correctChoiceInput = true;
                }
                else
                {
                    Console.Clear();
                    ShowMenu();
                    Console.WriteLine("Niepoprawny wybór");
                }
            }
            Console.WriteLine();
            switch (MenuChoice)
            {
                case 'a':
                    Console.Clear();
                    GamePreparationStart();
                    correctChoiceInput = false;
                    break;

                case 'b':
                    Console.Clear();
                    ShowMenu();
                    LoadStatistics("statistics.csv");
                    correctChoiceInput = false;
                    break;

                case 'c':
                    Exit();
                    break;
            }
            GetChoice();
        }

        public void GetPlayersName()
        {
            Console.WriteLine("Podaj nazwe gracza 1: ");
            this.Player1 = Console.ReadLine();
            Console.WriteLine("Podaj nazwe gracza 2: ");
            this.Player2 = Console.ReadLine();
        }
        public static void Board()
        {

            Console.WriteLine("     |     |      ");

            Console.WriteLine("  {0}  |  {1}  |  {2}", choiceEx[0], choiceEx[1], choiceEx[2]);

            Console.WriteLine("_____|_____|_____ ");

            Console.WriteLine("     |     |      ");

            Console.WriteLine("  {0}  |  {1}  |  {2}", choiceEx[3], choiceEx[4], choiceEx[5]);

            Console.WriteLine("_____|_____|_____ ");

            Console.WriteLine("     |     |      ");

            Console.WriteLine("  {0}  |  {1}  |  {2}", choiceEx[6], choiceEx[7], choiceEx[8]);

            Console.WriteLine("     |     |      ");
        }


        public void GamePreparationStart()
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine();
            Console.WriteLine();
            GetPlayersName();
            Console.WriteLine("W grze bierze udział " + Player1 + " oraz " + Player2);
            Console.WriteLine(Player1 + " używa 'X', a " + Player2 + " uzywa 'O'");
            Console.WriteLine("Plansza wygląda następująco:");
            Board();
            Console.WriteLine("Wciśnij dowolny klawisz aby kontynuować...");
            Console.ReadKey();
            Console.Clear();
            ShowMenu();
            Console.WriteLine();
            Console.WriteLine();
            Board();
            GameStartFinal();
        }

        public void GameStartFinal()
        {
            while (GameIsLive == true)
            {
                
                BoardReset();
                CorrectFieldInput = false;
                WinFlag = false;
                Turn = 1;
                while (WinFlag == false)
                {

                    if (Turn == 1 && GameIsLive == true)
                    {
                      //  GetStatistics(Player1, "statistics.csv");
                        Console.Clear();
                        ShowMenu();
                        ShowPoints(Player1, Player1Points, Player2, Player2Points);
                        Board();
                        Console.WriteLine("Ruch gracza " + Player1 + ". (X)");

                        while (CorrectFieldInput == false)
                        {
                            Console.WriteLine("Wybierz pole (0-9)");
                            while (!int.TryParse(Console.ReadLine(), out fieldChoice))
                            {
                                Console.WriteLine("Podaj odpowiednie pole (1-9)");
                            }

                            if (FieldChoice > 0 && FieldChoice < 10)
                            {
                                CorrectFieldInput = true;
                            }
                        }
                        CorrectFieldInput = false;

                        if (choiceEx[FieldChoice - 1] == 'X' || choiceEx[FieldChoice - 1] == 'O')
                        {
                            Console.WriteLine("Pozycja " + FieldChoice + " jest zajęta. Tracisz kolejkę.");
                            Turn = 2;
                        } 
                        else
                        {
                            Console.Clear();
                            BoardUpdate(FieldChoice, 'X');
                            ShowMenu();
                            ShowPoints(Player1, Player1Points, Player2, Player2Points);
                            Board();
                            WinCheck(Player1);
                            Turn = 2;

                            if (WinFlag == true)
                            {
                                Pointer1(Player1);
                                ChangeOrAddLine("statistics.csv", Player1, player1Points);
                                PlayAgreement();
                                break;
                            }
                        }
                    }

                    if (Turn == 2 && GameIsLive == true)
                    {
                        Console.Clear();
                        ShowMenu();
                        ShowPoints(Player1, Player1Points, Player2, Player2Points);
                        Board();
                        Console.WriteLine("Ruch gracza " + Player2 + ". (O)");

                        while (CorrectFieldInput == false)
                        {

                            Console.WriteLine("Wybierz pole (0-9)");
                            while (!int.TryParse(Console.ReadLine(), out fieldChoice))
                            {
                                Console.WriteLine("Podaj odpowiednie pole (1-9)");
                            }

                            if (FieldChoice > 0 && FieldChoice < 10)
                            {
                                CorrectFieldInput = true;
                            }
                        }
                        CorrectFieldInput = false;

                        if (choiceEx[FieldChoice - 1] == 'X' || choiceEx[FieldChoice - 1] == 'O')
                        {
                            Console.WriteLine("Pozycja " + FieldChoice + " jest zajęta. Tracisz kolejkę.");
                            Turn = 1;
                        }
                        else
                        {
                            BoardUpdate(FieldChoice, 'O');
                            Console.Clear();
                            ShowMenu();
                            ShowPoints(Player1, Player1Points, Player2, Player2Points);
                            Board();
                            WinCheck(Player2);
                            Turn = 1;
                            if (WinFlag == true)
                            {
                                Pointer2(Player2);
                                ChangeOrAddLine("statistics.csv", Player2, player2Points);
                                PlayAgreement();
                                break;
                            }
                        }

                    }

                }

            }
        }





        public void BoardUpdate(int field, char sign)
        {
            choiceEx[field - 1] = sign;
        }

        private void WinCheck(string winner)
        {
            if (choiceEx[0] == 'X' && choiceEx[4] == 'X' && choiceEx[8] == 'X')
            {
                WinFlag = true;
            }
            else if (choiceEx[0] == 'O' && choiceEx[4] == 'O' && choiceEx[8] == 'O')
            {
                WinFlag = true;
            }
            else if (choiceEx[0] == 'O' && choiceEx[3] == 'O' && choiceEx[6] == 'O')
            {
                WinFlag = true;
            }
            else if (choiceEx[0] == 'X' && choiceEx[3] == 'X' && choiceEx[6] == 'X')
            {
                WinFlag = true;
            }
            else if (choiceEx[1] == 'X' && choiceEx[4] == 'X' && choiceEx[7] == 'X')
            {
                WinFlag = true;
            }
            else if (choiceEx[1] == 'O' && choiceEx[4] == 'O' && choiceEx[7] == 'O')
            {
                WinFlag = true;
            }
            else if (choiceEx[2] == 'O' && choiceEx[5] == 'O' && choiceEx[8] == 'O')
            {
                WinFlag = true;
            }
            else if (choiceEx[2] == 'X' && choiceEx[5] == 'X' && choiceEx[8] == 'X')
            {
                WinFlag = true;
            }
            else if (choiceEx[0] == 'X' && choiceEx[1] == 'X' && choiceEx[2] == 'X')
            {
                WinFlag = true;
            }
            else if (choiceEx[3] == 'X' && choiceEx[4] == 'X' && choiceEx[5] == 'X')
            {
                WinFlag = true;
            }
            else if (choiceEx[6] == 'X' && choiceEx[7] == 'X' && choiceEx[8] == 'X')
            {
                WinFlag = true;
            }
            else if (choiceEx[0] == 'O' && choiceEx[1] == 'O' && choiceEx[2] == 'O')
            {
                WinFlag = true;
            }
            else if (choiceEx[3] == 'O' && choiceEx[4] == 'O' && choiceEx[5] == 'O')
            {
                WinFlag = true;
            }
            else if (choiceEx[6] == 'O' && choiceEx[7] == 'O' && choiceEx[8] == 'O')
            {
                WinFlag = true;
            }
            else
            {
                WinFlag = false;
            }

        }

        public void Pointer1(string player)
        {
            Console.WriteLine("Wygral gracz " + player);
            Player1Points++;
        }
        public void Pointer2(string player)
        {
            Console.WriteLine("Wygral gracz " + player);
            Player2Points++;
        }

        public void ShowPoints(string p1, int p1points, string p2, int p2points)
        {
            Console.WriteLine(p1 + ": " + p1points + "     " + p2 + ": " + p2points);
        }

        public void PlayAgreement()
        {

            Console.WriteLine("Czy chcesz zagrać ponownie? Y/N");
            char agreement = Console.ReadKey().KeyChar;
            while (CorrectAgreementInput == true)
            {
                if (agreement == 'y' || agreement == 'Y')
                {
                    this.GameIsLive = true;
                    this.CorrectAgreementInput = true;
                    Turn = 1;
                    break;
                }
                else if (agreement == 'n' || agreement == 'N')
                {
                    this.GameIsLive = false;
                    this.CorrectAgreementInput = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Podano bledna wartosc");
                }
            }
        }

        public void Refresh(string p1, int p1points, string p2, int p2points)
        {
            Console.Clear();
            ShowPoints(p1, p1points, p2, p2points);
            Board();
        }

        void Exit()
        {
            Environment.Exit(0);
        }

        void GetStatistics(string playerName, string filePath)
        {
            using (System.IO.FileStream File = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read))
            using (System.IO.StreamReader Reader = new System.IO.StreamReader(File))
            {
                List<string> lines = Reader.ReadToEnd().Split(new string[] { "/r/n" }, StringSplitOptions.None).ToList();
                File.Position = 0;

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Contains(playerName))
                    {
                        string name = lines[i].Substring(0, 3);
                        Console.WriteLine("Znaleziono gracza");
                        
                        break;
                    }
                }

            }

        }
       

        public static void ChangeOrAddLine(string filePath, string playerName, int playerScore)
        {
            Console.WriteLine("Zapisuje dane");
            string currentString = playerName + " " + playerScore;
            using (System.IO.FileStream File = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read))
            using (System.IO.StreamReader Reader = new System.IO.StreamReader(File))
            using (System.IO.StreamWriter Writer = new System.IO.StreamWriter(File))
                
            {
                List<string> lines = Reader.ReadToEnd().Split(new string[] { "/r/n" }, StringSplitOptions.None).ToList();
                File.Position = 0;
                bool playerFound = false;

                    for (int i = 0; i < lines.Count; i++)
                  {
                    playerFound = false;
                    if (lines[i].Contains(playerName))
                            {
                        playerFound = true;
                        Console.WriteLine("Znaleziono gracza, nadpisuje dane");
                        
                        
                        Writer.Write(currentString);
                        lines[i] = currentString;
                        break;
                            }
                        if (playerFound == false)
                            {
                        Console.WriteLine("Nie znaleziono gracza. Dodaje dane " + playerName);
                        File.Position = i;
                        Writer.WriteLine("\r\n");
                        Writer.WriteLine(currentString);
                           break;
                            }
                }   
            }
            
        }

        public void LoadStatistics(string filePath)
        {
            try
            {

                using (System.IO.FileStream File = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read))
                using (System.IO.StreamReader Reader = new System.IO.StreamReader(File))
                {
                    List<string> lines = Reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                    lines.ForEach(Console.WriteLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
               
        }


        }


    }

