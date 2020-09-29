USE [RASTimeSheets]
GO

/****** Object:  Table [tsm].[PresenceHours]    Script Date: 9/16/2019 7:07:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [tsm].[PresenceHours](
	[ID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[EmployeeID] [uniqueidentifier] NOT NULL,
	[Hours] [float] NOT NULL,
	[InTime] [nvarchar](max) NULL,
	[OutTime] [nvarchar](max) NULL,
 CONSTRAINT [PK_tsm.PresenceHours] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [tsm].[PresenceHours]  WITH CHECK ADD  CONSTRAINT [FK_tsm.PresenceHours_dbo.Users_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [tsm].[PresenceHours] CHECK CONSTRAINT [FK_tsm.PresenceHours_dbo.Users_EmployeeID]
GO

