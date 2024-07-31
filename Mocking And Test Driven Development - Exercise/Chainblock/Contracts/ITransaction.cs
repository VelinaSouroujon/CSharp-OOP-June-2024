namespace TransactionManager.Contracts
{
    public interface ITransaction
    {
        int Id { get; }

        TransactionStatus Status { get; set; }

        string From { get; }

        string To { get; }

        double Amount { get; }
    }
}
