using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Framework.Providers;

namespace BiteTheBullet.DNN.Modules.BTBRandomImage.Data 
{
    
	public class SqlDataProvider : DataProvider 
	{
        
		private const string providerType = "data";
        
		#region Private Members
		private ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
		private string _connectionString;
		private string _providerPath;
		private string _objectQualifier;
		private string _databaseOwner;
		#endregion
        
		#region Constructors
		public SqlDataProvider()
		{
			Provider objProvider = ((Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);
			if (objProvider.Attributes["connectionStringName"] != "" && System.Configuration.ConfigurationSettings.AppSettings[objProvider.Attributes["connectionStringName"]] != "") 
			{
				_connectionString = System.Configuration.ConfigurationSettings.AppSettings[objProvider.Attributes["connectionStringName"]];
			} 
			else 
			{
				_connectionString = objProvider.Attributes["connectionString"];
			}
			_providerPath = objProvider.Attributes["providerPath"];
			_objectQualifier = objProvider.Attributes["objectQualifier"];
			if (_objectQualifier != "" & _objectQualifier.EndsWith("_") == false) 
			{
				_objectQualifier += "_";
			}
			_databaseOwner = objProvider.Attributes["databaseOwner"];
			if (_databaseOwner != "" & _databaseOwner.EndsWith(".") == false) 
			{
				_databaseOwner += ".";
			}
		}
		#endregion

		#region Properties
		public string ConnectionString 
		{
			get 
			{
				return _connectionString;
			}
		}

		public string ProviderPath 
		{
			get 
			{
				return _providerPath;
			}
		}

		public string ObjectQualifier 
		{
			get 
			{
				return _objectQualifier;
			}
		}

		public string DatabaseOwner 
		{
			get 
			{
				return _databaseOwner;
			}
		}
		#endregion

		#region Private Methods
		private object GetNull(object Field)
		{
			return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value);
		}

		#endregion

		#region "BTBRandomImage Methods"
		public override IDataReader GetBTBRandomImage(int imageID, int moduleId)
		{
			return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "BTBRandomImageGet", imageID,moduleId);
		}

		public override IDataReader ListBTBRandomImage()
		{
			return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "BTBRandomImageList");
		}

		public override IDataReader GetBTBRandomImageByModules(int moduleID )
		{
			return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "BTBRandomImageGetByModules", moduleID);
		}
		
		public override int AddBTBRandomImage(int moduleID, string imageSrc, string imageAlt, string url)
		{
			return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner + ObjectQualifier + "BTBRandomImageAdd", moduleID, imageSrc, GetNull(imageAlt), url).ToString());
		}
	
		public override void UpdateBTBRandomImage(int imageID, int moduleID, string imageSrc, string imageAlt, string url)
		{
			SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "BTBRandomImageUpdate", imageID, moduleID, imageSrc, GetNull(imageAlt), url);
		}

		public override void DeleteBTBRandomImage(int imageID)
		{
			SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "BTBRandomImageDelete", imageID);
		}
		#endregion


	}
}

