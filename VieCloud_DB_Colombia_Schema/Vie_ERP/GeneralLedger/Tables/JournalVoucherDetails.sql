CREATE TABLE [GeneralLedger].[JournalVoucherDetails] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccounting]  INT             NOT NULL,
    [IdMainAccount] INT             NOT NULL,
    [IdThirdParty]  INT             NULL,
    [IdCostCenter]  INT             NULL,
    [DebitValue]    DECIMAL (21, 5) CONSTRAINT [DF_AccountingDetail_DebitValue] DEFAULT ((0)) NOT NULL,
    [CreditValue]   DECIMAL (21, 5) CONSTRAINT [DF_AccountingDetail_CreditValue] DEFAULT ((0)) NOT NULL,
    [Detail]        VARCHAR (MAX)   CONSTRAINT [DF_AccountingDetail_Detail] DEFAULT ('-') NULL,
    [IdRetention]   INT             NULL,
    [RetentionRate] DECIMAL (6, 3)  NULL,
    [BaseValue]     DECIMAL (18, 2) CONSTRAINT [DF_JournalVoucherDetails_BaseValue] DEFAULT ((0)) NULL,
    [BillingValue]  DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_JournalVoucherDetails__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingDetail_Accounting] FOREIGN KEY ([IdAccounting]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_AccountingDetail_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountingDetail_PUC] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountingDetail_RetentionConcept] FOREIGN KEY ([IdRetention]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountingDetail_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_Accounting];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_PUC];




GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_Accounting];


GO



GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_PUC];


GO



GO





GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_Accounting];


GO



GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_PUC];


GO



GO



GO
CREATE NONCLUSTERED INDEX [UX_JournalVoucherDetails_IdAccounting]
    ON [GeneralLedger].[JournalVoucherDetails]([IdAccounting] ASC)
    INCLUDE([IdMainAccount], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue], [Detail], [IdRetention], [RetentionRate], [BaseValue], [BillingValue]);


GO
CREATE NONCLUSTERED INDEX [IX_JournalVoucherDetails__IdAccounting]
    ON [GeneralLedger].[JournalVoucherDetails]([IdAccounting] ASC);


GO
ALTER INDEX [IX_JournalVoucherDetails__IdAccounting]
    ON [GeneralLedger].[JournalVoucherDetails] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IDX_JournalVoucherDetails_IdMainAccount_DB]
    ON [GeneralLedger].[JournalVoucherDetails]([IdMainAccount] ASC)
    INCLUDE([IdAccounting], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue]);


GO
-- Índice optimizado para JOINs del SP_ReportAuxiliar por IdMainAccount e IdAccounting
CREATE NONCLUSTERED INDEX [IX_JournalVoucherDetails__IdMainAccount__IdAccounting]
    ON [GeneralLedger].[JournalVoucherDetails]([IdMainAccount] ASC, [IdAccounting] ASC)
    INCLUDE ([IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue]);


GO

-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 01/02/2016
-- Description:	Trigger para que genere error cuando hayan datos inconsistentes
-- =============================================
create TRIGGER [GeneralLedger].[TriggerValidateDataLegalbook]
   ON [GeneralLedger].[JournalVoucherDetails]
   AFTER INSERT,UPDATE
AS 
BEGIN
	if (select count(*) from inserted 
	inner join GeneralLedger.MainAccounts ma on ma.Id = inserted.IdMainAccount 
	inner join GeneralLedger.JournalVouchers jv on jv.Id = inserted.IdAccounting 
	where jv.LegalBookId <> ma.LegalBookId ) > 0 begin
		THROW 51000, 'Existen cuentas contables que son de diferente libro al del comprobante', 1
	end
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado, monto total de la transacción comercial registrada (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor facturado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base, cantidad sobre la cual se calcula retención o impuesto (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención aplicado, tasa fiscal retenida en la transacción (DECIMAL 6,3)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentage de la retencion que se aplico', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'RetentionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'RetentionRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de retención, referencia a concepto de retención fiscal del tercero (FK RetentionConcepts)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle, descripción narrativa de la línea contable, concepto o justificación (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor crédito, movimiento acreedor en cuenta (DECIMAL 21,5)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Credito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor débito, movimiento deudor en cuenta (DECIMAL 21,5)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor debito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costo, unidad funcional o área a la que se imputa el gasto (FK CostCenter)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero, identificador del proveedor, paciente o entidad relacionada (FK ThirdParty)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta contable, referencia al Plan Único de Cuentas (PUC) principal (FK MainAccounts)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera de contabilización, referencia al comprobante diario (FK JournalVouchers)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de contabilización', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id autonumérico, identificador único de cada línea del detalle de comprobante contable (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líneas de detalle de los comprobantes contables (vouchers). Registra cada movimiento débito/crédito de un asiento contable, con su cuenta principal, tercero, centro de costo, retención aplicable y valores de base y facturación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails';
