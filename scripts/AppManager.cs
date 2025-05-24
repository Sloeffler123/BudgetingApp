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
    float totalIncome = 0;
    float totalExpense = 0;
    VBoxContainer container;
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
            Date = DateTime.Now.Date,
            Amount = 50,
            Type = "home"
        });

        currentBudget.Expenses.Add(new Transaction()
        {
            Name = "test2",
            Date = DateTime.Now.Date,
            Amount = 501,
            Type = "home"
        });

        currentBudget.Income.Add(new Transaction()
        {
            Name = "paycheck 1",
            Date = DateTime.Now.Date,
            Amount = 2000,
            Type = "home"
        });

        container = GetNode<VBoxContainer>("ScrollContainer/TransactionList");

        foreach (var value in currentBudget.Expenses)
        {
            Node tableRow = TransactionList.Instantiate();
            tableRow.GetNode<RichTextLabel>("Date").Text = value.Date.ToString("d");
            tableRow.GetNode<RichTextLabel>("Name").Text = value.Name;
            tableRow.GetNode<RichTextLabel>("Amount").Text = value.Amount.ToString();
            tableRow.GetNode<RichTextLabel>("Type").Text = value.Type;
            container.AddChild(tableRow);
            totalExpense += value.Amount;
        }

        GetNode<RichTextLabel>("TotalInE/ExpenseAmount").Text = "[center][b]" + totalExpense.ToString();

        foreach (var value in currentBudget.Income)
        {
            Node tableRow = TransactionList.Instantiate();
            tableRow.GetNode<RichTextLabel>("Date").Text = value.Date.ToString("d");
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
        AddTransactionWindow transactionMenu = AddTransactionMenu.Instantiate<AddTransactionWindow>();
        AddChild(transactionMenu);
        transactionMenu.AddTransaction += AddTransactionToTransaction;
    }

    private void AddTransactionToTransaction(string Name, string Date, float Amount, string Type, bool Income)
    {
        Transaction transaction = new Transaction()
        {
            Name = Name,
            Date = DateTime.Parse(Date).Date,
            Amount = Amount,
            Type = Type
        };
        if (Income)
        {
            currentBudget.Income.Add(transaction);
            totalIncome += transaction.Amount;
            GetNode<RichTextLabel>("TotalInE/IncomeAmount").Text = "[center][b]" + totalIncome.ToString();
        }
        else
        {
            currentBudget.Expenses.Add(transaction);
            totalExpense += transaction.Amount;
            GetNode<RichTextLabel>("TotalInE/ExpenseAmount").Text = "[center][b]" + totalExpense.ToString();

        }
        AddTransactionToList(transaction);
        
    }
    private void AddTransactionToList(Transaction transaction)
    {
        Node tableRow = TransactionList.Instantiate();
        tableRow.GetNode<RichTextLabel>("Date").Text = transaction.Date.ToString("d");
        tableRow.GetNode<RichTextLabel>("Name").Text = transaction.Name;
        tableRow.GetNode<RichTextLabel>("Amount").Text = transaction.Amount.ToString();
        tableRow.GetNode<RichTextLabel>("Type").Text = transaction.Type;
        container.AddChild(tableRow);
    }
}