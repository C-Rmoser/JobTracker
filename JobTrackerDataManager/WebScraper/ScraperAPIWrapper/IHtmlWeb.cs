using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using HtmlAgilityPack;

namespace JobTrackerDataManager.WebScraper.ScraperAPIWrapper;

public interface IHtmlWeb
{
    /// <summary>Gets or sets the automatic decompression.</summary>
    /// <value>The automatic decompression.</value>
    DecompressionMethods AutomaticDecompression { get; set; }

    /// <summary>
    /// Gets or Sets a value indicating if document encoding must be automatically detected.
    /// </summary>
    bool AutoDetectEncoding { get; set; }

    /// <summary>
    /// Gets or sets the Encoding used to override the response stream from any web request
    /// </summary>
    Encoding OverrideEncoding { get; set; }

    /// <summary>
    /// Gets or Sets a value indicating whether to get document only from the cache.
    /// If this is set to true and document is not found in the cache, nothing will be loaded.
    /// </summary>
    bool CacheOnly { get; set; }

    /// <summary>
    /// Gets or Sets a value indicating whether to get document from the cache if exists, otherwise from the web
    /// A value indicating whether to get document from the cache if exists, otherwise from the web
    /// </summary>
    bool UsingCacheIfExists { get; set; }

    /// <summary>
    /// Gets or Sets the cache path. If null, no caching mechanism will be used.
    /// </summary>
    string CachePath { get; set; }

    /// <summary>
    /// Gets a value indicating if the last document was retrieved from the cache.
    /// </summary>
    bool FromCache { get; }

    /// <summary>Gets the last request duration in milliseconds.</summary>
    int RequestDuration { get; }

    /// <summary>
    /// Gets the URI of the Internet resource that actually responded to the request.
    /// </summary>
    Uri ResponseUri { get; }

    /// <summary>Gets the last request status.</summary>
    HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets or Sets the size of the buffer used for memory operations.
    /// </summary>
    int StreamBufferSize { get; set; }

    /// <summary>
    /// Gets or Sets a value indicating if cookies will be stored.
    /// </summary>
    bool UseCookies { get; set; }

    /// <summary>Gets or sets a value indicating whether redirect should be captured instead of the current location.</summary>
    /// <value>True if capture redirect, false if not.</value>
    bool CaptureRedirect { get; set; }

    /// <summary>
    /// Gets or Sets the User Agent HTTP 1.1 header sent on any webrequest
    /// </summary>
    string UserAgent { get; set; }

    /// <summary>
    /// Gets or Sets a value indicating whether the caching mechanisms should be used or not.
    /// </summary>
    bool UsingCache { get; set; }

    /// <summary>
    /// Creates an instance of the given type from the specified Internet resource.
    /// </summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="type">The requested type.</param>
    /// <returns>An newly created instance.</returns>
    object CreateInstance(string url, Type type);

    /// <summary>
    /// Creates an instance of the given type from the specified Internet resource.
    /// </summary>
    /// <param name="htmlUrl">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="xsltUrl">The URL that specifies the XSLT stylesheet to load.</param>
    /// <param name="xsltArgs">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform.</param>
    /// <param name="type">The requested type.</param>
    /// <returns>An newly created instance.</returns>
    object CreateInstance(
        string htmlUrl,
        string xsltUrl,
        XsltArgumentList xsltArgs,
        Type type);

    /// <summary>
    /// Creates an instance of the given type from the specified Internet resource.
    /// </summary>
    /// <param name="htmlUrl">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="xsltUrl">The URL that specifies the XSLT stylesheet to load.</param>
    /// <param name="xsltArgs">An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> containing the namespace-qualified arguments used as input to the transform.</param>
    /// <param name="type">The requested type.</param>
    /// <param name="xmlPath">A file path where the temporary XML before transformation will be saved. Mostly used for debugging purposes.</param>
    /// <returns>An newly created instance.</returns>
    object CreateInstance(
        string htmlUrl,
        string xsltUrl,
        XsltArgumentList xsltArgs,
        Type type,
        string xmlPath);

