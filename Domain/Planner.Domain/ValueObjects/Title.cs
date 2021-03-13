using Planner.Domain.Utils;
using Planner.Domain.ValueObjects.Exceptions;
using System;

namespace Planner.Domain.ValueObjects
{
    public class Title
    {
        private string _text;

        public Title(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new TitleShouldNotBeEmptyException("The title is required!");

            _text = Capitalize(text);
        }

        private string Capitalize(string text)
        {
            Span<char> characters = stackalloc char[text.Length];
            text.AsSpan(1).CopyTo(characters.Slice(1));
            characters[0] = char.ToUpper(text[0]);
            return new string(characters);
        }

        public static implicit operator Title(string text)
        {
            return new Title(text);
        }

        public static implicit operator string(Title title)
        {
            return title._text;
        }

        public static bool operator ==(Title title, Title title2)
        {
            return StringUtils.RemoveAccents(title._text) == StringUtils.RemoveAccents(title2._text);
        }

        public static bool operator !=(Title title, Title title2)
        {
            return StringUtils.RemoveAccents(title._text) != StringUtils.RemoveAccents(title2._text);
        }
        public override string ToString()
        {
            return _text.ToString();
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
                return StringUtils.RemoveAccents(obj.ToString()) == StringUtils.RemoveAccents(_text);
            }

            return StringUtils.RemoveAccents(((Title)obj)._text) == StringUtils.RemoveAccents(_text);
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }
    }
}
