using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData
{
    public class Box
    {
		private const string InvalidSideMessage = "{0} cannot be zero or negative.";

        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public double Length
		{
			get 
			{
				return length; 
			}

			private set 
			{
				ValidateSide(value, nameof(Length));

				length = value; 
			}
		}

		public double Width
		{
			get 
			{
				return width; 
			}
			private set 
			{
				ValidateSide(value, nameof(Width));

				width = value; 
			}
		}
		public double Height
		{
			get 
			{
				return height; 
			}
			private set
			{
				ValidateSide(value, nameof(Height));

				height = value;
			}
		}

		public double SurfaceArea()
		{
			return 2 * (Length * Width + Length * Height + Width * Height);
		}
		public double LateralSurfaceArea()
		{
			return 2 * (Length * Height + Width * Height);
		}
		public double Volume()
		{
			return Length * Width * Height;
		}

        private void ValidateSide(double value, string side)
		{
			if (value <= 0)
			{
				throw new ArgumentException(string.Format(InvalidSideMessage, side));
			}
		}
	}
}
