CREATE TABLE [Treasury].[Consignment] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)    NOT NULL,
    [DocumentDate]        DATETIME        NOT NULL,
    [EntityBankAccountId] INT             NOT NULL,
    [MainAccountId]       INT             NOT NULL,
    [CostCenterId]        INT             NULL,
    [Description]         VARCHAR (MAX)   NULL,
    [Value]               DECIMAL (18, 2) NOT NULL,
    [OperativeUnitId]     INT             NOT NULL,
    [Status]              TINYINT         NOT NULL,
    [CreationUser]        VARCHAR (20)    CONSTRAINT [DF_ConsignmentTransfer_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME        CONSTRAINT [DF_ConsignmentTransfer_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)    NULL,
    [ModificationDate]    DATETIME        NULL,
    [ConfirmationUser]    VARCHAR (20)    NULL,
    [ConfirmationDate]    DATETIME        NULL,
    [AnnulmentUser]       VARCHAR (20)    NULL,
    [AnnulmentDate]       DATETIME        NULL,
    [TimeStamp]           ROWVERSION      NOT NULL,
    [ReversedUser]        VARCHAR (20)    NULL,
    [ReversedDate]        DATETIME        NULL,
    CONSTRAINT [PK_ConsignmentTransfer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Consignment_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_ConsignmentTransfer_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ConsignmentTransfer_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_ConsignmentTransfer_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[Consignment] NOCHECK CONSTRAINT [FK_Consignment_OperatingUnit];


GO
ALTER TABLE [Treasury].[Consignment] NOCHECK CONSTRAINT [FK_ConsignmentTransfer_CostCenter];


GO
ALTER TABLE [Treasury].[Consignment] NOCHECK CONSTRAINT [FK_ConsignmentTransfer_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[Consignment] NOCHECK CONSTRAINT [FK_ConsignmentTransfer_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento   1 - Registrado  2 - Confirmado  3 - Anulado  4 - Reversado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Consignment', @level2type = N'COLUMN', @level2name = N'Status';

