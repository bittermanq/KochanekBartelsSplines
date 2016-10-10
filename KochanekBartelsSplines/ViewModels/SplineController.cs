using System;
using KochanekBartelsSplines.ViewModels.Interfaces;

namespace KochanekBartelsSplines.ViewModels
{
    public class SplineController : ISplineController
    {
        private Action RedrawCallback { get; }


        private double _tension;
        public double Tension
        {
            get { return _tension; }
            set
            {
                _tension = value;
                RedrawCallback();
            }
        }

        private double _continuity;
        public double Continuity
        {
            get { return _continuity; }
            set
            {
                _continuity = value;
                RedrawCallback();
            }
        }

        private double _bias;
        public double Bias
        {
            get { return _bias; }
            set
            {
                _bias = value;
                RedrawCallback();
            }
        }

        private int _segments;
        public int Segments
        {
            get { return _segments; }
            set
            {
                _segments = value;
                RedrawCallback();
            }
        }


        public SplineController(Action redrawCallback)
        {
            RedrawCallback = redrawCallback;
        }
    }
}