using Godot;
using System;
using System.Collections.Generic;
public partial class AppManager : Control
{
    Budget currentBudget;
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
    }
}
