﻿using System.Diagnostics;

namespace LibNoise.Operator
{
    /// <summary>
    /// Provides a noise module that caches the last output value generated by a source
    /// module. [OPERATOR]
    /// </summary>
    public class Cache : ModuleBase
    {
        #region Fields

        private double _value;
        private bool _cached;
        private double _x;
        private double _y;
        private double _z;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Cache.
        /// </summary>
        public Cache()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Cache.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Cache(ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
        }

        #endregion

        #region ModuleBase Members

        /// <summary>
        /// Gets or sets a source module by index.
        /// </summary>
        /// <param name="index">The index of the source module to aquire.</param>
        /// <returns>The requested source module.</returns>
        public override ModuleBase this[int index]
        {
            get { return base[index]; }
            set
            {
                base[index] = value;
                _cached = false;
            }
        }

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(Modules[0] != null);
            if (!(_cached && _x == x && _y == y && _z == z))
            {
                _value = Modules[0].GetValue(x, y, z);
                _x = x;
                _y = y;
                _z = z;
            }
            _cached = true;
            return _value;
        }

        #endregion
    }
}