using Domain.Customers;
using FluentAssertions;

namespace Domain.UnitTest.Customers
{
    public class NameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_Should_ThrowArgumentException_WhenValuesIsInvalid(string? value)
        {
            Email Action() => Email.Create(value);

            // Assert
            FluentActions.Invoking(Action).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
        }
    }
}