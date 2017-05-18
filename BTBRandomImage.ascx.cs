using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Exceptions;
using BiteTheBullet.DNN.Modules.BTBRandomImage.Business;

namespace BiteTheBullet.DNN.Modules.BTBRandomImage
{
	public abstract class BTBRandomImage : PortalModuleBase, IActionable, IPortable
	{
		protected System.Web.UI.WebControls.HyperLink hlImage;
		protected System.Web.UI.WebControls.Image imgRandom;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Event Handlers

		private void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				BTBRandomImageController objBTBRandomImage = new BTBRandomImageController();
				ArrayList listImage;
				int upperLimit;

				if (!Page.IsPostBack) 
				{
					listImage = objBTBRandomImage.GetByModules(ModuleId);

					if(listImage.Count == 0)
					{
						//no images loaded hide the image control
						//we're done here
						imgRandom.Visible = false;
						return;
					}

					//pick a random image from the arraylist and display it
					upperLimit = listImage.Count;

					Random rand = new Random((int)DateTime.UtcNow.Ticks);
					BTBRandomImageInfo objImage = (BTBRandomImageInfo)listImage[rand.Next(upperLimit)];
					
					imgRandom.Visible = true;
					imgRandom.AlternateText = objImage.imageAlt;
					imgRandom.ImageUrl = PortalSettings.HomeDirectory + objImage.imageSrc;

					//check if we have an URL stored for this image and display the link as 
					//required
					if(objImage.Url != null && objImage.Url != String.Empty)
					{
						//hlImage.Visible = true;
						hlImage.NavigateUrl = Globals.LinkClick(objImage.Url, TabId, ModuleId, false);
						if(objImage.NewWindow)
						{
							hlImage.Target = "_blank";
						}
					}
				}
			} 
			catch (Exception exc) 
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		#endregion

		#region Optional Interfaces

		public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions 
		{
			get 
			{
				DotNetNuke.Entities.Modules.Actions.ModuleActionCollection Actions = new DotNetNuke.Entities.Modules.Actions.ModuleActionCollection();
				Actions.Add(GetNextActionID(), Localization.GetString(DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, LocalResourceFile), DotNetNuke.Entities.Modules.Actions.ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, false);
				return Actions;
			}
		}

		public string ExportModule(int ModuleID)
		{
			// included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			return null;
		}

		public void ImportModule(int ModuleID, string Content, string Version, int UserID)
		{
			// included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
		}

		#endregion

	}
}
