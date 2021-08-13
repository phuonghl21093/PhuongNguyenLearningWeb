using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web365Business.Front_End.IRepository;
using Web365Business.Front_End.Repository;
using Web365DA.RDBMS.Front_End.Repository;
using Web365Domain.Language;
using Web365Utility;

namespace Web365.App_Code
{

    public static class HiconHtmlHelper
    {
        private static readonly IArticleTypeRepositoryFE _articleType;
        private static readonly IArticleRepositoryFE _article;
        private static readonly IMenuRepositoryFE _menu;
        static HiconHtmlHelper()
        {
            _articleType = new ArticleTypeRepositoryFE(new ArticleTypeDAFERepository());
            _article = new ArticleRepositoryFE(new ArticleDAFERepository());
            _menu = new MenuRepositoryFE(new MenuDAFERepository());
        }



        public static MvcHtmlString LangSwitcher(RouteData routeData, LanguageItem language)
        {
            var liTagBuilder = new TagBuilder("li");
            var aTagBuilder = new TagBuilder("a");
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);

            var currentLang = routeValueDictionary.ContainsKey("lang") ? routeData.Values["lang"].ToString() : "vi";
            var lang = "/" + (language.ID != (int)StaticEnum.LanguageId.Vietnamese ? language.Code + "/" : string.Empty);
            var cate = string.Empty;
            var artl = string.Empty;
            if (currentLang != language.Code)
            {
                if (routeValueDictionary.ContainsKey("category"))
                {
                    var category =
                        _articleType.GetTypeInOtherLangByNameascii(routeValueDictionary["category"].ToString(),
                            language.ID);
                    cate = category != null ? category.NameAscii + "/" : string.Empty;
                }

                if (routeValueDictionary.ContainsKey("article"))
                {
                    var article = _article.GetSameInOtherLangByNameascii(routeValueDictionary["article"].ToString(),
                        language.ID);
                    if (article != null)
                    {
                        artl = article.TitleAscii;
                    }
                }

                if ((string)routeValueDictionary["controller"] == "Library")
                {
                    cate = language.ID == (int)StaticEnum.LanguageId.Vietnamese ? "thu-vien" : "library";
                    if (routeValueDictionary.ContainsKey("libraryCate"))
                    {
                        cate = string.Empty;
                        var menu = _menu.GetByNameAscii(routeValueDictionary["libraryCate"].ToString());
                        var otherLang = _menu.GetMenuItemByLanguage(menu.ID, language.ID);
                        artl = otherLang != null ? otherLang.Link + "/" : string.Empty;
                    }
                }

                aTagBuilder.MergeAttribute("href", lang + cate + artl);
            }
            else
            {
                aTagBuilder.MergeAttribute("href", "javascript:void(0);");
                aTagBuilder.AddCssClass("active");
            }
            aTagBuilder.AddCssClass("lang-item");
            var tempText = "<img src=\"/Content/images/vnm.png\">";
            if (language.Code.ToLower() == "en")
                tempText = "<img src=\"/Content/images/eng.png\">";
            aTagBuilder.InnerHtml = tempText;
            //aTagBuilder.SetInnerText(string.IsNullOrEmpty(language.Code) ? "" : language.Code.ToUpper());
            liTagBuilder.InnerHtml = aTagBuilder.ToString();
            return new MvcHtmlString(liTagBuilder.ToString());
        }
        public static MvcHtmlString MakeParentLanguageHyperLink(RouteData routeData, string name, string url, bool isHaveChild)
        {
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            var lang = routeValueDictionary.ContainsKey("lang") ? routeData.Values["lang"] + "/" : string.Empty;

            var aTagBuilder = new TagBuilder("a");
            aTagBuilder.MergeAttribute("href", "/" + lang + url);
            aTagBuilder.MergeAttribute("title", name);
            if (isHaveChild)
            {
                //aTagBuilder.MergeAttribute("class", "dropdown-toggle js-activated");
                //aTagBuilder.MergeAttribute("data-toggle", "dropdown");
                //aTagBuilder.MergeAttribute("role", "button");
                //aTagBuilder.MergeAttribute("aria-haspopup", "true");
                //aTagBuilder.MergeAttribute("aria-expanded", "false");
                aTagBuilder.InnerHtml = name + " <span class=\"caret\"></span>";
            }
            else
            {
                aTagBuilder.SetInnerText(name);
            }
            return new MvcHtmlString(aTagBuilder.ToString());
        }
        public static MvcHtmlString MakeLanguageHyperLink(RouteData routeData, string name, string url)
        {
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            var lang = routeValueDictionary.ContainsKey("lang") ? routeData.Values["lang"] + "/" : string.Empty;

            var aTagBuilder = new TagBuilder("a");
            aTagBuilder.MergeAttribute("href", "/" + lang + url);
            aTagBuilder.MergeAttribute("title", name);
            aTagBuilder.SetInnerText(name);

            return new MvcHtmlString(aTagBuilder.ToString());
        }

