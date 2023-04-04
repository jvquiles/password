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
            validation.Error.Should().Be("Password must be at least 8 characters");
        }
    }

    public class PasswordValidator
    {
        public PasswordValidationResult Validate(string password)
        {
            if (password == string.Empty)
            {
                return new PasswordValidationResult()
                {
                    IsValid = false,
                    Error = "Password must be at least 8 characters"
                };
            }

            return new PasswordValidationResult()
            {
                IsValid = true,
                Error = ""
            };
        }
    }

    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}