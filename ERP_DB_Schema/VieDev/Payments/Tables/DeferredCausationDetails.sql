CREATE TABLE [Payments].[DeferredCausationDetails] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdDeferredCausation] INT             NOT NULL,
    [IdMainAccount]       INT             NOT NULL,
    [IdCostCenter]        INT             NULL,
    [Value]               DECIMAL (20, 2) NOT NULL,
    [Nature]              TINYINT         NOT NULL,
    [DateNextPeriod]      DATETIME        NOT NULL,
    CONSTRAINT [PK_DeferredCausationD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausationD_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_DeferredCausationD_PUC] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DeferredCausationDetails_DeferredCausation] FOREIGN KEY ([IdDeferredCausation]) REFERENCES [Payments].[DeferredCausation] ([Id])
);


GO
ALTER TABLE [Payments].[DeferredCausationDetails] NOCHECK CONSTRAINT [FK_DeferredCausationD_CostCenter];


GO
ALTER TABLE [Payments].[DeferredCausationDetails] NOCHECK CONSTRAINT [FK_DeferredCausationD_PUC];


GO
ALTER TABLE [Payments].[DeferredCausationDetails] NOCHECK CONSTRAINT [FK_DeferredCausationDetails_DeferredCausation];