    /// <summary>
    /// Gets an HTML document from an Internet resource and saves it to the specified file.
    /// </summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="path">The location of the file where you want to save the document.</param>
    void Get(string url, string path);

    /// <summary>
    /// Gets an HTML document from an Internet resource and saves it to the specified file. - Proxy aware
    /// </summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="path">The location of the file where you want to save the document.</param>
    /// <param name="proxy"></param>
    /// <param name="credentials"></param>
    void Get(string url, string path, WebProxy proxy, NetworkCredential credentials);

    /// <summary>
    /// Gets an HTML document from an Internet resource and saves it to the specified file.
    /// </summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="path">The location of the file where you want to save the document.</param>
    /// <param name="method">The HTTP method used to open the connection, such as GET, POST, PUT, or PROPFIND.</param>
    void Get(string url, string path, string method);

    /// <summary>
    /// Gets an HTML document from an Internet resource and saves it to the specified file.  Understands Proxies
    /// </summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="path">The location of the file where you want to save the document.</param>
    /// <param name="credentials"></param>
    /// <param name="method">The HTTP method used to open the connection, such as GET, POST, PUT, or PROPFIND.</param>
    /// <param name="proxy"></param>
    void Get(
        string url,
        string path,
        WebProxy proxy,
        NetworkCredential credentials,
        string method);

    /// <summary>Gets the cache file path for a specified url.</summary>
    /// <param name="uri">The url fo which to retrieve the cache path. May not be null.</param>
    /// <returns>The cache file path.</returns>
    string GetCachePath(Uri uri);

    /// <summary>Gets an HTML document from an Internet resource.</summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(string url);

    /// <summary>Gets an HTML document from an Internet resource.</summary>
    /// <param name="uri">The requested Uri, such as new Uri("http://Myserver/Mypath/Myfile.asp").</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(Uri uri);

