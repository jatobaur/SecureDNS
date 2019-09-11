﻿using System;
using System.Net;
using System.Threading.Tasks;
using PipelineNet.Middleware;
using Texnomic.DNS.Models;
using Texnomic.DNS.Protocols;

namespace Texnomic.DNS.Servers.Middlewares
{
    public class TLSMiddleware : TLS, IAsyncMiddleware<Message, Message>
    {
        public TLSMiddleware(IPAddress IPAddress, string PublicKey) : base(IPAddress, PublicKey) { }

        public async Task<Message> Run(Message Message, Func<Message, Task<Message>> Next)
        {
            //var Response = await ResolveAsync(Message);

            //return await Next(Response);

            return await ResolveAsync(Message);
        }
    }
}