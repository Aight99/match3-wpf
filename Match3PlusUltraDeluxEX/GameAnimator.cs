using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Match3PlusUltraDeluxEX
{
    public class GameAnimator
    {
        public void DestroyAnimation(Image figure)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = figure.ActualWidth;
            animation.To = 0;
            animation.Duration = TimeSpan.FromSeconds(3);
            figure.BeginAnimation(Image.WidthProperty, animation);
        }
    }
}