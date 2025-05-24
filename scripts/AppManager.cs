using Godot;
using System;
using System.Collections.Generic;
using System.IO;

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
        container = GetNode<VBoxContainer>("TransactionList/ScrollContainer/TransactionList");
        LoadCSV("C:\\Users\\samlo\\Downloads\\AccountHistory.csv");
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
            GetNode<RichTextLabel>("TotalInE/Income/IncomeAmount").Text = "[center][b]" + totalIncome.ToString();
        }
        else
        {
            currentBudget.Expenses.Add(transaction);
            totalExpense += transaction.Amount;
            GetNode<RichTextLabel>("TotalInE/Expense/ExpenseAmount").Text = "[center][b]" + totalExpense.ToString();

        }
        AddTransactionToList(transaction);

        GetNode<TextureProgressBar>("TotalInE/TextureProgressBar").MaxValue = totalIncome;
        GetNode<TextureProgressBar>("TotalInE/TextureProgressBar").Value = totalExpense;

    }
    private void AddTransactionToList(Transaction transaction)
    {
        Node tableRow = TransactionList.Instantiate();
        tableRow.GetNode<RichTextLabel>("TransactionRow/Date").Text = transaction.Date.ToString("d");
        tableRow.GetNode<RichTextLabel>("TransactionRow/Name").Text = transaction.Name;
        tableRow.GetNode<RichTextLabel>("TransactionRow/Amount").Text = transaction.Amount.ToString();
        tableRow.GetNode<RichTextLabel>("TransactionRow/Type").Text = transaction.Type;
        container.AddChild(tableRow);
    }

    private void LoadCSV(string path)
    {
        List<string[]> parsedData = new List<string[]>();
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] fields = line.Split(',');
                parsedData.Add(fields);
            }
        }
        foreach (var tableRow in parsedData)
        {
            if (tableRow[0] == "Account Number")
            {
                continue;
            }
            AddTransactionToTransaction(tableRow[3], tableRow[1],
                tableRow[4] != "" ? float.Parse(tableRow[4]) : float.Parse(tableRow[5]),
                "Home", tableRow[4] == "");  
        }
    }
}