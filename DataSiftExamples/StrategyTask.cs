﻿using DataSift;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace DataSiftExamples
{
    static class StrategyTask
    {
        // Note that to run the example you must use an API key which corresponds to a valid identity with Media Strategies API access
        internal static void Run(string username, string apikey)
        {
            var client = new DataSiftClient(username, apikey);

            // TODO: Insert the service, recording id and strategy parameters you'd like to use
            var service = "";
            var recordingId = "";

            dynamic parameters = new
            {
                strategy = "top_urls",
                version = 1, 
                parameters = new
                {
                    keywords = new
                    {
                        any = new[] { "cloud", "platform as a service", "paas", "software as a service", "saas", "azure", "aws", "amazon web services", "vmware" }
                    },
                    audience = new
                    {
                        countries = new[] {"united states"},
                        seniorities = new[] { "manager", "director", "vp", "cxo", "partner", "owner" }
                    },
                    comparison_audience = new
                    {
                        country = "united states"
                    }
                }
            };

            Console.WriteLine("Running 'Strategy tasks' example...");

            var get = client.Pylon.Task.Get(service, "strategy");
            Console.WriteLine("\nCurrent list of tasks: " + JsonConvert.SerializeObject(get.Data));

            var create = client.Pylon.Task.Create(service, recordingId, "New insight task", "strategy", parameters);
            Console.WriteLine("\nCreated task: " + JsonConvert.SerializeObject(create.Data));

            var getOne = client.Pylon.Task.Get(service, "strategy", taskId: create.Data.id);
            Console.WriteLine("\nGot task: " + JsonConvert.SerializeObject(getOne.Data));

            while (getOne.Data.status != "completed")
            {
                Thread.Sleep(2000); // Wait for 2 seconds before checking again
                getOne = client.Pylon.Task.Get(service, "strategy", taskId: create.Data.id);
                Console.WriteLine("\nStatus: " + getOne.Data.status);
            }

            Console.WriteLine("\nResult: " + JsonConvert.SerializeObject(getOne.Data));

        }
    }
}
