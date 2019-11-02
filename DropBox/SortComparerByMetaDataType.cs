using Dropbox.Api.Files;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DropBox
{
    public class SortComparerByProjectType : IComparer<Metadata>
    {

        public int Compare(Metadata xMeta, Metadata yMeta)
        {

            if (xMeta.IsFile && yMeta.IsFile) return 0;
            else if (xMeta.IsFolder && yMeta.IsFile) return -1;
            else if (xMeta.IsFolder && yMeta.IsFolder) return 0;
            else if (xMeta.IsFile && yMeta.IsFolder) return 1;
            else throw new Exception("Unexpected");
        }
    }
}
