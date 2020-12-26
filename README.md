# TelegramBotProject1
This repository contains the implementation of the IDPOS game in to a telegram bot using C#

A project that integrates the game IDPOS in to a telegram bot.

IDPOS is a game where the computer generates a four digit number that contains non-repeating numbers from 0-9 
and the user attempts to guess the number. The user is given two types of data the first one tells the user 
if one of the numbers they guessed is in the generated number and the second one tells the user is any of the 
numbers in his guess have the correct position as the generated one. by using these two hints the user will attempt 
to guess the number.

The project uses the Telegram.bot api, .net core framework and MSsql

if you would like to create the database for the project your self the SQL script is below

//SLQ Script Start

CREATE DATABASE IDPOS_Database;

GO

USE IDPOS_Database;


CREATE TABLE HighScore_table
(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	username varchar(20) not null,
	Moves int not null
);

GO

CREATE PROC Record_score
(
	@username varchar(20),
	@Moves int
)
AS
BEGIN
	INSERT INTO HighScore_table(username,Moves)
	Values (@username,@Moves)
END

GO

//SQL Script End
