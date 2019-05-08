using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1.Media_Player
{
    class Media_Player
    {
        private MediaPlayer mediaPlayer;
        private string media_file_path;
        public bool MediaIsPlay { get; private set; }

        public static readonly string [ ] supportMediaFormat =
        {
            ".FLAC",
             ".MP3"
        };


        public Media_Player()
        {
            this.mediaPlayer = new MediaPlayer ( );

            this.mediaPlayer.MediaOpened += this.MediaPlayer_MediaOpened;
        }

        private void MediaPlayer_MediaOpened( object sender , EventArgs e )
        {
            this.MediaIsPlay = true;
        }

        public void Play( string media_file_path )
        {
            this.media_file_path = media_file_path;

            this.mediaPlayer.Open ( new Uri( this.media_file_path ));

            this.mediaPlayer.Play ( );
        }

        public void Close()
        {
            this.mediaPlayer.Close ( );
        }

    }
}
