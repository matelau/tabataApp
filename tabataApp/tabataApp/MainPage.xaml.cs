﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace tabataApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Timer timerObject; 
        private Windows.UI.Xaml.DispatcherTimer timer;
        int stateOfTimer; // the timer has 4 states, prep, work, rest, and cooldown
        int currentRound;
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        public void Start_timer()
        {

            //initialize timer
            initializeTimer();
            // Keep track of states
            stateOfTimer = 0;  
            timer = new DispatcherTimer();
            // set tick events
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(00,0,1);
            bool enabled = timer.IsEnabled;
            timer.Start();
           // timerProgress.IsEnabled = false;
          
        }

        /// <summary>
        /// Helper Method that creates a timer object from the users input
        /// Cooldown is defaulted to 60 seconds
        /// </summary>
        private void initializeTimer()
        {

            // still need to add more robust error checking and also make other textboxes not be able to be manipulated 
            int _rounds; int work; int _prep; int _rest; int _coolDown;

            if(!(int.TryParse(roundCount.Text, out _rounds)))
            { throw new ArgumentException();}
            if (!(int.TryParse(durationCount.Text, out work)))
            { throw new ArgumentException(); }
            if (!(int.TryParse(prepCount.Text, out _prep)))
            { throw new ArgumentException(); }
            if (!(int.TryParse(restCount.Text, out _rest)))
            { throw new ArgumentException(); }
            _coolDown = 60;

            timerObject = new Timer ( _rounds, work,  _prep, _rest, _coolDown);
            currentRound = 0;

        }


        /// <summary>
        /// Updates Timer UI based on Tick Events 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, object e)
        {
             int time;
             int timeSoFar;
            
             
            
             
            switch(stateOfTimer)
            {  
            
                // prep case      
                case 0:
                    state.Text = "Prep";
                    
                    
                    time = timerObject.PrepTime;
                    int.TryParse(counter.Text, out timeSoFar);
                    //int progress = timeSoFar / time;
                    //timerProgress. = progress; 
                    if (timeSoFar != time)
                     {
                            timeSoFar += 1;
                            counter.Text = "" + timeSoFar;
                           
                     }
                    else
                    {counter.Text = "00"; stateOfTimer++;}
                    break;
            
                //Work Case      
                case 1:
                    time = timerObject.RoundDuration;
                   
                    if (currentRound < timerObject.Rounds)
                    {
                        state.Text = "Work";
                        timerProgress.IsActive = true;
                        int.TryParse(counter.Text, out timeSoFar);
                        if (timeSoFar != time)
                        {
                            timeSoFar += 1;
                            counter.Text = "" + timeSoFar;
                        }
                        else
                        { currentRound++; counter.Text = "00"; stateOfTimer++; }
                    }
                    else
                    { stateOfTimer = 3; }
                    break;
            
                //Rest Case
                case 2:
                    state.Text = "Rest";
                    time = timerObject.Rest;
                    timerProgress.IsActive = false;
                    int.TryParse(counter.Text, out timeSoFar);
                    if (timeSoFar != time)
                     {
                            timeSoFar += 1;
                            counter.Text = "" + timeSoFar;
                     }
                    else
                    {counter.Text = "00"; stateOfTimer--;}
                    break;
            //final case Cool down
                case 3:
                    timerProgress.IsActive = false;
                    state.Text = "Cool Down";
                    time = timerObject.CoolDown;
                    int.TryParse(counter.Text, out timeSoFar);
                    if (timeSoFar != time)
                    {
                        timeSoFar += 1;
                        counter.Text = "" + timeSoFar;
                    }
                    else
                    { counter.Text = "00"; timer.Stop(); reset(); }
                    break;

            }
            

        }
        /// <summary>
        /// Starts and Stops the Timing Countdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {

            if (startButton.Content.Equals("Start"))
            {
                startButton.Content = "Stop";
                Start_timer();
            }
            else
            {
                startButton.Content = "Start";
                if (timer != null)
                { timer.Stop(); }
                reset();

            }
          
        }

        /// <summary>
        /// Helper Method to restore default Values to Text Boxes
        /// </summary>
        private void reset()
        {
            counter.Text = "00";
            roundCount.Text = "8";
            durationCount.Text = "20";
            prepCount.Text = "10";
            restCount.Text = "10";

            startButton.Content = "Start";
        }


    }
 
}
