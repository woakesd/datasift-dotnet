﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSift.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System.Net;
using DataSift;
using DataSiftTests.Account;


namespace DataSiftTests
{
    public class MockRestAPIRequest : IRestAPIRequest, IIngestAPIRequest
    {
        public RestAPIResponse Request(string endpoint, dynamic parameters = null, Method method = Method.GET)
        {

            string response = null;
            RestAPIResponse result = new RestAPIResponse();

            IDictionary<string, object> prms = null;
            if(parameters != null) prms = APIHelpers.ParseParameters(endpoint, parameters);

            switch (endpoint)
            {
                case "validate":
                    response = MockAPIResponses.Default.Validate;
                    result.StatusCode = HttpStatusCode.OK;
                    break;
                case "compile":
                    response = MockAPIResponses.Default.Compile;
                    result.StatusCode = HttpStatusCode.OK;
                    break;
                case "usage":
                    response = MockAPIResponses.Default.Usage;
                    result.StatusCode = HttpStatusCode.OK;
                    break;
                case "dpu":
                    response = MockAPIResponses.Default.DPU;
                    result.StatusCode = HttpStatusCode.OK;
                    break;
                case "balance":
                    response = MockAPIResponses.Default.Balance;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pull":

                    return PullRequest(prms);


                case "historics/get":
                    
                    if (prms.ContainsKey("id"))
                        response = MockAPIResponses.Default.HistoricsGetById;
                    else if (prms.ContainsKey("max"))
                        response = MockAPIResponses.Default.HistoricsGetMax1;
                    else if (prms.ContainsKey("with_estimate"))
                        response = MockAPIResponses.Default.HistoricsGetWithCompletion;
                    else
                        response = MockAPIResponses.Default.HistoricsGet;

                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "historics/prepare":
                    response = MockAPIResponses.Default.HistoricsPrepare;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "historics/start":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "historics/stop":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "historics/pause":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "historics/resume":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "historics/delete":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "historics/status":
                    response = MockAPIResponses.Default.HistoricsStatus;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "historics/update":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "push/get":

                    if (prms.ContainsKey("id"))
                        response = MockAPIResponses.Default.PushGetById;
                    else if (prms.ContainsKey("hash"))
                        response = MockAPIResponses.Default.PushGetByHash;
                    else if (prms.ContainsKey("historics_id"))
                        response = MockAPIResponses.Default.PushGetByHistoricsId;
                    else if (prms.ContainsKey("page"))
                        response = MockAPIResponses.Default.PushGetPage;
                    else if (prms.ContainsKey("per_page"))
                        response = MockAPIResponses.Default.PushGetPage;
                    else
                        response = MockAPIResponses.Default.PushGet;

                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "push/create":
                    response = MockAPIResponses.Default.PushCreate;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "push/delete":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "push/stop":
                    response = MockAPIResponses.Default.PushStop;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "push/pause":
                    response = MockAPIResponses.Default.PushPause;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "push/resume":
                    response = MockAPIResponses.Default.PushResume;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "push/log":

                    if (prms.ContainsKey("id"))
                        response = MockAPIResponses.Default.PushLogById;
                    else if (prms.ContainsKey("page"))
                        response = MockAPIResponses.Default.PushLogPage;
                    else if (prms.ContainsKey("per_page"))
                        response = MockAPIResponses.Default.PushLogPage;
                    else
                        response = MockAPIResponses.Default.PushLog;

                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "push/update":
                    response = MockAPIResponses.Default.PushUpdate;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "preview/get":
                    var id = (string)prms.First(p => p.Key == "id").Value;

                    switch (id)
                    {
                        case "e25d533cf287ec44fe66e8362running":
                            response = MockAPIResponses.Default.HistoricsPreviewRunning;
                            result.StatusCode = HttpStatusCode.Accepted;
                            break;
                        case "e25d533cf287ec44fe66e8362finished":
                            response = MockAPIResponses.Default.HistoricsPreviewFinished;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                    }
                    break;

                case "preview/create":
                    response = MockAPIResponses.Default.HistoricsPreviewCreate;
                    result.StatusCode = HttpStatusCode.Accepted;
                    break;

                case "source/get":

                    if (prms.ContainsKey("id"))
                        response = MockAPIResponses.Default.SourceGetById;
                    else if (prms.ContainsKey("page"))
                        response = MockAPIResponses.Default.SourceGetPage;
                    else if (prms.ContainsKey("per_page"))
                        response = MockAPIResponses.Default.SourceGetPage;
                    else
                        response = MockAPIResponses.Default.SourceGet;

                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "source/create":
                    response = MockAPIResponses.Default.SourceCreate;
                    result.StatusCode = HttpStatusCode.Created;
                    break;

                case "source/delete":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "source/start":
                    response = MockAPIResponses.Default.SourceStart;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "source/stop":
                    response = MockAPIResponses.Default.SourceStop;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "source/update":
                    response = MockAPIResponses.Default.SourceUpdate;
                    result.StatusCode = HttpStatusCode.Accepted;
                    break;

                case "source/log":

                    if (prms.ContainsKey("page"))
                        response = MockAPIResponses.Default.SourceLogPage;
                    else if (prms.ContainsKey("per_page"))
                        response = MockAPIResponses.Default.SourceLogPage;
                    else
                        response = MockAPIResponses.Default.SourceLog;

                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "source/resource/add":
                    response = MockAPIResponses.Default.SourceResourceAdd;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "source/resource/remove":
                    response = MockAPIResponses.Default.SourceResourceRemove;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "source/auth/add":
                    response = MockAPIResponses.Default.SourceAuthAdd;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "source/auth/remove":
                    response = MockAPIResponses.Default.SourceAuthRemove;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pylon/facebook/get":
                    if (prms.ContainsKey("id"))
                        response = PylonAPIResponses.Default.GetOne;
                    else
                        response = PylonAPIResponses.Default.Get;
                    
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pylon/facebook/validate":
                    response = PylonAPIResponses.Default.Validate;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pylon/facebook/compile":
                    response = PylonAPIResponses.Default.Compile;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pylon/facebook/start":
                    response = PylonAPIResponses.Default.Start;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pylon/facebook/stop":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "pylon/facebook/update":
                    result.StatusCode = HttpStatusCode.NoContent;
                    break;

                case "pylon/facebook/analyze":
                    var analyze_id = (string)prms.First(p => p.Key == "id").Value;

                    if (analyze_id.IndexOf("nested") != -1)
                    {
                        response = PylonAPIResponses.Default.AnalyzeNested;
                        result.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response = PylonAPIResponses.Default.Analyze;
                        result.StatusCode = HttpStatusCode.OK;
                    }
                    break;

                case "pylon/facebook/tags":
                    response = PylonAPIResponses.Default.Tags;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pylon/facebook/sample":
                    response = PylonAPIResponses.Default.Sample;
                    result.StatusCode = HttpStatusCode.OK;
                    break;

                case "pylon/" + Pylon.Task.VALID_SERVICE + "/task":
                    if(method == Method.POST)
                    {
                        response = Pylon.TaskAPIResponses.Default.Create;
                        result.StatusCode = HttpStatusCode.Created;
                    }

                    break;
                    
                case "pylon/" + Pylon.Task.VALID_SERVICE + "/task/" + Pylon.Task.VALID_TYPE:
                    if (method == Method.GET)
                    {
                        response = Pylon.TaskAPIResponses.Default.GetAll;
                        result.StatusCode = HttpStatusCode.OK;
                    }

                    break;

                case "pylon/" + Pylon.Task.VALID_SERVICE + "/task/" + Pylon.Task.VALID_TYPE + "/" + Pylon.Task.VALID_TASK_ID:
                    switch (method)
                    {
                        case Method.GET:
                            response = Pylon.TaskAPIResponses.Default.GetOne;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                    }

                    break;

                case "pylon/" + Pylon.Reference.VALID_SERVICE + "/reference":
                    if (method == Method.GET)
                    {
                        if (prms.ContainsKey("per_page") || prms.ContainsKey("page"))
                            response = Pylon.ReferenceDataAPIResponses.Default.GetAllSmallPage;
                        else
                            response = Pylon.ReferenceDataAPIResponses.Default.GetAll;

                        result.StatusCode = HttpStatusCode.OK;
                    }

                    break;

                case "pylon/" + Pylon.Reference.VALID_SERVICE + "/reference/" + Pylon.Reference.VALID_SLUG:
                    if (method == Method.GET)
                    {
                        response = Pylon.ReferenceDataAPIResponses.Default.Functions;
                        result.StatusCode = HttpStatusCode.OK;
                    }

                    break;

                case "account/usage":
                    response = AccountAPIResponses.Default.Account_Usage;
                    result.StatusCode = HttpStatusCode.OK;
                    break;
                    
                case "account/identity":
                    switch (method) {
                        case Method.POST:
                            response = AccountAPIResponses.Default.Identity_Create;
                            result.StatusCode = HttpStatusCode.Created;
                            break;
                        case Method.GET:
                            if (prms.ContainsKey("label"))
                                response = AccountAPIResponses.Default.Identity_Get_Label;
                            else if (prms.ContainsKey("page") || prms.ContainsKey("per_page"))
                                response = AccountAPIResponses.Default.Identity_Get_Page;
                            else
                                response = AccountAPIResponses.Default.Identity_Get;

                            result.StatusCode = HttpStatusCode.OK;
                            break;
                    }
                    
                    break;


                case "account/identity/" + Account.Identity.VALID_ID:
                    switch (method)
                    {
                        case Method.GET:
                            response = AccountAPIResponses.Default.Identity_Get_Id;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                        case Method.PUT:
                            response = AccountAPIResponses.Default.Identity_Update;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                        case Method.DELETE:
                            result.StatusCode = HttpStatusCode.NoContent;
                            break;
                    }

                    break;

                case "account/identity/" + Account.Token.VALID_IDENTITY + "/token":
                    switch (method)
                    {
                        case Method.POST:
                            response = AccountAPIResponses.Default.Token_Create;
                            result.StatusCode = HttpStatusCode.Created;
                            break;
                        case Method.GET:
                            if (prms.ContainsKey("service"))
                                response = AccountAPIResponses.Default.Token_GetAll;
                            else if (prms.ContainsKey("page") || prms.ContainsKey("per_page"))
                                response = AccountAPIResponses.Default.Token_GetAll;
                            else
                                response = AccountAPIResponses.Default.Token_GetAll;

                            result.StatusCode = HttpStatusCode.OK;
                            break;
                    }

                    break;

                case "account/identity/" + Account.Token.VALID_IDENTITY + "/token/" + Account.Token.VALID_SERVICE:
                    switch (method)
                    {
                        case Method.GET:
                            response = AccountAPIResponses.Default.Token_GetAll;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                        case Method.PUT:
                            response = AccountAPIResponses.Default.Token_Update;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                        case Method.DELETE:
                            result.StatusCode = HttpStatusCode.NoContent;
                            break;
                    }

                    break;


                case "account/identity/" + Account.Limit.VALID_IDENTITY + "/limit":
                    switch (method)
                    {
                        case Method.POST:
                            response = AccountAPIResponses.Default.Limit_Create;
                            result.StatusCode = HttpStatusCode.Created;
                            break;
                    }

                    break;

                case "account/identity/" + Account.Limit.VALID_IDENTITY + "/limit/" + Account.Token.VALID_SERVICE:
                    switch (method)
                    {
                        case Method.GET:
                            response = AccountAPIResponses.Default.Limit_GetByIdentityAndService;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                        case Method.PUT:
                            response = AccountAPIResponses.Default.Limit_Update;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                        case Method.DELETE:
                            result.StatusCode = HttpStatusCode.NoContent;
                            break;
                    }

                    break;

                case "account/identity/limit/" + Account.Token.VALID_SERVICE:
                    switch (method)
                    {
                        case Method.GET:
                            response = AccountAPIResponses.Default.Limit_AllByService;
                            result.StatusCode = HttpStatusCode.OK;
                            break;
                    }

                    break;
            }

            if(response != null)
            {
                result.Data = APIHelpers.DeserializeResponse(response);
            }
            
            return result;
            
        }

