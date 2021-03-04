using Planner.Domain.ValueObjects.Exceptions;
using System.Text.RegularExpressions;

namespace Planner.Domain.ValueObjects
{
    public sealed class Email
    {
        private string _text;

        const string RegexForValidation = @"^(([^<>()[\]\\.,;:\s@\']+(\.[^<>()[\]\\.,;:\s@\']+)*)|(\'.+\'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";


        public Email(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new EmailShouldNotBeEmptyException("The Email field is required");

            Regex regex = new Regex(RegexForValidation);
            Match match = regex.Match(text);

            if (!match.Success)
                throw new EmailInvalidException("Email Invalid");

            _text = text;
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator Email(string text)
        {
            return new Email(text);
        }

        public static implicit operator string(Email email)
        {
            return email._text;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is string)
            {
                return obj.ToString() == _text;
            }

            return ((Email)obj)._text == _text;
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }
    }
}
