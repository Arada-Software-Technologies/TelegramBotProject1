using System;

namespace TelegramBotProject1
{
    class Program
    {
        // the variable holds the generated number
        // *** it's a string so that it can store the numbers like 0123 ***
        static string Gen_num;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static private void GenerateNumber()
        {
            // variables hold the four digits that are going to be generated 
            int first = 0, second = 0, third = 0, fourth = 0;


            Random rnd = new Random();

            while (first == second || first == third || first == fourth || second == third || second == fourth || third == fourth)
            {
                first = rnd.Next(0, 10);
                second = rnd.Next(0, 10);
                third = rnd.Next(0, 10);
                fourth = rnd.Next(0, 10);
            }

            Gen_num = first.ToString() + second.ToString() + third.ToString() + fourth.ToString();

        }

        //returns the ID Score
        // if the user guesses 1234 and the generated number is 3657 the id score will be 1 since only the number 3 exists in the generated number
        static private int IDScore(string UserGuess_Num)
        {
            int id = 0;
            if (UserGuess_Num[0] == Gen_num[0] || UserGuess_Num[0] == Gen_num[1] || UserGuess_Num[0] == Gen_num[2] || UserGuess_Num[0] == Gen_num[3])
            {
                id++;
            }
            if (UserGuess_Num[1] == Gen_num[0] || UserGuess_Num[1] == Gen_num[1] || UserGuess_Num[1] == Gen_num[2] || UserGuess_Num[1] == Gen_num[3])
            {
                id++;
            }
            if (UserGuess_Num[2] == Gen_num[0] || UserGuess_Num[2] == Gen_num[1] || UserGuess_Num[2] == Gen_num[2] || UserGuess_Num[2] == Gen_num[3])
            {
                id++;
            }
            if (UserGuess_Num[3] == Gen_num[0] || UserGuess_Num[3] == Gen_num[1] || UserGuess_Num[3] == Gen_num[2] || UserGuess_Num[3] == Gen_num[3])
            {
                id++;
            }
            return id;
        }

    }
}
