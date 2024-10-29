using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JogoGourmet
{
    public class JogoGourmetForm : Form
    {
        private string currentFood;
        private string differentiatingQuestion;
        private string currentQuestion;
        private string lastFood;
        private readonly List<string> foods;
        private bool isAskingDifferentiator;

        public JogoGourmetForm()
        {
            this.Text = "Jogo Gourmet";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(400, 150);

            foods = new List<string> { "Massa", "Bolo de Chocolate" };
            currentFood = "";
            differentiatingQuestion = "";
            isAskingDifferentiator = false;

            var startButton = new Button();
            startButton.Text = "OK";
            startButton.Location = new System.Drawing.Point(150, 50);
            startButton.Click += StartButton_Click;

            var questionLabel = new Label();
            questionLabel.Text = "Pense em uma comida que você gosta";
            questionLabel.Location = new System.Drawing.Point(50, 20);
            questionLabel.AutoSize = true;

            this.Controls.Add(startButton);
            this.Controls.Add(questionLabel);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            currentFood = foods[0];
            currentQuestion = "O prato que você pensou é " + currentFood + "?";
            DisplayQuestionAndButtons();
        }

        private void DisplayQuestionAndButtons()
        {
            var yesButton = new Button();
            yesButton.Text = "Sim";
            yesButton.Location = new System.Drawing.Point(50, 80);
            yesButton.Click += YesButton_Click;

            var noButton = new Button();
            noButton.Text = "Não";
            noButton.Location = new System.Drawing.Point(150, 80);
            noButton.Click += NoButton_Click;

            var questionLabel = new Label();
            questionLabel.Text = currentQuestion;
            questionLabel.Location = new System.Drawing.Point(50, 50);
            questionLabel.AutoSize = true;

            this.Controls.Clear();
            this.Controls.Add(yesButton);
            this.Controls.Add(noButton);
            this.Controls.Add(questionLabel);
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            if (isAskingDifferentiator)
            {
                currentQuestion = "O prato que você pensou é " + currentFood + "?";
                isAskingDifferentiator = false; 
            }
            else if (currentFood == "Massa")
            {
                currentFood = foods[1];
                currentQuestion = "O prato que você pensou é " + currentFood + "?";
            }
            else
            {
                MessageBox.Show("Acertei de novo!");
                Application.Restart();
            }

            DisplayQuestionAndButtons();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            if (isAskingDifferentiator)
            {

                currentFood = lastFood;
                currentQuestion = "O prato que você pensou é " + currentFood + "?";
                isAskingDifferentiator = false;
            }
            else if (currentFood == "Massa")
            {
                string newFood = Microsoft.VisualBasic.Interaction.InputBox(
                    "Qual o prato que você está pensando?",
                    "Novo Prato",
                    "",
                    -1, -1);

                differentiatingQuestion = Microsoft.VisualBasic.Interaction.InputBox(
                    $"{newFood} é _____ mas {currentFood} não.",
                    "Nova Pergunta",
                    "",
                    -1, -1);

 
                lastFood = currentFood;
                currentFood = newFood;

                currentQuestion = $"O prato que você pensou é {differentiatingQuestion}?";
                isAskingDifferentiator = true; 
            }
            else
            {
                currentFood = differentiatingQuestion;
                currentQuestion = $"O prato que você pensou é {currentFood}?";
            }

            DisplayQuestionAndButtons();
        }
    }
}
