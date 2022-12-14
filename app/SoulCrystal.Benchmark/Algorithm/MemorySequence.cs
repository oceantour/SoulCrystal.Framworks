namespace SoulCrystal.Algorithm
{
    /// <summary> 记忆序列 </summary>
    /// <remarks> 概念未完善 </remarks>
    internal class MemorySequence
    {
        private readonly char[] _NumberSeeds = new char[10]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        private readonly char[] _BigSeeds = new char[26]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private readonly char[] _SmallSeeds = new char[26]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        private readonly char[] _SymbolsSeeds = new char[]
        {
            ' ', '!', '"', '#', '$', '%', '&', '(', ')', '*', '+', ',', '-', '.', '/',
            ':', ';', '<', '=', '>', '?', '@',
            '[', '\\', ']', '^', '_', '`',
            '{', '|', '}', '~'
        };
    }
}
