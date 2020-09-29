USE [RASTimeSheets]
GO

/****** Object:  Table [dbo].[Assignments]    Script Date: 9/16/2019 7:04:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Assignments](
	[ID] [uniqueidentifier] NOT NULL,
	[ProjectID] [uniqueidentifier] NULL,
	[TaskID] [uniqueidentifier] NOT NULL,
	[ResourceID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Assignments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignments_dbo.Tasks_TaskID] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_dbo.Assignments_dbo.Tasks_TaskID]
GO

ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignments_dbo.Users_ResourceID] FOREIGN KEY([ResourceID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_dbo.Assignments_dbo.Users_ResourceID]
GO

