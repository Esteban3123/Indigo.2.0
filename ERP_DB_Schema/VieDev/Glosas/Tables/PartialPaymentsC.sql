CREATE TABLE [Glosas].[PartialPaymentsC] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicatedConsecutive] INT           NOT NULL,
    [CustomerId]           INT           NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [State]                CHAR (1)      NOT NULL,
    [Comment]              VARCHAR (250) NULL,
    [ConfirmDateSystem]    DATETIME      NULL,
    [ConfirmUser]          VARCHAR (20)  NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [EntityId]             INT           NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityName]           VARCHAR (250) NULL,
    CONSTRAINT [PK_PartialPaymentsC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PartialPaymentsC_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id])
);


GO
ALTER TABLE [Glosas].[PartialPaymentsC] NOCHECK CONSTRAINT [FK_PartialPaymentsC_Customer];

