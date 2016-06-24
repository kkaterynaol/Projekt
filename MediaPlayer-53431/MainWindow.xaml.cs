using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace MediaPlayer.Audio_and_Video

{
    public partial class AudioVideoPlayerCompleteSample : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public AudioVideoPlayerCompleteSample()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Timer, ktory liczy czas odtwarzania pliku multimedialnego
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        /// <summary>
        /// Sprawdza, czy moze otworzyc okno i czy nie ma bledow
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Otwiera okno dla pobierania pliku multimedialnego
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                mePlayer.Source = new Uri(openFileDialog.FileName);
        }

        /// <summary>
        /// Metoda, ktora sprawdza, czy okno playera nie jest puste i czy zostal wczytany plik
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        /// <summary>
        /// Metoda, za pomoca ktorej player zaczyna odtwarzac plik
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        /// <summary>
        /// Metoda sprawdza, czy plik jest nagrywany i czy mozna go wstrzymac
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        /// <summary>
        /// Metoda wstrzyma odtwarzanie pliku
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        /// <summary>
        /// Metoda sprawdza, czy plik moze byc zatrzymany
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        /// <summary>
        /// Metoda zatrzymuje odtwarzanie pliku
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        /// <summary>
        /// Metoda steruje dzialaniem suwaka, zeby zmieniac czas i ogladac lub sluchac plik z dowolnego miejsca
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        /// <summary>
        /// Metoda steruje dzialaniem suwaka, zeby zmieniac czas i ogladac lub sluchac plik z dowolnego miejsca
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        /// <summary>
        /// Metoda steruje dzialaniem suwaka, zeby zmieniac czas i ogladac lub sluchac plik z dowolnego miejsca
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Metoda zmienia glosnosc odtwarzania
        /// </summary>
        /// <permission>Private</permission>
        /// <param name="sender">default value</param>
        /// <param name="e">default value</param>
        /// <returns>Void</returns>
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }


    }
}