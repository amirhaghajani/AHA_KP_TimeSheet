USE [RASTimeSheets]
GO

/****** Object:  Table [tsm].[Calendars]    Script Date: 9/16/2019 7:06:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [tsm].[Calendars](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[IsSaturdayWD] [bit] NULL,
	[IsSundayWD] [bit] NULL,
	[IsMondayWD] [bit] NULL,
	[IsTuesdayWD] [bit] NULL,
	[IsWednesdayWD] [bit] NULL,
	[IsThursdayWD] [bit] NULL,
	[IsFridayWD] [bit] NULL,
 CONSTRAINT [PK_tsm.Calendars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

