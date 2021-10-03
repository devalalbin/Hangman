using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            int userChoice;

            while (true)
            {


                Console.Clear();
                Console.WriteLine("Press 1 to play hangman enter 0 to exit");
                userChoice = GetNumberFromUser(); //User picks what function they want to use



                {
                    switch (userChoice)
                    {      //switch case statement as the "menu" on what 
                        case 0: return; // exit
                        case 1:
                            HangmanGame();
                            break;


                        default:
                            Console.WriteLine("Please enter a valid number! (0 to exit)");
                            break;





                    }
                }


            }
        }
        static int GetNumberFromUser()
        {
            int userInput = 0;
            bool succeeded = false;
            while (!succeeded)
            {
                succeeded = int.TryParse(Console.ReadLine(), out userInput);// checking if the character is a valid number
                if (!succeeded)
                {
                    Console.WriteLine("Enter 1 to play hangman 0 to exit");
                }
            }
            //Console.WriteLine(succeeded);
            return userInput;
        }



        static void HangmanGame()
        {

            int nrOfGuesses = 0; //initilizing starting values
            int maxGuesses = 10;
            string answer = GetRndWord(); // Gets a random answer as our answer
            string userGuess;
            bool win = false;
            bool end = false;


            StringBuilder incorrectLetters = new StringBuilder(); //stringbuilder to put all incorrect letters
            char[] correctLetters = new char[answer.Length]; // array of chars that sets the lenght of our masked/correct letters to same lenght as answer
            Console.WriteLine(" Welcome to hangman! (0 to exit)");
            for (int i = 0; i < answer.Length; i++)
            {
                correctLetters[i] = '_'; //masking our correct answer as _
                Console.Write(correctLetters[i]);
            }






            while (nrOfGuesses <= maxGuesses)
            {
                Console.WriteLine("Guess a letter or a word");
                Console.WriteLine("Number of guesses left " + (maxGuesses - nrOfGuesses));


                string correctString = new string(correctLetters);
                if (Equals(correctString, answer))
                {

                    Console.WriteLine("hey good job you guessed every letter right! " + correctString); //user won by guessing every letter correct
                    CheckIfGameOver(maxGuesses, nrOfGuesses, true, answer);
                    break;

                }
                userGuess = GetUserGuess(correctLetters, incorrectLetters); // get user guess here
                Console.WriteLine(correctLetters);
                if (userGuess.Length == 1) //check user guess is a letter else its a word
                {
                    //  User guess is a letter
                    nrOfGuesses++;
                    GuessTrueOrFalse(userGuess, incorrectLetters, answer, correctLetters);

                }
                else
                {
                    // User guess is a word
                    if (userGuess == answer)
                    {

                        win = true;


                    }
                    else
                    {
                        Console.WriteLine("Sorry! That is not the correct word (you lose 2 guesses)");
                        nrOfGuesses = nrOfGuesses + 2;



                    }
                }


                end = CheckIfGameOver(maxGuesses, nrOfGuesses, win, answer);
                if (end == true)
                {
                    break;
                }


            }




        }

        static string GetRndWord() // gets a randum string from 10 choices
        {
            while (true)
            {
                Random rnd = new Random();
                string[] randomAnswer =
                 {"CAT","DOG","SHARK","BIRD","BEAR","FISH","CHICKEN","HORSE","LOBSTER","MONKEY"}; // array of 10 strings

                return randomAnswer[rnd.Next(0, 9)];  // returns a random string from the array 10 choices
            }
        }

        static string GetUserGuess(char[] correctLetters, StringBuilder incorrectLetters) // returns a string or letter from the user (uppercased) while checking if they already guessed it
        {
            // first check if user has already guessed that letter, if so make user make a new guess


            string userGuess = Console.ReadLine().ToUpper();
            if (userGuess == "0") Environment.Exit(0);
            if (userGuess.Length == 1)
            {
                while (incorrectLetters.ToString().Contains(userGuess)) // while they are trying to guess a letter that they already guessed make them guess again
                {
                    Console.WriteLine("You already guessed that letter, Please make a new guess :");
                    Console.WriteLine("Incorrect letters guessed :" + incorrectLetters);
                    userGuess = Console.ReadLine().ToUpper();


                }
                for (int i = 0; i < correctLetters.Length; i++) //iterate through the correct guess array to make sure user dosent guess same letter twice
                {
                    while (userGuess[0] == correctLetters[i])
                    {
                        Console.WriteLine("That letter is already in the word! Please make a new guess :");
                        Console.WriteLine(correctLetters);
                        userGuess = Console.ReadLine().ToUpper();

                    }
                }
            }
            return userGuess;
        }

        static char[] GuessTrueOrFalse(string userGuess, StringBuilder incorrectletter, string answer, char[] correctLetters)
        {

            bool correctGuess = false;
            for (int i = 0; i < answer.Length; i++)  // iterate through the array to check to see if the user answer is correct
            {

                //Console.WriteLine("this is the comparison" + userGuess + answer[i]);
                if (string.Equals(userGuess[0], answer[i])) // Check if guess and answer is equal, if so add it to the CHAR array
                {
                    Console.WriteLine(userGuess + " IS A CORRECT GUESS");
                    Console.WriteLine("Incorrect letters guessed :" + incorrectletter);
                    correctLetters[i] = userGuess[0];
                    Console.WriteLine(correctLetters);
                    correctGuess = true;
                }

            }
            if (correctGuess == false)
            {
                incorrectletter.Append(userGuess[0]);
                Console.WriteLine("Your guess was incorrect");
                Console.WriteLine("Incorrect letters guessed :" + incorrectletter);

            }
            return correctLetters;
        }
        static bool CheckIfGameOver(int maxguesses, int guesses, bool win, string answer)
        { //returns win or loss bool

            if (win == false)
            {
                if (maxguesses <= guesses)
                {
                    Console.WriteLine("Sadly you did not guess the correct word, the word was " + answer + " better luck next time!");
                    Console.Read();
                    //Environment.Exit(0);
                    return true;

                }


            }
            else
            {
                Console.WriteLine("CONGRATULATIONS! the word was : " + answer + " You won hangman with " + (maxguesses - guesses) + " guesses left!");
                Console.Read();
                //Environment.Exit(0);
                return true;

            }

            return false; // end = false if endcondition is not met
        }

    }

}