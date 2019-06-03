using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Threading;

namespace WpfApp1.Media_Player
{
    class Media_Player
    {
        private MediaPlayer mediaPlayer;
        private string media_file_path;

        public bool MediaIsPlay { get; private set; }

        public bool MediaIsPause { get; private set; }

        public static readonly string [ ] supportMediaFormat =
        {
            ".FLAC",
             ".MP3"
        };

        public TimeSpan Duration
        {
            get
            {
                TimeSpan span=TimeSpan.Zero;

                var player = this.mediaPlayer;
                if (player != null && player.NaturalDuration.TimeSpan != null)
                {
                    span = player.NaturalDuration.TimeSpan;
                }

                return span;
            }
        }

        public TimeSpan CurrentPosition
        {
            get
            {
                if(this.mediaPlayer==null)
                {
                    return TimeSpan.Zero;
                }

                else
                {
                    return this.mediaPlayer.Position;
                }
            }
        }


        public Media_Player(string path)
        {
            this.media_file_path = path;
            this.Media_Player_Initialization ( this.media_file_path );
            this.mediaPlayer.MediaOpened += this.MediaPlayer_MediaOpened;

        }

        private void MediaPlayer_MediaOpened( object sender , EventArgs e )
        {
            this.MediaIsPlay = true;
            this.MediaIsPause = false;
        }

        private void Media_Player_Initialization( string file_path )
        {
            this.mediaPlayer = new MediaPlayer ( );

            this.mediaPlayer.Open ( new Uri ( file_path ) );

            this.mediaPlayer.Play ( );

        }

        public void Play()
        {
            if( this.mediaPlayer != null)
            {
                this.mediaPlayer.Play ( );
                this.MediaIsPause = false;
            }
        }

        public void Stop()
        {
            if ( this.mediaPlayer != null)
            {
                this.MediaIsPlay = false;
                this.Close ( );
            }
        }

        public void Pause()
        {
            if ( this.mediaPlayer != null && this.MediaIsPlay )
            {
                this.mediaPlayer.Pause ( );
                this.MediaIsPause = true;
            }
        }

        private void Close()
        {
            if ( this.mediaPlayer != null)
            {
                this.mediaPlayer.Close ( );
                this.mediaPlayer = null;
            }
        }

    }
}
