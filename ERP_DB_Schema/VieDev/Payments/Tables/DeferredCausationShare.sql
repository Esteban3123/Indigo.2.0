CREATE TABLE [Payments].[DeferredCausationShare] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DeferredCausationId] INT             NOT NULL,
    [PaymentMonth]        INT             NOT NULL,
    [PaymentYear]         INT             NOT NULL,
    [Value]               DECIMAL (20, 2) NOT NULL,
    [Amortized]           BIT             NOT NULL,
    [CreationUser]        VARCHAR (20)    NOT NULL,
    [CreationDate]        DATETIME        NOT NULL,
    [ModificationUser]    VARCHAR (20)    NULL,
    [ModificationDate]    DATETIME        NULL,
    CONSTRAINT [PK_DeferredCausationShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausationShare_DeferredCausation] FOREIGN KEY ([DeferredCausationId]) REFERENCES [Payments].[DeferredCausation] ([Id])
);


GO
ALTER TABLE [Payments].[DeferredCausationShare] NOCHECK CONSTRAINT [FK_DeferredCausationShare_DeferredCausation];

