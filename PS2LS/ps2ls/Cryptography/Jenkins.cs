using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ps2ls.Cryptography
{
    public static class Jenkins
    {
        //http://en.wikipedia.org/wiki/Jenkins_hash_function#one-at-a-time
        public static UInt32 OneAtATime(String key)
        {
            //[4:56:55 AM] Herbert Harrison: Yeah, you need to use signed ints (so Int32) inside the function and cast to UInt32 on return
            //[4:57:45 AM] Herbert Harrison: And some places use uppercase for the hashes
            Int32 hash = 0;

            for (Int32 i = 0; i < key.Length; ++i)
            {
                hash += key[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            return (UInt32)hash;
        }
    }
}
