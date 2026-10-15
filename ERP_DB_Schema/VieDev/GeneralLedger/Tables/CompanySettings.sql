CREATE TABLE [GeneralLedger].[CompanySettings] (
    [Id]                                    TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LastClosingDate]                       DATETIME     NOT NULL,
    [Consolidate]                           BIT          NOT NULL,
    [ConsolidateDate]                       DATETIME     NULL,
    [SMLV]                                  DECIMAL (18) NOT NULL,
    [UVT]                                   DECIMAL (18) NOT NULL,
    [ConsecutiveFiling]                     BIGINT       CONSTRAINT [DF_CompanySettings_ConsecutiveFiling] DEFAULT ((1)) NOT NULL,
    [CreationUser]                          VARCHAR (20) NOT NULL,
    [CreationDate]                          DATETIME     NOT NULL,
    [ModificationUser]                      VARCHAR (20) NULL,
    [ModificationDate]                      DATETIME     NULL,
    [TimeStamp]                             ROWVERSION   NOT NULL,
    [BusinessLine]                          TINYINT      NULL,
    [WorkIncomeControl]                     BIT          CONSTRAINT [DF__CompanySe__WorkI__5023149A] DEFAULT ((0)) NOT NULL,
    [OfficialCurrencyId]                    INT          CONSTRAINT [DF__CompanySe__Offic__0E7F6E6F] DEFAULT ((1)) NOT NULL,
    [SalePriceIncludeTax]                   BIT          CONSTRAINT [DF__CompanySe__SaleP__56901B07] DEFAULT ((1)) NOT NULL,
    [ThirdPartyCheckDigit]                  BIT          CONSTRAINT [DF__CompanySe__Third__6C4A51FC] DEFAULT ((1)) NOT NULL,
    [ProfitLostByExchangeCurrencyAccountId] INT          NULL,
    [ProfitLostJournalVoucherTypeId]        INT          NULL,
    [LostByExchangeCurrencyAccountId]       INT          NULL,
    [CostCenterId]                          INT          NULL,
    [TaxRegistration]                       TINYINT      CONSTRAINT [DF__CompanySe__TaxRe] DEFAULT ((1)) NOT NULL,
    [TransactionEconomicActivity]           BIT          DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CompanyParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanySettings_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CompanySettings_Currency] FOREIGN KEY ([OfficialCurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CompanySettings_JournalVoucherType] FOREIGN KEY ([ProfitLostJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CompanySettings_MainAccount] FOREIGN KEY ([ProfitLostByExchangeCurrencyAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CompanySettings_MainAccountLost] FOREIGN KEY ([LostByExchangeCurrencyAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[CompanySettings] NOCHECK CONSTRAINT [FK_CompanySettings_CostCenter];


GO
ALTER TABLE [GeneralLedger].[CompanySettings] NOCHECK CONSTRAINT [FK_CompanySettings_Currency];


GO
ALTER TABLE [GeneralLedger].[CompanySettings] NOCHECK CONSTRAINT [FK_CompanySettings_JournalVoucherType];


GO
ALTER TABLE [GeneralLedger].[CompanySettings] NOCHECK CONSTRAINT [FK_CompanySettings_MainAccount];


GO
ALTER TABLE [GeneralLedger].[CompanySettings] NOCHECK CONSTRAINT [FK_CompanySettings_MainAccountLost];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociar Actividad Económica en Transacciones: 
    0. NO
	1. Si
	', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TransactionEconomicActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo (CONTROL FISCAL)  2 - IVA Descontable  3 - IVA Mixto  4 - IVA al Costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dígito de control de terceros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ThirdPartyCheckDigit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta, incluye impuestos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SalePriceIncludeTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Aportes:       0. Calcula UVT Anual    1. Calcula UVT Mensual   ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'WorkIncomeControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Linea de Negocio :  1-Aseguramiento obligatorio  2-Aseguramiento voluntario  3-Prestación de servicios', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'BusinessLine';

