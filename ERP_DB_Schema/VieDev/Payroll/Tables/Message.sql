CREATE TABLE [Payroll].[Message] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LiquitadionId] INT           NOT NULL,
    [PayrollDate]   DATE          NOT NULL,
    [Error]         BIT           NOT NULL,
    [Description]   VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_Liquidation] FOREIGN KEY ([LiquitadionId]) REFERENCES [Payroll].[Liquidation] ([Id]) ON DELETE CASCADE
);


GO
ALTER TABLE [Payroll].[Message] NOCHECK CONSTRAINT [FK_Message_Liquidation];

