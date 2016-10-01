# What's this about?

It's some small code that shows a potential binding problem when using MahApps.Metro's "DropDownButton" control and how I got around it.

It uses MvvmLight (for `ViewModelBase`, which it honestly doesn't need) in addition to MahApps.Metro

# Basic idea
 - `DropDownButton` creates a Popup when you click on it, similarly to WPF's built-in `ContextMenu` control
 - All Popups in WPF have their own separate visual trees which means you can't get "back to" the DropDownButton's DataContext by doing `FindAncestor` in the binding.
 - I wanted to databind a `DropDownButton` to an `ObservableCollection` of viewmodels and have each item in the `DropDownButton`'s menu execute a command on the owning viewmodel when clicked.
 - The solution was to use `PlacementTarget`, which is provided by `ContextMenu`, but it was not immediately obvious that `DropDownButton` creates a `ContextMenu`
 - Therefore, just retarget your `FindAncestor` to find the `ContextMenu` at the root of the drop-down menu's popup
 - Then you can use `ContextMenu.PlacementTarget` to find the control that the context-menu popup is attached to, which turns out to be the `DropDownButton`.
 
# Other references
 - The [MahApps.Metro issue](https://github.com/MahApps/MahApps.Metro/issues/2181) where I posted this problem and solution
 - How to do the same binding in `ContextMenu`, with answers explaining [how `PlacementTarget` works](http://stackoverflow.com/questions/15033522/wpf-contextmenu-woes-how-do-i-set-the-datacontext-of-the-contextmenu)
 
# List of Files
Most files are unimportant except these.
 - [`MainWindow.xaml`](https://github.com/barbeque/DropDownButtonBindingBug/blob/master/MainWindow.xaml): The XAML view which describes the `DropDownButton` and sets up the bindings.
 - [`MainWindowViewModel.cs`](https://github.com/barbeque/DropDownButtonBindingBug/blob/master/MainWindowViewModel.cs): The view model which contains the ObservableCollection holding entities
 - [`EntityViewModel.cs`](https://github.com/barbeque/DropDownButtonBindingBug/blob/master/EntityViewModel.cs): The view model which defines an entity to display in the menu
 - [`App.xaml`](https://github.com/barbeque/DropDownButtonBindingBug/blob/master/App.xaml): Very simple example of a merged ResourceDictionary for bootstrapping a MahApps.Metro app from the nuget package.
