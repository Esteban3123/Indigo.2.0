CREATE TABLE [Payments].[PaymentTransferOtherConcept] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PaymentTransferId]           INT             NOT NULL,
    [AccountPayableConceptNoteId] INT             NOT NULL,
    [MainAccountId]               INT             NOT NULL,
    [ThirdPartyId]                INT             NULL,
    [CostCenterId]                INT             NULL,
    [Nature]                      TINYINT         NOT NULL,
    [Value]                       NUMERIC (20, 2) NOT NULL,
    CONSTRAINT [PK_PaymentTransferOtherConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentTransferOtherConcept_AccountPayableConceptNotes] FOREIGN KEY ([AccountPayableConceptNoteId]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_PaymentTransfer] FOREIGN KEY ([PaymentTransferId]) REFERENCES [Payments].[PaymentTransfer] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico del concepto en traslado de pago; monto de débito o crédito contable (NUMERIC 20,2)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable del concepto: tipo de movimiento (débito/crédito, TINYINT); clasificación de la transacción', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Naturaleza', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo asociado; unidad funcional, departamento o área de facturación que genera el concepto (FK Payroll.CostCenter)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero acreedor o deudor; proveedor, institución, entidad o prestador involucrado en el pago (FK Common.ThirdParty, nullable)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal en el mayor general; cuenta de gasto, pasivo o activo que registra el movimiento (FK GeneralLedger.MainAccounts)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de nota por pagar; vincula el detalle a la categoría de gasto, factura o acreencia (FK Payments.AccountPayableConceptNotes)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del traslado de pago padre; referencia a la cabecera que agrupa todos los conceptos del movimiento de tesorería (FK Payments.PaymentTransfer)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'PaymentTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del traslado de la cabecera', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'PaymentTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'PaymentTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY INT); llave primaria que identifica cada detalle de concepto en traslado de pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los conceptos contables adicionales (otros conceptos) asociados a una transferencia de pago, incluyendo la cuenta contable, tercero, centro de costo, naturaleza débito/crédito y valor de cada movimiento auxiliar generado durante la transferencia.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept';
