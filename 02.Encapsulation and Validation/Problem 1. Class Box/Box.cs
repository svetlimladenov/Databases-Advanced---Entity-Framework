using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_1.Class_Box
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length,double width,double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double GetSurfaceArea()
        {
            return (2 * this.Length * this.Width) + (2 * this.length * this.height) + (2 * this.width * this.height);
        }

        public double GetLateralSurfaceArea()
        {
            return 2 * this.Length * this.Height + 2 * this.Width * this.Height;
        }

        public double GetVolume()
        {
            return this.Width * this.Height * this.Length;
        }
        public double Height
        {
            get
            {
                if (this.height <= 0)
                {
                    throw new ArgumentException($"{nameof(Height)} cannot be zero or negative. ");
                }
                return this.height;
            }
            private set { this.height = value; }
        }

        public double Width
        {
            get
            {
                if (this.width <= 0)
                {
                    throw new ArgumentException($"{nameof(Width)} cannot be zero or negative. ");
                }
                return this.width;
            }
            private set { this.width = value; }
        }

        public double Length
        {
            get
            {
                if (this.length <= 0)
                {
                    throw new ArgumentException($"{nameof(Length)} cannot be zero or negative. ");
                }
                return this.length;
            }
            private set { this.length = value; }
        }

    }
}
