﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Aliyun.FunctionCompute.SDK.Config;
using Aliyun.FunctionCompute.SDK.Constants;
using RestSharp;

namespace Aliyun.FunctionCompute.SDK.Request
{
    public class GetFunctionRequest : RequestBase
    {

        public string ServiceName { get; set; }
        public string FunctionName { get; set; }
        public string Qualifier { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public GetFunctionRequest(string serviceName, string functionName, string qualifier = null, Dictionary<string, string> customHeaders = null)
        {
            Contract.Requires(string.IsNullOrEmpty(functionName) == false);
            Contract.Requires(string.IsNullOrEmpty(serviceName) == false);
            this.ServiceName = serviceName;
            this.FunctionName = functionName;
            this.Qualifier = qualifier;
            this.Headers = customHeaders;
        }

        public virtual string GetPath()
        {
            if (string.IsNullOrEmpty(this.Qualifier))
            {
                return string.Format(Const.SINGLE_FUNCTION_PATH, Const.API_VERSION, this.ServiceName, this.FunctionName);
            }
            else
            {
                return string.Format(Const.SINGLE_FUNCTION_WITH_QUALIFIER_PATH, Const.API_VERSION,
                    this.ServiceName, this.Qualifier, this.FunctionName);
            }
        }

        public RestRequest GenHttpRequest(FCConfig cfg)
        {
            this.Config = cfg;
            this.Headers = this.BuildCommonHeaders("GET", this.GetPath(), this.Headers);
            var request = new RestRequest(this.GetPath(), Method.GET);

            foreach (var item in this.Headers)
                request.AddHeader(item.Key, item.Value);

            return request;

        }
    }
}
