using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HttpsOnlyModule
/// </summary>
public class HttpsOnlyModule : IHttpModule
{

    public HttpsOnlyModule() { }
    #region IHttpModule Members
    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    public void Init(HttpApplication context)
    {
        context.PreRequestHandlerExecute +=
       new EventHandler(OnPreRequestHandlerExecute);
    }
    /// <summary>
    /// Handle switching between HTTP and HTTPS.
    /// It only switches the scheme when necessary.
    /// Note: By scheme we mean HTTP scheme or HTTPS scheme.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void OnPreRequestHandlerExecute(object sender, EventArgs e)
    {
        // obtain a reference to the ASPX Page 
        System.Web.UI.Page Page = HttpContext.Current.Handler as System.Web.UI.Page;
        string[] _pageName = null;
        if (Page != null)
            _pageName = Page.ToString().Split('.');

        // if a valid Page was not found, exit
        if (Page == null)
        {
            return;
        }

        // check if the Page is decorated with the RequireSSL attribute
        bool requireSSL = (Page.GetType().GetCustomAttributes(
                             typeof(RequireSSL), true).Length > 0);

        // check if the Page is currently using the HTTPS scheme
        bool isSecureConnection =
          HttpContext.Current.ApplicationInstance.Request.IsSecureConnection;

        // acquire the URI (eg~ http://localhost/default.aspx)
        Uri baseUri = HttpContext.Current.ApplicationInstance.Request.Url;

        // if the Page requires SSL and it is not currently
        // using HTTPS, switch out the scheme
        if (requireSSL && !isSecureConnection)
        {
            string[] _actualPageName = _pageName[1].Split('_');

            // switch the HTTP scheme to the HTTPS scheme
            string url = "https://localhost:12345/Security/" + _actualPageName[1] + "";
            //string url = baseUri.ToString().Replace(
            //                 baseUri.Scheme, Uri.UriSchemeHttps);

            // perform a 301 redirect to the secure url
            PermanentRedirect(url);
        }
        // if the page does not require SSL and it is currently
        // using the HTTPS scheme, switch out the scheme
        else if (!requireSSL && isSecureConnection)
        {
            string[] _actualPageName = _pageName[1].Split('_');
            // switch the HTTPS scheme to the HTTP scheme
            string url = "http://localhost:12345/Unsecured/" + _actualPageName[1] + "";
            //string url = baseUri.ToString().Replace(baseUri.Scheme,
            //                                        Uri.UriSchemeHttp);

            // perform a 301 redirect to the non-secure url
            PermanentRedirect(url);
        }
    }
    private void PermanentRedirect(string url)
    {
        HttpContext.Current.Response.Status = "301 Moved Permanently";
        HttpContext.Current.Response.AddHeader("Location", url);
    }
    #endregion

}