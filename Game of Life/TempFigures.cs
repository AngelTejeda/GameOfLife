namespace Game_of_Life
{
    public static class TempFigures
    {
        public static int[,] GetBoat()
        {
            return new int[,]
                {
                    { 1, 1, 0 },
                    { 1, 0, 1 },
                    { 0, 1, 0 }
                };
        }

        public static int[,] GetShip()
        {
            return new int[,]
            {
                    { 0, 1, 0 },
                    { 0, 0, 1 },
                    { 1, 1, 1 }
            };

        }

        public static int[,] GetBlinker()
        {
            return new int[,]
            {
                    { 1, 1, 1}
            };
        }

        public static int[,] GetAircraft()
        {
            return new int[,]
            {
                    { 1, 1, 0, 0 },
                    { 1, 0, 0, 1 },
                    { 0, 0, 0, 1 },
                    { 0, 0, 1, 1 }
            };
        }

        public static int[,] GetPython()
        {
            return new int[,]
            {
                    { 0, 0, 0, 1, 1 },
                    { 1, 0, 1, 0, 1 },
                    { 1, 1, 0, 0, 0 }
            };
        }

        public static int[,] GetEye()
        {
            return new int[,]
            {
                    { 0, 0, 1, 0, 0 },
                    { 0, 1, 0, 1, 0 },
                    { 1, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 1 },
                    { 0, 1, 0, 1, 0 },
                    { 0, 0, 1, 0, 0 }
            };
        }

        public static int[,] GetFlower()
        {
            return new int[,]
            {
                    { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                    { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                    { 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0 },
            };
        }
    }
}