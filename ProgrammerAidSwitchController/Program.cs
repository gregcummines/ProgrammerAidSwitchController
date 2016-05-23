using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication2
{
    enum Aid
    {
        LeftAid1,
        LeftAid2,
        RightAid1,
        RightAid2
    }

    public class Program
    {
        static OutputPort outA0;
        static OutputPort outA1;
        static OutputPort outA2;
        static OutputPort outA3;
        static OutputPort outA4;
        static OutputPort outA5;
        static OutputPort outD0;
        static OutputPort outD1;

        static InputPort inD2;
        static InputPort inD3;
        static InputPort inD4;
        static InputPort inD5;
        static InputPort inD6;
        static InputPort inD7;
        static InputPort inD8;


        static bool firstPress1 = true;
        static bool firstPress2 = true;
        static bool firstPress3 = true;
        static bool firstPress4 = true;
        static bool firstPress5 = true;
        static bool firstPress6 = true;
        static bool firstPress7 = true;

        static bool b1 = true;
        static bool b2 = true;
        static bool b3 = true;
        static bool b4 = true;
        static bool b5 = true;
        static bool b6 = true;
        static bool b7 = true;

        static bool buttonPressed1;
        static bool buttonPressed2;
        static bool buttonPressed3;
        static bool buttonPressed4;
        static bool buttonPressed5;
        static bool buttonPressed6;
        static bool buttonPressed7;

        public static void Main()
        {
            outA0 = new OutputPort(Pins.GPIO_PIN_A0, true);
            outA1 = new OutputPort(Pins.GPIO_PIN_A1, true);
            outA2 = new OutputPort(Pins.GPIO_PIN_A2, true);
            outA3 = new OutputPort(Pins.GPIO_PIN_A3, true);
            outA4 = new OutputPort(Pins.GPIO_PIN_A4, true);
            outA5 = new OutputPort(Pins.GPIO_PIN_A5, true);
            outD0 = new OutputPort(Pins.GPIO_PIN_D0, true);
            outD1 = new OutputPort(Pins.GPIO_PIN_D1, true);

            inD2 = new InputPort(Pins.GPIO_PIN_D2, false, Port.ResistorMode.PullUp);
            inD3 = new InputPort(Pins.GPIO_PIN_D3, false, Port.ResistorMode.PullUp);
            inD4 = new InputPort(Pins.GPIO_PIN_D4, false, Port.ResistorMode.PullUp);
            inD5 = new InputPort(Pins.GPIO_PIN_D5, false, Port.ResistorMode.PullUp);
            inD6 = new InputPort(Pins.GPIO_PIN_D6, false, Port.ResistorMode.PullUp);
            inD7 = new InputPort(Pins.GPIO_PIN_D7, false, Port.ResistorMode.PullUp);
            inD8 = new InputPort(Pins.GPIO_PIN_D8, false, Port.ResistorMode.PullUp);

            while (true)
            {
                // Continue watching for button presses and process
                // any changes we need
                ProcessButtonForProgrammer1();
                ProcessButtonForProgrammer2();
                ProcessButtonForAllProgrammersOff();
                ProcessButtonForLeftAid1();
                ProcessButtonForRightAid1();
                ProcessButtonForLeftAid2();
                ProcessButtonForRightAid2();
            }
        }

        private static bool AreAnyProgrammersOn()
        {
            return b1 || b2;
        }

        private static void ClearProgrammerState(bool programmer1 = true, bool programmer2 = true)
        {
            outA2.Write(true);

            if (programmer1)
            {
                b1 = true;
                outA0.Write(true);
            }
            if (programmer2)
            {
                b2 = true;
                outA1.Write(true);
            }
        }

        private static void ClearAidState(Aid aidTogglingOn)
        {
            if (aidTogglingOn == Aid.LeftAid1)
            {
                //b4 = true;
                //outA3.Write(true);
            }
            if (aidTogglingOn == Aid.LeftAid2)
            {
                //b5 = true;
                //outA4.Write(true);
            }

            if (aidTogglingOn == Aid.RightAid1)
            {
                //b6 = true;
                //outA5.Write(true);
            }

            if (aidTogglingOn == Aid.RightAid2)
            {
                //b7 = true;
                //outD0.Write(true);
            }
        }

        private static void ProcessButtonForProgrammer1()
        {
            buttonPressed1 = !inD2.Read();

            if (buttonPressed1 && firstPress1)
            {
                ClearProgrammerState(false, true);

                firstPress1 = false;
                b1 = !b1;
                outA0.Write(b1);
                Thread.Sleep(200);
            }
            else if (!buttonPressed1)
            {
                firstPress1 = true;
            }
        }

        private static void ProcessButtonForProgrammer2()
        {
            buttonPressed2 = !inD3.Read();

            if (buttonPressed2 && firstPress2)
            {
                //ClearAidState();
                ClearProgrammerState(true, false);

                firstPress2 = false;
                b2 = !b2;
                outA1.Write(b2);
                Thread.Sleep(200);
            }
            else if (!buttonPressed2)
            {
                firstPress2 = true;
            }
        }

        private static void ProcessButtonForAllProgrammersOff()
        {
            buttonPressed3 = !inD4.Read();

            if (buttonPressed3)
            {
                ClearProgrammerState(true, true);
                outA2.Write(false);
            }
        }

        private static void ProcessButtonForLeftAid1()
        {
            buttonPressed4 = !inD5.Read();

            if (buttonPressed4 && firstPress4)
            {
                // If there are any programmers on, clear all aids except the other side
                if (AreAnyProgrammersOn())
                {
                    // If this aid is being turned on
                    if (b4)
                    {
                        ClearAidState(Aid.LeftAid1);
                    }
                }

                firstPress4 = false;
                b4 = !b4;
                outA3.Write(b4);
                Thread.Sleep(200);
            }
            else if (!buttonPressed4)
            {
                firstPress4 = true;
            }
        }

        private static void ProcessButtonForLeftAid2()
        {
            buttonPressed5 = !inD6.Read();

            if (buttonPressed5 && firstPress5)
            {
                // If there are any programmers on, clear all aids except the other side
                if (AreAnyProgrammersOn())
                {
                    if (b5)
                    {
                        ClearAidState(Aid.LeftAid2);
                    }
                }

                firstPress5 = false;
                b5 = !b5;
                outA4.Write(b5);
                Thread.Sleep(200);
            }
            else if (!buttonPressed5)
            {
                firstPress5 = true;
            }
        }

        private static void ProcessButtonForRightAid1()
        {
            buttonPressed6 = !inD7.Read();

            if (buttonPressed6 && firstPress6)
            {
                // If there are any programmers on, clear all aids except the other side
                if (AreAnyProgrammersOn())
                {
                    if (b6)
                    {
                        ClearAidState(Aid.RightAid1);
                    }
                }

                firstPress6 = false;
                b6 = !b6;
                outA5.Write(b6);
                Thread.Sleep(200);
            }
            else if (!buttonPressed6)
            {
                firstPress6 = true;
            }
        }

        private static void ProcessButtonForRightAid2()
        {
            buttonPressed7 = !inD8.Read();

            if (buttonPressed7 && firstPress7)
            {
                // If there are any programmers on, clear all aids except the other side
                if (AreAnyProgrammersOn())
                {
                    if (b7)
                    {
                        ClearAidState(Aid.RightAid2);
                    }
                }

                firstPress7 = false;
                b7 = !b7;
                outD0.Write(b7);
                Thread.Sleep(200);
            }
            else if (!buttonPressed7)
            {
                firstPress7 = true;
            }
        }
    }
}
