using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeneXus.Server.ExternalTool.Slack
{
    public class SlackClient
    {
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();
        private List<Field> _fields;

        public SlackClient(string urlWithAccessToken)
        {
            _uri = new Uri(urlWithAccessToken);
            _fields = new List<Field>();
        }

        public Uri GetUri()
        {
            return this._uri;
        }

        public List<Field> GetFields()
        {
            return this._fields;
        }

        public void SetFields(List<Field> aFields)
        {
            this._fields = aFields;
        }

        //Post a message using simple strings
        public Result PostMessage(string channel, string username, string preText = null, string text = null,
            string title = null, string title_link = null, string color = null, string footer = null,
            string authorName = null)
        {

            string fallBack = preText + " - " + title + " - " + title_link + " - ";

            Attachment attachment = new Attachment(fallBack, preText, title, title_link, text, color, 
                this._fields, footer, authorName);

            List<Attachment> attachments = new List<Attachment>();
            attachments.Add(attachment);

            Payload payload = new Payload(channel, username, attachments);

            Result result = PostMessage(payload);

            return result;
        }

        //Post a message using a Payload object
        public Result PostMessage(Payload payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);
            Result result = new Result(200, payload.Channel + " - ok", payload.Channel + " - ok");
            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                data["payload"] = payloadJson;
                try
                {
                    var response = client.UploadValues(_uri, "POST", data);

                }
                catch (System.Net.WebException e)
                {
                    result = this.ErrorResult(e, payload.Channel);
                }
            }
            return result;
        }

        public void AddField(string title, string value, bool isShort)
        {
            Field field = new Field(title, value, isShort);
            this._fields.Add(field);
        }

        private Result ErrorResult(System.Net.WebException e, string aChannel)
        {
            string descResult = aChannel + " - ";
            int codeResult = (int)e.Status;
            string aShortDescResult = aChannel + " - ";
            switch (e.Status)
            {
                //1
                case WebExceptionStatus.NameResolutionFailure:
                    descResult += "The name resolver service could not resolve the host name.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //2
                case WebExceptionStatus.ConnectFailure:
                    descResult += "The remote service point could not be contacted at the transport level.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //3
                case WebExceptionStatus.ReceiveFailure:
                    descResult += "A complete response was not received from the remote server.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //4
                case WebExceptionStatus.SendFailure:
                    descResult += "A complete request could not be sent to the remote server.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //5
                case WebExceptionStatus.PipelineFailure:
                    descResult += "The request was a pipelined request and the connection was " +
                        "closed before the response was received.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //6
                case WebExceptionStatus.RequestCanceled:
                    descResult += "The request was canceled.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //7
                case WebExceptionStatus.ProtocolError:
                    HttpWebResponse resp = (HttpWebResponse)e.Response;
                    codeResult = (int)resp.StatusCode;
                    string[] arrDesc = this.DescProtocolError(resp);
                    descResult += arrDesc[0];
                    aShortDescResult += arrDesc[1];
                    break;
                //8
                case WebExceptionStatus.ConnectionClosed:
                    descResult += "The connection was prematurely closed.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //9
                case WebExceptionStatus.TrustFailure:
                    descResult += "A server certificate could not be validated.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //10
                case WebExceptionStatus.SecureChannelFailure:
                    descResult += "An error occurred while establishing a connection using SSL.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //11
                case WebExceptionStatus.ServerProtocolViolation:
                    descResult += "The server response was not a valid HTTP response.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //12
                case WebExceptionStatus.KeepAliveFailure:
                    descResult += "The connection for a request that specifies the Keep-alive header " +
                        "was closed unexpectedly.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //13
                case WebExceptionStatus.Pending:
                    descResult += "An internal asynchronous request is pending.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //14
                case WebExceptionStatus.Timeout:
                    descResult += "No response was received during the time-out period for a request.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //15
                case WebExceptionStatus.ProxyNameResolutionFailure:
                    descResult += "The name resolver service could not resolve the proxy host name.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //16
                case WebExceptionStatus.UnknownError:
                    descResult += "An exception of unknown type has occurred.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //17
                case WebExceptionStatus.MessageLengthLimitExceeded:
                    descResult += "A message was received that exceeded the specified limit when sending" +
                        " a request or receiving a response from the server.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //18
                case WebExceptionStatus.CacheEntryNotFound:
                    descResult += "The specified cache entry was not found.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //19
                case WebExceptionStatus.RequestProhibitedByCachePolicy:
                    descResult += "The request was not permitted by the cache policy.";
                    aShortDescResult += e.Status.ToString();
                    break;
                //20
                case WebExceptionStatus.RequestProhibitedByProxy:
                    descResult += "This request was not permitted by the proxy.";
                    aShortDescResult += e.Status.ToString();
                    break;
                default:
                    descResult += e.Message;
                    aShortDescResult += e.Status.ToString();
                    break;
            }
            Result result = new Result(codeResult, descResult, aShortDescResult);
            return result;
        }
        
        private string[] DescProtocolError(HttpWebResponse resp)
        {
            string[] ret = new string[2];
            string descResult = "";
            string shortDescResult = "";
            switch (resp.StatusCode)
            {
                //300
                case HttpStatusCode.Ambiguous:
                    descResult = "Multiple options for the resource from which the client may choose.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //301
                case HttpStatusCode.Moved:
                    descResult = "This and all future requests should be directed to the given URI.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //302
                case HttpStatusCode.Found:
                    descResult = "Tells the client to look at another URL.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //303
                case HttpStatusCode.RedirectMethod:
                    descResult = "The response to the request can be found under another URI using " +
                        "the GET method.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //304
                case HttpStatusCode.NotModified:
                    descResult = "Indicates that the resource has not been modified since the version " +
                        "specified by the request headers If-Modified-Since or If-None-Match.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //305
                case HttpStatusCode.UseProxy:
                    descResult = "The requested resource is available only through a proxy, " +
                        "the address for which is provided in the response.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //306
                case HttpStatusCode.Unused:
                    descResult = "No longer used.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //307
                case HttpStatusCode.RedirectKeepVerb:
                    descResult = "The request should be repeated with another URI.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //400
                case HttpStatusCode.BadRequest:
                    descResult = "The request could not be understood by the server. - " +
                        "The data sent in your request cannot be understood as presented. - " +
                        "The user used in your request does not actually exist.";
                    shortDescResult = resp.StatusDescription + " - invalid_payload - user_not_found";
                    break;
                //401
                case HttpStatusCode.Unauthorized:
                    descResult = "The server refuses to fulfill the request.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //402
                case HttpStatusCode.PaymentRequired:
                    descResult = "PaymentRequired.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //403
                case HttpStatusCode.Forbidden:
                    descResult = "The server refuses to fulfill the request. - " +
                        "The team associated with your request has some kind of restriction on the " +
                        "webhook posting in this context.";
                    shortDescResult = resp.StatusDescription + " - action_prohibited";
                    break;
                //404
                case HttpStatusCode.NotFound:
                    descResult = "The requested resource does not exist on the server. - " +
                        "The channel associated with your request does not exist.";
                    shortDescResult = resp.StatusDescription + " - channel_not_found";
                    break;
                //405
                case HttpStatusCode.MethodNotAllowed:
                    descResult = "The request method is not allowed on the requested resource.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //406
                case HttpStatusCode.NotAcceptable:
                    descResult = "The client has indicated with Accept headers that it will not " +
                        "accept any of the available representations of the resource.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //407
                case HttpStatusCode.ProxyAuthenticationRequired:
                    descResult = "The requested proxy requires authentication.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //408
                case HttpStatusCode.RequestTimeout:
                    descResult = "The client did not send a request within the time the server was " +
                        "expecting the request.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //409
                case HttpStatusCode.Conflict:
                    descResult = "The request could not be carried out because of a conflict on the server.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //410
                case HttpStatusCode.Gone:
                    descResult = "The requested resource is no longer available. - " +
                        "The channel has been archived and doesn't accept further messages, " +
                        "even from your incoming webhook.";
                    shortDescResult = resp.StatusDescription + " - channel_is_archived";
                    break;
                //411
                case HttpStatusCode.LengthRequired:
                    descResult = "The required Content-length header is missing.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //412
                case HttpStatusCode.PreconditionFailed:
                    descResult = "A condition set for this request failed, and the request cannot be " +
                        "carried out.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //413
                case HttpStatusCode.RequestEntityTooLarge:
                    descResult = "The request is too large for the server to process.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //414
                case HttpStatusCode.RequestUriTooLong:
                    descResult = "The URI is too long.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //415
                case HttpStatusCode.UnsupportedMediaType:
                    descResult = "The request is an unsupported type.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //416
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    descResult = "The range of data requested from the resource cannot be returned.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //417
                case HttpStatusCode.ExpectationFailed:
                    descResult = "An expectation given in an Expect header could not be met by the server.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //426
                case HttpStatusCode.UpgradeRequired:
                    descResult = "The client should switch to a different protocol such as TLS/1.0.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //500
                case HttpStatusCode.InternalServerError:
                    descResult = "A generic error has occurred on the server. - " +
                        "Something strange and unusual happened that was likely not your fault at all.";
                    shortDescResult = resp.StatusDescription + " - rollup_error";
                    break;
                //501
                case HttpStatusCode.NotImplemented:
                    descResult = "The server does not support the requested function.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //502
                case HttpStatusCode.BadGateway:
                    descResult = "An intermediate proxy server received a bad response from another " +
                        "proxy or the origin server.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //503
                case HttpStatusCode.ServiceUnavailable:
                    descResult = "The server is temporarily unavailable, usually due to high load or " +
                        "maintenance.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //504
                case HttpStatusCode.GatewayTimeout:
                    descResult = "An intermediate proxy server timed out while waiting for a response " +
                        "from another proxy or the origin server.";
                    shortDescResult = resp.StatusDescription;
                    break;
                //505
                case HttpStatusCode.HttpVersionNotSupported:
                    descResult = "The requested HTTP version is not supported by the server.";
                    shortDescResult = resp.StatusDescription;
                    break;
                default:
                    descResult = resp.StatusDescription;
                    shortDescResult = resp.StatusDescription;
                    break;
            }
            ret[0] = descResult;
            ret[1] = shortDescResult;
            return ret;
        }
    }
}
