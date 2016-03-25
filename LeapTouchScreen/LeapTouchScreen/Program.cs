using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Leap;

namespace LeapTouchScreen
{
    public enum State
    {
        Sleeping,
        Calibrating,
        Tracking,
    }

    public class Program
    {

        public static State state;
        static Controller controller;
        static Timer timer;

        static Frame frame;
        static Hand hand;
        static FingerList pointer;
        static Vector pointerPosition;
        static Vector handPosition;
        static Vector cursorPosition;
        static float touchX;
        static float touchY;

        static Vector lowerL;
        static Vector upperL;
        static Vector upperR;
        static Vector lowerR;

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Init()
        {
            controller = new Controller();
            controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
            MessageBox.Show("Calibration initizalized. Lower L: 1, Upper L: 2, Upper R: 3, Lower R: 4");
            state = State.Calibrating;
            initTimer();
        }

        // Code to initialize timer
        private static void initTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 1;
            timer.Start();
        }

        // Handles when timer hits
        private static void timer_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.Sleeping:
                    // nothing
                    break;
                case State.Calibrating:
                    Calibrate();
                    break;
                case State.Tracking:
                    Track();
                    break;
            }
        }


        public static void Calibrate_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            int key = (int)e.KeyChar;
            if (state == State.Calibrating)
            {
                switch (key)
                {
                    // number 1
                    case 49 :
                        Calibrate();
                        if (pointerPosition.x == 0 && pointerPosition.y == 0 && pointerPosition.z == 0)
                        {
                            lowerL = pointerPosition;
                            Console.WriteLine("lowerL: " + lowerL);
                            MessageBox.Show("Cant see dat hand!");
                        } else {
                            lowerL = pointerPosition;
                            Console.WriteLine("lowerL: " + lowerL);
                        }
                        break;
                    // number 2
                    case 50:
                        Calibrate();
                        if (pointerPosition.x == 0 && pointerPosition.y == 0 && pointerPosition.z == 0)
                        {
                            MessageBox.Show("Cant see dat hand!");
                        } else {
                            upperL = pointerPosition;
                            Console.WriteLine("upperL: " + upperL);
                        }
                        
                        break;
                    // number 3
                    case 51:
                        Calibrate();
                        if (pointerPosition.x == 0 && pointerPosition.y == 0 && pointerPosition.z == 0)
                        {
                            MessageBox.Show("Cant see dat hand!");
                        } else {
                            upperR = pointerPosition;
                            Console.WriteLine("upperR: " + upperR);
                        }
                        break;
                    // number 4
                    case 52:
                        Calibrate();
                        if (pointerPosition.x == 0 && pointerPosition.y == 0 && pointerPosition.z == 0)
                        {
                            MessageBox.Show("Cant see dat hand!");
                        } else {
                            lowerR = pointerPosition;
                            Console.WriteLine("lowerR: " + lowerR);
                        }
                        break;
                    // space key!
                    case 32:
                        MessageBox.Show("Done!");
                        state = State.Tracking;
                        break;
                    default:
                        MessageBox.Show("open ur eyes bruh");
                        break;
                }
            } else {
                MessageBox.Show("Why tf you pressin?");
            }
        }

        private static void Calibrate()
        {
            frame = controller.Frame();
            hand = frame.Hands.Rightmost;
            pointer = hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX);
            pointerPosition = pointer[0].Bone(Bone.BoneType.TYPE_DISTAL).Center;
        }

        // Handles leap tracking data
        private static void Track()
        {
            frame = controller.Frame();
            hand = frame.Hands.Rightmost;
            pointer = hand.Fingers.FingerType(Finger.FingerType.TYPE_INDEX);
            pointerPosition = pointer[0].Bone(Bone.BoneType.TYPE_DISTAL).Center;
            handPosition = hand.PalmPosition;

            // UNDONE do the math that finds the proper cursor position. sounds like ass buddy
            touchX = (pointerPosition.x + 230.0f) * (4.0f) + 1920.0f;
            touchY = 1080.0f - ((pointerPosition.y - 140) * (4.153846153f));

            if (hand.TimeVisible > 0)
            {
                WindowsHandler.SetCursorPos((int)touchX, (int)touchY);
            }

            // TODO Add detection for gestures
        }
    }
}