        public static MvcHtmlString MakeLanguageHyperLinkWithHTML(RouteData routeData, string name, string url, string attribute = "")
        {
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            var lang = routeValueDictionary.ContainsKey("lang") ? routeData.Values["lang"] + "/" : string.Empty;

            var aTagBuilder = new TagBuilder("a");
            aTagBuilder.MergeAttribute("href", "/" + lang + url);
            aTagBuilder.MergeAttribute("title", name);
            if (!string.IsNullOrEmpty(attribute))
            {
                var key = attribute.Split(',')[0];
                var value = attribute.Split(',')[1];
                aTagBuilder.MergeAttribute(key, value);
            }
            aTagBuilder.InnerHtml = name;

            return new MvcHtmlString(aTagBuilder.ToString());
        }

        public static String GetHyperLinkWithLanguage(RouteData routeData, string url)
        {
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            var lang = routeValueDictionary.ContainsKey("lang") ? routeData.Values["lang"] + "/" : string.Empty;
            return ("/" + lang + url);
        }

        public static MvcHtmlString MakeLanguageHyperLinkWithMultiAttr(RouteData routeData, string name, string url, Dictionary<String, String> attrs = null, bool includeTitle = true)
        {
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            var lang = routeValueDictionary.ContainsKey("lang") ? routeData.Values["lang"] + "/" : string.Empty;

            var aTagBuilder = new TagBuilder("a");
            aTagBuilder.MergeAttribute("href", "/" + lang + url);
            if (includeTitle)
            {
                aTagBuilder.MergeAttribute("title", name);
            }

            if (attrs != null)
            {
                foreach (var attr in attrs)
                {
                    if (!string.IsNullOrEmpty(attr.Key))
                    {
                        aTagBuilder.MergeAttribute(attr.Key, attr.Value);
                    }
                }
            }
            aTagBuilder.InnerHtml = name;

            return new MvcHtmlString(aTagBuilder.ToString());
        }

        public static MvcHtmlString MakeSupportSlider(RouteData routeData, string name, string url)
        {
            var routeValueDictionary = new RouteValueDictionary(routeData.Values);
            var lang = routeValueDictionary.ContainsKey("lang") ? routeData.Values["lang"] + "/" : string.Empty;

            var aTagBuilder = new TagBuilder("a");
            aTagBuilder.MergeAttribute("href", "/" + lang + url);
            aTagBuilder.MergeAttribute("title", name);
            aTagBuilder.SetInnerText(name);

            return new MvcHtmlString(aTagBuilder.ToString());
        }

        private static string GetPlainTextFromHtml(string htmlString)
        {
            if (!String.IsNullOrEmpty(htmlString))
            {
                string htmlTagPattern = "<.*?>";
                var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                htmlString = regexCss.Replace(htmlString, string.Empty);
                htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty, RegexOptions.Multiline);
                htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                htmlString = htmlString.Replace("&nbsp;", string.Empty);

                return htmlString;
            }
            return String.Empty;
        }

        public static String TruncateContentInHtml(string html, int size, string extent)
        {
            string onlyContent = GetPlainTextFromHtml(html);
            string result = String.Empty;
            if (!String.IsNullOrEmpty(onlyContent))
            {
                result = onlyContent.Length > size ? (onlyContent.Substring(0, size - extent.Length) + extent) : onlyContent;
            }
            return result;
        }
    }
}