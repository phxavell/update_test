﻿using System;
using System.Windows.Forms;
using MaterialSkin;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace AUDIO
{
    public partial class OUVIRMICROFONE : MaterialSkin.Controls.MaterialForm
    {
        public string AUDIO1;
        public string AUDIO2;

        public OUVIRMICROFONE()
        {
            InitializeComponent();
            Interacao1();
            TimeStart1();
        }

        public void Interacao1()
        {
            //https://www.naturalreaders.com/online/ - Cria vozes
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = @"C:\TESTES_AVELL\audiofilesAlertas\AudioGravado.mp3";
            wplayer.controls.play();
        }

        public void TimeStart1()
        {
            if (AUDIO1 != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 2;

                relogio.Tick += delegate {
                    tempo -= 1;
                    //lblTime.Text = tempo.ToString();-Não precisa visualizar isso.
                    if (tempo == 0)
                    {
                        relogio.Stop();

                        AUDIO1 = "OK";

                        //Ouvir o áudio gravado no LoopBack
                        //https://www.naturalreaders.com/online/ - Cria vozes
                        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                        wplayer.URL = @"C:\TESTES_AVELL\recordfiles\Jigulina.wav";
                        wplayer.controls.play();

                        TimeStart2();
                    }
                };
                relogio.Start();
            }
        }


        public void TimeStart2()
        {
            if (AUDIO2 != "OK")
            {
                Timer relogio = new Timer();
                relogio.Interval = 1000;
                int tempo = 20;

                relogio.Tick += delegate {
                    tempo -= 1;
                    lblTime.Text = tempo.ToString();
                    if (tempo == 0)
                    {
                        relogio.Stop();
                        //Chamar o form de confirmação

                        AUDIO2 = "OK";

                        VALIDACONFIRMA2 formValidaOK02 = new VALIDACONFIRMA2();
                        this.Hide();
                        formValidaOK02.ShowDialog();
                    }
                };
                relogio.Start();
            }
        }
    }
}
