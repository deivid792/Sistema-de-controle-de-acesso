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
            Assert.Contains( password.Errors, p => p.Message == "A quantidade mínima de caracteres é 8");
        }
        [Fact]
        public void It_should_not_exceed_20_characters()
        {
            var password = Password.Create("Luffy@1074EpseContando");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Errors, p => p.Message == "A quantidade máxima de caracteres é 20");
        }
        [Fact]
        public void It_must_contain_at_least_one_capital_letter()
        {
            var password = Password.Create("luffy@1074ep");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Errors, p => p.Message == "A senha deve conter maiúsculas, minúsculas, números e caracteres especiais.");
        }
        [Fact]
        public void must_contain_at_least_one_lowercase_letter()
        {
            var password = Password.Create("LUFY@1074EP");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Errors, p => p.Message == "A senha deve conter maiúsculas, minúsculas, números e caracteres especiais.");
        }
        [Fact]
        public void must_contain_at_least_one_special_character()
        {
            var password = Password.Create("luffy1074ep");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Errors, p => p.Message == "A senha deve conter maiúsculas, minúsculas, números e caracteres especiais.");
        }
        [Fact]
        public void It_should_not_contain_blank_spaces_in_the_middle()
        {
            var password = Password.Create("Luffy@ 1074ep");

            Assert.True(password.HasErrors);
            Assert.Contains(password.Errors, p => p.Message == "Não pode haver espaços em branco no meio");
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
