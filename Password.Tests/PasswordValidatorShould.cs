using FluentAssertions;

namespace Password.Tests
{
    public class PasswordValidatorShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DoNotValidateEmptyPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate(string.Empty);
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("PasswordValidator");
        }
    }

    public class PasswordValidator
    {
        public PasswordValidationResult Validate(string password)
        {
            throw new NotImplementedException();
        }
    }

    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}