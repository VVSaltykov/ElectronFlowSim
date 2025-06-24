using Confluent.Kafka;

namespace ElectronFlowSim.API.Services.Kafka;

public interface IKafkaProducerService
{
    // Основной метод с сериализацией и ключом
    Task ProduceAsync<T>(string topic, string key, T message);

    // Метод без ключа (авто-генерация)
    Task ProduceAsync<T>(string topic, T message);

    // Сырой метод — без сериализации
    Task ProduceRawAsync(string topic, string key, string value);

    // Метод с callback
    Task ProduceAsync<T>(string topic, string key, T message, Action<DeliveryReport<string, string>> deliveryHandler);
}