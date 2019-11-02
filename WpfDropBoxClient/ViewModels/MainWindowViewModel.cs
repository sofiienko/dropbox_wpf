using DropBox;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfDropBoxClient.ViewModels
{

    public class MainWindowViewModel : ViewModelAbs
    {
        private DropBoxService _dropBoxService;


        private List<INode> _nodes;
        public List<INode> Nodes
        {
            get => _nodes;
            set
            {
                _nodes = value;
                NotifyPropertyChanged(nameof(Nodes));
            }
        }

        public MainWindowViewModel()
        {
            _dropBoxService = new DropBoxService("DHX22C_IokAAAAAAAAAALwF4FK8yu4cHqfKsnulc0zfuwiuNqJ9vw-qw1XksqdWc");
            _dropBoxService.GetTree().ContinueWith(t => Nodes = t.Result);
        }



    //    public TreeNode


    }
}
