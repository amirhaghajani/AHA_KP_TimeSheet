USE [RASTimeSheets]
GO

/****** Object:  Table [tsm].[WorkHours]    Script Date: 9/16/2019 7:07:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [tsm].[WorkHours](
	[ID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[EmployeeID] [uniqueidentifier] NOT NULL,
	[TaskID] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[Hours] [float] NOT NULL,
	[WorkflowStageID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[PreviousStage] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_tsm.WorkHours] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [tsm].[WorkHours] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [PreviousStage]
GO

ALTER TABLE [tsm].[WorkHours]  WITH CHECK ADD  CONSTRAINT [FK_tsm.WorkHours_dbo.Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [tsm].[WorkHours] CHECK CONSTRAINT [FK_tsm.WorkHours_dbo.Projects_ProjectId]
GO

ALTER TABLE [tsm].[WorkHours]  WITH CHECK ADD  CONSTRAINT [FK_tsm.WorkHours_dbo.Tasks_TaskID] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Tasks] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [tsm].[WorkHours] CHECK CONSTRAINT [FK_tsm.WorkHours_dbo.Tasks_TaskID]
GO

ALTER TABLE [tsm].[WorkHours]  WITH CHECK ADD  CONSTRAINT [FK_tsm.WorkHours_dbo.Users_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [tsm].[WorkHours] CHECK CONSTRAINT [FK_tsm.WorkHours_dbo.Users_EmployeeID]
GO

ALTER TABLE [tsm].[WorkHours]  WITH CHECK ADD  CONSTRAINT [FK_tsm.WorkHours_tsm.WorkflowStages_WorkflowStageID] FOREIGN KEY([WorkflowStageID])
REFERENCES [tsm].[WorkflowStages] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [tsm].[WorkHours] CHECK CONSTRAINT [FK_tsm.WorkHours_tsm.WorkflowStages_WorkflowStageID]
GO

