/****** Object: Table [dbo].[TrackerEvents] Script Date: 10/25/2020 9:46:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TrackerEvents] (
    [EventID]     INT           IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIME2 (7) NOT NULL,
    [TrackerID]   INT           NOT NULL,
    [IPAddress]   NVARCHAR (45) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_TrackerEvents_TrackerID]
    ON [dbo].[TrackerEvents]([TrackerID] ASC);


GO
ALTER TABLE [dbo].[TrackerEvents]
    ADD CONSTRAINT [PK_TrackerEvents] PRIMARY KEY CLUSTERED ([EventID] ASC);


GO
ALTER TABLE [dbo].[TrackerEvents]
    ADD CONSTRAINT [FK_TrackerEvents_Trackers_TrackerID] FOREIGN KEY ([TrackerID]) REFERENCES [dbo].[Trackers] ([TrackerID]) ON DELETE CASCADE;


