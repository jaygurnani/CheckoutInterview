using CheckoutInterview.Models;
using CheckoutInterview.Repositories;
using CheckoutInterview.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CheckoutInterview.Tests;

public class Tests
{
    private IPaymentRecordService paymentRecordService;
    private Mock<IPaymentRecordRepository> paymentRecordRepository;
    private Mock<IBankService> bankService;
    private Mock<ILogger<PaymentRecordService>> logger;
    private int mockMerchantId;
    private int mockPaymentRecordId;

    private Payment mockPayment;
    private PaymentRecord mockPaymentRecord;

    [SetUp]
    public void Setup()
    {
        paymentRecordRepository = new Mock<IPaymentRecordRepository>();
        bankService = new Mock<IBankService>();
        logger = new Mock<ILogger<PaymentRecordService>>();
        mockPayment = new Payment
        {
            Amount = 100,
            CreditCardNumber = "4844832146549",
            Currency = Currency.USD,
            CVV = "123",
            ExpiryMonth = "01",
            ExpiryYear = "12",
        };
        mockMerchantId = 1;
        mockPaymentRecordId = 20;
        mockPaymentRecord = new PaymentRecord
        {
            IsSuccess = true,
            MerchantId = mockMerchantId,
            PaymentRecordId = mockPaymentRecordId,
            Payment = mockPayment
        };

        paymentRecordService = new PaymentRecordService(paymentRecordRepository.Object, bankService.Object, logger.Object);
    }

    [Test]
    public void ValidatesInput()
    {
        // Given
        var invalidCreditCard = "0000000000000001";

        // When 
        mockPayment.CreditCardNumber = invalidCreditCard;


        // Then
        Assert.Throws<ApplicationException>(() => paymentRecordService.Insert(mockMerchantId, mockPayment));
    }

    [Test]
    public void CanCreatePaymentRecord()
    {
        // Given
        paymentRecordRepository.Setup(
            x => x.Insert(
                It.IsAny<int>(),
                It.IsAny<Payment>(),
                It.IsAny<bool>())).Returns(mockPaymentRecordId);

        // When
        int result = paymentRecordService.Insert(mockMerchantId, mockPayment);

        // Then
        Assert.That(mockPaymentRecordId, Is.EqualTo(result));
    }

    [Test]
    public void CanRetrievePaymentRecord()
    {
        // Given
        int? nextPaymentRecordId = 21;
        paymentRecordRepository.Setup(x => x.GetPaymentRecord(
            It.IsAny<int>(),
            It.IsAny<int>())).Returns(Tuple.Create<PaymentRecord, int?>(mockPaymentRecord, nextPaymentRecordId));

        // When
        var result = paymentRecordService.GetPaymentRecord(mockMerchantId, mockPaymentRecordId);

        // Then
        Assert.That(mockPaymentRecord, Is.EqualTo(result.Item1));
        Assert.That(nextPaymentRecordId, Is.EqualTo(result.Item2));
    }
}
