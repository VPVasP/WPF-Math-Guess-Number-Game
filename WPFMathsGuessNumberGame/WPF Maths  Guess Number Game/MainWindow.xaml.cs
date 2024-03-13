using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Maths__Guess_Number_Game
{

    public partial class MainWindow : Window
    {
        //Int values and Heart Count
        private int number1, number2, result, score;
        private int heartCount = 3;
        //The button list for guessing an answer
        private List<Button> buttonList = new List<Button>();
        
        private readonly Random _rand = new Random();
        private MediaPlayer mediaPlayer = new MediaPlayer();


        public MainWindow()
        {
            InitializeComponent();

            UpdateButtonsContent();//Initialize the buttons with numbers
            UpdateHeartsUI(); //Initialize the Hearts UI
            UpdateScoreUI(); //Initialize the Score UI
            PlayMusic("HappyVibe.wav"); //Play the main Music
        }

        //Method that handles when a button is clicked for guessing
        private void Guess(Button button)
        {
            if (button.Content != null)//we check if the button has content
            {
                string? buttonContent = button.Content.ToString()?.Trim(); //Get the Content of a button
                string expectedResult = result.ToString(); //get the expected result as a string
                if (buttonContent == expectedResult) //if the button content matches the result
                {
                    Debug.WriteLine("Correct Answer");
                    score += 5;//Add Score
                    UpdateScoreUI(); //Update the Score UI
                    PlaySoundEffect("CorrectAnswer.wav"); //Play the Correct Answer sound Effect
                    UpdateHeartsUI();//Update the Hearts UI
                    UpdateButtonsContent();//Update the buttons with numbers
                }
                else
                {
                    Debug.WriteLine("Wrong Answer");
                    heartCount -= 1; //Lose one heart
                    Debug.WriteLine(heartCount);
                    PlaySoundEffect("WrongAnswer.wav");//Play the Wrong Answer sound Effect
                    UpdateHeartsUI();//Update the Hearts UI
                }
            }
        }
        //method to update the score UI
        private void UpdateScoreUI()
        {
            ScoreText.Text = "Score: " + score.ToString();
        }
        //method to update the hearts display based on the  heart count
        private void UpdateHeartsUI()
        {
            //show or hide the hearts based on the heart count
            if (heartCount == 3)
            {
                Heart3.Visibility = Visibility.Visible;
                Heart2.Visibility = Visibility.Visible;
                Heart1.Visibility = Visibility.Visible;
            }
            if (heartCount == 2)
            {
                Heart3.Visibility = Visibility.Collapsed;
                Heart2.Visibility = Visibility.Visible;
                Heart1.Visibility = Visibility.Visible;
            }
            if (heartCount == 1)
            {
                Heart3.Visibility = Visibility.Collapsed;
                Heart2.Visibility = Visibility.Collapsed;
                Heart1.Visibility = Visibility.Visible;
            }
            if (heartCount == 0)
            {
                Heart3.Visibility = Visibility.Collapsed;
                Heart2.Visibility = Visibility.Collapsed;
                Heart1.Visibility = Visibility.Collapsed;
            }
        }


        //methods to handle button clicks for answers
        #region AnwserButtons
        private void AnswerOne(object sender, RoutedEventArgs e)
        {
            Guess(FirstAnswerButton);
        }

        private void AnswerTwo(object sender, RoutedEventArgs e)
        {
            Guess(SecondAnswerButton);
        }

        private void AnswerThree(object sender, RoutedEventArgs e)
        {
            Guess(ThirdAnswerButton);
        }

        private void AnswerFour(object sender, RoutedEventArgs e)
        {
            Guess(FourthAnswerButton);
        }
        #endregion AnswerButtons
        private void UpdateButtonsContent()
        {
            //Clear the list and add new buttons
            buttonList.Clear();
            buttonList.Add(FirstAnswerButton);
            buttonList.Add(SecondAnswerButton);
            buttonList.Add(ThirdAnswerButton);
            buttonList.Add(FourthAnswerButton);
            //Make both numbers random and calculate the result
            number1 = _rand.Next(1, 10);
            number2 = _rand.Next(1, 10);
            result = number1 + number2;

            //Update the ui based on the numbers 
            FirstNumberText.Text = number1.ToString() + "+";
            SecondNumberText.Text = number2.ToString() + "=";
            ResultText.Text = "?";

            //generate a random loop for shuffling
            List<int> numbers = new List<int> { 1, 2, 3, 4 };
            numbers = GenerateRandomLoop(numbers);
            int resultIndex = _rand.Next(0, buttonList.Count);

            //assign numbers to buttons, with one button having the correct result
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (i == resultIndex)
                {
                    buttonList[i].Content = result.ToString();
                }
                else
                {
                    buttonList[i].Content = numbers[i];
                }
            }
        }
        //Fisher-Yates Shuffle Algorithm
        public List<int> GenerateRandomLoop(List<int> listToShuffle)
        {
            for (int i = listToShuffle.Count - 1; i > 0; i--)
            {
                var k = _rand.Next(i + 1);
                var value = listToShuffle[k];
                listToShuffle[k] = listToShuffle[i];
                listToShuffle[i] = value;
            }
            return listToShuffle;
        }


        //Method to play a sound effect
        private void PlaySoundEffect(string audioClipFilePath)
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = audioClipFilePath;
            soundPlayer.Play();
        }

        //Method to play the main music
        private void PlayMusic(string audioClipFilePath)
        {
            mediaPlayer.Open(new Uri(audioClipFilePath, UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }



        //Method to mute/unmute the main Music
        private void MuteMusic(object sender, RoutedEventArgs e)
        {
            mediaPlayer.IsMuted = !mediaPlayer.IsMuted;

            //Update the mute button icon based on if it's muted or not
            if (mediaPlayer.IsMuted)
            {
                MuteButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri("/UnmuteIcon.png", UriKind.Relative)),
                    Stretch = Stretch.UniformToFill
                };
            }
            else
            {
                MuteButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri("/MuteIcon.png", UriKind.Relative)),
                    Stretch = Stretch.UniformToFill
                };
            }
        }
    }
    }