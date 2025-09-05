namespace EasySteering {
    public static class WheelInputManager
    {
        public static float throtle;
        public static float brake;
        public static float clutch;
        public static float steer;
        public static int steerRot = 900;
        public static int minGear = 0;
        public static int maxGear = 8;
        public static int currentGear;

        public static bool handBrake;

        private static bool upShiftBeingPressed;
        private static bool downShiftBeingPressed;
        private static LogitechGSDK.DIJOYSTATE2ENGINES rec;
        public static void Tick() {
            steerUpdate();
        }

        private static void steerUpdate() {

            if(LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0)) {
                rec = LogitechGSDK.LogiGetStateUnity(0);

                GearChange();
                Throttle();
                Brake();
                Clutch();
                Steering();

                //Handbrake
                if(rec.rgbButtons[0] == 128) {
                    handBrake = true;
                } else {
                    handBrake = false;
                }           
            }
        }

        private static void GearChange() {
            //Gear Up
            if(rec.rgbButtons[4] == 128 && !upShiftBeingPressed) {
                upShiftBeingPressed = true;
                if(currentGear == maxGear) {
                    return;
                } else {
                    currentGear++;
                }
            } else if(rec.rgbButtons[4] == 128 && upShiftBeingPressed) {
                return;
            }else {
                upShiftBeingPressed = false;
            }

            //Gear Down
            if(rec.rgbButtons[5] == 128 && !downShiftBeingPressed) {
                downShiftBeingPressed = true;
                if(currentGear == minGear) {
                    return;
                } else {
                    currentGear--;
                }
            } else if(rec.rgbButtons[5] == 128 && downShiftBeingPressed) {
                return;
            }else {
                downShiftBeingPressed = false;
            }
        }

        private static void Throttle() {
            //throttle
            int rawThrottle = rec.lY;
            throtle = (((float)rawThrottle / -32768f) + 1) / 2;
        }

        private static void Brake() {
            //brake 
            int rawBrake = rec.lRz;
            brake = (((float)rawBrake / -32768f) + 1) / 2;
        }

        private static void Clutch() {
            //clutch
            int rawClutch = rec.rglSlider[0];
            clutch = (((float)rawClutch / -32768f) + 1) / 2;
        }

        private static void Steering() {
            //steer
            int rawSteer = rec.lX;
            steer = ((((float)rawSteer / -32768f) * -1) * 900) / steerRot;
        }
    }
}

