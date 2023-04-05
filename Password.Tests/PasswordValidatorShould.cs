using System.Text.RegularExpressions;
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
            validation.Error.Should().Be("Password must be at least 8 characters\nThe password must contain at least 2 numbers\npassword must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidate1CharacterPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("a");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("Password must be at least 8 characters\nThe password must contain at least 2 numbers\npassword must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidate2CharacterPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("ab");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("Password must be at least 8 characters\nThe password must contain at least 2 numbers\npassword must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidate1Character2NumbersPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("a12");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("Password must be at least 8 characters\npassword must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidateNumberlessPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("abcdefgh");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("The password must contain at least 2 numbers\npassword must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidate1NumberInPassword()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("1bcdefgh");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("The password must contain at least 2 numbers\npassword must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidate1NumberInPasswordAtEnd()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("abcdefg1");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("The password must contain at least 2 numbers\npassword must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidateNoCapitalLetter()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("abcdef21");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("password must contain at least one capital letter\npassword must contain at least one special character");
        }

        [Test]
        public void DoNotValidateNoSpecialCharacter()
        {
            var validator = new PasswordValidator();
            var validation = validator.Validate("aBcdef21");
            validation.IsValid.Should().Be(false);
            validation.Error.Should().Be("password must contain at least one special character");
        }
    }

    public class PasswordValidator
    {
        public PasswordValidationResult Validate(string password)
        {
            var validation = new PasswordValidationResult()
            {
                IsValid = true,
                Error = ""
            };

            if (password == string.Empty || password.Length < 8)
            {
                validation.IsValid = false;
                validation.Error += "Password must be at least 8 characters";
            }

            CheckTwoNumberRule(password, validation, new Regex("(\\D*\\d){2,}"), "The password must contain at least 2 numbers");

            CheckCapitalLetterRule(password, validation, new Regex(".*[A-Z].*"), "password must contain at least one capital letter");

            CheckSpecialLetterRule(password, validation, new Regex(".*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?].*"), "password must contain at least one special character");

            return validation;
        }

        private static void CheckSpecialLetterRule(string password, PasswordValidationResult validation, Regex specialCharacterRegex, string specialCharacterErrorDescription)
        {
            if (!specialCharacterRegex.IsMatch(password))
            {
                validation.IsValid = false;
                if (!string.IsNullOrEmpty(validation.Error))
                {
                    validation.Error += "\n";
                }

                validation.Error += specialCharacterErrorDescription;
            }
        }

        private static void CheckCapitalLetterRule(string password, PasswordValidationResult validation, Regex capitalLetterRegex, string capitalLetterErrorDescription)
        {
            if (!capitalLetterRegex.IsMatch(password))
            {
                validation.IsValid = false;
                if (!string.IsNullOrEmpty(validation.Error))
                {
                    validation.Error += "\n";
                }

                validation.Error += capitalLetterErrorDescription;
            }
        }

        private static void CheckTwoNumberRule(string password, PasswordValidationResult validation, Regex twoNumbersRegex, string twoNumberErrorDescription)
        {
            if (!twoNumbersRegex.IsMatch(password))
            {
                validation.IsValid = false;
                if (!string.IsNullOrEmpty(validation.Error))
                {
                    validation.Error += "\n";
                }

                validation.Error += twoNumberErrorDescription;
            }
        }
    }

    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }
    }
}