using System;

using System.Web;
using CookComputing.XmlRpc;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Collections.Generic;

using System.IO;
using StructureMap;
using Chronicles.Services;
using System.Security.Authentication;
using Chronicles.Entities;
using System.Collections;
using System.Diagnostics;


namespace Chronicles.Web.Services
{
    [XmlRpcService(
    Name = "blogger",
    Description = "This is a sample XML-RPC service illustrating method calls with simple parameters and return type.",
            AutoDocumentation = true)]
    //[XmlRpcUrl("http://localhost:3333/MetaBlogApi.ashx")]
    public class MetaWeblogAPI : XmlRpcService
    {
        private PostServices postServices;
        private UserServices userServices;
        private TagServices tagServices;
 
        public MetaWeblogAPI()
        {
            try
            {
                postServices = ObjectFactory.GetInstance<PostServices>();
                userServices = ObjectFactory.GetInstance<UserServices>();
                tagServices = ObjectFactory.GetInstance<TagServices>();
            }
            catch
            {
                Debug.WriteLine(ObjectFactory.WhatDoIHave());
                throw;
            }
        }

        private void AuthenticateUser(string username, string password)
        {
            string authenticationToken;

            if (!userServices.AuthenticateUser(username, password, out authenticationToken))
            {
                throw new AuthenticationException("Username or password is incorrect");
            }
        }

        private static string [] ToStringArray(IList<Tag> tags)
        {
            List<string> tagStr = new List<string>();

            foreach (Tag t in tags)
            {
                tagStr.Add(t.TagName);
            }

            return tagStr.ToArray();
        }

        private static XmlRpcStruct PostToStruct(Post p)
        {
            /* 
             *    1. struct {  
                   2.     string postid;  
                   3.     DateTime dateCreated;  
                   4.     string title;  
                   5.     string description;  
                   6.     string[] categories;  
                   7.     bool publish;  
                   8. }  
             */
            XmlRpcStruct x = new XmlRpcStruct();
            x.Add("postid", p.Id);
            x.Add("dateCreated", p.ScheduledDate);
            x.Add("title", p.Title);
            x.Add("description", p.Body);
            x.Add("categories", ToStringArray(p.Tags));
            x.Add("link", "#");
            x.Add("publish", p.Approved);
            return x;
        }

        private static Post StructToPost(XmlRpcStruct x, bool publish)
        {
            /* 
             *    1. struct {  
                   2.     string postid;  
                   3.     DateTime dateCreated;  
                   4.     string title;  
                   5.     string description;  
                   6.     string[] categories;  
                   7.     bool publish;  
                   8. }  
             */
            Post p = new Post
                        {
                            Approved = publish,
                            Body = x["description"].ToString(),
                            Title = x["title"].ToString(),
                            Id = x.ContainsKey("postid") ? Convert.ToInt32(x["postid"]) : 0
                        };

            if (x.ContainsKey("categories"))
            {
                IEnumerable categories = x["categories"] as IEnumerable;

                if (categories != null)
                {
                    foreach(object category in categories)
                    {
                        if (category != null)
                        {
                            Tag tag = new Tag { TagName = category.ToString() };
                            p.AddTag(tag);
                        }
                    }
                }
            }
            
            return p;
        }

        [XmlRpcMethod("blogger.getUsersBlogs")]
        public XmlRpcStruct[] GetUsersBlogs(string appKey, string username, string password)
        {
            AuthenticateUser(username, password);

            //Create structure for blog list
            XmlRpcStruct rpcstruct = new XmlRpcStruct();
            rpcstruct.Add("blogid", "123"); // Blog Id
            rpcstruct.Add("blogName", "madaboutcode"); // Blog Name
            rpcstruct.Add("url", "http://192.168.1.105/blog/home/index"); // Blog URL
            XmlRpcStruct[] datarpcstruct = new XmlRpcStruct[] { rpcstruct };
            return datarpcstruct;
        }

        [XmlRpcMethod("metaWeblog.setTemplate")]
        public bool SetTemplate(string appKey, string blogid, string username, string password, string template, string templateType)
        {
            throw new NotImplementedException();
        }

        [XmlRpcMethod("metaWeblog.getCategories")]
        public XmlRpcStruct[] GetCategories(string blogid, string username, string password)
        {
            AuthenticateUser(username, password);

            IList<Tag> tags = tagServices.GetAll();
            List<XmlRpcStruct> structs = new List<XmlRpcStruct>(tags.Count);

            foreach (Tag t in tags)
            {
                XmlRpcStruct x = new XmlRpcStruct();
                x.Add("description", t.TagName);
                x.Add("categoryid", t.Id);
                structs.Add(x);
            }

            return structs.ToArray();
        }

        [XmlRpcMethod("metaWeblog.getRecentPosts")]
        public XmlRpcStruct[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            AuthenticateUser(username, password);

            List<XmlRpcStruct> structs = new List<XmlRpcStruct>(numberOfPosts);

            foreach (Post p in postServices.GetLatestPosts(numberOfPosts))
            {
                XmlRpcStruct x = PostToStruct(p);
                structs.Add(x);
            }

            return structs.ToArray();
        }

        [XmlRpcMethod("metaWeblog.getTemplate")]
        public string GetTemplate(string appKey, string blogid, string username, string password, string templateType)
        {
            AuthenticateUser(username, password);

            throw new NotImplementedException();

            return "";
        }

        [XmlRpcMethod("metaWeblog.editPost")]
        public bool EditPost(string postid, string username, string password, XmlRpcStruct rpcstruct, bool publish)
        {
            AuthenticateUser(username, password);

            throw new NotImplementedException();

            return true;
        }

        [XmlRpcMethod("metaWeblog.newPost")]
        public bool NewPost(string blogid, string username, string password, XmlRpcStruct rpcstruct, bool publish)
        {
            AuthenticateUser(username, password);

            Post p = StructToPost(rpcstruct, publish);

            postServices.AddPost(p);

            return true;
        }

        [XmlRpcMethod("metaWeblog.getPost")]
        public XmlRpcStruct GetPost(string postid, string username, string password)
        {
            AuthenticateUser(username, password);

            int postId = Convert.ToInt32(postid);

            Post p = postServices.GetPostById(postId);

            if (p == null)
                throw new Exception("Post with id " + postid + " was not found");

            return PostToStruct(p);
        }

        [XmlRpcMethod("blogger.deletePost")]
        public bool DeletePost(string appKey, string postid, string username, string password, bool publish)
        {
            AuthenticateUser(username, password);

            throw new NotImplementedException();

            return false;
        }

        [XmlRpcMethod("metaWeblog.newMediaObject")]
        public XmlRpcStruct NewMediaObject(string blogid, string username, string password, XmlRpcStruct rpcstruct)
        {
            AuthenticateUser(username, password);

            throw new NotImplementedException();

            return null;
        }
    }
}