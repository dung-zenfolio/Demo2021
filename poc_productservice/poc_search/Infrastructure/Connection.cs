using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_search
{
    public class Connection : Interfaces.IConnection
    {
        private IConfiguration _configuration;
        public ElasticClient _client { get; set; }

        public Connection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OpenConnection()
        {
            var uri = new Uri(_configuration.GetSection("ElasticSearch")["ClusterUrl"]);

            var settings = new ConnectionSettings(uri).DefaultIndex("poc_product");

            _client = new ElasticClient(settings);
        }
    }
}
