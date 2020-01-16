using System;
using System.Collections.Generic;

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
        public static StyleBuilder Default(string prop, string value) => new StyleBuilder(prop, value);

        /// <summary>
        /// Creates an empty StyleBuilder used to define conditional CSS Style used in a component.
        /// Call Build() to return the completed CSS Style as a string. 
        /// </summary>
        public static StyleBuilder Empty() => new StyleBuilder();

        /// <summary>
        /// Creates a StyleBuilder used to define conditional CSS Style used in a component.
        /// Call Build() to return the completed CSS Style as a string. 
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        public StyleBuilder(string prop, string value) => stringBuffer = stringBuffer = $"{prop}:{value};";

        /// <summary>
        /// Adds a raw string to the builder that will be concatenated with the next style or value added to the builder.
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
        /// Adds a style to the builder with separator and closing semicolon.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">CSS Style to add</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value) => AddValue($"{prop}:{value};");

        /// <summary>
        /// Adds a style to the builder with separator and closing semicolon.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">CSS Style to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value, bool when = true) => when ? this.AddStyle(prop, value) : this;

        /// <summary>
        /// Adds a style to the builder with separator and closing semicolon.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value">CSS Style to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(string prop, string value, Func<bool> when = null) => this.AddStyle(prop, value, when());

        /// <summary>
        /// Adds a conditional nested StyleBuilder to the builder with separator and closing semicolon.
        /// </summary>
        /// <param name="builder">CSS Style Builder to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Style is added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(StyleBuilder builder, bool when = true) => when ? this.AddValue(builder.Build()) : this;

        /// <summary>
        /// Adds a style to the builder with separator and closing semicolon.
        /// </summary>
        /// <param name="builder">CSS Style Builder to conditionally add.</param>
        /// <param name="when">Condition in which the CSS Styles are added.</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyle(StyleBuilder builder, Func<bool> when = null) => this.AddStyle(builder, when());

        /// <summary>
        /// Adds a conditional in-line style when it exists in a dictionary to the builder with separator.
        /// Null safe operation.
        /// </summary>
        /// <param name="additionalAttributes">Additional Attribute splat parameters</param>
        /// <returns>StyleBuilder</returns>
        public StyleBuilder AddStyleFromAttributes(IReadOnlyDictionary<string, object> additionalAttributes) =>
            additionalAttributes == null ? this :
                !additionalAttributes.ContainsKey("style") ? this :
                    this.AddValue(additionalAttributes["style"].ToString());

        /// <summary>
        /// Finalize the completed Style as a string.
        /// </summary>
        /// <returns>string</returns>
        public string Build()
        {
            // String buffer finalization code
            return stringBuffer != null ? stringBuffer.Trim() : string.Empty;
        }

        // ToString should only and always call Build to finalize the rendered string.
        public override string ToString() => Build();
    }
}
