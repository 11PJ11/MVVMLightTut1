using System;

namespace MVVMLighTut1.Infrastructure.Messaging
{
    public interface IDeliverMessagesToViewModels
    {
        void SendMessage<TMessage>(TMessage message) where TMessage : Message;
        void SubscribeTo<TMessage>(object subscriber, Action<TMessage> callback) where TMessage : Message;
    }
}