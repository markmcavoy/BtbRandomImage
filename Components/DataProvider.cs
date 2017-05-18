using System;
using System.Data;
using System.Web.Caching;
using System.Reflection;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Localization;

namespace BiteTheBullet.DNN.Modules.BTBRandomImage.Data
{
	public abstract class DataProvider
	{
		#region Shared/Static Methods
        // singleton reference to the instantiated object 
		private static DataProvider objProvider = null;

		// constructor
		static DataProvider()
		{
			CreateProvider();
		}

		// dynamically create provider
		private static void CreateProvider()
		{
			objProvider = ((DataProvider)DotNetNuke.Framework.Reflection.CreateObject("data", "BiteTheBullet.DNN.Modules.BTBRandomImage.Data", "BiteTheBullet.DNN.Modules.BTBRandomImage"));
		}

		// return the provider
		public static DataProvider Instance()
		{
			return objProvider;
		}

		#endregion

		#region "BTBRandomImage Abstract Methods"
		public abstract IDataReader GetBTBRandomImage(int imageID,int moduleId);
		public abstract IDataReader ListBTBRandomImage();
		public abstract IDataReader GetBTBRandomImageByModules(int moduleID );
		public abstract int AddBTBRandomImage(int moduleID , string imageSrc , string imageAlt, string url);
		public abstract void UpdateBTBRandomImage(int imageID, int moduleID , string imageSrc , string imageAlt, string url);
		public abstract void DeleteBTBRandomImage(int imageID);
		#endregion
	}
}
