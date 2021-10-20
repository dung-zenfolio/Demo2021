using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_search.Interfaces
{
    public interface IConnection
    {
        void OpenConnection();

        ElasticClient _client { get; }
    }
}
