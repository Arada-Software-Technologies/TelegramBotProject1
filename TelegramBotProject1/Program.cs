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

        //keyboard buttons for the game 
        static KeyboardButton[][] button = { new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[1] };

        //Defining Keyboard that contains the buttons above
        static ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup();

        //holds the id of the last send message by the bot either for deleting or editing the message 
        static int previous_msgID, markup_msgID;

        //holds the number of moves made by the user 
        static int moves = 1;

        //holds the number of digits typed by the user
        static int No_ofDigits = 0;

        //array that holds the digits entered by the user
        static int[] digits = new int[4];

        //string that holds the game message
        static string gameMsg_string= "You can start guessing using the <b>keyboard</b> below\n\n                    ID        POS \n1. _ _ _ _";

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            digits[0] = -1;
            digits[1] = -1;
            digits[2] = -1;
            digits[3] = -1;
            
            
            bot.StartReceiving();
            bot.OnMessage += Bot_OnMessage;
            
            
            
            

            Console.ReadLine();
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
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
                    Guess(e);
                }
                else if (e.Message.Text == "2")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "3")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "4")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "5")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "6")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "7")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "8")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "9")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "0")
                {
                    Guess(e);
                }
                else if (e.Message.Text == "Quit")
                {
                    playing = false;
                    Welcome(e);
                }
                else if (e.Message.Text == "<--")
                {
                    Guess(e);
                }
                else if(e.Message.Text == "Submit")
                {
                    if (No_ofDigits == 4)
                    {

                        int id = IDScore((digits[0].ToString() + digits[1].ToString() + digits[2].ToString() + digits[3].ToString()));
                        int pos = PosScore((digits[0].ToString() + digits[1].ToString() + digits[2].ToString() + digits[3].ToString()));
                        gameMsg_string +=" |  " + id.ToString() + "        " + pos.ToString() + "\n" + (++moves).ToString() + ". _ _ _ _";
                        digits[0] = -1;
                        digits[1] = -1;
                        digits[2] = -1;
                        digits[3] = -1;
                        No_ofDigits = 0;
                        bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
                        Message m = bot.EditMessageTextAsync(
                            chatId: e.Message.Chat,
                            messageId: previous_msgID,
                            text: gameMsg_string,
                            parseMode: ParseMode.Html
                        ).Result;

                    }
                    else
                    {
                        //prompt the user to enter four digits
                    }
                    
                }
                else
                {
                    //send notification to the user that they should use the keyboard and also delete the message they sent
                }

            }





        }

        //function for the start game message
        static void StartGame(Telegram.Bot.Args.MessageEventArgs e)
        {         
            //deletes btn_txt1 from the chat
            bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
            bot.DeleteMessageAsync(e.Message.Chat.Id, markup_msgID);


            // Defining the Keyboard buttons for the game


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
            button[4][0] = new KeyboardButton("Submit");



            keyboard.Keyboard = button;

            //adjusts the size of the custom keyboard to what is only is needed
            keyboard.ResizeKeyboard = true;

            playing = true;
            GenerateNumber();

            Message Keyboard_msg = bot.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: "<b>Game Started!</b> \n\nGood Luck!!!",
                parseMode: ParseMode.Html,
                disableNotification: true,
                replyMarkup: keyboard
                ).Result;

            Console.WriteLine(previous_msgID);
            Message m= bot.EditMessageTextAsync(
                    chatId: e.Message.Chat,
                    messageId: previous_msgID,
                    text: gameMsg_string,
                    parseMode: ParseMode.Html
                    ).Result;
            Console.WriteLine(m.Text);
        }

        //the function edits the game text to add the number that was pressed by the user
        static void Guess(Telegram.Bot.Args.MessageEventArgs e)
        {
            bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
            if (No_ofDigits < 4)
            {
                gameMsg_string = gameMsg_string.Substring(0, gameMsg_string.Length - 8);
                if (e.Message.Text == "<--")
                {
                    if (No_ofDigits > 0)
                    {
                        digits[No_ofDigits - 1] = -1;
                        No_ofDigits--;
                    }
                }
                else
                {
                    if (!IfRepeated(int.Parse(e.Message.Text)))
                    {
                        digits[No_ofDigits] = int.Parse(e.Message.Text);
                        No_ofDigits++;
                    }
                    else
                    {
                        //notify the user that they cant add the same number twice!!!!
                    }

                }

                gameMsg_string += func();


                try
                {
                    Message m = bot.EditMessageTextAsync(
                    chatId: e.Message.Chat,
                    messageId: previous_msgID,
                    text: gameMsg_string,
                    parseMode: ParseMode.Html
                    ).Result;
                }
                catch (AggregateException)
                {

                }
            }
            else
            {
                //notify the user that no more than 4 digits can be entered 
            }

        }

        static bool IfRepeated(int x)
        {           
            for(int i=0; i<4; i++)
            {
                if (digits[i] != -1)
                {
                    if (digits[i] == x)
                    {
                        return true;
                    }
                }
                else
                    return false;
            }
            return false;
        }

        static string func()
        {
            string x="";

            for(int i=0; i<4; i ++)
            {
                if (digits[i] != -1)
                {
                    x += " " + digits[i].ToString();
                }
                else
                {
                    x += " _";
                }
                
            }
            return x;
        }

        //function for welcome message
        static void Welcome(Telegram.Bot.Args.MessageEventArgs e)
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
            bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            Message Keyboard_msg = bot.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: "<b>Hello!</b>",
            parseMode: ParseMode.Html,
            disableNotification: true,
            replyMarkup: keyboard
            ).Result;

            markup_msgID = Keyboard_msg.MessageId;

            //sends welcome message and also send the user the custom keyboard defined on line 45
            Message welcome_msg = bot.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: "<b>Welcome</b> to the @IDposGameBot. \n\nYou can start using the buttons below to navigate through the bot. \n\nFor help use /help or press the button below ",
            parseMode: ParseMode.Html,
            disableNotification: true            
            ).Result;

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
            Console.WriteLine(Gen_num);
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
