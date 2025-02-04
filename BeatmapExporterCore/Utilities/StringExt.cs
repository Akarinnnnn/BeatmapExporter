namespace BeatmapExporterCore.Utilities
{
    public static class StringExt
    {
        /// <summary>
        /// Truncates a string to the specified length of characters
        /// </summary>
        public static string Trunc(this string str, int len) => string.IsNullOrEmpty(str) ? str : str.Length <= len ? str : str[..len];

        /// <summary>
        /// Removes characters that should not be in a (Windows) file name
        /// </summary>
        public static string RemoveFilenameCharacters(this string str)
        {
            return string.Create(str.Length, str, static (span, original) =>
            {
                original.CopyTo(span);
                
                // " is ok to explorer
                ReadOnlySpan<char> escapeChars = stackalloc[] { '\"', '*', '<', '>', ':', '/', '\\', '|', '?' };
                int escapeIndex = -1;
                while ((escapeIndex = span.IndexOfAny(escapeChars)) != -1)
                {
                    span[escapeIndex] = '_';
                    span = span[(escapeIndex + 1)..];
                }
            });
        }

        /// <summary>
        /// Returns an array of strings which split the original string by comma (,)
        /// </summary>
        public static string[] CommaSeparatedArg(this string str) => str.Split(",").Select(s => s.Trim()).ToArray();
    }
}
