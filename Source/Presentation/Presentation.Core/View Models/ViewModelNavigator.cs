using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Auxilia.Delegation.Commands;

namespace Auxilia.Presentation.ViewModels
{
    //public class ViewModelNavigator : ObservableObject, IEnumerable<ViewModelBase>
    //{
    //    private readonly List<ViewModelBase> _children = new List<ViewModelBase>();
    //    private ViewModelBase _current;

    //    public ViewModelNavigator(ViewModelBase owner)
    //    {
    //        Owner = owner;
    //    }

    //    public ViewModelBase Owner { get; }
    //    public IReadOnlyList<ViewModelBase> Children
    //    {
    //        get => _children.AsReadOnly();
    //    }

    //    public ViewModelBase Current
    //    {
    //        get => _current;
    //        private set => SetProperty(ref _current, value);
    //    }

    //    public ICommand ChangeViewModelCommand
    //    {
    //        get => new ActionCommand<ViewModelBase>(
    //            "Change View Model",
    //            "Change the current view.",
    //            SetCurrent,
    //            _children.Contains);
    //    }

    //    public ViewModelBase this[string name]
    //    {
    //        get => Children.FirstOrDefault(v => v.Name == name);
    //    }
    //    public ViewModelBase this[int index]
    //    {
    //        get => Children.ElementAtOrDefault(index);
    //    }

    //    public void Add(ViewModelBase child)
    //    {
    //        if (this[child.Name] != null)
    //            throw new ArgumentException($"View model with the name '{child.Name}' already exists.", nameof(child));

    //        _children.Add(child);
    //    }

    //    public void SetCurrent(string viewModelName)
    //    {
    //        SetCurrent(this[viewModelName]);
    //    }
    //    public void SetCurrent(ViewModelBase viewModel)
    //    {
    //        if (viewModel == null)
    //        {
    //            Current = null;
    //            return;
    //        }

    //        if (!Children.Contains(viewModel))
    //            Add(viewModel);

    //        viewModel.Update();
    //        Current = viewModel;
    //        RaisePropertyChanged(nameof(Children));
    //    }

    //    public IEnumerator<ViewModelBase> GetEnumerator()
    //    {
    //        return _children.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}
