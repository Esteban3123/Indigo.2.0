CREATE TABLE [Integrations].[Outbox] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [OccurredOn]    DATETIME       NOT NULL,
    [EventType]     NVARCHAR (200) NOT NULL,
    [AggregateType] NVARCHAR (100) NULL,
    [AggregateKey]  NVARCHAR (50)  NULL,
    [Payload]       NVARCHAR (MAX) NOT NULL,
    [IsPublished]   BIT            CONSTRAINT [DF_Outbox_IsPublished] DEFAULT ((0)) NOT NULL,
    [Action]        VARCHAR (100)  NOT NULL,
    CONSTRAINT [PK_Outbox_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Outbox_IsPublished]
    ON [Integrations].[Outbox]([IsPublished] ASC, [OccurredOn] ASC);

