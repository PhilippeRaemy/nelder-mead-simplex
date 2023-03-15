using System;

namespace NumUtils.Common
{
    using System.Globalization;

    public class Vector
    {
        public Vector(int dimensions)
        {
            Components = new double[dimensions];
            NDimensions = dimensions;
        }

        public int NDimensions { get; }

        public double this[int index]
        {
            get => Components[index];
            set => Components[index] = value;
        }

        public double[] Components { get; }

        /// <summary>
        /// Add another vector to this one
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector Add(Vector v)
        {
            if (v.NDimensions != NDimensions)
                throw new ArgumentException("Can only add vectors of the same dimensionality");

            var vector = new Vector(v.NDimensions);
            for (var i = 0; i < v.NDimensions; i++) 
                vector[i] = this[i] + v[i];
            return vector;
        }

        /// <summary>
        /// Subtract another vector from this one
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector Subtract(Vector v)
        {
            if (v.NDimensions != NDimensions)
                throw new ArgumentException("Can only subtract vectors of the same dimensionality");

            var vector = new Vector(v.NDimensions);
            for (var i = 0; i < v.NDimensions; i++) 
                vector[i] = this[i] - v[i];
            return vector;
        }

        /// <summary>
        /// Multiply this vector by a scalar value
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public Vector Multiply(double scalar)
        {
            var scaledVector = new Vector(NDimensions);
            for (var i = 0; i < NDimensions; i++) 
                scaledVector[i] = this[i] * scalar;
            return scaledVector;
        }

        /// <summary>
        /// Compute the dot product of this vector and the given vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double DotProduct(Vector v)
        {
            if (v.NDimensions != NDimensions)
                throw new ArgumentException("Can only compute dot product for vectors of the same dimensionality");

            double sum = 0;
            for (var i = 0; i < v.NDimensions; i++) 
                sum += this[i] * v[i];
            return sum;
        }

        public override string ToString()
        {
            var components = new string[Components.Length];
            for (var i = 0; i < components.Length; i++) 
                components[i] = Components[i].ToString(CultureInfo.InvariantCulture);
            return "[ " + string.Join(", ", components) + " ]";
        }
    }
}
