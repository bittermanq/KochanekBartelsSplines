using System;
using KochanekBartelsSplines.TestApp.Helpers.Interfaces;
using KochanekBartelsSplines.TestApp.Models;

namespace KochanekBartelsSplines.TestApp.Helpers
{
    public class SplineSettingsController : DrawableBase, ISplineSettingsController
    {
        private double _tension;
        public double Tension
        {
            get { return _tension; }
            set
            {
                _tension = value;
                Redraw();
            }
        }

        private double _continuity;
        public double Continuity
        {
            get { return _continuity; }
            set
            {
                _continuity = value;
                Redraw();
            }
        }

        private double _bias;
        public double Bias
        {
            get { return _bias; }
            set
            {
                _bias = value;
                Redraw();
            }
        }

        private int _segments;
        public int Segments
        {
            get { return _segments; }
            set
            {
                _segments = value;
                Redraw();
            }
        }


        public SplineSettingsController(Action redrawCallback)
        {
            RedrawCallback = redrawCallback;
        }
    }
}