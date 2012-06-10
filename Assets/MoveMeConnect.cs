using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using UnityEngine;

static class MoveMeConnect
    {
        public static PSMoveClientThreadedRead moveClient;
        //public static mouseMoveWindow form;
        public static Thread updateGuiThread;
        public static bool _updateGuiThreadQuit;

        public static bool client_connected;
        public static bool client_paused;
        public static int selected_move;
        public static int selected_nav;
        public static uint update_delay;
        public static bool image_paused;
        public static uint image_update_delay;
        public static bool sliced_image_paused;
        public static uint sliced_image_update_delay;

        public static bool[] reset_enabled;

        private static void updateGui()
        {
            while (_updateGuiThreadQuit == false)
            {
                try
                {
                    //form.Invoke(form.updateGuiDelegate);
                    Thread.Sleep(Convert.ToInt32(update_delay));
                }
                catch
                {
                    return;
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            client_connected = false;
            client_paused = false;
            selected_move = 0;
            selected_nav = 0;
            update_delay = 16;
            reset_enabled = new bool[PSMoveSharpState.PSMoveSharpNumMoveControllers];
            image_paused = true;
            image_update_delay = 32;
            sliced_image_paused = true;
            sliced_image_update_delay = 32;
            for (int i = 0; i < PSMoveSharpState.PSMoveSharpNumMoveControllers; i++)
            {
                reset_enabled[i] = true;
            }

            _updateGuiThreadQuit = false;
            updateGuiThread = new Thread(new ThreadStart(updateGui));

            //System.Windows.Forms.Application.EnableVisualStyles();
            //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //form = new mouseMoveWindow();
            updateGuiThread.Start();
            //System.Windows.Forms.Application.Run(form);

            _updateGuiThreadQuit = true;
            updateGuiThread.Join();

            client_disconnect();
        }

        public static void client_connect(String server_address, int server_port)
        {
            moveClient = new PSMoveClientThreadedRead();

            try
            {
                moveClient.Connect(Dns.GetHostAddresses(server_address)[0].ToString(), server_port);
                moveClient.StartThread();
            }
            catch
            {
                return;
            }

            client_connected = true;

            //Properties.Settings.Default.most_recent_server = server_address;
            //Properties.Settings.Default.Save();
        }

        public static void client_disconnect()
        {
            try
            {
                client_paused = false;

                moveClient.StopThread();
                moveClient.Close();
            }
            catch
            {
                return;
            }

            client_connected = false;
        }

        public static PSMoveSharpState getState()
        {
            return moveClient.GetLatestState();
        }

        public static void calibrate(int gemNum)
        {
            moveClient.SendRequestPacket(PSMoveClientDelegate.ClientRequest.PSMoveClientRequestCalibrateController, Convert.ToUInt32(gemNum));
        }
		/*
        public static void setColorAndTrack(int gemNum, Color color)
        {
            
            float r = color.r;
            float g = color.g;
            float b = color.b;
            //moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestTrackHues);
            //moveClient.SendRequestPacket(PSMoveClient.ClientRequest.PSMoveClientRequestTrackHues, Convert.ToUInt32(getHueFromColor(color)));
        }
		
        wiki-
        public static int getHueFromColor(Color color)
        {
            float r, g, b;
            int h = 0;
            r = color.r;
            g = color.g;
            b = color.b;
            if (r >= g && g >= b)
            {
                h = (int)(60 * GetFraction(r, g, b));
            }
            else if (g > r && r >= b)
            {
                h = (int)(60 * (2 - GetFraction(g, r, b)));
            }
            else if (g >= b && b > r)
            {
                h = (int)(60 * (2 + GetFraction(g, b, r)));
            }
            else if (b > g && g > r)
            {
                h = (int)(60 * (4 - GetFraction(b, g, r)));
            }
            else if (b > r && r >= g)
            {
                h = (int)(60 * (4 + GetFraction(b, r, g)));
            }
            else if (r >= b && b > g)
            {
                h = (int)(60 * (6 - GetFraction(r, b, g)));
            }
            return h;
        }

        private static float GetFraction(float h, float m, float l)
        {
            if (h == l)
            {
                return 0;
            }
            return (m - l) / (h - l);
        }
        -wiki */

    }
