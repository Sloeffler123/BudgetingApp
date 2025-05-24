using Godot;
using System;

public partial class AddTransactionWindow : Panel
{
    [Signal]
    public delegate void AddTransactionEventHandler(string Name, string Date, float Amount, int Type, bool Income);
    public void _on_cancel_button_down()
    {
        QueueFree();
    }

    public void _on_add_button_down()
    {
        EmitSignal(SignalName.AddTransaction,
            GetNode<LineEdit>("VBoxContainer/Name/LineEdit").Text,
            GetNode<LineEdit>("VBoxContainer/Date/LineEdit").Text,
            float.Parse(GetNode<LineEdit>("VBoxContainer/Amount/LineEdit").Text),
            GetNode<OptionButton>("VBoxContainer/Type/Type").Selected,
            GetNode<CheckButton>("VBoxContainer/IncomeCheckButton/CheckButton").ButtonPressed
        );
        QueueFree();

    }
}
