﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02.Graphic_Editor
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
    }
}
