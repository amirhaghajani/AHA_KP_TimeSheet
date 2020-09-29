USE [RASTimeSheets]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 9/16/2019 7:05:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[SPID] [int] NULL,
	[Code] [nvarchar](50) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[EMail] [nvarchar](max) NULL,
	[Mobile] [nvarchar](max) NULL,
	[OrganizationUnitID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.OrganizationUnits_OrganizationUnitID] FOREIGN KEY([OrganizationUnitID])
REFERENCES [dbo].[OrganizationUnits] ([ID])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.OrganizationUnits_OrganizationUnitID]
GO

