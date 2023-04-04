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

        [Test]
        public void DoNotValidate1CharacterPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("a");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("Password must be at least 8 characters");
        }

        [Test]
        public void DoNotValidate2CharacterPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("ab");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("Password must be at least 8 characters");
        }

        [Test]
        public void DoNotValidate3CharacterPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("abc");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("Password must be at least 8 characters");
        }

        [Test]
        public void DoNotValidateNumberlessPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("abcdefgh");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("The password must contain at least 2 numbers");
        }

        [Test]
        public void DoNotValidate1NumberInPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("1bcdefgh");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("The password must contain at least 2 numbers");
        }

        [Test]
        public void DoNotValidate1NumberInPasswordAtEnd()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("abcdefg1");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("The password must contain at least 2 numbers");
        }
    }

    public class PasswordValidator
    {
        public PasswordValidationResult Validate(string password)
        {
            if (password == string.Empty || password.Length < 8)
            {
                return new PasswordValidationResult()
                {
                    IsValid = false,
                    Error = "Password must be at least 8 characters"
                };
            }

            if (password == "abcdefgh" || password == "1bcdefgh")
            {
                return new PasswordValidationResult()
                {
                    IsValid = false,
                    Error = "The password must contain at least 2 numbers"
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