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

        //text for the main menu keyboard
        static string btn_txt1 = "Start Playing!";
        static string btn_txt2 = "Change In Game UserName";
        static string btn_txt3 = "Highscore Board";
        static string btn_txt4 = "Help";
        static string btn_txt5 = "About";

        //true if a game is being played false otherwise
        static bool playing = false;

        //message object that holds the massage id of the game so that it can be edited
        static Message game_msg ,welcome_msg;

        static int previous_msgID;

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            
            bot.StartReceiving();
            bot.OnMessage += Bot_OnMessage;
            
            
            
            

            Console.ReadLine();
        }

        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!playing)
            {
                if (e.Message.Text == "/start")
                {
                    Welcome(e);
                }
                else if (e.Message.Text == btn_txt1)
                {
                    StartGame(e);
                }
            }            
            else
            {
                if(e.Message.Text == "1")
                {
                    
                }
                else if (e.Message.Text == "2")
                {

                }
                else if (e.Message.Text == "3")
                {

                }
                else if (e.Message.Text == "4")
                {

                }
                else if (e.Message.Text == "5")
                {

                }
                else if (e.Message.Text == "6")
                {

                }
                else if (e.Message.Text == "7")
                {

                }
                else if (e.Message.Text == "8")
                {

                }
                else if (e.Message.Text == "9")
                {

                }
                else if (e.Message.Text == "0")
                {

                }
                else if (e.Message.Text == "Quit")
                {
                    //delets the message that was sent by the bot before
                    await bot.DeleteMessageAsync(e.Message.Chat.Id, previous_msgID);
                    playing = false;
                    Welcome(e);
                }
                else if (e.Message.Text == "<--")
                {

                }
                else
                {
                    //send notification to the user that they should use the keyboard and also delete the message they sent
                }

            }
            
            



        }

        static async void StartGame(Telegram.Bot.Args.MessageEventArgs e)
        {
            //delets the message that was sent by the bot before
            await bot.DeleteMessageAsync(e.Message.Chat.Id, previous_msgID);

            //deletes btn_txt1 from the chat
            await bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            // Defining the Keyboard buttons for the game
            KeyboardButton[][] button = { new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[3] };

            button[0][0] = new KeyboardButton("1");
            button[0][1] = new KeyboardButton("2");
            button[0][2] = new KeyboardButton("3");
            button[1][0] = new KeyboardButton("4");
            button[1][1] = new KeyboardButton("5");
            button[1][2] = new KeyboardButton("6");
            button[2][0] = new KeyboardButton("7");
            button[2][1] = new KeyboardButton("8");
            button[2][2] = new KeyboardButton("9");
            button[3][0] = new KeyboardButton("Quit");
            button[3][1] = new KeyboardButton("0");
            button[3][2] = new KeyboardButton("<--");


            //Defining Keyboard that contains the buttons above
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);

            //adjusts the size of the custom keyboard to what is only is needed
            keyboard.ResizeKeyboard = true;

            playing = true;
            GenerateNumber();    


            game_msg = await bot.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: "<b>Game Started!</b> \n\n Good Luck!!!\n\nNumber to Guess: <b>****</b> \n\n        ID  POS \n1. ",
            parseMode: ParseMode.Html,
            disableNotification: true,
            replyMarkup: keyboard
            );

            previous_msgID = game_msg.MessageId;
        }

        static async void Welcome(Telegram.Bot.Args.MessageEventArgs e)
        {
            // Defining the Keyboard buttons for the main menu
            KeyboardButton[][] button = { new KeyboardButton[1], new KeyboardButton[2], new KeyboardButton[2] };

            button[0][0] = new KeyboardButton(btn_txt1);
            button[1][0] = new KeyboardButton(btn_txt2);
            button[1][1] = new KeyboardButton(btn_txt3);
            button[2][0] = new KeyboardButton(btn_txt4);
            button[2][1] = new KeyboardButton(btn_txt5);


            //Defining the Keyboard that contains the buttons above
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);

            //adjusts the size of the custom keyboard to what is only is needed
            keyboard.ResizeKeyboard = true;

            //deletes the '/start' text
            await bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            //sends welcome message and also send the user the custom keyboard defined on line 45
            welcome_msg = await bot.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: "<b>Welcome</b> to the @IDposGameBot. \n\nYou can start using the buttons below to navigate through the bot. \n\nFor help use /help or press the button below ",
            parseMode: ParseMode.Html,
            disableNotification: true,
            replyMarkup: keyboard
            );

            previous_msgID = welcome_msg.MessageId;
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
