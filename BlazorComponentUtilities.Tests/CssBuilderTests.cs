using FluentAssertions;
using System;
using Xunit;

namespace BlazorComponentUtilities.Tests
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
        [Fact]
        public void ShouldBulidConditionalCssBuilderClasses()
        {
            //arrange
            var hasTwo = false;
            var hasThree = true;
            Func<bool> hasFive = () => false;

            //act
            var ClassToRender = new CssBuilder("item-one")
                            .AddClass("item-two", when: hasTwo)
                            .AddClass(new CssBuilder("item-three")
                                            .AddClass("item-foo", false)
                                            .AddClass("item-sub-three"), 
                                            when: hasThree)
                            .AddClass("item-four")
                            .AddClass("item-five", when: hasFive)
                            .Build();
            //assert
            ClassToRender.Should().Be("item-one item-three item-sub-three item-four");
        }
        [Fact]
        public void ShouldBulidEmptyClasses()
        {
            //arrange
            var shouldShow = false;

            //act
            var ClassToRender = new CssBuilder()
                            .AddClass("some-class", shouldShow)
                            .Build();
            //assert
            ClassToRender.Should().Be(string.Empty);
        }
    }
}
