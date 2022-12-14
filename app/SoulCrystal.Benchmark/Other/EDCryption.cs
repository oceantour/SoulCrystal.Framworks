using System;
using System.Linq;

namespace SoulCrystal.Other
{
    internal class EDCryption
    {
        public const int Rand = 590127;
        public const int DividEnd = 7;

        /// <summary> 加密 </summary>
        /// <returns> 秘钥, 商 </returns>
        public static (int, int) Encryption(int number)
        {
            var nums = $"{number}".Select(_ => int.Parse($"{_}")).ToList(); // 拆分需要加密的数
            var keys = new int[nums.Count]; // 获取拆分后反转后的数
            var quotient = 0; // 商

            for (int idx = 0; idx < keys.Length; idx++)
            {
                var sum = nums[idx] + Rand;
                quotient += sum / DividEnd;
                keys[keys.Length - 1 - idx] = sum % DividEnd;
            }

            var key = ""; // 将 keys 组装
            foreach (var item in keys)
            {
                key += item;
            }

            return (Convert.ToInt32(key), quotient / key.Length);
        }

        /// <summary> 解密 </summary>
        /// <param name="key"> 秘钥 </param>
        /// <param name="quotient"> 商 </param>
        /// <returns> 数据 </returns>
        public static int Decryption(int key, int quotient)
        {
            var data = "";

            var nums = $"{key}".Select(_ => int.Parse($"{_}")).ToList();
            var keys = new int[nums.Count];

            for (int idx = 0; idx < keys.Length; idx++)
            {
                var num = nums[keys.Length - 1 - idx] + quotient * DividEnd;
                data += num - Rand;
            }

            return Convert.ToInt32(data);
        }
    }
}
