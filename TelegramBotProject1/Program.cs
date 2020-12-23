using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotProject1
{
    class Program
    {
        // the variable holds the generated number
        // *** it's a string so that it can store the numbers like 0123 ***
        static string Gen_num;

        static TelegramBotClient bot = new TelegramBotClient("1487222844:AAFs5T_Wl_xiLkvkxPMAQCXoTmD7caA2pjc");
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            bot.StartReceiving();
            bot.OnMessage += Bot_OnMessage;

            Console.ReadLine();
        }

        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string welcomeMessage1 = "<b>Welcome.</b>";
            string welcomeMessage2 = "This is the @IDposGameBot. Use the buttons below to navigate through the bot. If you need help you can also use /help to get more info.";

            if (e.Message.Text == "/start")
            {
                //bot.DeleteMessageAsync("@IDposGameBot", e.Message.MessageId);
                Message message = await bot.SendTextMessageAsync(
                chatId: e.Message.Chat, // or a chat id: 123456789
                text: (welcomeMessage1 + welcomeMessage2),
                parseMode: ParseMode.Html,
                disableNotification: true,
                replyToMessageId: e.Message.MessageId,
                replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[][]
                                                        {
                                                            new [] { new InlineKeyboardButton() { Text = "btn 1"} }, // buttons in row 1
                                                            new [] { new InlineKeyboardButton() { Text = "btn 2"} }, // buttons in row 2
                                                            new [] { new InlineKeyboardButton() { Text = "btn 3"} }// buttons in row 3
                                                        }
                                                      )
                );
            }



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

        // function returns the posititon score 

        // if the user guesses 1234 and the generated number is 1342 the pos score will be only 1
        // since only the number 1 is in the correct position in both cases

        static private int PosScore(string UserGuess_Num)
        {
            int pos = 0;

            if (Gen_num[0] == UserGuess_Num[0])
                pos++;
            if (Gen_num[1] == UserGuess_Num[1])
                pos++;
            if (Gen_num[2] == UserGuess_Num[2])
                pos++;
            if (Gen_num[3] == UserGuess_Num[3])
                pos++;

            return pos;
        }

    }
}
