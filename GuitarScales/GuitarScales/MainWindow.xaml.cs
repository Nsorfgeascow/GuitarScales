using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GuitarScales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Brush backgroundcolour = new SolidColorBrush(Color.FromArgb(255, 205, 192, 176));
        List<ScaleG.Notes> scale = new List<ScaleG.Notes>();

        public MainWindow()
        {
            InitializeComponent();

            InitializeGrid();
            ScaleG.CreateScales(); // only -dur
            scale.Clear();

        }

        private static UIElement GetChildren(Grid grid, int row, int column)
        {
            UIElement tmp = null;
            foreach (UIElement child in grid.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    tmp = child;
                }
            }
            return tmp;
        }

        private void InitializeGrid()
        {
            int width = 75;
            int height = 30;
            for(int i = 0; i < 13; i++)
            {
                if(i == 0)
                {
                    width = 40;
                }
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(width);
                grid.ColumnDefinitions.Add(cd);
                width = 75;

                for (int j = 0; j < 7; j++)
                {
                    if(j == 6)
                    {
                        height = 20;
                    }
                    RowDefinition rd = new RowDefinition();
                    rd.Height = new GridLength(height);
                    grid.RowDefinitions.Add(rd);
                }
            }

            ScaleG.Notes n = ScaleG.Notes.E;

            for (int i = 0; i < 7; i++)
            {
                if (i == 1)
                    n++;

                for (int j = 0; j < 13; j++)
                {
                    if(i == 6)
                    {
                        TextBlock t = new TextBlock();
                        t.Text = j.ToString();
                        Grid.SetRow(t, i);
                        Grid.SetColumn(t, j);
                        grid.Children.Add(t);
                    } else
                    {
                        Button b = new Button();

                        ScaleG.Notes k = (ScaleG.Notes)((int)n % 12);

                        if (i == 1)
                        {
                            b.Content = string.Format(((ScaleG.Notes)((int)(n + j + 6 * i) % 12)).ToString());
                        }
                        else
                            b.Content = string.Format(((ScaleG.Notes)((int)(n + j + 7 * i) % 12)).ToString());

                        b.Background = backgroundcolour;
                        b.Foreground = backgroundcolour;
                        b.Click += btn_Click;
                        Grid.SetRow(b, i);
                        Grid.SetColumn(b, j);
                        grid.Children.Add(b);
                    }
                }
            }

            void btn_Click(object sender, RoutedEventArgs e)
            {
                Button currentButton = (Button)sender;
                Button tmp;

                if (currentButton.Foreground == Brushes.Black)
                {
                    currentButton.Foreground = Brushes.Red;

                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 13; j++)
                        {
                            tmp = (Button)GetChildren(grid, i, j);

                            if (tmp.Content.ToString() == currentButton.Content.ToString())
                            {
                                if (tmp.Foreground == tmp.Background)
                                {
                                    tmp.Foreground = Brushes.Black;
                                }
                                else
                                {
                                    tmp.Foreground = backgroundcolour;
                                }
                            }
                        }
                    }

                    if(currentButton.Foreground != backgroundcolour)
                        currentButton.Foreground = Brushes.Red;

                    Enum.TryParse((string)currentButton.Content, out ScaleG.Notes v);

                    if (scale.Contains(v))
                    {
                        scale.Remove(v);
                    }
                    else
                        scale.Add(v);
                }

                FittingScales.Text = null;
                FittingScales.Text = ScaleG.CompareToScales(scale); ;
            }
        }

        private void FillFreatboard(string Notes)
        {
            Button tmp;

            for(int k = 0; k < Notes.Length; k++)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        tmp = (Button)GetChildren(grid, i, j);

                        if (tmp.Content.ToString() == Notes[k].ToString())
                        {
                            if (tmp.Foreground == tmp.Background)
                            {
                                tmp.Foreground = Brushes.Black;
                            }
                            else
                            {
                                tmp.Foreground = backgroundcolour;
                            }
                        }
                    }
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button tmp;
            scale.Clear();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    tmp = (Button)GetChildren(grid, i, j);
                    tmp.Foreground = backgroundcolour;
                }
            }

            FittingScales.Text = null;
            FittingScales.Text = ScaleG.CompareToScales(scale);
        }
    }
}
