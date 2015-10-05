using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace _2Dtank_sim_game.models
{
    abstract class GameObject
    {
        public Image img;

        public void setup()
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("ms-appx:/img/tank0.png"));
            
        }
    }
    
}
