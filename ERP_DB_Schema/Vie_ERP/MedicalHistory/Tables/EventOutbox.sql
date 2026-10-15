CREATE TABLE [MedicalHistory].[EventOutbox] (
    [ID]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Aggregate_type] NVARCHAR (100)   NOT NULL,
    [Aggregate_id]   NVARCHAR (100)   NOT NULL,
    [Event_type]     NVARCHAR (100)   NOT NULL,
    [Payload]        NVARCHAR (MAX)   NOT NULL,
    [Status]         NVARCHAR (50)    DEFAULT ('pending') NOT NULL,
    [Retry_count]    INT              DEFAULT ((0)) NOT NULL,
    [Error_message]  NVARCHAR (MAX)   NULL,
    [Created_at]     DATETIME2 (7)    DEFAULT (sysutcdatetime()) NOT NULL,
    [Processed_at]   DATETIME2 (7)    NULL,
    [EntitySource]   VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

