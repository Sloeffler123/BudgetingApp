using Godot;
using System;

public partial class TransactionRow : Panel
{
    public Transaction CurrentTransaction;

    public int currentIndex;
    public int previousIndex;


    [Signal]
    public delegate void EditTransactionEventHandler(int currentIndex, int previousIndex, string guid);

    public void _on_type_item_focused(int index)
    {
        CurrentTransaction.Type = (TransactionType)index;
        previousIndex = currentIndex;
        currentIndex = index;
        EmitSignal(SignalName.EditTransaction, currentIndex, previousIndex, CurrentTransaction.id.ToString());
    }
}
