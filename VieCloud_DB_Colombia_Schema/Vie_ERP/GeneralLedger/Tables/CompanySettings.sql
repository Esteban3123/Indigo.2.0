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
    [AppliesCatalogPropertyandServices]      BIT          CONSTRAINT [DF_CompanySettings_AppliesCatalogPropertyandServices] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CompanyParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompanySettings_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CompanySettings_Currency] FOREIGN KEY ([OfficialCurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CompanySettings_JournalVoucherType] FOREIGN KEY ([ProfitLostJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CompanySettings_MainAccount] FOREIGN KEY ([ProfitLostByExchangeCurrencyAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CompanySettings_MainAccountLost] FOREIGN KEY ([LostByExchangeCurrencyAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);








GO



GO



GO



GO



GO





GO



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para asociar actividad económica en transacciones contables: 0=No aplica, 1=Sí aplica. Permite vincular código CIIU/actividad económica a movimientos del libro mayor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TransactionEconomicActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asociar Actividad Económica en Transacciones: 
    0. NO
	1. Si
	', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TransactionEconomicActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TransactionEconomicActivity';












GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de registro y control del IVA (TINYINT): 1=IVA al Costo (Control Fiscal), 2=IVA Descontable, 3=IVA Mixto, 4=IVA al Costo. Determina tratamiento fiscal en el libro mayor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo (CONTROL FISCAL)  2 - IVA Descontable  3 - IVA Mixto  4 - IVA al Costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (INT FK) al Centro de Costo asociado a cuentas de ganancia/pérdida. Permite asignación de gastos e ingresos por unidad funcional o área.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del Centro de costo asociado a las cuentas de ganancia o perdida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (INT FK) a Cuenta Principal (MainAccount) del libro oficial para contabilizar pérdidas por diferencia en tasa de cambio, variación cambiaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LostByExchangeCurrencyAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo donde se registra la cuenta contable del libro oficial donde se va contabilizar la ganancia o perdida por cambios en la tasa de cambio', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LostByExchangeCurrencyAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LostByExchangeCurrencyAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (INT FK) a Tipo de Comprobante Contable (JournalVoucherType) utilizado para registrar ajustes por cambio en tasa de cambio, diferencia cambiaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante contable para realizar el movimiento de ajuste por cambio de la tasa', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (INT FK) a Cuenta Principal (MainAccount) del libro oficial para contabilizar ganancias por diferencia en tasa de cambio, variación cambiaria positiva.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostByExchangeCurrencyAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo donde se registra la cuenta contable del libro oficial donde se va contabilizar la ganancia o perdida por cambios en la tasa de cambio', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostByExchangeCurrencyAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ProfitLostByExchangeCurrencyAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT): activa/desactiva validación de dígito verificador en terceros (proveedores, clientes). Común en Colombia para RUT/NIT.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ThirdPartyCheckDigit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dígito de control de terceros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ThirdPartyCheckDigit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ThirdPartyCheckDigit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT): especifica si precio de venta incluye impuestos (IVA, retenciones) o es precio neto. Afecta cálculo de tarifa en factura.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SalePriceIncludeTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio de venta, incluye impuestos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SalePriceIncludeTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SalePriceIncludeTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (INT FK) a Moneda Oficial del Sistema (Currency). Define divisa base de contabilización, libro mayor, reportes financieros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda Oficial del Sitema', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de cálculo de aportes (BIT): 0=Calcula UVT anual, 1=Calcula UVT mensual. Afecta base de retenciones, contribuciones en nómina.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'WorkIncomeControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Aportes:       0. Calcula UVT Anual    1. Calcula UVT Mensual   ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'WorkIncomeControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'WorkIncomeControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de línea de negocio (TINYINT): 1=Aseguramiento obligatorio, 2=Aseguramiento voluntario, 3=Prestación de servicios. Segmento de actividad económica.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'BusinessLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Linea de Negocio :  1-Aseguramiento obligatorio  2-Aseguramiento voluntario  3-Prestación de servicios', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'BusinessLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'BusinessLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) de creación, modificación o registro del evento. Auditoría de cambios en configuración.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/hora (DATETIME) de última modificación del registro de configuración. Auditoría, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó última modificación. Auditoría, responsabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/hora (DATETIME) de creación del registro de configuración. Trazabilidad histórica.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que creó el registro. Auditoría, responsabilidad inicial.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo (BIGINT) para radicación de cuentas por pagar, comprobantes. Numeración secuencial obligatoria en contabilidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsecutiveFiling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el consecutivo de radicacion de cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsecutiveFiling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsecutiveFiling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de Valor Tributario (DECIMAL 18), factor índice tributario anual en Colombia. Base para cálculo de impuestos, multas, sanciones.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'UVT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Valor Tributario', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'UVT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'UVT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario Mínimo Mensual Legal Vigente (DECIMAL 18), referencia salarial anual. Base para cálculo de aportes, cotizaciones, retenciones.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SMLV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Mínimo Mensual Legal Vigente ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SMLV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'SMLV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/hora (DATETIME) de última conciliación, consolidación o cierre de períodos contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsolidateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica la fecha de conciliacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsolidateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'ConsolidateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si está activa la consolidación de períodos, cuentas o centros de costo.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Consolidate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica si consolida', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Consolidate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Consolidate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/hora (DATETIME) del último cierre contable, corte fiscal o período. Referencia para reaperturas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LastClosingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del ultimo cierre', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LastClosingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'LastClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (TINYINT IDENTITY) del registro de configuración de empresa. Clave primaria, inmutable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del Registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general de la empresa para el módulo de contabilidad (libro mayor). Guarda parámetros fiscales y contables globales como el último cierre contable, valores de referencia (SMLV, UVT), moneda oficial, manejo de impuestos en precios de venta y cuentas para diferencias en cambio de moneda extranjera.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'CompanySettings';
