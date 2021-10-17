using System;
using System.Collections.Generic;
using System.Text;

namespace poc_manager.Interfaces
{
    public interface IRabbitConnection
    {
        void OpenConnection();
        void CreateQueue();
    }
}
