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

        public string MediaError { get; private set; }

        public static readonly string [ ] supportMediaFormat =
        {
            ".FLAC",
             ".MP3"
        };

        public TimeSpan Duration
        {
            get
            {
                if ( this.mediaPlayer == null )
                {
                    return TimeSpan.Zero;
                }
                else
                {
                    return this.mediaPlayer.NaturalDuration.TimeSpan;
                }
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


        public Media_Player()
        {
            this.mediaPlayer = new MediaPlayer ( );

            this.mediaPlayer.MediaOpened += this.MediaPlayer_MediaOpened;
            this.mediaPlayer.MediaFailed += this.MediaPlayer_MediaFailed;
        }

        private void MediaPlayer_MediaFailed( object sender , ExceptionEventArgs e )
        {
            this.MediaError = e.ErrorException.Message;
            this.MediaIsPlay = false;
        }

        private void MediaPlayer_MediaOpened( object sender , EventArgs e )
        {
            this.MediaIsPlay = true;
        }

        public void Play( string media_file_path )
        {
            this.media_file_path = media_file_path;

            this.mediaPlayer.Open ( new Uri ( this.media_file_path ) );

            this.mediaPlayer.Play ( );


            this.MediaIsPlay = true;
        }

        public void Stop()
        {
            if ( this.MediaIsPlay )
            {
                this.Close ( );
                this.mediaPlayer = new MediaPlayer ( );
            }
        }


        public void Close()
        {
            this.mediaPlayer.Close ( );
        }

    }
}
