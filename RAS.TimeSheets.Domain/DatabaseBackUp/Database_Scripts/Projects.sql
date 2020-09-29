USE [RASTimeSheets]
GO

/****** Object:  Table [dbo].[Projects]    Script Date: 9/16/2019 7:04:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Projects](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[OwnerID] [uniqueidentifier] NULL,
	[CalendarID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Projects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Projects_dbo.Users_OwnerID] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Users] ([ID])
GO

ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_dbo.Projects_dbo.Users_OwnerID]
GO

ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Projects_tsm.Calendars_CalendarID] FOREIGN KEY([CalendarID])
REFERENCES [tsm].[Calendars] ([ID])
GO

ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_dbo.Projects_tsm.Calendars_CalendarID]
GO

