using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface INode
    {
        string Id { get; }

        string Name { get; }
        string Path { get; }
        List<INode>  InnerItems{ get; }
        INode ParentItem { get; }
        DateTime DateTime { get; }
    }
}
