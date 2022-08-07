using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using HtmlAgilityPack;

namespace JobTrackerDataManager.WebScraper.ScraperAPIWrapper;

public class HtmlWebWrapper : IHtmlWeb
{
    private readonly HtmlWeb _api;
    public DecompressionMethods AutomaticDecompression { get; set; }
    public bool AutoDetectEncoding { get; set; }
    public Encoding OverrideEncoding { get; set; }
    public bool CacheOnly { get; set; }
    public bool UsingCacheIfExists { get; set; }
    public string CachePath { get; set; }
    public bool FromCache { get; }
    public int RequestDuration { get; }
    public Uri ResponseUri { get; }
    public HttpStatusCode StatusCode { get; }
    public int StreamBufferSize { get; set; }
    public bool UseCookies { get; set; }
    public bool CaptureRedirect { get; set; }
    public string UserAgent { get; set; }
    public bool UsingCache { get; set; }

    public HtmlWebWrapper(HtmlWeb api)
    {
        _api = api;
    }

    public object CreateInstance(string url, Type type)
    {
        throw new NotImplementedException();
    }

    public object CreateInstance(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, Type type)
    {
        throw new NotImplementedException();
    }

    public object CreateInstance(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, Type type, string xmlPath)
    {
        throw new NotImplementedException();
    }

    public void Get(string url, string path)
    {
        throw new NotImplementedException();
    }

    public void Get(string url, string path, WebProxy proxy, NetworkCredential credentials)
    {
        throw new NotImplementedException();
    }

    public void Get(string url, string path, string method)
    {
        throw new NotImplementedException();
    }

    public void Get(string url, string path, WebProxy proxy, NetworkCredential credentials, string method)
    {
        throw new NotImplementedException();
    }

    public string GetCachePath(Uri uri)
    {
        throw new NotImplementedException();
    }

    public HtmlDocument Load(string url)
    {
        return _api.Load(url);
    }

    public HtmlDocument Load(Uri uri)
    {
        throw new NotImplementedException();
    }

    public HtmlDocument Load(string url, string proxyHost, int proxyPort, string userId, string password)
    {
        throw new NotImplementedException();
    }

    public HtmlDocument Load(Uri uri, string proxyHost, int proxyPort, string userId, string password)
    {
        throw new NotImplementedException();
    }

    public HtmlDocument Load(string url, string method)
    {
        throw new NotImplementedException();
    }

    public HtmlDocument Load(Uri uri, string method)
    {
        throw new NotImplementedException();
    }

    public HtmlDocument Load(string url, string method, WebProxy proxy, NetworkCredential credentials)
    {
        throw new NotImplementedException();
    }

    public HtmlDocument Load(Uri uri, string method, WebProxy proxy, NetworkCredential credentials)
    {
        throw new NotImplementedException();
    }

    public void LoadHtmlAsXml(string htmlUrl, XmlTextWriter writer)
    {
        throw new NotImplementedException();
    }

    public void LoadHtmlAsXml(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, XmlTextWriter writer)
    {
        throw new NotImplementedException();
    }

    public void LoadHtmlAsXml(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, XmlTextWriter writer,
        string xmlPath)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password,
        string domain)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password,
        string domain,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password, string domain)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password, string domain,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, NetworkCredential credentials)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(string url, NetworkCredential credentials,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(Uri uri, Encoding encoding, NetworkCredential credentials)
    {
        throw new NotImplementedException();
    }

    public Task<HtmlDocument> LoadFromWebAsync(Uri uri, Encoding encoding, NetworkCredential credentials,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}