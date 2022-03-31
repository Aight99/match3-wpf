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
            animation.From = 1;
            animation.To = 0;
            animation.Duration = TimeSpan.FromMilliseconds(200);
            figure.BeginAnimation(Image.OpacityProperty, animation);
        }
        
        public void DestroyAnimation(TextBlock figure)
        {
            // DoubleAnimation animation = new DoubleAnimation();
            // animation.From = 1;
            // animation.To = 0;
            // animation.Duration = TimeSpan.FromMilliseconds(200);
            // figure.BeginAnimation(TextBlock.OpacityProperty, animation);
        }
    }
}