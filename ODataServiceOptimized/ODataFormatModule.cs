using System;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web;

namespace ODataService
{
    /// <summary>
    /// Intercepts WCF Data Services requests that include a $format parameter on the query
    /// string and alters the request headers according to the OData specification.
    /// </summary>
    public sealed class ODataFormatModule : IHttpModule
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ODataFormatModule"/> class.
        /// </summary>
        public ODataFormatModule()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the application instance.
        /// </summary>
        public HttpApplication Application
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current HTTP context.
        /// </summary>
        public HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }

        /// <summary>
        /// Gets the current request.
        /// </summary>
        public HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:HttpApplication"/> that provides access to the methods, 
        /// properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {

            Application = context;
            Application.BeginRequest += Application_BeginRequest;

        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements
        /// <see cref="T:IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            Application.BeginRequest -= Application_BeginRequest;
            Application = null;
        }

        /// <summary>
        /// Forces the <see cref="T:NameValueCollection"/> to allow modifications by using reflection to
        /// set the IsReadOnly property to false.
        /// </summary>
        /// <param name="collection">The collection to make writable.</param>
        /// <returns>The original collection after the IsReadOnly property has been hacked.</returns>
        private static NameValueCollection MakeWritable(NameValueCollection collection)
        {

            var collectionType = collection.GetType();
            var isReadOnlyProperty = collectionType.GetProperty(
                "IsReadOnly",
                BindingFlags.Instance |
                BindingFlags.IgnoreCase |
                BindingFlags.NonPublic
            );

            isReadOnlyProperty.SetValue(collection, false, null);

            return collection;

        }

        /// <summary>
        /// Gets a content type for the corresponding format parameter according to the OData specification
        /// http://www.odata.org/developers/protocols/uri-conventions#FormatSystemQueryOption
        /// </summary>
        /// <param name="format">The value of the $format querystring parameter.</param>
        /// <returns>The corresponding Accept request header value.</returns>
        private static string MapToMediaType(string format)
        {

            Contract.Requires(format != null);

            switch (format.ToLowerInvariant())
            {
                case "atom":
                    return "application/atom+xml";
                case "xml":
                    return "application/xml";
                case "json":
                    return "application/json";
                default:
                    return format;
            }   // switch

        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the BeginRequest event of the Application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:EventArgs"/> instance containing the event data.</param>
        private void Application_BeginRequest(object sender, EventArgs e)
        {

            var format = Request.QueryString["$format"];

            if (!String.IsNullOrWhiteSpace(format))
            {

                // Ordinarily, Request.Headers is read-only so we need to
                // use some reflection to get around that. Ugly, I know.
                var requestQuery = MakeWritable(Request.QueryString);
                var requestHeaders = MakeWritable(Request.Headers);

                // Set the Accept header the way a well-behaved json client
                // would have done, which WCF Data Services does support
                requestHeaders["Accept"] = MapToMediaType(format);

                // Use URL-rewriting to remove the $format part of the querystring
                // Otherwise, if it gets to WCF Data Services, it barfs
                requestQuery.Remove("$format");
                Context.RewritePath(Request.FilePath, Request.PathInfo, requestQuery.ToString());

            }   // if

        }

        #endregion

    }   // class
}