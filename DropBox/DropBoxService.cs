using Dropbox.Api.Files;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropBox
{
    public class DropBoxService
    {
        protected readonly Dropbox.Api.DropboxClient _client;

        public DropBoxService(string token)
        {
            _client = new Dropbox.Api.DropboxClient(token);
        }

        public async Task<List<INode>> GetTree()
        {
            var responseItems = await _client.Files.ListFolderAsync(path: String.Empty,
                                    recursive: true,
                                    includeMediaInfo: false,
                                    includeDeleted: false,
                                    includeHasExplicitSharedMembers: false,
                                    includeMountedFolders: true,
                                    limit: null,
                                    sharedLink: null,
                                    includePropertyGroups: null,
                                    includeNonDownloadableFiles: true);

            if (responseItems?.Entries == null) throw new Exception("Null data");

            List<INode> nodes = new List<INode>();

            var responseNodes = responseItems.Entries.OrderBy(i => i, new SortComparerByProjectType()).ToList();
            foreach (var item in responseNodes)
            {
                var node = ConvertToINode(item, nodes);
                if(node.ParentItem == null)
                {
                    nodes.Add(node);
                }
            }

            return nodes;
        }


        private INode ConvertToINode(Dropbox.Api.Files.Metadata metadata, List<INode> filledNodes)
        {
            if (metadata.IsFile)
            {
                var file = new File
                {
                    Id = metadata.AsFile.Id,
                    Name = metadata.AsFile.Name,
                    Path = metadata.PathLower,
                    InnerItems = null
                };

                file.ParentItem = GetParent(file, filledNodes);

                return file;
            }

            else if (metadata.IsFolder)
            {
                var folder = new Folder
                {
                    Id = metadata.AsFolder.Id,
                    Name = metadata.AsFolder.Name,
                    Path = metadata.PathLower,
                    InnerItems = new List<INode>(),
                };

                folder.ParentItem = GetParent(folder, filledNodes);
                return folder;
            }
            else throw new Exception("Item is not file and not folder");
        }

        private INode GetParent(INode node, List<INode> filledNodes)
        {
            var startIndex = node.Path.IndexOf("/" + node.Name.ToLower());
            if (startIndex == 0) return null;

            var path = node.Path.Substring(0, startIndex);
            var parent = filledNodes.FirstOrDefault(i => i.Path == path);
            if (parent != null)
            {
                parent.InnerItems.Add(node);
            }

            return parent;
        }

    }
}
