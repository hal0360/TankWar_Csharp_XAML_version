using _2Dtank_sim_game.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _2Dtank_sim_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer timer;
        private Game game;
        private bool stopflag;
        private List<BitmapImage> smallExplode = new List<BitmapImage>();
        private List<BitmapImage> bigExplode = new List<BitmapImage>();
        private List<BitmapImage> smallImpact = new List<BitmapImage>();
        private List<BitmapImage> bigImpact = new List<BitmapImage>();
        private List<BitmapImage> muzzle = new List<BitmapImage>();
        private List<BitmapImage> bigMuzzle = new List<BitmapImage>();
        private List<BitmapImage> smoke = new List<BitmapImage>();
        private List<BitmapImage> firesmoke = new List<BitmapImage>();
        private BitmapImage playerImage;
        private BitmapImage playerTurImage;
        private BitmapImage enemyImage;
        private BitmapImage enemyTurImage;
        private BitmapImage bigEmenyImage;
        private BitmapImage bigEmenyTurImage;
        private BitmapImage bulletImage;
        private BitmapImage bulletBigImage;
        private BitmapImage houseImage;
        private BitmapImage playerWreck;
        private BitmapImage smallWreck;
        private BitmapImage bigWreck;

        public MainPage()
        {

            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Cross, 1);

            // ApplicationView. = ApplicationViewWindowingMode.FullScreen;
            stopflag = false;
            BitmapImage dummyMap;
            this.InitializeComponent();
 

            for (int i = 11; i < 32; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                smallExplode.Add(dummyMap);
            }

            for (int i = 116; i < 153; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                bigExplode.Add(dummyMap);
            }

            for (int i = 1; i < 11; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                smallImpact.Add(dummyMap);
            }

            for (int i = 88; i < 100; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                bigImpact.Add(dummyMap);
            }

            for (int i = 54; i < 58; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                muzzle.Add(dummyMap);
            }

            for (int i = 48; i < 54; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                bigMuzzle.Add(dummyMap);              
            }

            for (int i = 100; i < 116; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                smoke.Add(dummyMap);
            }

            for (int i = 32; i < 48; i += 1)
            {
                dummyMap = new BitmapImage(new Uri("ms-appx:/img/" + i + ".png"));
                setCanvas1(dummyMap);
                firesmoke.Add(dummyMap);
            }

            playerImage = new BitmapImage(new Uri("ms-appx:/img/tank0.png"));
            setCanvas1(playerImage);
            playerTurImage = new BitmapImage(new Uri("ms-appx:/img/turret0.png"));
            setCanvas1(playerTurImage);
            enemyImage = new BitmapImage(new Uri("ms-appx:/img/tank1.png"));
            setCanvas1(enemyImage);
            enemyTurImage = new BitmapImage(new Uri("ms-appx:/img/turret1.png"));
            setCanvas1(enemyTurImage);
            bigEmenyImage = new BitmapImage(new Uri("ms-appx:/img/tank2.png"));
            setCanvas1(bigEmenyImage);
            bigEmenyTurImage = new BitmapImage(new Uri("ms-appx:/img/turret2.png"));
            setCanvas1(bigEmenyTurImage);
            bulletImage = new BitmapImage(new Uri("ms-appx:/img/bullet0.png"));
            setCanvas1(bulletImage);
            bulletBigImage = new BitmapImage(new Uri("ms-appx:/img/bullet1.png"));
            setCanvas1(bulletBigImage);
            houseImage = new BitmapImage(new Uri("ms-appx:/img/house1.png"));
            setCanvas1(houseImage);
            playerWreck = new BitmapImage(new Uri("ms-appx:/img/wrack0.png"));
            setCanvas1(playerWreck);
            smallWreck = new BitmapImage(new Uri("ms-appx:/img/wrack1.png"));
            setCanvas1(smallWreck);
            bigWreck = new BitmapImage(new Uri("ms-appx:/img/wrack2.png"));
            setCanvas1(bigWreck);

            //canvas2.Children.Clear();

            game = new Game();
            game.player.img = setCanvas(playerImage);
            game.player.turret.img = setCanvas(playerTurImage);
            Canvas.SetZIndex(game.player.img, 2);
            Canvas.SetZIndex(game.player.turret.img, 2);

            for (int i = 0; i < game.houses.Count; i += 1)
            {
                game.houses[i].img = setCanvas(houseImage);
                Canvas.SetLeft(game.houses[i].img, game.houses[i].posX - 45.5);
                Canvas.SetTop(game.houses[i].img, game.houses[i].posY - 72);
                game.houses[i].img.RenderTransform = new RotateTransform() { CenterX = 45.5, CenterY = 72, Angle = game.houses[i].angle };
            }

                //carBitmap.ImageOpened += (sender, e) =>
                // {
                //  textBlock1.Text = img.ActualWidth.ToString();
                // };


            Window.Current.CoreWindow.KeyDown += game_KeyDown;
            Window.Current.CoreWindow.KeyUp += game_KeyUp;
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100/3);
            timer.Start();

        }

        private Image setCanvas(BitmapImage bi)
        {
            Image img = new Image();
            img.Source = bi;
            canvas1.Children.Add(img);
            return img;
        }

        private Image setCanvas1(BitmapImage bi)
        {
            Image img = new Image();
            img.Source = bi;
            canvas2.Children.Add(img);
            return img;
        }

        private void timer_Tick(object sender, object args)
        {

            for (int i = 0; i < game.removal.Count; i += 1)
            {
                canvas1.Children.Remove(game.removal[i]);  
            }
            game.removal.Clear();


            for (int i = 0; i < game.wrecks.Count; i += 1)
            {
                if (game.wrecks[i].img == null)
                {
                    if (game.wrecks[i].type == 0)
                    {
                        game.wrecks[i].img = setCanvas(playerWreck);
                    }
                    else if (game.wrecks[i].type == 1)
                    {
                        game.wrecks[i].img = setCanvas(smallWreck);
                    }
                    else
                    {
                        game.wrecks[i].img = setCanvas(bigWreck);
                    }
                    Canvas.SetZIndex(game.wrecks[i].img, 1);

                }

                if (game.wrecks[i].type == 0 || game.wrecks[i].type == 1)
                {
                    Canvas.SetLeft(game.wrecks[i].img, game.wrecks[i].posX - smallWreck.PixelWidth / 2);
                    Canvas.SetTop(game.wrecks[i].img, game.wrecks[i].posY - smallWreck.PixelHeight / 2);
                    game.wrecks[i].img.RenderTransform = new RotateTransform() { CenterX = smallWreck.PixelWidth / 2, CenterY = smallWreck.PixelHeight / 2, Angle = game.wrecks[i].angle };

                }
                else
                {
                    Canvas.SetLeft(game.wrecks[i].img, game.wrecks[i].posX - bigWreck.PixelWidth / 2);
                    Canvas.SetTop(game.wrecks[i].img, game.wrecks[i].posY - bigWreck.PixelHeight / 2);
                    game.wrecks[i].img.RenderTransform = new RotateTransform() { CenterX = bigWreck.PixelWidth / 2, CenterY = bigWreck.PixelHeight / 2, Angle = game.wrecks[i].angle };

                }
            }

            // game.player.action();
            // TransformGroup transgroup = new TransformGroup();
            // transgroup.Children.Add(new TranslateTransform() {X = tk.posX, Y = tk.posY });
            // transgroup.Children.Add(new RotateTransform() { CenterX = 0.5, CenterY = 0.5, Angle = tk.angle });
            // tk.img.RenderTransform = transgroup;

            if (!game.dead) {
                if (game.player.live <= game.player.maxlive / 2)
                {
                    if (game.player.smokeimg == null)
                    {
                        game.player.smokeimg = setCanvas(bulletImage);
                        Canvas.SetZIndex(game.player.smokeimg, 2);
                    }

                    game.player.smokeimg.Source = smoke[game.player.smokecount];
                    Canvas.SetLeft(game.player.smokeimg, game.player.posX - smoke[game.player.smokecount].PixelWidth / 2);
                    Canvas.SetTop(game.player.smokeimg, game.player.posY - smoke[game.player.smokecount].PixelHeight);
                    game.player.smokeimg.RenderTransform = new RotateTransform() { CenterX = smoke[game.player.smokecount].PixelWidth / 2, CenterY = smoke[game.player.smokecount].PixelHeight, Angle = 0 };

                }
            

            Canvas.SetLeft(game.player.img, game.player.posX - game.player.offX);
            Canvas.SetTop(game.player.img, game.player.posY - game.player.offY);
            game.player.img.RenderTransform = new RotateTransform() { CenterX = game.player.offX, CenterY = game.player.offY, Angle = game.player.angle };

            Canvas.SetLeft(game.player.turret.img, game.player.turret.posX - game.player.turret.offX );
            Canvas.SetTop(game.player.turret.img, game.player.turret.posY - game.player.turret.offY );
            game.player.turret.img.RenderTransform = new RotateTransform() { CenterX = game.player.turret.offX, CenterY = game.player.turret.offY, Angle = game.player.turret.angle*180/Math.PI };

}

            for (int i = 0; i < game.playerBullets.Count; i += 1)
            {
                if (game.playerBullets[i].img == null)
                {
                    game.playerBullets[i].img = setCanvas(bulletImage);
                    Canvas.SetZIndex(game.playerBullets[i].img, 4);
                }        

                Canvas.SetLeft(game.playerBullets[i].img, game.playerBullets[i].posX - bulletImage.PixelWidth/2);
                Canvas.SetTop(game.playerBullets[i].img, game.playerBullets[i].posY - bulletImage.PixelHeight/2);
            }

            for (int i = 0; i < game.enemyBullets.Count; i += 1)
            {
                if (game.enemyBullets[i].img == null)
                {
                    if(game.enemyBullets[i].imgName == "bullet0.png")
                    {
                        game.enemyBullets[i].img = setCanvas(bulletImage);
                    }
                    else
                    {
                        game.enemyBullets[i].img = setCanvas(bulletBigImage);
                    }
                    Canvas.SetZIndex(game.enemyBullets[i].img, 4);
                }

                if (game.enemyBullets[i].imgName == "bullet0.png")
                {
                    Canvas.SetLeft(game.enemyBullets[i].img, game.enemyBullets[i].posX - bulletImage.PixelWidth / 2);
                    Canvas.SetTop(game.enemyBullets[i].img, game.enemyBullets[i].posY - bulletImage.PixelHeight / 2);
                }
                else
                {
                    Canvas.SetLeft(game.enemyBullets[i].img, game.enemyBullets[i].posX - bulletBigImage.PixelWidth / 2);
                    Canvas.SetTop(game.enemyBullets[i].img, game.enemyBullets[i].posY - bulletBigImage.PixelHeight / 2);
                }
            }

            for (int i = 0; i < game.enemyTanks.Count; i += 1)
            {
                if (game.enemyTanks[i].img == null)
                {
                    if (game.enemyTanks[i].imgName == "tank1.png")
                    {
                        game.enemyTanks[i].img = setCanvas(enemyImage);
                    }
                    else
                    {
                        game.enemyTanks[i].img = setCanvas(bigEmenyImage);
                    }
                }
                if (game.enemyTanks[i].turret.img == null)
                {
                    if (game.enemyTanks[i].turret.imgName == "turret1.png")
                    {
                        game.enemyTanks[i].turret.img = setCanvas(enemyTurImage);
                    }
                    else
                    {
                        game.enemyTanks[i].turret.img = setCanvas(bigEmenyTurImage);
                    }
                    Canvas.SetZIndex(game.enemyTanks[i].img, 2);
                    Canvas.SetZIndex(game.enemyTanks[i].turret.img, 2);
                }

                if (game.enemyTanks[i].live <= game.enemyTanks[i].maxlive/2)
                {
                    if (game.enemyTanks[i].smokeimg == null)
                    {
                        game.enemyTanks[i].smokeimg = setCanvas(bulletImage);
                        Canvas.SetZIndex(game.enemyTanks[i].smokeimg, 2);
                    }

                    game.enemyTanks[i].smokeimg.Source = smoke[game.enemyTanks[i].smokecount];
                    Canvas.SetLeft(game.enemyTanks[i].smokeimg, game.enemyTanks[i].posX - smoke[game.enemyTanks[i].smokecount].PixelWidth / 2);
                    Canvas.SetTop(game.enemyTanks[i].smokeimg, game.enemyTanks[i].posY - smoke[game.enemyTanks[i].smokecount].PixelHeight);
                    game.enemyTanks[i].smokeimg.RenderTransform = new RotateTransform() { CenterX = smoke[game.enemyTanks[i].smokecount].PixelWidth / 2, CenterY = smoke[game.enemyTanks[i].smokecount].PixelHeight, Angle = 0 };

                }

                Canvas.SetLeft(game.enemyTanks[i].img, game.enemyTanks[i].posX - game.enemyTanks[i].offX);
                Canvas.SetTop(game.enemyTanks[i].img, game.enemyTanks[i].posY - game.enemyTanks[i].offY);
                game.enemyTanks[i].img.RenderTransform = new RotateTransform() { CenterX = game.enemyTanks[i].offX, CenterY = game.enemyTanks[i].offY, Angle = game.enemyTanks[i].angle };

                Canvas.SetLeft(game.enemyTanks[i].turret.img, game.enemyTanks[i].turret.posX - game.enemyTanks[i].turret.offX);
                Canvas.SetTop(game.enemyTanks[i].turret.img, game.enemyTanks[i].turret.posY - game.enemyTanks[i].turret.offY);
                game.enemyTanks[i].turret.img.RenderTransform = new RotateTransform() { CenterX = game.enemyTanks[i].turret.offX, CenterY = game.enemyTanks[i].turret.offY, Angle = game.enemyTanks[i].turret.angle * 180 / Math.PI };
            }

            for (int i = 0; i < game.smokes.Count; i += 1)
            {
                if (game.smokes[i].img == null)
                {
                    game.smokes[i].img = setCanvas(bulletImage);
                    Canvas.SetZIndex(game.smokes[i].img, 3);
                }
                game.smokes[i].img.Source = firesmoke[game.smokes[i].count];
                Canvas.SetLeft(game.smokes[i].img, game.smokes[i].posX - firesmoke[game.smokes[i].count].PixelWidth / 2);
                Canvas.SetTop(game.smokes[i].img, game.smokes[i].posY - firesmoke[game.smokes[i].count].PixelHeight);
            }

                for (int i = 0; i < game.effects.Count; i += 1)
            {
                if (game.effects[i].img == null)
                {
                    game.effects[i].img = setCanvas(bulletImage);
                    Canvas.SetZIndex(game.effects[i].img, 5);
                }

                switch (game.effects[i].type)
                {
                    case 1:
                        game.effects[i].img.Source = muzzle[game.effects[i].count];
                        Canvas.SetLeft(game.effects[i].img, game.effects[i].posX - muzzle[game.effects[i].count].PixelWidth / 2);
                        Canvas.SetTop(game.effects[i].img, game.effects[i].posY - muzzle[game.effects[i].count].PixelHeight);
                        game.effects[i].img.RenderTransform = new RotateTransform() { CenterX = muzzle[game.effects[i].count].PixelWidth / 2, CenterY = muzzle[game.effects[i].count].PixelHeight, Angle = game.effects[i].angle * 180 / Math.PI };
                        break;
                    case 2:
                        game.effects[i].img.Source = bigMuzzle[game.effects[i].count];
                        Canvas.SetLeft(game.effects[i].img, game.effects[i].posX - bigMuzzle[game.effects[i].count].PixelWidth / 2);
                        Canvas.SetTop(game.effects[i].img, game.effects[i].posY - bigMuzzle[game.effects[i].count].PixelHeight);
                        game.effects[i].img.RenderTransform = new RotateTransform() { CenterX = bigMuzzle[game.effects[i].count].PixelWidth / 2, CenterY = bigMuzzle[game.effects[i].count].PixelHeight, Angle = game.effects[i].angle * 180 / Math.PI };
                        break;
                    case 3:
                        game.effects[i].img.Source = smallExplode[game.effects[i].count];
                        Canvas.SetLeft(game.effects[i].img, game.effects[i].posX - smallExplode[game.effects[i].count].PixelWidth / 2);
                        Canvas.SetTop(game.effects[i].img, game.effects[i].posY - smallExplode[game.effects[i].count].PixelHeight /2);
                        game.effects[i].img.RenderTransform = new RotateTransform() { CenterX = smallExplode[game.effects[i].count].PixelWidth / 2, CenterY = smallExplode[game.effects[i].count].PixelHeight / 2, Angle = game.effects[i].angle * 180 / Math.PI };
                        break;
                    case 4:
                        game.effects[i].img.Source = bigExplode[game.effects[i].count];
                        Canvas.SetLeft(game.effects[i].img, game.effects[i].posX - bigExplode[game.effects[i].count].PixelWidth / 2);
                        Canvas.SetTop(game.effects[i].img, game.effects[i].posY - bigExplode[game.effects[i].count].PixelHeight/2);
                        game.effects[i].img.RenderTransform = new RotateTransform() { CenterX = bigExplode[game.effects[i].count].PixelWidth / 2, CenterY = bigExplode[game.effects[i].count].PixelHeight /2, Angle = game.effects[i].angle * 180 / Math.PI };
                        break;
                    case 5:
                        game.effects[i].img.Source = smallImpact[game.effects[i].count];
                        Canvas.SetLeft(game.effects[i].img, game.effects[i].posX - smallImpact[game.effects[i].count].PixelWidth / 2);
                        Canvas.SetTop(game.effects[i].img, game.effects[i].posY - smallImpact[game.effects[i].count].PixelHeight /2);
                        game.effects[i].img.RenderTransform = new RotateTransform() { CenterX = smallImpact[game.effects[i].count].PixelWidth / 2, CenterY = smallImpact[game.effects[i].count].PixelHeight /2, Angle = game.effects[i].angle * 180 / Math.PI };
                        break;
                    case 6:
                        game.effects[i].img.Source = bigImpact[game.effects[i].count];
                        Canvas.SetLeft(game.effects[i].img, game.effects[i].posX - bigImpact[game.effects[i].count].PixelWidth / 2);
                        Canvas.SetTop(game.effects[i].img, game.effects[i].posY - bigImpact[game.effects[i].count].PixelHeight / 2);
                        game.effects[i].img.RenderTransform = new RotateTransform() { CenterX = bigImpact[game.effects[i].count].PixelWidth / 2, CenterY = bigImpact[game.effects[i].count].PixelHeight / 2, Angle = game.effects[i].angle * 180 / Math.PI };
                        break;
                }
            }

            game.update();
            //tk.turret.img.RenderTransform = new RotateTransform() { Angle = 0 };

            game.player.turret.aim();

        }

        private void game_KeyDown(CoreWindow sender, KeyEventArgs args)
        {   
            switch (args.VirtualKey)
            {
                case VirtualKey.A:
                    game.player.act = Act.left;
                    break;
                case VirtualKey.D:
                    game.player.act = Act.right;
                    break;
                case VirtualKey.W:
                    game.player.act = Act.up;
                    break;
                case VirtualKey.S:
                    game.player.act = Act.down;
                    break;
                case VirtualKey.Space:
                    if (stopflag)
                    {
                        timer.Start();
                        stopflag = false;
                    }
                    else
                    {
                        timer.Stop();
                        stopflag = true;
                    }                
                    break;
            }
        }

        private void game_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            game.player.act = Act.stop;
        }

        private void nigger(object sender, PointerRoutedEventArgs e)
        {
            game.player.turret.targetAngle = Math.Atan2(e.GetCurrentPoint(canvas1).Position.X - game.player.posX, game.player.posY - e.GetCurrentPoint(canvas1).Position.Y);
        }

        private void missinglink(object sender, PointerRoutedEventArgs e)
        {
            game.player.turret.fire();

            

        }
    }
}
