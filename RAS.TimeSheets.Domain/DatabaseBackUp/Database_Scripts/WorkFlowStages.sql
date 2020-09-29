USE [RASTimeSheets]
GO

/****** Object:  Table [tsm].[WorkflowStages]    Script Date: 9/16/2019 7:07:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [tsm].[WorkflowStages](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Order] [int] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[IsFirst] [bit] NOT NULL,
	[IsLast] [bit] NOT NULL,
 CONSTRAINT [PK_tsm.WorkflowStages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

