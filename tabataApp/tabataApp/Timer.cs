﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace tabataApp
{
    /// <summary>
    /// Model for the MainPage Timer
    /// A Timer is able to keep track of number of rounds, number of seconds per round, prep time and cool down 
    /// as provided or load default values on start up (if not provided).
    /// 
    /// A Timer can also calculate the total time the current selections would take and provide a countdown 
    /// </summary>
    class Timer
    {
    
        private int rounds; // number of working rounds

        public int Rounds
        {
            get { return rounds; }
            set { rounds = value; }
        }
        private int roundDuration; // length of working rounds

        public int RoundDuration
        {
            get { return roundDuration; }
            set { roundDuration = value; }
        }
        private int prepTime; // time before the round begins

        public int PrepTime
        {
            get { return prepTime; }
            set { prepTime = value; }
        }
        private int rest; // length of rest duration between work rounds

        public int Rest
        {
            get { return rest; }
            set { rest = value; }
        }
        private int coolDown; // optional value the user can set to create a break between workouts

        public int CoolDown
        {
            get { return coolDown; }
            set { coolDown = value; }
        }

        /// <summary>
        /// Zero-Parameter Constructor which will load with tabata default values
        /// </summary>
        public Timer()
        {
            rounds = 8;
            roundDuration = 20;
            prepTime = 10;
            rest = 10;
            coolDown = 30;
        }

        /// <summary>
        /// Timer Constructor to create a specific number of rounds, work length, prep time, rest duration and a cool down between workouts
        /// </summary>
        /// <param name="_rounds"></param>
        /// <param name="work"></param>
        /// <param name="_prep"></param>
        /// <param name="_rest"></param>
        /// <param name="_coolDown"></param>
        public Timer(int _rounds, int work, int _prep, int _rest, int _coolDown)
        {
            rounds = _rounds;
            roundDuration = work;
            prepTime = _prep;
            rest = _rest;
            coolDown = _coolDown;
        }

    }
}
