using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Maths__Guess_Number_Game
{

    public partial class MainWindow : Window
    {
        private int number1, number2, result, score;
        private int heartCount = 3;
        private List<Button> buttonList = new List<Button>();
        private readonly Random _rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            UpdateButtonsContent();
            ButtonsContent();
        }



        private void Guess(Button button)
        {
            string buttonContent = button.Content.ToString();
            string expectedResult = result.ToString().Trim();
            if (buttonContent == expectedResult)
            {
                Debug.WriteLine("Correct Answer");
                score += 5;
                UpdateScoreUI();

                UpdateButtonsContent();
            }
            else
            {
                Debug.WriteLine("Wrong Answer");
                heartCount -= 1;
                UpdateHeartsUI();
                HeartsCheck();
            }
        }

        private void UpdateScoreUI()
        {
            ScoreText.Text = "Score: " + score.ToString();
        }

        private void UpdateHeartsUI()
        {
            Hearts.Text = "Hearts: " + heartCount.ToString();
        }

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

        private void ButtonsContent()
        {
            buttonList.Add(FirstAnswerButton);
            buttonList.Add(SecondAnswerButton);
            buttonList.Add(ThirdAnswerButton);
            buttonList.Add(FourthAnswerButton);

          
            List<int> numbers = new List<int> { 1, 2, 3, 4 };

            numbers = GenerateRandomLoop(numbers);
            int resultIndex = _rand.Next(0, buttonList.Count);

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

        private void UpdateButtonsContent()
        {
            
            number1 = _rand.Next(1, 10);
            number2 = _rand.Next(1, 10);
            result= number1 + number2;

            FirstNumberText.Text = number1.ToString() + "+";
            SecondNumberText.Text = number2.ToString() + "=";
            ResultText.Text = "?";

            List<int> numbers = new List<int> { 1, 2, 3, 4 };
            numbers = GenerateRandomLoop(numbers);
            int resultIndex = _rand.Next(0, buttonList.Count);

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

        private void HeartsCheck()
        {
            UpdateHeartsUI();
            if (heartCount == 0)
            {
                Debug.WriteLine("Lost Game");
            }
        }
    }
}