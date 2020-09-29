USE [RASTimeSheets]
GO

/****** Object:  Table [tsm].[Holidays]    Script Date: 9/16/2019 7:06:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [tsm].[Holidays](
	[ID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[CalendarID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_tsm.Holidays] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [tsm].[Holidays]  WITH CHECK ADD  CONSTRAINT [FK_tsm.Holidays_tsm.Calendars_CalendarID] FOREIGN KEY([CalendarID])
REFERENCES [tsm].[Calendars] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [tsm].[Holidays] CHECK CONSTRAINT [FK_tsm.Holidays_tsm.Calendars_CalendarID]
GO

