﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Texnomic.DNS.Abstractions;
using Texnomic.DNS.Abstractions.Enums;
using Texnomic.DNS.Protocols;
using Texnomic.DNS.Models;
using Texnomic.DNS.Options;

namespace Texnomic.DNS.Tests.Providers.TLS
{
    [TestClass]
    public class Quad9
    {
        private ushort ID;
        private IProtocol Resolver;
        private IMessage RequestMessage;
        private IMessage ResponseMessage;


        [TestInitialize]
        public void Initialize()
        {
            ID = (ushort)new Random().Next();

            var TLSOptions = new TLSOptions()
            {
                Host = "9.9.9.9"
            };

            var TLSOptionsMonitor = Mock.Of<IOptionsMonitor<TLSOptions>>(Options => Options.CurrentValue == TLSOptions);

            Resolver = new Protocols.TLS(TLSOptionsMonitor);

            RequestMessage = new Message()
            {
                ID = ID,
                RecursionDesired = true,
                Questions = new List<IQuestion>()
                {
                    new Question()
                    {
                        Domain = Domain.FromString("google.com"),
                        Class = RecordClass.Internet,
                        Type = RecordType.A
                    }
                }
            };
        }

        [TestMethod]
        public async Task QueryAsync()
        {
            ResponseMessage = await Resolver.ResolveAsync(RequestMessage);

            Assert.AreEqual(ID, ResponseMessage.ID);
            Assert.IsNotNull(ResponseMessage.Questions);
            Assert.IsNotNull(ResponseMessage.Answers);
            Assert.IsInstanceOfType(ResponseMessage.Answers.First().Record, typeof(DNS.Records.A));
        }
    }
}
