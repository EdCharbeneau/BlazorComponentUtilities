using FluentAssertions;
using System;
using Xunit;

namespace Components.Utilities.Tests
{
    public class CssBuilderTests
    {
        [Fact]
        public void ShouldBulidConditionalCssClasses()
        {
            //arrange
            var hasTwo = false;
            var hasThree = true;
            Func<bool> hasFive = () => false;

            //act
            var ClassToRender = new CssBuilder("item-one")
                            .AddClass("item-two", when: hasTwo)
                            .AddClass("item-three", when: hasThree)
                            .AddClass("item-four")
                            .AddClass("item-five", when: hasFive)
                            .Build();
            //assert
            ClassToRender.Should().Be("item-one item-three item-four");
        }
    }
}
