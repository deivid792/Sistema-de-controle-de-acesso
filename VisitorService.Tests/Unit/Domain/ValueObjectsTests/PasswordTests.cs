using VisitorService.Domain.ValueObject;

namespace VisitorService.Domain.Tests.PasswordTests
{
    public class PasswordTests
    {
        [Fact]
        public void is_should_contain_at_least_8_characters()
        {
            var password = Password.Create("Luffy@2");

            Assert.True(password.HasErrors);
            Assert.Contains( password.Notification, p => p.Message == "A senha deve conter pelo menos 8 caracteres");
        }
        [Fact]
        public void It_should_not_exceed_12_characters()
        {
            var password = Password.Create("Luffy@1074eps");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Notification, p => p.Message == "A senha não deve exceder 12 caracteres");
        }
        [Fact]
        public void It_must_contain_at_least_one_capital_letter()
        {
            var password = Password.Create("luffy@1074ep");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Notification, p => p.Message == "A senha deve conter pelo menos uma letra maiúscula");
        }
        [Fact]
        public void must_contain_at_least_one_lowercase_letter()
        {
            var password = Password.Create("LUFY@1074EP");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Notification, p => p.Message == "A senha deve conter pelo menos uma letra minúscula");
        }
        [Fact]
        public void must_contain_at_least_one_special_character()
        {
            var password = Password.Create("luffy1074ep");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Notification, p => p.Message == "A senha deve conter pelo menos um caractere especial");
        }
        [Fact]
        public void It_should_not_contain_blank_spaces_in_the_middle()
        {
            var password = Password.Create("Luffy@ 1074ep");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Notification, p => p.Message == "A senha não deve conter espaços em branco no meio");
        }
        [Fact]
        public void It_must_create_a_valid_error_free_and_clean_object()
        {
            var password = Password.Create(" Luffy@1074ep ");

            Assert.False(password.HasErrors);
            Assert.Equal("Luffy@1074ep", password.Value);
        }
    }
}
