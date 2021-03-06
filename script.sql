if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BTBRandomImageAdd]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BTBRandomImageAdd]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BTBRandomImageDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BTBRandomImageDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BTBRandomImageGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BTBRandomImageGet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BTBRandomImageGetByModules]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BTBRandomImageGetByModules]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BTBRandomImageList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BTBRandomImageList]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BTBRandomImageUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[BTBRandomImageUpdate]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BTBRandomImage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[BTBRandomImage]
GO

CREATE TABLE [dbo].[BTBRandomImage] (
	[imageID] [int] IDENTITY (1, 1) NOT NULL ,
	[moduleID] [int] NOT NULL ,
	[imageSrc] [varchar] (1000) COLLATE Latin1_General_CI_AS NOT NULL ,
	[imageAlt] [varchar] (500) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BTBRandomImage] WITH NOCHECK ADD 
	CONSTRAINT [PK_BTBRandomImage] PRIMARY KEY  CLUSTERED 
	(
		[imageID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BTBRandomImage] ADD 
	CONSTRAINT [FK_BTBRandomImage_Modules] FOREIGN KEY 
	(
		[moduleID]
	) REFERENCES [dbo].[Modules] (
		[ModuleID]
	) ON DELETE CASCADE  NOT FOR REPLICATION 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.BTBRandomImageAdd
	@moduleID int,
	@imageSrc varchar(1000),
	@imageAlt varchar(500)
AS

INSERT INTO BTBRandomImage (
	[moduleID],
	[imageSrc],
	[imageAlt]
) VALUES (
	@moduleID,
	@imageSrc,
	@imageAlt
)

select SCOPE_IDENTITY()

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.BTBRandomImageDelete
	@imageID int
AS

DELETE FROM BTBRandomImage
WHERE
	[imageID] = @imageID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.BTBRandomImageGet
	@imageID int
	,@moduleId int 
AS

SELECT
	[imageID],
	[moduleID],
	[imageSrc],
	[imageAlt]
FROM BTBRandomImage
WHERE
	[imageID] = @imageID
	AND [moduleid]=@moduleId 

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.BTBRandomImageGetByModules
	@moduleID int
AS

SELECT
	[imageID],
	[moduleID],
	[imageSrc],
	[imageAlt]
FROM BTBRandomImage
WHERE
	[moduleID]=@moduleID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.BTBRandomImageList
AS

SELECT
	[imageID],
	[moduleID],
	[imageSrc],
	[imageAlt]
FROM BTBRandomImage

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE dbo.BTBRandomImageUpdate
	@imageID int, 
	@moduleID int, 
	@imageSrc varchar(1000), 
	@imageAlt varchar(500) 
AS

UPDATE BTBRandomImage SET
	[moduleID] = @moduleID,
	[imageSrc] = @imageSrc,
	[imageAlt] = @imageAlt
WHERE
	[imageID] = @imageID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

