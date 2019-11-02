﻿using System;
using System.Collections.Generic;

namespace Models
{
    public class File : INode
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsNode { get; set; }

        public DateTime DateTime { get; set; }

        public List<INode> InnerItems { get; set; }

        public INode ParentItem { get; set; }
    }


}