    /// <summary>Gets an HTML document from an Internet resource.</summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="proxyHost">Host to use for Proxy</param>
    /// <param name="proxyPort">Port the Proxy is on</param>
    /// <param name="userId">User Id for Authentication</param>
    /// <param name="password">Password for Authentication</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(
        string url,
        string proxyHost,
        int proxyPort,
        string userId,
        string password);

    /// <summary>Gets an HTML document from an Internet resource.</summary>
    /// <param name="uri">The requested Uri, such as new Uri("http://Myserver/Mypath/Myfile.asp").</param>
    /// <param name="proxyHost">Host to use for Proxy</param>
    /// <param name="proxyPort">Port the Proxy is on</param>
    /// <param name="userId">User Id for Authentication</param>
    /// <param name="password">Password for Authentication</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(
        Uri uri,
        string proxyHost,
        int proxyPort,
        string userId,
        string password);

    /// <summary>Loads an HTML document from an Internet resource.</summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="method">The HTTP method used to open the connection, such as GET, POST, PUT, or PROPFIND.</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(string url, string method);

    /// <summary>Loads an HTML document from an Internet resource.</summary>
    /// <param name="uri">The requested URL, such as new Uri("http://Myserver/Mypath/Myfile.asp").</param>
    /// <param name="method">The HTTP method used to open the connection, such as GET, POST, PUT, or PROPFIND.</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(Uri uri, string method);

    /// <summary>Loads an HTML document from an Internet resource.</summary>
    /// <param name="url">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="method">The HTTP method used to open the connection, such as GET, POST, PUT, or PROPFIND.</param>
    /// <param name="proxy">Proxy to use with this request</param>
    /// <param name="credentials">Credentials to use when authenticating</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(
        string url,
        string method,
        WebProxy proxy,
        NetworkCredential credentials);

    /// <summary>Loads an HTML document from an Internet resource.</summary>
    /// <param name="uri">The requested Uri, such as new Uri("http://Myserver/Mypath/Myfile.asp").</param>
    /// <param name="method">The HTTP method used to open the connection, such as GET, POST, PUT, or PROPFIND.</param>
    /// <param name="proxy">Proxy to use with this request</param>
    /// <param name="credentials">Credentials to use when authenticating</param>
    /// <returns>A new HTML document.</returns>
    HtmlDocument Load(
        Uri uri,
        string method,
        WebProxy proxy,
        NetworkCredential credentials);

    /// <summary>
    /// Loads an HTML document from an Internet resource and saves it to the specified XmlTextWriter.
    /// </summary>
    /// <param name="htmlUrl">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="writer">The XmlTextWriter to which you want to save to.</param>
    void LoadHtmlAsXml(string htmlUrl, XmlTextWriter writer);

    /// <summary>
    /// Loads an HTML document from an Internet resource and saves it to the specified XmlTextWriter, after an XSLT transformation.
    /// </summary>
    /// <param name="htmlUrl">The requested URL, such as "http://Myserver/Mypath/Myfile.asp".</param>
    /// <param name="xsltUrl">The URL that specifies the XSLT stylesheet to load.</param>
    /// <param name="xsltArgs">An XsltArgumentList containing the namespace-qualified arguments used as input to the transform.</param>
    /// <param name="writer">The XmlTextWriter to which you want to save.</param>
    void LoadHtmlAsXml(
        string htmlUrl,
        string xsltUrl,
        XsltArgumentList xsltArgs,
        XmlTextWriter writer);

    /// <summary>
    /// Loads an HTML document from an Internet resource and saves it to the specified XmlTextWriter, after an XSLT transformation.
    /// </summary>
    /// <param name="htmlUrl">The requested URL, such as "http://Myserver/Mypath/Myfile.asp". May not be null.</param>
    /// <param name="xsltUrl">The URL that specifies the XSLT stylesheet to load.</param>
    /// <param name="xsltArgs">An XsltArgumentList containing the namespace-qualified arguments used as input to the transform.</param>
    /// <param name="writer">The XmlTextWriter to which you want to save.</param>
    /// <param name="xmlPath">A file path where the temporary XML before transformation will be saved. Mostly used for debugging purposes.</param>
    void LoadHtmlAsXml(
        string htmlUrl,
        string xsltUrl,
        XsltArgumentList xsltArgs,
        XmlTextWriter writer,
        string xmlPath);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    Task<HtmlDocument> LoadFromWebAsync(string url);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        CancellationToken cancellationToken);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        Encoding encoding,
        CancellationToken cancellationToken);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        Encoding encoding,
        string userName,
        string password);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        Encoding encoding,
        string userName,
        string password,
        CancellationToken cancellationToken);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    /// <param name="domain">Domain to use for credentials in the web request</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        Encoding encoding,
        string userName,
        string password,
        string domain);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    /// <param name="domain">Domain to use for credentials in the web request</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        Encoding encoding,
        string userName,
        string password,
        string domain,
        CancellationToken cancellationToken);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    /// <param name="domain">Domain to use for credentials in the web request</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        string userName,
        string password,
        string domain);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    /// <param name="domain">Domain to use for credentials in the web request</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        string userName,
        string password,
        string domain,
        CancellationToken cancellationToken);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        string userName,
        string password);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="userName">Username to use for credentials in the web request</param>
    /// <param name="password">Password to use for credentials in the web request</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        string userName,
        string password,
        CancellationToken cancellationToken);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="credentials">The credentials to use for authenticating the web request</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        NetworkCredential credentials);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="url">Url to the html document</param>
    /// <param name="credentials">The credentials to use for authenticating the web request</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        string url,
        NetworkCredential credentials,
        CancellationToken cancellationToken);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="uri">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    /// <param name="credentials">The credentials to use for authenticating the web request</param>
    Task<HtmlDocument> LoadFromWebAsync(
        Uri uri,
        Encoding encoding,
        NetworkCredential credentials);

    /// <summary>
    /// Begins the process of downloading an internet resource
    /// </summary>
    /// <param name="uri">Url to the html document</param>
    /// <param name="encoding">The encoding to use while downloading the document</param>
    /// <param name="credentials">The credentials to use for authenticating the web request</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    Task<HtmlDocument> LoadFromWebAsync(
        Uri uri,
        Encoding encoding,
        NetworkCredential credentials,
        CancellationToken cancellationToken);
}