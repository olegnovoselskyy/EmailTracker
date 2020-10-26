/****** Object: Table [dbo].[Trackers] Script Date: 10/25/2020 9:45:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Trackers] (
    [TrackerID]   INT              IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIME2 (7)    NOT NULL,
    [ExternalID]  UNIQUEIDENTIFIER NOT NULL
);


