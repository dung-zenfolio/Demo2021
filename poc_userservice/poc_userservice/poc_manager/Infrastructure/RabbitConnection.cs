using RabbitMQ.Client;
using System.Text;
using poc_manager.Interfaces;
using RabbitMQ.Client.Events;
using poc_service.services.Interfaces;
using poc_resource.DTO;
using Newtonsoft.Json;

namespace poc_manager.Infrastructure
{
    public class RabbitConnection : IRabbitConnection
    {
        private IConnection _connection;
        private IModel _channel;
        private IUserService _userService;

        public RabbitConnection(IUserService userService)
        {
            _userService = userService;
        }

        public void OpenConnection()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            CreateQueue();
        }

        public void CreateQueue()
        {
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "UserQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            Receive();
            //}
        }

        public void Publish(object message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            _channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
        }

        public void Receive()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _userService.AddNewUserAsync(JsonConvert.DeserializeObject<UserDto>(message));
            };

            _channel.BasicConsume(queue: "UserQueue",
                                 autoAck: true,
                                 consumer: consumer);

        }
    }
}
