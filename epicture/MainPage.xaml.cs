using System;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace epicture
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Favorites favorites = new Favorites();

        public MainPage()
        {
            this.InitializeComponent();
            PickAFileButton.Click += new RoutedEventHandler(PickAFileButton_Click);
            HomeButton.Click += new RoutedEventHandler(HomeButton_Click);

            foreach (var favorite in favorites.list)
            {
                Image disp = new Image();
                disp.Source = new BitmapImage(
                new Uri(favorite.image, UriKind.Absolute));
                disp.MaxWidth = 1000;
                disp.Tag = favorite.image;
                disp.DoubleTapped += new DoubleTappedEventHandler(UnfavPic_Click);
                ContentsPanel.Children.Add(disp);

                Button fav = new Button();
                fav.Content = "💔";
                fav.Tag = favorite.image;
                fav.Click += new RoutedEventHandler(UnfavButton_Click);
                ContentsPanel.Children.Add(fav);
            }
        }

        private async void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentsPanel.Children.Clear();

            foreach (var favorite in favorites.list)
            {
                Image disp = new Image();
                disp.Source = new BitmapImage(
                new Uri(favorite.image, UriKind.Absolute));
                disp.MaxWidth = 1000;
                disp.Tag = favorite.image;
                disp.DoubleTapped += new DoubleTappedEventHandler(UnfavPic_Click);
                ContentsPanel.Children.Add(disp);

                Button fav = new Button();
                fav.Content = "💔";
                fav.Tag = favorite.image;
                fav.Click += new RoutedEventHandler(UnfavButton_Click);
                ContentsPanel.Children.Add(fav);
            }
        }

            private async void PickAFileButton_Click(object sender, RoutedEventArgs e)
            {
                // Clear previous returned file name, if it exists, between iterations of this scenario
                OutputTextBlock.Text = "";

                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");
                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file == null)
                {
                    OutputTextBlock.Text = "Operation cancelled.";
                    return;
                }
                // Application now has read/write access to the picked file
                OutputTextBlock.Text = "Picked photo: " + file.Name;


                Stream stream = await file.OpenStreamForReadAsync();
                GetDescription(stream);
            }

            public async void GetDescription(Stream stream)
            {
                Analyze cognitive = new Analyze();
                var visionTask = cognitive.AnalyzePictureAsync(stream);
                await Task.WhenAll(visionTask);
                var vision = await visionTask;

                if (vision != null && vision.Tags != null && vision.Captions[0] != null)
                    Box.PlaceholderText = vision.Captions[0].Text;
            }
        
            void FavButton_Click(object sender, RoutedEventArgs e)
            {
                Button dynamicButton = sender as Button;
                favorites.Add(dynamicButton.Tag.ToString());
            }

        void FavPic_Click(object sender, RoutedEventArgs e)
        {
            Image dynamicImage = sender as Image;
            favorites.Add(dynamicImage.Tag.ToString());
        }

        void UnfavButton_Click(object sender, RoutedEventArgs e)
        {
            Button dynamicButton = sender as Button;
            favorites.Delete(dynamicButton.Tag.ToString());
            ContentsPanel.Children.Clear();

            foreach (var favorite in favorites.list)
            {
                Image disp = new Image();
                disp.Source = new BitmapImage(
                new Uri(favorite.image, UriKind.Absolute));
                disp.MaxWidth = 1000;
                disp.Tag = favorite.image;
                disp.DoubleTapped += new DoubleTappedEventHandler(UnfavPic_Click);
                ContentsPanel.Children.Add(disp);

                Button fav = new Button();
                fav.Content = "💔";
                fav.Tag = favorite.image;
                fav.Click += new RoutedEventHandler(UnfavButton_Click);
                ContentsPanel.Children.Add(fav);
            }
        }

        void UnfavPic_Click(object sender, RoutedEventArgs e)
        {
            Image dynamicImage = sender as Image;
            favorites.Delete(dynamicImage.Tag.ToString());
            ContentsPanel.Children.Clear();

            foreach (var favorite in favorites.list)
            {
                Image disp = new Image();
                disp.Source = new BitmapImage(
                new Uri(favorite.image, UriKind.Absolute));
                disp.MaxWidth = 1000;
                disp.Tag = favorite.image;
                disp.DoubleTapped += new DoubleTappedEventHandler(UnfavPic_Click);
                ContentsPanel.Children.Add(disp);

                Button fav = new Button();
                fav.Content = "💔";
                fav.Tag = favorite.image;
                fav.Click += new RoutedEventHandler(UnfavButton_Click);
                ContentsPanel.Children.Add(fav);
            }
        }


        private void Box_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
            {
                ContentsPanel.Children.Clear();
                Filter img = new Filter();

            if (String.IsNullOrEmpty(img.search = args.QueryText))
                return;

                String layout_ = ((ComboBoxItem)Layout.SelectedItem).Content.ToString();
                if (!String.Equals(layout_, "All"))
                    img.aspect = layout_;

                String size_ = ((ComboBoxItem)PicSize.SelectedItem).Content.ToString();
                if (!String.Equals(size_, "All"))
                    img.size = size_;

                String type_ = ((ComboBoxItem)DotType.SelectedItem).Content.ToString();
                if (!String.Equals(type_, "All"))
                    img.type = type_;

                if (LicenseBox.IsChecked == true)
                    img.isFree = "Public";

                img.nbResult = 21;
                if (!String.IsNullOrEmpty(PicNumber.Text) && PicNumber.Text.All(char.IsDigit))
                    img.nbResult = Convert.ToInt32(PicNumber.Text);

                Search bs = new Search(img);

                foreach (var pic in bs.picList)
                {
                Image disp = new Image();
                    disp.Source = new BitmapImage(
                    new Uri(pic, UriKind.Absolute));
                    disp.MaxWidth = 1000;
                disp.Tag = pic;
                disp.DoubleTapped += new DoubleTappedEventHandler(FavPic_Click);
                ContentsPanel.Children.Add(disp);

                    Button fav = new Button();
                    fav.Content = "❤️";
                    fav.Tag = pic;
                    fav.Click += new RoutedEventHandler(FavButton_Click);
                    ContentsPanel.Children.Add(fav);
            }

                //private async void UnfavButton_Click(object sender, RoutedEventArgs e)
                //{
                //}
            }

        }
}
