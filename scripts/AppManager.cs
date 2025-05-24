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
    [Export]
    public PackedScene TransactionCategoryLineItem;
    public static Budget currentBudget;
    float totalIncome = 0;
    float totalExpense = 0;
    bool lastTransactionStyleBoxLight;
    VBoxContainer container;
    Dictionary<string, float> catagoryExpenses;
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

    private void AddTransactionToTransaction(string Name, string Date, float Amount, int Type, bool Income)
    {
        Name = Name.Replace("POS Withdrawal - ", "");
        Transaction transaction = new Transaction()
        {
            Name = Name,
            Date = DateTime.Parse(Date).Date,
            Amount = Amount,
            Type = (TransactionType)Type
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
        StyleBox box;
        if (lastTransactionStyleBoxLight)
        {
            box = ResourceLoader.Load<StyleBox>("res://Scenes/transaction_row_dark.tres");
        }
        else
        {
            box = ResourceLoader.Load<StyleBox>("res://Scenes/transaction_row_light.tres");
        }
        lastTransactionStyleBoxLight = !lastTransactionStyleBoxLight;
        AddTransactionToList(transaction, box);

        GetNode<TextureProgressBar>("TotalInE/TextureProgressBar").MaxValue = totalIncome;
        GetNode<TextureProgressBar>("TotalInE/TextureProgressBar").Value = totalExpense;

    }
    private void AddTransactionToList(Transaction transaction, StyleBox box)
    {
        TransactionRow tableRow = TransactionList.Instantiate<TransactionRow>();

        tableRow.AddThemeStyleboxOverride("panel", box);
        tableRow.GetNode<RichTextLabel>("TransactionRow/Date").Text = transaction.Date.ToString("d");
        tableRow.GetNode<RichTextLabel>("TransactionRow/Name").Text = transaction.Name;
        tableRow.GetNode<RichTextLabel>("TransactionRow/Amount").Text = transaction.Amount.ToString();
        tableRow.GetNode<OptionButton>("TransactionRow/Type").Selected = (int)transaction.Type;
        container.AddChild(tableRow);
        tableRow.EditTransaction += onEditTransaction;
    }

    private void onEditTransaction(int currentIndex, int previousIndex, string guid)
    {
        
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
                4, tableRow[4] == "");
        }
    }

    private void SetUpCategories()
    {
        foreach (var item in Enum.GetValues(typeof(TransactionType)))
        {
            Node transactionLineItem = TransactionCategoryLineItem.Instantiate();
            transactionLineItem.GetNode<Label>("Type").Text = item.ToString();
            transactionLineItem.GetNode<Label>("Actual").Text = "0";
            transactionLineItem.GetNode<Label>("Difference").Text = "0";
            GetNode("CategoryTransactionItems/Body").AddChild(transactionLineItem);
        }
    }

    private void UpdateCategoriesValues(Transaction transaction)
    {
        foreach (var item in GetNode("TransactionCategoryItems/Body").GetChildren())
        {
            if (item.GetNode<Label>("Type").Text == transaction.Type.ToString())
            {

            }
        }
    }

}