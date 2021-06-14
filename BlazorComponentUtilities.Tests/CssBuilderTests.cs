using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BlazorComponentUtilities.Tests
{
    public class CssBuilderTests
    {

        [Fact]
        public void ShouldConstructWithDefaultValue()
        {
            //arrange
            var ClassToRender = CssBuilder.Default("item-one").Build();

            //assert
            ClassToRender.Should().Be("item-one");
        }


        [Fact]
        public void ShouldConstructWithEmpty()
        {
            //arrange
            var ClassToRender = CssBuilder.Empty().NullIfEmpty();

            //assert
            ClassToRender.Should().BeNull();
        }

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

        [Fact]
        public void ShouldBuildClassesWithFunc()
        {
            {
                //arrange
                // Simulates Razor Components attribute splatting feature
                IReadOnlyDictionary<string, object> attributes = new Dictionary<string, object> { { "class", "my-custom-class-1" } };

                //act
                var ClassToRender = new CssBuilder("item-one")
                                .AddClass(()=> attributes["class"].ToString(), when: attributes.ContainsKey("class"))
                                .Build();
                //assert
                ClassToRender.Should().Be("item-one my-custom-class-1");
            }
        }

        [Fact]
        public void ShouldBuildClassesFromAttributes()
        {
            {
                //arrange
                // Simulates Razor Components attribute splatting feature
                IReadOnlyDictionary<string, object> attributes = new Dictionary<string, object> { { "class", "my-custom-class-1" } };

                //act
                var ClassToRender = new CssBuilder("item-one")
                                .AddClassFromAttributes(attributes)
                                .Build();
                //assert
                ClassToRender.Should().Be("item-one my-custom-class-1");
            }
        }

        [Fact]
        public void ShouldNotThrowWhenNullFor_BuildClassesFromAttributes()
        {
            {
                //arrange
                // Simulates Razor Components attribute splatting feature
                IReadOnlyDictionary<string, object> attributes = null;

                //act
                var ClassToRender = new CssBuilder("item-one")
                                .AddClassFromAttributes(attributes)
                                .Build();
                //assert
                ClassToRender.Should().Be("item-one");
            }
        }

        [Fact]
        public void ForceNullForWhitespace_BuildClassesFromAttributes()
        {
            {
                //arrange
                // Simulates Razor Components attribute splatting feature
                IReadOnlyDictionary<string, object> attributes = null;

                //act
                var ClassToRender = new CssBuilder()
                                .AddClassFromAttributes(attributes)
                                .NullIfEmpty();
                //assert
                ClassToRender.Should().BeNull();
            }
        }

        [Fact]
        public void ShouldNotThrowNoKeyExceptionWithDictionary()
        {
            {
                //arrange
                // Simulates Razor Components attribute splatting feature
                IReadOnlyDictionary<string, object> attributes = new Dictionary<string, object> { { "foo", "bar" } };

                //act
                var ClassToRender = new CssBuilder("item-one")
                                .AddClass(() => attributes["string"].ToString(), when: attributes.ContainsKey("class"))
                                .Build();
                //assert
                ClassToRender.Should().Be("item-one");
            }
        }

        [Fact]
        public void ShouldBulidConditionalCssClassesUsingPrefix()
        {
            //arrange
            var hasTwo = false;
            var hasThree = true;
            Func<bool> hasFive = () => false;

            //act
            var ClassToRender = new CssBuilder("default")
                            .SetPrefix("item-")
                            .AddClass("two", when: hasTwo)
                            .AddClass("three", when: hasThree)
                            .AddClass("four")
                            .AddClass("five", when: hasFive)
                            .Build();
            //assert
            ClassToRender.Should().Be("default item-three item-four");
        }

        [Fact]
        public void ShouldBulidConditionalCssClassesUsingMultiplePrefixes()
        {
            //arrange
            var hasTwo = true;
            var hasThree = true;
            Func<bool> hasFive = () => false;

            //act
            var ClassToRender = new CssBuilder("default")
                            .AddClass("no-prefix-two", when: hasTwo)
                            .SetPrefix("item-")
                            .AddClass("three", when: hasThree)
                            .SetPrefix("namespace-")
                            .AddClass("four")
                            .AddClass("five", when: hasFive)
                            .Build();
            //assert
            ClassToRender.Should().Be("default no-prefix-two item-three namespace-four");
        }

        [Fact]
        public void ShouldBuildClassesFromAttributesWithPrefix()
        {
            {
                //arrange
                // Simulates Razor Components attribute splatting feature
                IReadOnlyDictionary<string, object> attributes = new Dictionary<string, object> { { "class", "my-custom-class-1" } };

                //act
                var ClassToRender = new CssBuilder("item-one")
                                .SetPrefix("pre-")
                                .AddClassFromAttributes(attributes)
                                .Build();
                //assert
                ClassToRender.Should().Be("item-one pre-my-custom-class-1");
            }
        }

        [Fact]
        public void ShouldBuildClassesFromAttributesWithClearedPrefix()
        {
            {
                //arrange
                // Simulates Razor Components attribute splatting feature
                IReadOnlyDictionary<string, object> attributes = new Dictionary<string, object> { { "class", "my-custom-class-1" } };

                //act
                var ClassToRender = new CssBuilder("item-one")
                                .SetPrefix("item-")
                                .AddClass("two")
                                .SetPrefix(String.Empty)
                                .AddClassFromAttributes(attributes)
                                .Build();
                //assert
                ClassToRender.Should().Be("item-one item-two my-custom-class-1");
            }
        }
    }
}
