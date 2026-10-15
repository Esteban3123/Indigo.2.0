CREATE TABLE [Treasury].[ConstitutionCashSmaller] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)    NOT NULL,
    [DocumentDate]          DATE            NOT NULL,
    [DocumentType]          TINYINT         NOT NULL,
    [CashRegisterSmallerId] INT             NOT NULL,
    [SourceType]            TINYINT         NOT NULL,
    [CashRegisterId]        INT             NULL,
    [EntityBankAccountId]   INT             NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [OperatingUnitId]       INT             NOT NULL,
    [Status]                TINYINT         NOT NULL,
    [CreationUser]          VARCHAR (20)    NOT NULL,
    [CreationDate]          DATETIME        NOT NULL,
    [ModificationUser]      VARCHAR (20)    NULL,
    [ModificationDate]      DATETIME        NULL,
    [ConfirmationUser]      VARCHAR (20)    NULL,
    [ConfirmationDate]      DATETIME        NULL,
    [AnnulmentUser]         VARCHAR (20)    NULL,
    [AnnulmentDate]         DATETIME        NULL,
    [TimeStamp]             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_ConstitutionCashSmaller__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConstitutionCashSmaller_CashRegisters] FOREIGN KEY ([CashRegisterSmallerId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_ConstitutionCashSmaller_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_FundCashSmaller_CashReceipts] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_FundCashSmaller_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[ConstitutionCashSmaller] NOCHECK CONSTRAINT [FK_ConstitutionCashSmaller_CashRegisters];


GO
ALTER TABLE [Treasury].[ConstitutionCashSmaller] NOCHECK CONSTRAINT [FK_ConstitutionCashSmaller_OperatingUnit];


GO
ALTER TABLE [Treasury].[ConstitutionCashSmaller] NOCHECK CONSTRAINT [FK_FundCashSmaller_CashReceipts];


GO
ALTER TABLE [Treasury].[ConstitutionCashSmaller] NOCHECK CONSTRAINT [FK_FundCashSmaller_EntityBankAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del documento  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de fuente de entrada o salida del dinero  1 - Caja Mayor  2 - Cuenta Bancaria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'SourceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de documento  1 - Aumento de Caja Menor  2 - Disminucion de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ConstitutionCashSmaller', @level2type = N'COLUMN', @level2name = N'DocumentType';

