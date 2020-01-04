using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BlazorComponentUtilities.Tests
{
    public class StyleBuilderTests
    {
        [Fact]
        public void ShouldBulidConditionalInlineStyles()
        {
            //arrange
            var hasBorder = true;
            var isOnTop = false;
            var top = 2;
            var bottom = 10;
            var left = 4;
            var right = 20;

            //act
            var ClassToRender = new StyleBuilder("background-color", "DodgerBlue")
                            .AddStyle("border-width", $"{top}px {right}px {bottom}px {left}px", when: hasBorder)
                            .AddStyle("z-index", "999", when: isOnTop)
                            .AddStyle("z-index", "-1", when: !isOnTop)
                            .AddStyle("padding", "35px")
                            .Build();
            //assert
            ClassToRender.Should().Be("background-color:DodgerBlue;border-width:2px 20px 10px 4px;z-index:-1;padding:35px;");
        }

        [Fact]
        public void ShouldBulidConditionalInlineStylesFromAttributes()
        {

            //arrange
            var hasBorder = true;
            var isOnTop = false;
            var top = 2;
            var bottom = 10;
            var left = 4;
            var right = 20;

            //act
            var StyleToRender = new StyleBuilder("background-color", "DodgerBlue")
                            .AddStyle("border-width", $"{top}px {right}px {bottom}px {left}px", when: hasBorder)
                            .AddStyle("z-index", "999", when: isOnTop)
                            .AddStyle("z-index", "-1", when: !isOnTop)
                            .AddStyle("padding", "35px")
                            .Build();

            IReadOnlyDictionary<string, object> attributes = new Dictionary<string, object> { { "style", StyleToRender } };

            var ClassToRender = new StyleBuilder().AddStyleFromAttributes(attributes).Build();
            //assert
            ClassToRender.Should().Be("background-color:DodgerBlue;border-width:2px 20px 10px 4px;z-index:-1;padding:35px;");
        }

    }
}
