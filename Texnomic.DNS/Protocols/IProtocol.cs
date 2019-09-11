﻿using System;
using System.Threading.Tasks;
using Texnomic.DNS.Models;

namespace Texnomic.DNS.Protocols
{
    public interface IProtocol : IDisposable
    {
        byte[] Resolve(byte[] Query);
        Message Resolve(Message Query);

        Task<byte[]> ResolveAsync(byte[] Query);
        Task<Message> ResolveAsync(Message Query);
    }
}