using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Network
{
    public class IvyBuffer
    {
        public byte[] data
        {
            private set;
            get;
        }

        public int offset
        {
            private set;
            get;
        }

        public int length
        {
            private set;
            get;
        }

        public IvyBuffer(byte[] buf, int off, int len)
        {
            data = buf;
            offset = off;
            length = len;
        }
    }
}
