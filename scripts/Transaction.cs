using System;

public class Transaction
{
    public Guid id = Guid.NewGuid();
    public string Name;
    public float Amount;
    public DateTime Date;
    public TransactionType Type;
}
