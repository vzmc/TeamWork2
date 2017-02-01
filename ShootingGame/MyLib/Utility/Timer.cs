using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Utility
{
    public class Timer
    {
        //フィールド
        private int timer;  //現在時間
        public int NowTime
        {
            get
            {
                return timer;
            }
            set
            {
                timer = value;
            }
        }
        private int limitTime;     //制限時間

        public Timer()
        {
            limitTime = 60;
            Initialize();
        }

        public Timer(int setTime)
        {
            limitTime = setTime;
            Initialize();
        }

        public void Initialize()
        {
            timer = limitTime;
        }

        public void Update()
        {
            timer--;
        }

        public bool IsTime()
        {
            return timer <= 0;
        }

        public float Rate()
        {
            return Math.Min(1.0f, (float)timer / limitTime);
        }

        public void ChangeTime(int limitTime)
        {
            this.limitTime = limitTime;
            Initialize();
        }
    }
}
