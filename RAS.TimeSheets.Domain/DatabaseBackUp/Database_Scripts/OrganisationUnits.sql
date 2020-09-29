USE [RASTimeSheets]
GO

/****** Object:  Table [dbo].[OrganizationUnits]    Script Date: 9/16/2019 7:04:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrganizationUnits](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[ManagerID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.OrganizationUnits] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[OrganizationUnits]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrganizationUnits_dbo.Users_User_ID] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Users] ([ID])
GO

ALTER TABLE [dbo].[OrganizationUnits] CHECK CONSTRAINT [FK_dbo.OrganizationUnits_dbo.Users_User_ID]
GO

