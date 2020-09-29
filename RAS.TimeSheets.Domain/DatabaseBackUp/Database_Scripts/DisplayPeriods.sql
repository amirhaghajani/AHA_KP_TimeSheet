USE [RASTimeSheets]
GO

/****** Object:  Table [tsm].[DisplayPeriods]    Script Date: 9/16/2019 7:06:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [tsm].[DisplayPeriods](
	[ID] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[IsWeekly] [bit] NOT NULL,
	[NumOfDays] [int] NOT NULL,
	[EmployeeID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_tsm.DisplayPeriods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [tsm].[DisplayPeriods]  WITH CHECK ADD  CONSTRAINT [FK_tsm.DisplayPeriods_dbo.Users_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [tsm].[DisplayPeriods] CHECK CONSTRAINT [FK_tsm.DisplayPeriods_dbo.Users_EmployeeID]
GO

