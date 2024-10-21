using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace WordGuessApplication
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        WindowsMediaPlayer soundEffect = new WindowsMediaPlayer();

        ArrayList wrongGuesses = new ArrayList();
        Random rand = new Random();

        string[] words = { "PROGRAM", "BROWSE", "CELLPHONE", "COMPUTER", "DOWNLOAD", "SYSTEM", "DIGITAL", "DEVICE", "NETWORK", "DESKTOP" };
        string wordToGuess;
        char[] maskedWord;
        int hintCounter = 0;
        const int maxHints = 5;


        public Form1()
        {
            InitializeComponent();
            TheWord();
        }

        private void TheWord()
        {
            bgMusic();

            label3.Text = $"{hintCounter}/{maxHints}";
            wordToGuess = words[rand.Next(0, words.Length)];

            maskedWord = new string('?', wordToGuess.Length).ToCharArray();
            label2.Text = new string(maskedWord);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string guessWord = txtGuess.Text.ToUpper();
            StringBuilder message = new StringBuilder();

            if (string.IsNullOrWhiteSpace(guessWord))
            {
                MessageBox.Show("Please enter a guess");
            }
            else if (guessWord == wordToGuess)
            {
                winnerSE();
                label2.Text = wordToGuess;
                message.Append("Correct! The word is " + wordToGuess);
                MessageBox.Show(message.ToString());
                disableAllbtns();
                askToPlayAgain();
            }
            else
            {
                wrongAnswerSE();
                wrongGuesses.Add(guessWord);
                listBox1.Items.Add(guessWord);

                message.Append("Wrong guess! Try again.");
                MessageBox.Show(message.ToString());
            }
            txtGuess.Clear();
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            if (hintCounter >= maxHints)
            {
                btnHint.Enabled = false;
                wrongAnswerSE();
                MessageBox.Show("You have used all 5 hints.");
            }
            else if (hintCounter < maxHints)
            {
                hintSE();
                int randLetter = rand.Next(0, wordToGuess.Length);
                while (true)
                {
                    if (maskedWord[randLetter] == '?')
                    {
                        maskedWord[randLetter] = wordToGuess[randLetter];

                        label2.Text = new string(maskedWord);

                        hintCounter++;
                        label3.Text = $"{hintCounter}/{maxHints}";

                        break;
                    }
                    else
                    {
                        randLetter = rand.Next(0, wordToGuess.Length);
                    }
                }          
            }
        }

        private void skipBtn_Click(object sender, EventArgs e)
        {
            loserSE();
            label2.Text = wordToGuess;
            MessageBox.Show("You Skip! \nThe Word is " + wordToGuess);          
            disableAllbtns();
            askToPlayAgain();
        }
        private void askToPlayAgain()
        {
            DialogResult result = MessageBox.Show("Do you want to play again?", "Play Again", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                wrongGuesses.Clear();
                listBox1.Items.Clear();
                txtGuess.Clear();
                hintCounter = 0;
                disableAllbtns();
                TheWord();               
            }
            else
            {
                MessageBox.Show("Thank you for playing! :)");
                this.Close();
            }
        }
        private void disableAllbtns()
        {
            txtGuess.Enabled = true;
            button1.Enabled = true;
            btnHint.Enabled = true;
            skipBtn.Enabled = true;
        }
        private void bgMusic()
        {
            player.URL = "Windmill Hut - The Legend of Zelda_ Ocarina Of Time.mp3";
            player.controls.play();
        }
        private void winnerSE()
        {
            player.URL = "YEHEY CLAP SOUND EFFECT Awarding.mp3";
        }
        private void loserSE()
        {
            player.URL = "The Price is Right Losing Horn - Sound Effect (HD).mp3";
        }
        private void wrongAnswerSE()
        {
            soundEffect.URL = "wrong answer Sound effect No Copyright.mp3";
            soundEffect.controls.play();
        }
        private void hintSE()
        {
            soundEffect.URL = "Game Magic Hint Sound Effect  SFX Sound.mp3";
            soundEffect.controls.play();
        }
    }
}

