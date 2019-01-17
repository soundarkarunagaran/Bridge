using System;
using System.IO;

namespace Bridge.Translator
{
    public sealed class ReferenceInfo : IEquatable<ReferenceInfo>
    {
        public ReferenceInfo(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            FilePath = filePath;

            var info = new FileInfo(filePath);
            CreationDate = info.CreationTime;
            Length = info.Length;
        }

        public string FilePath { get; }

        public DateTime CreationDate { get; }

        public long Length { get; }

        public bool Equals(ReferenceInfo other)
        {
            return other != null
                && FilePath == other.FilePath
                && CreationDate == other.CreationDate
                && Length == other.Length;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = FilePath.GetHashCode();
                result = (result * 397) ^ (Length != 0 ? Length.GetHashCode() : 0);
                result = (result * 397) ^ (CreationDate.GetHashCode());
                return result;
            }
        }
    }
}
