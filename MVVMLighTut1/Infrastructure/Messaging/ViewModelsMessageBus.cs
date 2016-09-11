using System;
using GalaSoft.MvvmLight.Messaging;

namespace MVVMLighTut1.Infrastructure.Messaging
{
    public class ViewModelsMessageBus : IDeliverMessagesToViewModels
    {
        public void SendMessage<TMessage>
            (TMessage message) 
            where TMessage : Message
        {
            Messenger.Default.Send(message);
        }

        public void SubscribeTo<TMessage>
            (object subscriber, Action<TMessage> callback)
            where TMessage : Message
        {
            Messenger.Default.Register(subscriber, callback);
        }
    }
}