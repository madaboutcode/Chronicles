 
 
using System;

namespace Chronicles.Framework
{
	public class AppConfiguration
	{
		private IAppConfigProvider configProvider;
		public AppConfiguration(IAppConfigProvider provider)
		{
			configProvider = provider;
		}
		
		public string NoOfHomePagePosts
		{
			get
			{
				return configProvider.GetConfig("NoOfHomePagePosts");
			}
		}
		public string RecentPostsBlockCount
		{
			get
			{
				return configProvider.GetConfig("RecentPostsBlockCount");
			}
		}
		public string ConnectionString
		{
			get
			{
				return configProvider.GetConfig("ConnectionString");
			}
		}
	}
}
