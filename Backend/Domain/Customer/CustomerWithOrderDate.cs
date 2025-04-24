namespace Domain.Customer.Repositories;

public class CustomerWithOrderDate
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime LastOrderDate { get; set; }
    public DateTime NextPredictedOrder { get; set; }
}