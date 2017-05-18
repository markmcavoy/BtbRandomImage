using System;
using System.Data;
using BiteTheBullet.DNN.Modules.BTBRandomImage.Data;

namespace BiteTheBullet.DNN.Modules.BTBRandomImage.Business
{
	public class BTBRandomImageInfo
	{
		#region "Private Members"
		int _imageID;
		int _moduleID;
		string _imageSrc;
		string _imageAlt;
		string _url;
		bool _newWindow;
		#endregion
		
		#region "Constructors"
		public BTBRandomImageInfo()
		{
		}

		public BTBRandomImageInfo(int imageID, int moduleID , string imageSrc ,
									string imageAlt, string url, bool newWindow)
		{
			this.imageID = imageID;
			this.moduleID = moduleID;
			this.imageSrc = imageSrc;
			this.imageAlt = imageAlt;
			this.Url = url;
			this.NewWindow = newWindow;
		}
		#endregion
		
		#region "Public Properties"
		public int imageID
		{
			get{return _imageID;}
			set{_imageID = value;}
		}
		
		public int moduleID
		{
			get{return _moduleID;}
			set{_moduleID = value;}
		}

		public string imageSrc
		{
			get{return _imageSrc;}
			set{_imageSrc = value;}
		}

		public string imageAlt
		{
			get{return _imageAlt;}
			set{_imageAlt = value;}
		}

		public string Url
		{
			get{ return _url;}
			set{ _url = value;}
		}

		public bool NewWindow
		{
			get{ return _newWindow;}
			set{ _newWindow = value;}
		}
		#endregion
	}
}