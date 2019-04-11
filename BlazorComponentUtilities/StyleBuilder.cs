using System;

namespace BlazorComponentUtilities
{
    public struct StyleBuilder
    {
        private string stringBuffer;

        /// <summary>
        /// Creates a StyleBuilder used to define conditional CSS Style used in a component.
        /// Call Build() to return the completed CSS Style as a string. 
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        public StyleBuilder(string prop, string value) => stringBuffer = stringBuffer = $"{prop}:{value};";

        /// <summary>
        /// Adds a raw string to the builder that will be concatenated with the next class or value added to the builder.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns>StyleBuilder</returns>
        private StyleBuilder AddValue(string style)
        {
            stringBuffer += style;
            return this;
        }

        /// <summary>
        /// Adds a CSS Style to the builder with space separator.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">CSS Style to add</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value) => AddValue($"{prop}:{value};");

        /// <summary>
        /// Adds a conditional CSS Style to the builder with space separator.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">CSS Style to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value, bool when = true) => when ? this.AddStyle(prop, value) : this;

        /// <summary>
        /// Adds a conditional CSS Style to the builder with space separator.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">CSS Style to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value, Func<bool> when = null) => this.AddStyle(prop, value, when());

        /// <summary>
        /// Adds a conditional CSS Style to the builder with space separator.
        /// </summary>
        /// <param name="builder">CSS Style Builder to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(StyleBuilder builder, bool when = true) => when ? this.AddValue(builder.Build()) : this;

        /// <summary>
        /// Adds a conditional CSS Style to the builder with space separator.
        /// </summary>
        /// <param name="builder">CSS Style Builder to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Styles are added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(StyleBuilder builder, Func<bool> when = null) => this.AddStyle(builder, when());

        /// <summary>
        /// Finalize the completed CSS Style as a string.
        /// </summary>
        /// <returns>string</returns>
        public string Build()
        {
            // String buffer finalization code
            return stringBuffer.Trim();
        }

        // ToString should only and always call Build to finalize the rendered string.
        public override string ToString() => Build();
    }
}
