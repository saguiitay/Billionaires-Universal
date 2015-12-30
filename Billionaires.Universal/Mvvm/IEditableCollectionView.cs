namespace Billionaires.Universal.Mvvm
{
    /// <summary>
    /// Implements a WinRT version of the IEditableCollectionView interface.
    /// </summary>
    public interface IEditableCollectionView
    {
        bool CanAddNew { get; }
        bool CanRemove { get; }
        bool IsAddingNew { get; }
        object CurrentAddItem { get; }
        object AddNew();
        void CancelNew();
        void CommitNew();

        bool CanCancelEdit { get; }
        bool IsEditingItem { get; }
        object CurrentEditItem { get; }
        void EditItem(object item);
        void CancelEdit();
        void CommitEdit();
    }
}