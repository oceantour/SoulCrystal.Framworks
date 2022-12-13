using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace SoulCrystal
{
    /// <summary> Universe 类型 ID，存在任意地方在过去至未来的唯一标识 </summary>
    public readonly partial struct Universeid : INullable
    {
        #region -- 字段(Fields) --

        /// <summary> UniverseID 转换字符串时为空 </summary>
        private const String _NullString = "nil";
        /// <summary> UniverseID 基础的大小 </summary>
        private const Int32 _BasicOfSize = 16;
        /// <summary> UniverseID 字节值 </summary>
        private readonly byte[] u_value;

        #endregion

        #region -- 属性(Properties) --

        public static readonly Universeid Null = new Universeid(true);

        public static readonly Universeid Empty = new Universeid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        #endregion

        #region -- 构造(Constructions) --

        private Universeid(Boolean isNull)
        {
            u_value = null;
        }

        private Universeid(Int32 a, Int32 b, Int32 c, Int32 d, Int32 e, Int32 f, Int32 g, Int32 h, Int32 i, Int32 j, Int32 k)
        {
            u_value = new byte[_BasicOfSize];
        }

        #endregion

        #region -- 函数(Functions) --

        #endregion

        #region -- 生成(Generate) --

        #endregion

        #region -- 解析(Analysis) --

        #endregion

        #region -- 运算符(Operational) --

        public static Boolean operator ==(Universeid x, Universeid y)
        {
            Boolean xIsNull = x.IsNull, yIsNull = y.IsNull;
            if (xIsNull || yIsNull)
            {
                return (xIsNull && yIsNull) ? true : false;
            }

            ReadOnlySpan<byte> valueSpan = x.u_value;
            return valueSpan.SequenceEqual(y.u_value);
        }

        public static Boolean operator !=(Universeid x, Universeid y) => !(x == y);

        #endregion

        #region -- Universeid 运算重载 --

        public override Boolean Equals(Object value)
        {
            if (value is null) return false;
            if (value.GetType() != typeof(Universeid)) return false;
            return this == (Universeid)value;
        }

        public Boolean Equals(Universeid value) => this == value;

        public override Int32 GetHashCode()
        {
            if (IsNull) { return 0; }

            var a = ((int)u_value[3] << 24) | ((int)u_value[2] << 16) | ((int)u_value[1] << 8) | u_value[0];
            var b = (short)(((int)u_value[5] << 8) | u_value[4]);
            var c = (short)(((int)u_value[7] << 8) | u_value[6]);
            return a ^ (((int)b << 16) | (int)(ushort)c) ^ (((int)u_value[10] << 24) | u_value[15]);
        }

        #endregion

        #region -- INullable 成员 --

        /// <summary> 获取一个布尔值，该值标识 Universeid 结构是否为空。 </summary>
        public Boolean IsNull => u_value is null;

        /// <summary> 获取一个布尔值，该值标识 Universeid 结构是否为空或其均值为指定常数。 </summary>
        public Boolean IsNullOrEmpty => u_value is null || this == Empty;

        #endregion
    }

    public struct SiroGuid
    {
        #region -- 字段 --

        private byte[] s_value;
        private const string _NullString = "nil";
        private const int _SizeOfGuid = 16;

        private static readonly int _MaxTenthMilliseconds = 863999999;
        private static readonly DateTime _BaseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static readonly SiroGuid Null = new SiroGuid(isNull: true);
        public static readonly SiroGuid Empty = new SiroGuid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        public static int LastDays;
        public static int LastTenthMilliseconds;

        public static readonly char[] CharToHexStringHigh = new char[256]
        {
            '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',
            '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1',
            '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2', '2',
            '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3', '3',
            '4', '4', '4', '4', '4', '4', '4', '4', '4', '4', '4', '4', '4', '4', '4', '4',
            '5', '5', '5', '5', '5', '5', '5', '5', '5', '5', '5', '5', '5', '5', '5', '5',
            '6', '6', '6', '6', '6', '6', '6', '6', '6', '6', '6', '6', '6', '6', '6', '6',
            '7', '7', '7', '7', '7', '7', '7', '7', '7', '7', '7', '7', '7', '7', '7', '7',
            '8', '8', '8', '8', '8', '8', '8', '8', '8', '8', '8', '8', '8', '8', '8', '8',
            '9', '9', '9', '9', '9', '9', '9', '9', '9', '9', '9', '9', '9', '9', '9', '9',
            'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a',
            'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b',
            'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c', 'c',
            'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd', 'd',
            'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e', 'e',
            'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f', 'f',
        };

        public static readonly char[] CharToHexStringLow = new char[256]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
        };

        #endregion

        #region -- 属性 --

        public bool IsNull => s_value == null;
        public bool IsNullOrEmpty
        {
            get
            {
                if (s_value != null)
                {
                    return this == Empty;
                }
                return true;
            }
        }

        public DateTime DateTime
        {
            get
            {
                if (IsNull)
                {
                    return DateTime.MinValue;
                }

                byte[] array = new byte[4];
                byte[] array2 = new byte[4];
                Array.Copy(s_value, s_value.Length - 6, array, 2, 2);
                Array.Copy(s_value, s_value.Length - 4, array2, 0, 4);
                Array.Reverse(array);
                Array.Reverse(array2);
                int num = BitConverter.ToInt32(array, 0);
                int num2 = BitConverter.ToInt32(array2, 0);
                DateTime dateTime = _BaseDate.AddDays(num);
                if (num2 > _MaxTenthMilliseconds)
                {
                    num2 = _MaxTenthMilliseconds;
                }

                num2 /= 10;
                return dateTime.AddMilliseconds(num2);
            }
        }

        public static ReadOnlySpan<byte> GuidComparisonOrders => new byte[16] { 10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3 };

        #endregion

        #region -- 构造函数 --

        private SiroGuid(bool isNull) => s_value = null;
        private SiroGuid(Guid g) => s_value = g.ToByteArray();
        private SiroGuid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
        {
            s_value = new byte[_SizeOfGuid];
            s_value[15] = k;
            s_value[14] = j;
            s_value[13] = i;
            s_value[12] = h;
            s_value[11] = g;
            s_value[10] = f;
            s_value[9] = e;
            s_value[8] = d;
            s_value[7] = (byte)(c >> 8);
            s_value[6] = (byte)c;
            s_value[5] = (byte)(b >> 8);
            s_value[4] = (byte)b;
            s_value[3] = (byte)(a >> 24);
            s_value[2] = (byte)(a >> 16);
            s_value[1] = (byte)(a >> 8);
            s_value[0] = (byte)a;
        }

        public SiroGuid(byte[] value, SiroGuidSequentialSegmentType sequentialType = SiroGuidSequentialSegmentType.Guid, bool isOwner = false)
        {
            if (value == null || value.Length != 16)
            {
                throw GetException();
                static ArgumentException GetException()
                {
                    return new ArgumentException("value 的长度不是 16 个字节。");
                }
            }

            if (sequentialType == SiroGuidSequentialSegmentType.Guid)
            {
                if (isOwner)
                {
                    s_value = value;
                    return;
                }

                s_value = new byte[16];
                FastCopy(ref s_value[0], ref value[0]);

                static void FastCopy(ref byte destination, ref byte src)
                {
                    if (IntPtr.Size >= 8)
                    {
                        Unsafe.As<byte, long>(ref destination) = Unsafe.As<byte, long>(ref src);
                        Unsafe.As<byte, long>(ref Unsafe.Add(ref destination, 8)) = Unsafe.As<byte, long>(ref Unsafe.Add(ref src, 8));
                    }
                    else
                    {
                        Unsafe.As<byte, int>(ref destination) = Unsafe.As<byte, int>(ref src);
                        Unsafe.As<byte, int>(ref Unsafe.Add(ref destination, 4)) = Unsafe.As<byte, int>(ref Unsafe.Add(ref src, 4));
                        Unsafe.As<byte, int>(ref Unsafe.Add(ref destination, 8)) = Unsafe.As<byte, int>(ref Unsafe.Add(ref src, 8));
                        Unsafe.As<byte, int>(ref Unsafe.Add(ref destination, 12)) = Unsafe.As<byte, int>(ref Unsafe.Add(ref src, 12));
                    }
                }

                return;
            }

            ReadOnlySpan<byte> guidComparisonOrders = GuidComparisonOrders;
            s_value = new byte[16];
            _ = s_value[15];
            _ = ref guidComparisonOrders[15];
            for (int num = 16; num > 0; num--)
            {
                int num2 = num - 1;
                s_value[guidComparisonOrders[num2]] = value[num2];
            }
        }

        #endregion

        #region -- 生成 --

        public static SiroGuid NewComb()
        {
            return NewComb(DateTime.UtcNow);
        }

        public static SiroGuid NewComb(DateTime timestamp)
        {
            byte[] array = Guid.NewGuid().ToByteArray();
            int days = new TimeSpan(timestamp.Ticks - _BaseDate.Ticks).Days;
            int num = (int)(timestamp.TimeOfDay.TotalMilliseconds * 10.0);
            int lastDays = LastDays;
            int lastTenthMilliseconds = LastTenthMilliseconds;
            if (days == lastDays)
            {
                if (num > lastTenthMilliseconds)
                {
                    Interlocked.CompareExchange(ref LastTenthMilliseconds, num, lastTenthMilliseconds);
                }
                else
                {
                    if (LastTenthMilliseconds < int.MaxValue)
                    {
                        Interlocked.Increment(ref LastTenthMilliseconds);
                    }

                    num = LastTenthMilliseconds;
                }
            }
            else
            {
                Interlocked.CompareExchange(ref LastDays, days, lastDays);
                Interlocked.CompareExchange(ref LastTenthMilliseconds, num, lastTenthMilliseconds);
            }

            byte[] bytes = BitConverter.GetBytes(days);
            byte[] bytes2 = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
                Array.Reverse(bytes2);
            }

            Array.Copy(bytes, bytes.Length - 2, array, array.Length - 6, 2);
            Array.Copy(bytes2, 0, array, array.Length - 4, 4);
            return new SiroGuid(array, SiroGuidSequentialSegmentType.Guid, isOwner: true);
        }

        #endregion

        #region -- 运算 --

        public static bool operator ==(SiroGuid x, SiroGuid y)
        {
            bool isNull = x.IsNull;
            bool isNull2 = y.IsNull;
            if (isNull || isNull2)
            {
                if (!(isNull && isNull2))
                {
                    return false;
                }

                return true;
            }

            return ((ReadOnlySpan<byte>)x.s_value).SequenceEqual((ReadOnlySpan<byte>)y.s_value);
        }

        public static bool operator !=(SiroGuid x, SiroGuid y)
        {
            return !(x == y);
        }

        public static bool operator <(SiroGuid x, SiroGuid y)
        {
            bool isNull = x.IsNull;
            bool isNull2 = y.IsNull;
            if (isNull || isNull2)
            {
                if (!isNull || isNull2)
                {
                    return false;
                }

                return true;
            }

            if (Compare(x, y) != 0)
            {
                return false;
            }

            return true;
        }

        public static bool operator >(SiroGuid x, SiroGuid y)
        {
            bool isNull = x.IsNull;
            bool isNull2 = y.IsNull;
            if (isNull || isNull2)
            {
                if (!(!isNull && isNull2))
                {
                    return false;
                }

                return true;
            }

            if (Compare(x, y) != SiroGuidComparison.GT)
            {
                return false;
            }

            return true;
        }

        public static bool operator <=(SiroGuid x, SiroGuid y)
        {
            bool isNull = x.IsNull;
            bool isNull2 = y.IsNull;
            if (isNull || isNull2)
            {
                return isNull;
            }

            SiroGuidComparison SiroGuidComparison = Compare(x, y);
            if (SiroGuidComparison != 0 && SiroGuidComparison != SiroGuidComparison.EQ)
            {
                return false;
            }

            return true;
        }

        public static bool operator >=(SiroGuid x, SiroGuid y)
        {
            bool isNull = x.IsNull;
            bool isNull2 = y.IsNull;
            if (isNull || isNull2)
            {
                return isNull2;
            }

            SiroGuidComparison SiroGuidComparison = Compare(x, y);
            if (SiroGuidComparison != SiroGuidComparison.GT && SiroGuidComparison != SiroGuidComparison.EQ)
            {
                return false;
            }

            return true;
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value.GetType() != typeof(SiroGuid))
            {
                return false;
            }

            return this == (SiroGuid)value;
        }

        public override int GetHashCode()
        {
            if (IsNull)
            {
                return 0;
            }

            int num = (s_value[3] << 24) | (s_value[2] << 16) | (s_value[1] << 8) | s_value[0];
            short num2 = (short)((s_value[5] << 8) | s_value[4]);
            short num3 = (short)((s_value[7] << 8) | s_value[6]);
            return num ^ ((num2 << 16) | (ushort)num3) ^ ((s_value[10] << 24) | s_value[15]);
        }

        private static SiroGuidComparison Compare(SiroGuid x, SiroGuid y)
        {
            ReadOnlySpan<byte> guidComparisonOrders = GuidComparisonOrders;

            _ = ref guidComparisonOrders[15];
            for (int i = 0; i < 16; i++)
            {
                uint num = x.s_value[guidComparisonOrders[i]];
                uint num2 = y.s_value[guidComparisonOrders[i]];
                if (num != num2)
                {
                    if (num >= num2)
                    {
                        return SiroGuidComparison.GT;
                    }

                    return SiroGuidComparison.LT;
                }
            }

            return SiroGuidComparison.EQ;
        }

        #endregion

        #region -- ToString --

        public override string ToString()
        {
            return ToString(SiroGuidFormatStringType.Comb);
        }

        public string ToString(SiroGuidFormatStringType formatType)
        {
            int num;
            switch (formatType)
            {
                case SiroGuidFormatStringType.Guid32Digits:
                case SiroGuidFormatStringType.Comb32Digits:
                    num = 32;
                    break;
                default:
                    num = 36;
                    break;
            }

            string text = new string('\0', num);
            Span<char> destination = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(text.AsSpan()), num);
            TryFormat(destination, formatType, out int _);
            return text;
        }

        #endregion

        #region -- unsafe --

        private unsafe static int HexsToChars(char* guidChars, int a, int b)
        {
            char[] charToHexStringHigh = CharToHexStringHigh;
            char[] charToHexStringLow = CharToHexStringLow;
            *guidChars = charToHexStringHigh[a];
            guidChars[1] = charToHexStringLow[a];
            guidChars[2] = charToHexStringHigh[b];
            guidChars[3] = charToHexStringLow[b];
            return 4;
        }

        public unsafe bool TryFormat(Span<char> destination, SiroGuidFormatStringType formatType, out int charsWritten)
        {
            if (IsNull)
            {
                return Empty.TryFormat(destination, formatType, out charsWritten);
            }

            int num;
            bool flag;
            bool flag2;
            switch (formatType)
            {
                case SiroGuidFormatStringType.Guid:
                    num = 36;
                    flag = true;
                    flag2 = false;
                    break;
                case SiroGuidFormatStringType.Guid32Digits:
                    num = 32;
                    flag = false;
                    flag2 = false;
                    break;
                case SiroGuidFormatStringType.Comb32Digits:
                    num = 32;
                    flag = false;
                    flag2 = true;
                    break;
                default:
                    num = 36;
                    flag = true;
                    flag2 = true;
                    break;
            }

            if ((uint)destination.Length < (uint)num)
            {
                charsWritten = 0;
                return false;
            }

            ref byte source = ref s_value[0];
            IntPtr intPtr = (IntPtr)0;
            fixed (char* ptr = &MemoryMarshal.GetReference(destination))
            {
                if (flag2)
                {
                    char* ptr2 = ptr + HexsToChars(ptr, Unsafe.AddByteOffset(ref source, intPtr + 10), Unsafe.AddByteOffset(ref source, intPtr + 11));
                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 12), Unsafe.AddByteOffset(ref source, intPtr + 13));
                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 14), Unsafe.AddByteOffset(ref source, intPtr + 15));
                    if (flag)
                    {
                        char* num2 = ptr2;
                        ptr2 = num2 + 1;
                        *num2 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 8), Unsafe.AddByteOffset(ref source, intPtr + 9));
                    if (flag)
                    {
                        char* num3 = ptr2;
                        ptr2 = num3 + 1;
                        *num3 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 6), Unsafe.AddByteOffset(ref source, intPtr + 7));
                    if (flag)
                    {
                        char* num4 = ptr2;
                        ptr2 = num4 + 1;
                        *num4 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 4), Unsafe.AddByteOffset(ref source, intPtr + 5));
                    if (flag)
                    {
                        char* num5 = ptr2;
                        ptr2 = num5 + 1;
                        *num5 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr), Unsafe.AddByteOffset(ref source, intPtr + 1));
                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 2), Unsafe.AddByteOffset(ref source, intPtr + 3));
                }
                else
                {
                    char* ptr2 = ptr + HexsToChars(ptr, Unsafe.AddByteOffset(ref source, intPtr + 3), Unsafe.AddByteOffset(ref source, intPtr + 2));
                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 1), Unsafe.AddByteOffset(ref source, intPtr));
                    if (flag)
                    {
                        char* num6 = ptr2;
                        ptr2 = num6 + 1;
                        *num6 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 5), Unsafe.AddByteOffset(ref source, intPtr + 4));
                    if (flag)
                    {
                        char* num7 = ptr2;
                        ptr2 = num7 + 1;
                        *num7 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 7), Unsafe.AddByteOffset(ref source, intPtr + 6));
                    if (flag)
                    {
                        char* num8 = ptr2;
                        ptr2 = num8 + 1;
                        *num8 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 8), Unsafe.AddByteOffset(ref source, intPtr + 9));
                    if (flag)
                    {
                        char* num9 = ptr2;
                        ptr2 = num9 + 1;
                        *num9 = '-';
                    }

                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 10), Unsafe.AddByteOffset(ref source, intPtr + 11));
                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 12), Unsafe.AddByteOffset(ref source, intPtr + 13));
                    ptr2 += HexsToChars(ptr2, Unsafe.AddByteOffset(ref source, intPtr + 14), Unsafe.AddByteOffset(ref source, intPtr + 15));
                }
            }

            charsWritten = num;
            return true;
        }

        #endregion

        #region -- enmu --

        private enum SiroGuidComparison
        {
            LT,
            EQ,
            GT
        }

        private enum GuidParseThrowStyle : byte
        {
            None,
            All,
            AllButOverflow
        }

        public enum SiroGuidSequentialSegmentType
        {
            Guid, // Guid 格式，顺序字节（6位）在尾部，适用于微软体系数据库。
            Comb, // CombGuid 格式，顺序字节（6位）在头部，适用于非微软体系数据库。
        }

        public enum SiroGuidFormatStringType
        {
            /// <summary>
            ///     Guid 格式字符串，用一系列指定格式的小写十六进制位表示，由连字符("-")分隔的 32 位数字，这些十六进制位分别以 8 个、4 个、4 个、4 个和
            ///     12 个位为一组并由连字符分隔开。
            ///     顺序字节（6位）在尾部，适用于微软体系数据库。
            /// </summary>
            Guid,
            /// <summary>
            ///    Guid 格式字符串，用一系列指定格式的小写十六进制位表示，32 位数字，这些十六进制位分别以 8 个、4 个、4 个、4 个和 12 个位为一组合并而成。
            ///    顺序字节（6位）在尾部，适用于微软体系数据库。
            /// </summary>
            Guid32Digits,
            /// <summary>
            ///    CombGuid 格式字符串，用一系列指定格式的小写十六进制位表示，由连字符("-")分隔的 32 位数字，这些十六进制位分别以 12 个和 4 个、4
            ///    个、4 个、8 个位为一组并由连字符分隔开。
            ///    顺序字节（6位）在头部，适用于非微软体系数据库。
            /// </summary>
            Comb,
            /// <summary>
            ///    CombGuid 格式字符串，用一系列指定格式的小写十六进制位表示，32 位数字，这些十六进制位分别以 12 个和 4 个、4 个、4 个、8 个位为一组合并而成。
            ///    顺序字节（6位）在头部，适用于非微软体系数据库。
            /// </summary>
            Comb32Digits
        }

        #endregion
    }
}
