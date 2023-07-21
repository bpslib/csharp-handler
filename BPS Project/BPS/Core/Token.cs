/**
* 
* MIT License
*
* Copyright (c) 2021 Carlos Eduardo de Borba Machado
*
*/
namespace BPSLib.Core
{
    internal class Token
    {
        internal TokenCategory Category { get; }

        internal string Image { get; }

        internal int Line { get; }

        internal int Collumn { get; }

        internal Token(TokenCategory category, string image, int line, int collumn)
        {
            Category = category;
            Image = image;
            Line = line;
            Collumn = collumn;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var t = (Token)obj;
                return GetHashCode().Equals(t.GetHashCode());
            }
        }

        public override int GetHashCode()
        {
            var imageHash = Image != null ? Image.GetHashCode() : 666;
            return imageHash * 17 + Category.GetHashCode() * 7 + Line * Collumn;
        }
    }
}
