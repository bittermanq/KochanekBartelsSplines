using System;
using System.Drawing;

namespace KochanekBartelsSplines.TestApp.Models
{
    public class AnchorPoint
    {
        private readonly Action _updateBitmapChannel;
        
        public Point Position { get; set; }
        public bool IsActive { get; set; }

        private double _tension;
        public double Tension
        {
            get { return _tension; }
            set
            {
                _tension = value;
                _updateBitmapChannel();
            }
        }
        
        private double _continuity;
        public double Continuity
        {
            get { return _continuity; }
            set
            {
                _continuity = value;
                _updateBitmapChannel();
            }
        }
        
        private double _bias;
        public double Bias
        {
            get { return _bias; }
            set
            {
                _bias = value;
                _updateBitmapChannel();
            }
        }

        public AnchorPoint(Point position, bool isActive, Action updateBitmapChannel)
        {
            _updateBitmapChannel = updateBitmapChannel;

            Position = position;
            IsActive = isActive;
        }
    }
}