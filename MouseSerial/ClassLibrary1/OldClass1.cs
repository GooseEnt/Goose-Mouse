using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class OldClass1
    {
        public class Serial
        {
            public static void begin(int rate)
            {
            }

            public static void println(string s)
            {
            }
            public static void println(float s)
            {
            }

            public static void print(string s)
            {
            }
            public static void print(float s)
            {
            }


        }

        public int analogRead(int i)
        {
            return a[i];
        }
        public int digitalRead(int i)
        {
            return sw;
        }

        public static void delay(int i)
        {
        }


        public static float atan2(float a, float b)
        {
            return (float)Math.Atan2(a, b);
        }
        public static float max(float a, float b)
        {
            return (float)Math.Max(a, b);
        }
        public static float cos(float a)
        {
            return (float)Math.Cos(a);
        }
        public static float sin(float a)
        {
            return (float)Math.Sin(a);
        }
        public static float round(float a)
        {
            return (float)Math.Round(a);
        }
        public static float abs(float a)
        {
            return (float)Math.Abs(a);
        }
        public static float sqrt(float a)
        {
            return (float)Math.Sqrt(a);
        }


        public int[] a;
        public int sw;
        public int HIGH = 1;









        public float Xcen = 0;
        public float Ycen = 0;
        public float Zval = 0;
        float Xmax = 360;
        float Ymax = 400;
        float Zmax = 100;
        float M1 = 0;
        float M2 = 0;
        float M3 = 0;
        float M4 = 0;
        float M5 = 0;
        float Offset1 = -316.99f;
        float Offset2 = -315.00f;
        float Offset3 = -315.11f;
        float Offset4 = -313.00f;
        float Offset5 = -315.00f;
        float MouseTiltEst = 0;
        float AngleToMag = 0;
        float AngleToNull = 0;
        float RadiusToNull = 0;
        public float XZoneA = 0;
        public float YZoneA = 0;
        public float XZoneB = 0;
        public float YZoneB = 0;
        public float WeightedX = 0;
        public float WeightedY = 0;
        public float ZoneAMixFraction = 0;
        public float ZoneBMixFraction = 0; // value 0-1 for confidence in X&Y Zone B values
        public float OverallConf = 0;
        float BestGuessAbsX = 0;
        float BestGuessAbsY = 0;
        public float ZoneAAbsOffsetX = 0;
        public float ZoneAAbsOffsetY = 0;
        public float ZoneBAbsOffsetX = 0;
        public float ZoneBAbsOffsetY = 0;
        public float ZoneAAbsX = 0;
        public float ZoneAAbsY = 0;
        public float ZoneBAbsX = 0;
        public float ZoneBAbsY = 0;
        public float InterpolatedRadius = 0;
        int PrintCount = 0;

        public void loop()
        {
            // put your main code here, to run repeatedly:
            M1 = 0;
            M2 = 0;
            M3 = 0;
            M4 = 0;
            M5 = 0;
            for (int k = 0; k < 100; k++)
            {
                M1 += analogRead(7);
                M2 += analogRead(6);
                M3 += analogRead(4);
                M4 += analogRead(8);
                M5 += analogRead(5);
            }
            if (digitalRead(0) == HIGH)
            {
                Offset1 = -M1 / 100;
                Offset2 = -M2 / 100;
                Offset3 = -M3 / 100;
                Offset4 = -M4 / 100;
                Offset5 = -M5 / 100;
            }
            M1 = -(M1 / 100 + Offset1);
            M2 = -(M2 / 100 + Offset2);
            M3 = -(M3 / 100 + Offset3);

            M4 = -(M4 / 100 + Offset4);
            M5 = -(M5 / 100 + Offset5);

            Zval = M4 / Zmax;
            Ycen = (M2 - M3) / Ymax;
            Xcen = (M5 - M1) / Xmax;
            RotateXY(MouseTiltEst);
            ZoneAB();
            EstimateConfidenceValues();
            CalculateWeightedAvg();

            PrintCount++;
            if (PrintCount == 100)
            {
                PrintCount = 0;
                Serial.println("nerd");
                Serial.println(M1);
                Serial.println(M2);
                Serial.println(M3);
                Serial.println(M4);
                Serial.println(M5);
                Serial.print("Xcen:");
                Serial.println(Xcen);
                Serial.print("Ycen:");
                Serial.println(Ycen);
                Serial.print("Zval:");
                Serial.println(Zval);
                Serial.print("AngleToMag:");
                Serial.println(AngleToMag);
                Serial.println(Offset1);
            }
            delay(10);

        }

        void ZoneAB()
        {
            XZoneA = .4f * Xcen;
            YZoneA = .4f * Ycen;
            if (Zval < 0)
            {
                XZoneA = -XZoneA;
                YZoneA = -YZoneA;
            }
            float RadiusPythagorean = sqrt(XZoneA * XZoneA + YZoneA * YZoneA);
            float RadiusEstZ = .5f * (1 - abs(Zval));
            if (RadiusEstZ < 0)
            {
                RadiusEstZ = 0;
            }
            if (RadiusEstZ > .5f)
            {
                RadiusEstZ = .5f;
            }
            float RadiusWeight = (1 - abs(Zval)) * 2;
            if (RadiusWeight > 1)
            {
                RadiusWeight = 1;
            }
            InterpolatedRadius = RadiusWeight * RadiusEstZ + (1 - RadiusWeight) * RadiusPythagorean;
            float ScaleFactor = InterpolatedRadius / RadiusPythagorean;
            XZoneA = XZoneA * ScaleFactor;
            YZoneA = YZoneA * ScaleFactor;
            ZoneAAbsOffsetX = round(BestGuessAbsX - XZoneA);
            ZoneAAbsOffsetY = round(BestGuessAbsY - YZoneA);
            ZoneAAbsX = XZoneA + ZoneAAbsOffsetX;
            ZoneAAbsY = YZoneA + ZoneAAbsOffsetY;
            //  AngleToMag = atan2 (Ypos, Xpos);
            XZoneB = .4f * Ycen;
            YZoneB = .4f * Xcen;



            bool FlipAngleOfField = false;
            ZoneBAbsOffsetX = round(BestGuessAbsX + .5f) - .5f;
            ZoneBAbsOffsetY = round(BestGuessAbsY + .5f) - .5f;
            float XPlusPointFive = ZoneBAbsOffsetX + 0.5f;
            float YPlusPointFive = ZoneBAbsOffsetY + 0.5f;
            float Sum = round(YPlusPointFive + XPlusPointFive);
            int IntegerSum = (int)Sum;
            if (IntegerSum % 2 == 0)
            { FlipAngleOfField = true; }
            else
            { FlipAngleOfField = false; }


            float RadiusToNull = sqrt(XZoneB * XZoneB + YZoneB * YZoneB);
            float AngleOfField = atan2(Ycen, Xcen);
            if (FlipAngleOfField)
            {
                AngleOfField += 3.14159f;
            }
            float AngleOfPos = 3.14159f / 2f - AngleOfField;
            XZoneB = RadiusToNull * cos(AngleOfPos);
            YZoneB = RadiusToNull * sin(AngleOfPos);
            ZoneBAbsX = XZoneB + ZoneBAbsOffsetX;
            ZoneBAbsY = YZoneB + ZoneBAbsOffsetY;




        }


        void RotateXY(float angle)
        {

        }

        void EstimateConfidenceValues()
        {
            float ZoneBConfidence = 1 - max(abs(Xcen), abs(Ycen));
            if (abs(Zval) > .9)
            {
                ZoneBMixFraction = 0;
            }
            if (abs(Zval) <= .9f && abs(Zval) > .1f)
            {
                ZoneBMixFraction = (.9f - abs(Zval)) / .8f;
            }
            if (abs(Zval) <= .1)
            {
                ZoneBMixFraction = 1;
            }
            ZoneAMixFraction = 1 - ZoneBMixFraction;
            OverallConf = (abs(Zval) - .1f);
            if (abs(Zval) > .1)
            {
                OverallConf = (abs(Zval) - .1f) + .1f * ZoneBConfidence;
            }
            else
            {
                OverallConf = .1f * ZoneBConfidence;
            }
        }
        void CalculateWeightedAvg()
        {
            float ratio = 10000;
            float WeightA = ZoneAMixFraction;
            float WeightB = ZoneBMixFraction;

            WeightB = 1 - WeightA;
            WeightedX = WeightA * ZoneAAbsX + WeightB * ZoneBAbsX;
            WeightedY = WeightA * ZoneAAbsY + WeightB * ZoneBAbsY;
            BestGuessAbsX = WeightedX;
            BestGuessAbsY = WeightedY;
        }

    }
}
