using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotProject1
{
    class Program
    {
        //if true bot has started if otherwise false
        static bool start = false;
        // the variable holds the generated number
        // *** it's a string so that it can store the numbers like 0123 ***
        static string Gen_num;

        static TelegramBotClient bot = new TelegramBotClient("1487222844:AAFs5T_Wl_xiLkvkxPMAQCXoTmD7caA2pjc");

        //text for the main menu keyboard
        static string btn_txt1 = "Start Playing!";        
        static string btn_txt3 = "Highscore Board";
        static string btn_txt4 = "Help";
        static string btn_txt5 = "About";

        //true if a game is being played false otherwise
        static bool playing = false;

        //holds the name of the user
        static string name;

        //holds the id of the last send message by the bot either for deleting or editing the message 
        static int previous_msgID, markup_msgID, error_msgID;

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
                if (start)
                {
                    if (e.Message.Text == btn_txt1)
                    {
                        StartGame(e);
                        deleteErrorMsg(e);
                    }
                    else
                    {                       
                        Message msg = bot.SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "<b>Please</b> use the keyboard provided below!!!",
                                parseMode: ParseMode.Html,
                                disableNotification: true
                                ).Result;
                        error_msgID = msg.MessageId;
                        //propmpt the user to use the keyboard
                        //delets the users text                        
                        bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
                    }
                }
                else
                {
                    if (e.Message.Text == "/start")
                    {
                        name = e.Message.From.FirstName + " " + e.Message.From.LastName;
                        deleteErrorMsg(e);
                        Welcome(e);
                        start = true;
                    }
                    else
                    {
                        Message msg = bot.SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "<b>Please</b> type '/start' to start the bot",
                                parseMode: ParseMode.Html,
                                disableNotification: true
                                ).Result;
                        error_msgID = msg.MessageId;
                        //propmpt the user to type /start
                        //delets the users text
                        bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
                    }
                }     
            }            
            else
            {
                if(e.Message.Text == "1")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "2")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "3")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "4")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "5")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "6")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "7")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "8")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "9")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "0")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if (e.Message.Text == "Quit" || e.Message.Text == "Go Back To The Main Menu")
                {
                    deleteErrorMsg(e);
                    refresh(e);
                }
                else if (e.Message.Text == "<--")
                {
                    deleteErrorMsg(e);
                    Guess(e);
                }
                else if(e.Message.Text == "Submit")
                {
                    deleteErrorMsg(e);
                    if (No_ofDigits == 4)
                    {

                        int id = IDScore((digits[0].ToString() + digits[1].ToString() + digits[2].ToString() + digits[3].ToString()));
                        int pos = PosScore((digits[0].ToString() + digits[1].ToString() + digits[2].ToString() + digits[3].ToString()));
                        
                        //adding the idscore and the posscore to the game interface string
                        gameMsg_string +=" |  " + id.ToString() + "        " + pos.ToString() + "\n" + (++moves).ToString() + ". _ _ _ _";

                        //reseting the digit array for the second guess
                        digits[0] = -1;
                        digits[1] = -1;
                        digits[2] = -1;
                        digits[3] = -1;

                        No_ofDigits = 0;

                        //delets the users text
                        bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

                        //sends the game interface to the user with the scores
                        bot.EditMessageTextAsync(
                            chatId: e.Message.Chat,
                            messageId: previous_msgID,
                            text: gameMsg_string,
                            parseMode: ParseMode.Html
                        );

                        //if the pos is equa to 4 the user has won
                        //this sends the congratulations message 
                        if (pos == 4)
                        {
                            //game ends

                            bot.DeleteMessageAsync(e.Message.Chat.Id, markup_msgID);
                            KeyboardButton button = new KeyboardButton("Go Back To The Main Menu");
                            
                            //Defining the Keyboard that contains the buttons above
                            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);

                            //adjusts the size of the custom keyboard to what is only is needed
                            keyboard.ResizeKeyboard = true;                            

                            Message Keyboard_msg = bot.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "Congratulations!!! \n\nYou have guessed the number in " + (moves - 1).ToString() + " tries. Check the Highscore board to see if you havee made it in to the list. \n\nTo go back to the main menu press the button below",
                            parseMode: ParseMode.Html,
                            disableNotification: true,
                            replyMarkup: keyboard
                            ).Result;

                            //stors the id of the above message so that it can be deleted when going back to the menu
                            markup_msgID = Keyboard_msg.MessageId;
                        }
                    }
                    else
                    {
                        //game continues

                        //delets the users text
                        bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

                        Message msg = bot.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "<b>Please</b> enter 4 digits before submitting\n you have currently entered only " + No_ofDigits.ToString() + " digits.",
                            parseMode: ParseMode.Html,
                            disableNotification: true                          
                            ).Result;
                        error_msgID = msg.MessageId;
                        //prompt the user to enter four digits
                    }
                    
                }
                else
                {
                    //delets the users text
                    bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

                    Message msg = bot.SendTextMessageAsync(
                            chatId: e.Message.Chat,
                            text: "<b>Please</b> use the keyboard provided below!!! \n there is no need to type to play this game.",
                            parseMode: ParseMode.Html,
                            disableNotification: true
                            ).Result;
                    error_msgID = msg.MessageId;

                    //send notification to the user that they should use the keyboard and also delete the message they sent
                }

            }





        }
        
        //delets error messages sent by the bot if there are any
        //if there arent the code wont execute
        static void deleteErrorMsg(Telegram.Bot.Args.MessageEventArgs e)
        {
            //catches message not found exception
            try
            {
                bot.DeleteMessageAsync(e.Message.Chat.Id, error_msgID);
            }
            catch(Exception)
            {

            }            
        }

        //removes all the data from a previous game so that a new one can be started 
        static void refresh(Telegram.Bot.Args.MessageEventArgs e)
        {
            digits[0] = -1;
            digits[1] = -1;
            digits[2] = -1;
            digits[3] = -1;
            gameMsg_string = "You can start guessing using the <b>keyboard</b> below\n\n                    ID        POS \n1. _ _ _ _";
            No_ofDigits = 0;
            moves = 1;
            playing = false;
            bot.DeleteMessageAsync(e.Message.Chat.Id, previous_msgID);
            bot.DeleteMessageAsync(e.Message.Chat.Id, markup_msgID);
            Welcome(e);
        }

        //function for the start game message
        static void StartGame(Telegram.Bot.Args.MessageEventArgs e)
        {         
            //deletes btn_txt1 from the chat
            bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
            bot.DeleteMessageAsync(e.Message.Chat.Id, markup_msgID);

            //keyboard buttons for the game 
            KeyboardButton[][] button = { new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[3], new KeyboardButton[1] };

            //Defining Keyboard that contains the buttons above
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup();

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

            //sends the keyboard for the game and also sends a text message for good luck
            Message Keyboard_msg = bot.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: "<b>Game Started!</b> \n\nGood Luck!!!",
                parseMode: ParseMode.Html,
                disableNotification: true,
                replyMarkup: keyboard
                ).Result;
            
            //storing the id of the above message  for deletion 
            markup_msgID = Keyboard_msg.MessageId;

            //edits the welcome message to make it look like the interface of the game
            Message m= bot.EditMessageTextAsync(
                    chatId: e.Message.Chat,
                    messageId: previous_msgID,
                    text: gameMsg_string,
                    parseMode: ParseMode.Html
                    ).Result;
            
        }

        //the function edits the game text to add the number that was pressed by the user
        static void Guess(Telegram.Bot.Args.MessageEventArgs e)
        {
            //delets the text sent by the user 
            bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            // removes the last 8 charactes of the string so that the nuw number that the user enters can be added 
            gameMsg_string = gameMsg_string.Substring(0, gameMsg_string.Length - 8);

            if (e.Message.Text == "<--")
            {
                //if the user hasnt entered any digits no nee to run this code as there aren't any numbers to remove
                if (No_ofDigits > 0)
                {
                    //changes the number entered previously to -1 so that the user can enter another number in place of the number he added before
                    digits[No_ofDigits - 1] = -1;

                    //number of digits entered by the user is decreased 
                    No_ofDigits--;
                }
            }
            else
            {
                //checks if the user enters more than 4 digits
                if (No_ofDigits < 4)
                {
                    if (!IfRepeated(int.Parse(e.Message.Text)))
                    {
                        //adds the number entered by the user to the digits array
                        //later it will be added to the game interface
                        digits[No_ofDigits] = int.Parse(e.Message.Text);

                        No_ofDigits++;
                    }
                    else
                    {
                        

                        Message msg = bot.SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "<b>Please</b> dont try to add the same number twice!!!!",
                                parseMode: ParseMode.Html,
                                disableNotification: true
                                ).Result;
                        error_msgID = msg.MessageId;

                        //send notification to the user that they should use the keyboard and also delete the message they sent
                        //notify the user that they cant add the same number twice!!!!
                    }
                }
                else
                {
                    Message msg = bot.SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "<b>Please</b> dont try to enter more than 4 digits",
                                parseMode: ParseMode.Html,
                                disableNotification: true
                                ).Result;
                    error_msgID = msg.MessageId;
                    //notify the user that no more than 4 digits can be entered 
                }
            }

            
            //adds the digits to the game interface string 
            gameMsg_string += func();

            //sends the game interface string to the user
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

        //checks if the user entered a number twice 
        static bool IfRepeated(int x)
        {           
            for(int i=0; i<4; i++)
            {
                //checks for -1 to not do more loops that it has to since -1 means the user hasent entered a digit in that row of the array
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

        // generates the the string that is outputted when the user enters a number 
        // also works if the user uses the backspace aka '<--' it just replaces the last number with _
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
            KeyboardButton[][] button = { new KeyboardButton[1], new KeyboardButton[1], new KeyboardButton[2] };

            button[0][0] = new KeyboardButton(btn_txt1);            
            button[1][0] = new KeyboardButton(btn_txt3);
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
            text: "<b>Hello!</b> " + name + ",",
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
