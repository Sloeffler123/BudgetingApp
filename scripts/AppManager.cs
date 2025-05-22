using Godot;
using System;
using System.Collections.Generic;

public partial class AppManager : Control
{
    [Export]
    public PackedScene TransactionList;
    [Export]
    public PackedScene AddTransactionMenu;
    public static Budget currentBudget;
    public override void _Ready()
    {
        currentBudget = new Budget()
        {
            Name = "current budget",
            Expenses = new List<Transaction>(),
            Income = new List<Transaction>()
        };
        currentBudget.Expenses.Add(new Transaction()
        {
            Name = "test",
            Date = DateTime.Now,
            Amount = 50,
            Type = "home"
        });
        currentBudget.Expenses.Add(new Transaction()
        {
            Name = "test2",
            Date = DateTime.Now,
            Amount = 501,
            Type = "home"
        });
        currentBudget.Income.Add(new Transaction()
        {
            Name = "paycheck 1",
            Date = DateTime.Now,
            Amount = 2000,
            Type = "home"
        });
        VBoxContainer container = GetNode<VBoxContainer>("ScrollContainer/TransactionList");
        float totalExpense = 0;
        foreach (var value in currentBudget.Expenses)
        {
            Node tableRow = TransactionList.Instantiate();
            tableRow.GetNode<RichTextLabel>("Date").Text = value.Date.ToString();
            tableRow.GetNode<RichTextLabel>("Name").Text = value.Name;
            tableRow.GetNode<RichTextLabel>("Amount").Text = value.Amount.ToString();
            tableRow.GetNode<RichTextLabel>("Type").Text = value.Type;
            container.AddChild(tableRow);
            totalExpense += value.Amount;
        }
        GetNode<RichTextLabel>("TotalInE/ExpenseAmount").Text = "[center][b]" + totalExpense.ToString();
        float totalIncome = 0;
        foreach (var value in currentBudget.Income)
        {
            Node tableRow = TransactionList.Instantiate();
            tableRow.GetNode<RichTextLabel>("Date").Text = value.Date.ToString();
            tableRow.GetNode<RichTextLabel>("Name").Text = value.Name;
            tableRow.GetNode<RichTextLabel>("Amount").Text = value.Amount.ToString();
            tableRow.GetNode<RichTextLabel>("Type").Text = value.Type;
            container.AddChild(tableRow);
            totalIncome += value.Amount;
        }
        GetNode<RichTextLabel>("TotalInE/IncomeAmount").Text = "[center][b]" + totalIncome.ToString();
    }

    public void _on_add_transaction_button_down()
    {
        Node transactionMenu = AddTransactionMenu.Instantiate();
        AddChild(transactionMenu);
    }
}

