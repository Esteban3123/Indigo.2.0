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
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_CostCenter];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_PUC];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_RetentionConcept];


GO
ALTER TABLE [GeneralLedger].[JournalVoucherDetails] NOCHECK CONSTRAINT [FK_AccountingDetail_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [UX_JournalVoucherDetails_IdAccounting]
    ON [GeneralLedger].[JournalVoucherDetails]([IdAccounting] ASC)
    INCLUDE([IdMainAccount], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue], [Detail], [IdRetention], [RetentionRate], [BaseValue], [BillingValue]);


GO
CREATE NONCLUSTERED INDEX [IX_JournalVoucherDetails__IdAccounting]
    ON [GeneralLedger].[JournalVoucherDetails]([IdAccounting] ASC);


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor facturado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor base', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentage de la retencion que se aplico', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'RetentionRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Credito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor debito', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de contabilización', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'IdAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVoucherDetails', @level2type = N'COLUMN', @level2name = N'Id';

