
namespace ImgComparer.Utils
{
    class StringUtil
    {
        public static string trimDuplicates(string str, char c)
        {
            string res = "";
            int occ = 0;
            foreach(char c0 in str)
            {
                if (c0 == c)
                {
                    occ++;
                }
                else
                {
                    occ = 0;
                }

                if (occ <= 1)
                {
                    res += c0;
                }
            }
            return res;
        }
    }
}