        public PullAPIResponse PullRequest(IDictionary<string, object> prms)
        {
            string response = null;
            PullAPIResponse result = new PullAPIResponse() { PullDetails = new PullInfo() };

            var id = (string)prms.First(p => p.Key == "id").Value;

            switch (id)
            {
                case "08b923395b6ce8bfa4d96f57jsonmeta":
                    response = MockAPIResponses.Default.PullJsonMetaFormat;
                    result.PullDetails.Format = "json_meta";
                    break;
                case "08b923395b6ce8bfa4d96f5jsonarray":
                    response = MockAPIResponses.Default.PullJsonArrayFormat;
                    result.PullDetails.Format = "json_array";
                    break;
                case "08b923395b6ce8bfa4d96jsonnewline":
                    response = MockAPIResponses.Default.PullJsonNewLineFormat;
                    result.PullDetails.Format = "json_new_line";
                    break;
            }

            result.StatusCode = HttpStatusCode.OK;

            if (response != null)
            {
                result.Data = APIHelpers.DeserializeResponse(response, result.PullDetails.Format);
            }

            return result;
        }

        public RestAPIResponse Ingest(string endpoint, dynamic data, Method method = Method.POST)
        {
            string response = null;
            RestAPIResponse result = new RestAPIResponse();

            string body = APIHelpers.SerializeToJsonLD(data);

            response = "{\"accepted\":1, \"total_message_bytes\": 1691 }";
            
            result.StatusCode = HttpStatusCode.OK;
            result.Data = APIHelpers.DeserializeResponse(response);

            return result;
        }
    }
}
