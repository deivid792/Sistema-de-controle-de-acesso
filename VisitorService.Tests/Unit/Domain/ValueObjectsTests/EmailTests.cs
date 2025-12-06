using VisitorService.Domain.ValueObject;

namespace VisitorService.Domain.Tests.EmailTests
{
    public class EmailTests
    {

        [Fact]
        public void Should_Create_Valid_Email()
        {
            var email = Email.Create("user@outlook.com");

            Assert.False(email.HasErrors);
            Assert.Equal("user@outlook.com", email.Value!.ToString());
        }

        [Fact]
        public void It_should_have_normalized_lowercase()
        {
            var email = Email.Create(" UsER@gMail.com ");

            Assert.Equal("user@gmail.com", email.Value);
        }
    }
}
