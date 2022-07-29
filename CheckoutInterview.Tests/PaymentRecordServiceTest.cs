using CheckoutInterview.Repositories;

namespace CheckoutInterview.Tests;

public class Tests
{
    private IPaymentRecordService paymentRecordService;
    private IPaymentRecordRepository paymentRecordRepository;

    [SetUp]
    public void Setup()
    {
        paymentRecordRepository = new Mock<PaymentRecorRepository>();
        paymentRecordService = new PaymentRecordService()
    }

    [Test]
    public void ValidatesInput()
    {
        paymentRecordService.Insert()
    }

    [Test]
    public void CanCreatePaymentRecord()
    {

    }

    [Test]
    public void CanRetrievePaymentRecord()
    {

    }
}
