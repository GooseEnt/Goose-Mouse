using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1
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

        public int analogRead (int i)
        {
            if (i > 9)
            {
                return 0;
            }
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
            return (float) Math.Atan2(a, b);
        }
        public static float max(float a, float b)
        {
            return (float)Math.Max(a, b);
        }
        public static float min(float a, float b)
        {
            return (float)Math.Min(a, b);
        }
        public static float cos(float a)
        {
            return (float)Math.Cos (a);
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
        public static float sign(float a)
        {
            return (float)Math.Sign (a);
        }
        public static float sqrt(float a)
        {
            return (float)Math.Sqrt(a);
        }


        public int [] a;
        public int sw;
        public int HIGH = 1;









        public float Xcen = 0;
        public float Ycen = 0;
        public float Zval = 0;
        float Xmax = 220;
        float Ymax = 200;
        float Zmax = 300;
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
        float MouseTiltEstRadians = 0;
        float AngleToMag = 0;
        float AngleToNull = 0;
        float RadiusToNull = 0;
        public float XZoneA = 0;
        public float YZoneA = 0;
        public float XZoneB = 0;
        public float YZoneB = 0;
     public   float WeightedX = 0;
     public   float WeightedY = 0;
   public     float ZoneAMixFraction = 0;
  public      float ZoneBMixFraction = 0; // value 0-1 for confidence in X&Y Zone B values
     public   float OverallConf = 0;
        float BestGuessAbsX = 0;
        float BestGuessAbsY = 0;
     public   float ZoneAAbsOffsetX = 0;
        public float ZoneAAbsOffsetY = 0;
        public float ZoneBAbsOffsetX = 0;
        public float ZoneBAbsOffsetY = 0;
        public float ZoneAAbsX = 0;
        public float ZoneAAbsY = 0;
        public float ZoneBAbsX = 0;
        public float ZoneBAbsY = 0;
        public float InterpolatedRadius = 0;
        public float MouseX1 = 0;
        public float MouseY1 = 0;
        public float MouseX2 = 0;
        public float MouseY2 = 0;
        public float MouseX3 = 0;
        public float MouseY3 = 0;
        public float MouseX4 = 0;
        public float MouseY4 = 0;
        public float OverallConfidence1 = 0;
        public float OverallConfidence2 = 0;
        public float OverallConfidence3 = 0;
        public float OverallConfidence4 = 0;
        public float MidpointX12 = 0;
        public float MidpointY12 = 0;
        public float MidpointX34 = 0;
        public float MidpointY34 = 0;
        public float MouseOriginX = 0;
        public float MouseOriginY = 0;
        int PrintCount = 0;

        // temporary testing code, remove this later:
        public float ZoneAAbsX1 = 0;
        public float ZoneAAbsY1 = 0;
        public float ZoneBAbsX1 = 0;
        public float ZoneBAbsY1 = 0;

        public float ZoneAAbsX2 = 0;
        public float ZoneAAbsY2 = 0;
        public float ZoneBAbsX2 = 0;
        public float ZoneBAbsY2 = 0;

        public float ZoneAAbsX3 = 0;
        public float ZoneAAbsY3 = 0;
        public float ZoneBAbsX3 = 0;
        public float ZoneBAbsY3 = 0;

        public float ZoneAAbsX4 = 0;
        public float ZoneAAbsY4 = 0;
        public float ZoneBAbsX4 = 0;
        public float ZoneBAbsY4 = 0;

        public float xcenSensor1 = 0;
        public float ycenSensor1 = 0;
        public float zvalSensor1 = 0;
        public float xcenSensor2 = 0;
        public float ycenSensor2 = 0;
        public float zvalSensor2 = 0;

public        float zoneDconfidence = 0.0f;
public         float zoneDx = 0;
 public       float zoneDy = 0;



        // end of temporary tessting code


        int multiplexedanalogRead(int pinnum)
        {
            return (analogRead(pinnum));
        }


        public void loop()
        {
            BestGuessAbsX = MouseOriginX;
            BestGuessAbsY = MouseOriginY;
            SetXcenYcenZval(6, 8, 1, 7, 9);

            xcenSensor1 = Xcen;
            ycenSensor1 = Ycen;
            zvalSensor1 = Zval;

            ProcessSensorReading();
            MouseX1 = WeightedX;
            MouseY1 = WeightedY;
            OverallConfidence1 = OverallConf;

            ZoneAAbsX1 = ZoneAAbsX;  // test code remove this line
            ZoneAAbsY1 = ZoneAAbsY;  // test code remove this line
            ZoneBAbsX1 = ZoneBAbsX;  // test code remove this line
            ZoneBAbsY1 = ZoneBAbsY;  // test code remove this line


            BestGuessAbsX = MouseOriginX + .32f * sin(MouseTiltEstRadians);
            BestGuessAbsY = MouseOriginY + .32f * cos(MouseTiltEstRadians);
            SetXcenYcenZval(2, 8, 4, 3, 5);

            xcenSensor2 = Xcen;
            ycenSensor2 = Ycen;
            zvalSensor2 = Zval;

            ProcessSensorReading();
            MouseX2 = WeightedX;
            MouseY2 = WeightedY;
            OverallConfidence2 = OverallConf;

            ZoneAAbsX2 = ZoneAAbsX;  // test code remove this line
            ZoneAAbsY2 = ZoneAAbsY;  // test code remove this line
            ZoneBAbsX2 = ZoneBAbsX;  // test code remove this line
            ZoneBAbsY2 = ZoneBAbsY;  // test code remove this line


            float rawRatio = OverallConfidence1 / (OverallConfidence1 + OverallConfidence2);
            float ratio = 0;
            // temporary test code:
        if (rawRatio <= .25 )
            {
                ratio = 0.0f;
            }
        if (rawRatio > 0.25 && rawRatio <= 0.4)
            {
                ratio = (rawRatio - 0.25f) * 0.5f / 0.15f;
            }
        if (rawRatio > 0.4 && rawRatio < 0.6)
            {
                ratio = 0.5f;
            }
        if (rawRatio >= 0.6 && rawRatio < 0.75)
            {
                ratio = 0.5f + (rawRatio - 0.6f) * 0.5f / 0.15f;
            }
        if (rawRatio >= 0.75)
            {
                ratio = 1.0f;
            }
            float midPointZfield = 0.5f * (zvalSensor1 + zvalSensor2);
            float midpointXfield = 0.5f * (xcenSensor1 + xcenSensor2);
            float midpointYfield = 0.5f * (ycenSensor1 + ycenSensor2);
            zoneDx = 0;
            zoneDy = 0;
            if (abs(midPointZfield ) < .25) {
                if (abs(midpointXfield) > 4 * abs(midpointYfield))
                {
                    zoneDx = -midPointZfield * sign(midpointXfield);
                    zoneDy = 0.3f * (xcenSensor1 - xcenSensor2) * sign(midpointXfield);
                }

                if (abs(midpointYfield) > 4 * abs(midpointXfield)) {
                    zoneDy = -midPointZfield * sign(midpointYfield );
                    zoneDx = 0.3f * (ycenSensor1 - ycenSensor2) * sign(midpointYfield);
                }


            }

            // end temporary code

            MidpointX12 = ratio * (MouseX1 + .16f * sin(MouseTiltEstRadians)) + (1.0f - ratio) * (MouseX2 - .16f * sin(MouseTiltEstRadians));
            MidpointY12 = ratio * (MouseY1 + .16f * cos(MouseTiltEstRadians)) + (1.0f - ratio) * (MouseY2 - .16f * cos(MouseTiltEstRadians));
            MouseOriginX = MidpointX12 - .16f * sin(MouseTiltEstRadians);
            MouseOriginY = MidpointY12 - .16f * cos(MouseTiltEstRadians);




            return;


            BestGuessAbsX = MouseOriginX + .64f * sin(MouseTiltEstRadians);
            BestGuessAbsY = MouseOriginY + .64f * cos(MouseTiltEstRadians);
            SetXcenYcenZval(19, 4, 18, 17, 16);
            ProcessSensorReading();
            MouseX3 = WeightedX;
            MouseY3 = WeightedY;
            OverallConfidence3 = OverallConf;

            ZoneAAbsX3 = ZoneAAbsX;  // test code remove this line
            ZoneAAbsY3 = ZoneAAbsY;  // test code remove this line
            ZoneBAbsX3 = ZoneBAbsX;  // test code remove this line
            ZoneBAbsY3 = ZoneBAbsY;  // test code remove this line



            BestGuessAbsX = MouseOriginX + .96f * sin(MouseTiltEstRadians);
            BestGuessAbsY = MouseOriginY + .96f * cos(MouseTiltEstRadians);
            SetXcenYcenZval(15, 18, 14, 13, 12);
            ProcessSensorReading();
            MouseX4 = WeightedX;
            MouseY4 = WeightedY;
            OverallConfidence4 = OverallConf;

            ZoneAAbsX4 = ZoneAAbsX;  // test code remove this line
            ZoneAAbsY4 = ZoneAAbsY;  // test code remove this line
            ZoneBAbsX4 = ZoneBAbsX;  // test code remove this line
            ZoneBAbsY4 = ZoneBAbsY;  // test code remove this line


            ratio = OverallConfidence3 / (OverallConfidence3 + OverallConfidence4);
            MidpointX34 = ratio * (MouseX3 + .16f * sin(MouseTiltEstRadians)) + (1.0f - ratio) * (MouseX4 - .16f * sin(MouseTiltEstRadians));
            MidpointY34 = ratio * (MouseY3 + .16f * cos(MouseTiltEstRadians)) + (1.0f - ratio) * (MouseY4 - .16f * cos(MouseTiltEstRadians));

            MouseTiltEstRadians = atan2(MidpointY34 - MidpointY12, MidpointX34 - MidpointX12) - 3.14159f/2f;
        }



            void SetXcenYcenZval (int Sense1, int Sense2, int Sense3, int Sense4, int Sense5) { 
            M1 = 0;
            M2 = 0;
            M3 = 0;
            M4 = 0;
            M5 = 0;
            for (int k = 0; k < 10; k++)
            {
                M1 += multiplexedanalogRead(Sense1);
                M2 += multiplexedanalogRead(Sense2);
                M3 += multiplexedanalogRead(Sense3);
                M4 += multiplexedanalogRead(Sense4);
                M5 += multiplexedanalogRead(Sense5);
            }
            if (digitalRead(0) == HIGH)
            {
                Offset1 = -M1 / 10;
                Offset2 = -M2 / 10;
                Offset3 = -M3 / 10;
                Offset4 = -M4 / 10;
                Offset5 = -M5 / 10;
            }
            M1 = -(M1 / 10 + Offset1);
            M2 = -(M2 / 10 + Offset2);
            M3 = -(M3 / 10 + Offset3);

            M4 = -(M4 / 10 + Offset4);
            M5 = -(M5 / 10 + Offset5);

            Zval = M4 / Zmax;
            Ycen = (M2 - M3) / Ymax;
            Xcen = (M5 - M1) / Xmax;

        }

        

            void ProcessSensorReading () {

          
            RotateXY(MouseTiltEstRadians);
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
            XZoneA = .3f * Xcen;
            YZoneA = .3f * Ycen;
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
            ZoneAAbsOffsetX = round (BestGuessAbsX - XZoneA);
            ZoneAAbsOffsetY = round (BestGuessAbsY - YZoneA);
            ZoneAAbsX = XZoneA + ZoneAAbsOffsetX;
            ZoneAAbsY = YZoneA + ZoneAAbsOffsetY;
            //  AngleToMag = atan2 (Ypos, Xpos);
            XZoneB = .4f * Ycen;
            YZoneB = .4f * Xcen;
//temporary change:
            XZoneB = .5f * Ycen;
            YZoneB = .5f * Xcen;
            if (abs(Zval) > .25)
            {
        //        BestGuessAbsX = ZoneAAbsX;
          //      BestGuessAbsY = ZoneAAbsY;
            }
            

            
            // end of temporary change



            bool FlipAngleOfField = false;
            ZoneBAbsOffsetX = round(BestGuessAbsX + .5f) - .5f;
            ZoneBAbsOffsetY = round(BestGuessAbsY + .5f) - .5f;

            // test code:
            if (abs(Zval) > 0.1)
            {
                ZoneBAbsOffsetX = round(ZoneAAbsX + .5f) - .5f;
                ZoneBAbsOffsetY = round(ZoneAAbsY + .5f) - .5f;
            }
            // end test code

            float XPlusPointFive = ZoneBAbsOffsetX + 0.5f;
            float YPlusPointFive = ZoneBAbsOffsetY + 0.5f;
            float Sum = round (YPlusPointFive + XPlusPointFive);
            int IntegerSum = (int)Sum;
            if (IntegerSum % 2 == 0)
             {   FlipAngleOfField = true;}
            else
            { FlipAngleOfField = false; }


            float RadiusToNull = sqrt(XZoneB * XZoneB + YZoneB * YZoneB);
            float AngleOfField = atan2 (Ycen, Xcen);
            if (FlipAngleOfField)
            {
                AngleOfField += 3.14159f;
            }
            float AngleOfPos = 3.14159f / 2f - AngleOfField;
            XZoneB = RadiusToNull * cos(AngleOfPos);
            YZoneB = RadiusToNull * sin(AngleOfPos);

            // testing code:  (adjusts the ZoneB value near dead zone to use Z-field information)
            /*
            float fieldMag = sqrt(Xcen * Xcen + Ycen * Ycen);
            if (fieldMag > 0.5)
            {
                float weightByZ = (fieldMag - 0.5f) / .2f;
                if (weightByZ > 1.0) { weightByZ = 1.0f; }

                if (abs(Ycen) < 0.1f)
                {
                    float ZbasedX = -Zval * 1.0f * sign(Xcen);
                    float weightByZ2 = (0.1f - abs(Ycen)) / 0.05f;
                    if (weightByZ2 > 1.0) { weightByZ2 = 1.0f; }
                  XZoneB = weightByZ * weightByZ2 * ZbasedX + (1.0f - weightByZ * weightByZ2) * XZoneB;
                   
                }
                if (abs(Xcen) < 0.1f)
                {
                    float ZbasedY = -Zval * 1.0f * sign(Ycen);
                    float weightByZ2 = (0.1f - abs(Xcen)) / 0.05f;
                    if (weightByZ2 > 1.0) { weightByZ2 = 1.0f; }
                   YZoneB = weightByZ * weightByZ2 * ZbasedY + (1.0f - weightByZ * weightByZ2) * YZoneB;
                                    }

            }
            */
            // end testing code

            ZoneBAbsX = XZoneB + ZoneBAbsOffsetX;
            ZoneBAbsY = YZoneB + ZoneBAbsOffsetY;




        }


        void RotateXY(float angle)
        {

        }

        void EstimateConfidenceValues()
        {
            float ZoneBConfidence = 1 - max(abs(Xcen), abs(Ycen));
            if (abs(Zval) > .6)
            {
                ZoneBMixFraction = 0;
            }
            if (abs(Zval) <= .6f && abs(Zval) > .2f)
            {
                ZoneBMixFraction = (.6f - abs(Zval)) /.4f;
            }
            if (abs(Zval) <= .2)
            {
                ZoneBMixFraction = 1;
            }

            // test code:
            float maxXY = max(abs(Xcen), abs(Ycen));

            if (maxXY > 0.4f) {
                float factor = (0.6f - maxXY) / 0.2f;
                if (factor < 0.0f) { factor = 0.0f; }
                ZoneBMixFraction = ZoneBMixFraction * factor;
            }
            // end test

            ZoneAMixFraction = 1 - ZoneBMixFraction;
            OverallConf = (abs(Zval)-.1f);
            if (abs (Zval) > .1)
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
