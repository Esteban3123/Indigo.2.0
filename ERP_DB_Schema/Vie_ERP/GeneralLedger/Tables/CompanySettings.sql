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
    [AppliesCatalogPropertyandServices]     BIT          CONSTRAINT [DF_CompanySettings_AppliesCatalogPropertyandServices] DEFAULT ((0)) NOT NULL,
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica Catálogo de Bienes y Servicios: 
    0. NO
	1. Si
	', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'AppliesCatalogPropertyandServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo (CONTROL FISCAL)  2 - IVA Descontable  3 - IVA Mixto  4 - IVA al Costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del Centro de costo asociado a las cuentas de ganancia o perdida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo donde se registra la cuenta contable del libro oficial donde se va contabilizar la ganancia o perdida por cambios en la tasa de cambio', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LostByExchangeCurrencyAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante contable para realizar el movimiento de ajuste por cambio de la tasa', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo donde se registra la cuenta contable del libro oficial donde se va contabilizar la ganancia o perdida por cambios en la tasa de cambio', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostByExchangeCurrencyAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dígito de control de terceros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ThirdPartyCheckDigit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta, incluye impuestos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SalePriceIncludeTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda Oficial del Sitema', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Aportes:       0. Calcula UVT Anual    1. Calcula UVT Mensual   ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'WorkIncomeControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Linea de Negocio :  1-Aseguramiento obligatorio  2-Aseguramiento voluntario  3-Prestación de servicios', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'BusinessLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el consecutivo de radicacion de cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsecutiveFiling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Valor Tributario', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'UVT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Mínimo Mensual Legal Vigente ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SMLV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica la fecha de conciliacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsolidateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica si consolida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Consolidate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha del ultimo cierre', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LastClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del Registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Id';

