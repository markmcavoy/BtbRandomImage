using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Exceptions;
using BiteTheBullet.DNN.Modules.BTBRandomImage.Business;

namespace BiteTheBullet.DNN.Modules.BTBRandomImage
{
	public abstract class BTBRandomImageEdit : PortalModuleBase
	{

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			this.cmdDeleteImage.Click += new System.Web.UI.ImageClickEventHandler(this.cmdDeleteImage_Click);
			this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
			this.cmdAddImage.Click += new System.EventHandler(this.cmdAddImage_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Controls

		#endregion
		protected System.Web.UI.WebControls.TextBox txtAlt;
		protected System.Web.UI.WebControls.ListBox lstImages;
		protected System.Web.UI.WebControls.ImageButton cmdDeleteImage;
		protected System.Web.UI.WebControls.Label lblImages;
		protected System.Web.UI.WebControls.LinkButton cmdCancel;
		protected System.Web.UI.WebControls.LinkButton cmdUpdate;
		protected System.Web.UI.WebControls.Panel pnlAddImage;
		protected System.Web.UI.WebControls.LinkButton cmdDone;
		protected System.Web.UI.WebControls.LinkButton cmdAddImage;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAlt;
		protected DotNetNuke.UI.UserControls.UrlControl ctlURL;
		protected DotNetNuke.UI.UserControls.UrlControl ctlLink;

		#region Private Members
		#endregion

		#region Event Handlers
		private void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				if (!Page.IsPostBack) 
				{
					DataBindList();
					lblImages.Text = Localization.GetString("lblImages.Text", LocalResourceFile);
				}
			} 
			catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		

		private void cmdUpdate_Click(object sender, EventArgs e)
		{
			try 
			{
				if (Page.IsValid == true) 
				{
					BTBRandomImageInfo objBTBRandomImage = new BTBRandomImageInfo();
					objBTBRandomImage = ((BTBRandomImageInfo)CBO.InitializeObject(objBTBRandomImage, typeof(BTBRandomImageInfo)));
					
					int fileId = Int32.Parse(ctlURL.Url.Substring(7));

					FileController fileController = new FileController();
					FileInfo fi = fileController.GetFileById(fileId, this.PortalId);
					objBTBRandomImage.imageSrc = fi.Folder + fi.FileName;
					objBTBRandomImage.imageAlt = txtAlt.Text;
					objBTBRandomImage.moduleID = this.ModuleId;
					objBTBRandomImage.Url = ctlLink.Url;

					BTBRandomImageController objCtlBTBRandomImage = new BTBRandomImageController();
					objCtlBTBRandomImage.Add(objBTBRandomImage);

					//update the url DNN table with the URL parameters
					UrlController urlController = new UrlController();
					urlController.UpdateUrl(this.PortalId, ctlLink.Url, ctlLink.UrlType, false, false,
												this.ModuleId, ctlLink.NewWindow);

					DataBindList();
				}
			} 
			catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			pnlAddImage.Visible = false;
		}

		private void cmdDeleteImage_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string selectedImage = lstImages.SelectedValue;
			if(selectedImage != null && selectedImage != String.Empty)
			{
				try
				{
					int imageId = Int32.Parse(selectedImage);
					BTBRandomImageController controller = new BTBRandomImageController();
					controller.Delete(imageId);

					DataBindList();
				}
				catch(FormatException ex)
				{
					//we've not got a valid int in the lstImage value
					Exceptions.ProcessModuleLoadException(this, ex);
				}
				catch(Exception ex)
				{
					Exceptions.ProcessModuleLoadException(this, ex);
				}
			}
		}

		private void cmdDone_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Globals.NavigateURL(), true);
		}

		private void cmdAddImage_Click(object sender, System.EventArgs e)
		{
			pnlAddImage.Visible = true;
		}

		#endregion

		

		private void DataBindList()
		{
			BTBRandomImageController objCtlBTBRandomImage = new BTBRandomImageController();

			ArrayList images = objCtlBTBRandomImage.GetByModules(this.ModuleId);
			lstImages.DataSource = images;
			lstImages.DataBind();
		}

		

		
	}
}