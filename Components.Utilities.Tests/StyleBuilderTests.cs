using FluentAssertions;
using System;
using Xunit;

namespace Components.Utilities.Tests
{
    public class StyleBuilderTests
    {
        [Fact]
        public void ShouldBulidConditionalCssStyles()
        {
            //arrange
            var hasBorder = true;
            var isOnTop = false;

            //act
            var top = 2;
            var bottom = 10;
            var left = 4;
            var right = 20;
            var ClassToRender = new StyleBuilder("background-color", "DodgerBlue")
                            .AddStyle("border-width", $"{top}px {right}px {bottom}px {left}px", when: hasBorder)
                            .AddStyle("z-index", "999", when: isOnTop)
                            .AddStyle("z-index", "-1", when: !isOnTop)
                            .AddStyle("padding", "35px")
                            .Build();
            //assert
            ClassToRender.Should().Be("background-color:DodgerBlue;border-width:2px 20px 10px 4px;z-index:-1;padding:35px;");
        }

    }
}
