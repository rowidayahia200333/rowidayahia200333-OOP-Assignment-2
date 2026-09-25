namespace  rowidayahia200333_OOP_Assignment_2.LeetCode.1456_MaxVowelsInSubstring;


// public class Solution
//{
//    public static void Main(string[] args)
//    {

//    }

//}
public class Solution
{
    public int MaxVowels(string s, int k)
    {
        bool isVowel(char ch)
        {
            if (ch == 'a' || ch == 'i' ||
                  ch == 'o' || ch == 'e' ||
                  ch == 'u') return true;

            return false;
        }
        int sum = 0;
        int maxSum = 0;
        for (int i = 0; i < k; i++)
        {
            if (isVowel(s[i])) sum++;
        }
        maxSum = sum;
        for (int end = k; end < s.Length; end++)
        {
            if (isVowel(s[end - k])) sum--;

            if (isVowel(s[end])) sum++;

            if (sum > maxSum) maxSum = sum;
        }
        return maxSum;
    }
}
