CREATE TABLE [Payments].[DeferredCausation] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable]  INT             NOT NULL,
    [BillNumber]        VARCHAR (20)    NULL,
    [IdMainAccount]     INT             NOT NULL,
    [PeriodsNumber]     INT             NOT NULL,
    [TypeDistribution]  TINYINT         NOT NULL,
    [InitialDate]       DATETIME        NOT NULL,
    [EndDate]           DATETIME        CONSTRAINT [DF_DeferredCausation_EndDate] DEFAULT (((8)/(1))/(2015)) NOT NULL,
    [IdThirdParty]      INT             NOT NULL,
    [IdCostCenter]      INT             NULL,
    [ValueCreditPeriod] DECIMAL (20, 2) NOT NULL,
    [Status]            TINYINT         NOT NULL,
    [CreationUser]      VARCHAR (20)    NOT NULL,
    [CreationDate]      DATETIME        NOT NULL,
    [ModificationUser]  VARCHAR (20)    NULL,
    [ModificationDate]  DATETIME        NULL,
    [ConfirmationUser]  VARCHAR (20)    NULL,
    [ConfirmationDate]  DATETIME        NULL,
    [AnnulmentUser]     VARCHAR (20)    NULL,
    [AnnulmentDate]     DATETIME        NULL,
    [TimeStamp]         ROWVERSION      NOT NULL,
    CONSTRAINT [PK_DeferredCausation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausation_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_DeferredCausation_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_DeferredCausation_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DeferredCausation_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_AccountPayable];


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_CostCenter];


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_MainAccounts];


GO
ALTER TABLE [Payments].[DeferredCausation] NOCHECK CONSTRAINT [FK_DeferredCausation_ThirdParty];

