using Godot;
using System;

public partial class AddTransactionWindow : Panel
{
    public void _on_cancel_button_down()
    {
        QueueFree();
    }

    public void _on_add_button_down()
    {
        if (GetNode<CheckButton>("VBoxContainer/IncomeCheckButton/CheckButton").ButtonPressed == true)
        {
            AppManager.currentBudget.Income.Add(new Transaction()
            {
                Name = GetNode<LineEdit>("VBoxContainer/Name/LineEdit").Text,
                Date = DateTime.Parse(GetNode<LineEdit>("VBoxContainer/Date/LineEdit").Text),
                Amount = int.Parse(GetNode<LineEdit>("VBoxContainer/Amount/LineEdit").Text),
                Type = GetNode<LineEdit>("VBoxContainer/Type/LineEdit").Text,
            });
            QueueFree();
        }
        else
        {
            AppManager.currentBudget.Expenses.Add(new Transaction()
            {
                Name = GetNode<LineEdit>("VBoxContainer/Name/LineEdit").Text,
                Date = DateTime.Parse(GetNode<LineEdit>("VBoxContainer/Date/LineEdit").Text),
                Amount = int.Parse(GetNode<LineEdit>("VBoxContainer/Amount/LineEdit").Text),
                Type = GetNode<LineEdit>("VBoxContainer/Type/LineEdit").Text,
            });
            QueueFree();
        }
    }
}
