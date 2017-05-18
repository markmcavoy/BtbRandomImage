using System;
using System.Data;
using System.Collections;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Localization;
using BiteTheBullet.DNN.Modules.BTBRandomImage.Data;

namespace BiteTheBullet.DNN.Modules.BTBRandomImage.Business
{
	public class BTBRandomImageController : DotNetNuke.Entities.Modules.IPortable
	{
		#region "Public Methods"		
		public BTBRandomImageInfo Get(int imageID,int moduleId )
		{
			return (BTBRandomImageInfo)DotNetNuke.Common.Utilities.CBO.FillObject(DataProvider.Instance().GetBTBRandomImage(imageID,moduleId), typeof(BTBRandomImageInfo));
		}

		public ArrayList List()
		{
			return DotNetNuke.Common.Utilities.CBO.FillCollection(DataProvider.Instance().ListBTBRandomImage(), typeof(BTBRandomImageInfo));
		}

		public ArrayList GetByModules(int moduleID )
		{
			return DotNetNuke.Common.Utilities.CBO.FillCollection(DataProvider.Instance().GetBTBRandomImageByModules(moduleID), typeof(BTBRandomImageInfo));
		}
	
		public int Add(BTBRandomImageInfo objBTBRandomImage)
		{
			return (int)DataProvider.Instance().AddBTBRandomImage(objBTBRandomImage.moduleID, objBTBRandomImage.imageSrc, objBTBRandomImage.imageAlt, objBTBRandomImage.Url);
		}
	
		public void Update(BTBRandomImageInfo objBTBRandomImage)
		{
			DataProvider.Instance().UpdateBTBRandomImage(objBTBRandomImage.imageID, objBTBRandomImage.moduleID, 
										objBTBRandomImage.imageSrc, objBTBRandomImage.imageAlt,
										objBTBRandomImage.Url);
		}
		
		public void Delete(int imageID)
		{
			DataProvider.Instance().DeleteBTBRandomImage(imageID);
		}
		#endregion

		#region "Optional Interfaces"		
	
		public string ExportModule(int ModuleID)
		{
			StringBuilder sb;
			ArrayList imageList;


			imageList = this.GetByModules(ModuleID);
			if(imageList.Count > 0)
			{
				sb = new StringBuilder();
				sb.Append("<RandomImages>");
				
				foreach(BTBRandomImageInfo image in imageList)
				{
					sb.Append("<RandomImage>");
					sb.Append("<imageSrc>");
					sb.Append(Globals.XMLEncode(image.imageSrc));
					sb.Append("</imageSrc>");
					sb.Append("<imageAlt>");
					sb.Append(Globals.XMLEncode(image.imageAlt));
					sb.Append("</imageAlt>");
					sb.Append("<url>");
					sb.Append(Globals.XMLEncode(image.Url));
					sb.Append("</url>");
					sb.Append("</RandomImage>");
				}
				sb.Append("</RandomImages>");

				return sb.ToString();
			}
			else
				return "";
			
		}

		public void ImportModule(int ModuleID, string Content, string Version, int UserId)
		{
			XmlNode xmlImages;

			xmlImages = Globals.GetContent(Content, "RandomImages");

			foreach(XmlNode xmlImage in xmlImages)
			{
				BTBRandomImageInfo image = new BTBRandomImageInfo();
				image.moduleID = ModuleID;
				image.imageSrc = xmlImage["imageSrc"].InnerText;
				image.imageAlt = xmlImage["imageAlt"].InnerText;

				if(Version == "01.01.00")
				{
					image.Url = xmlImage["url"].InnerText;
				}

				this.Add(image);
			}
		}
		#endregion
	}
}