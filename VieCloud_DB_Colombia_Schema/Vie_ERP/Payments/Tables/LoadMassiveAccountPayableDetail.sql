CREATE TABLE [Payments].[LoadMassiveAccountPayableDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoadMassiveAccountPayableId] INT             NOT NULL,
    [AccountPayableConceptId]     INT             NOT NULL,
    [ThirdPartyId]                INT             NOT NULL,
    [CostCenterId]                INT             NULL,
    [Detail]                      VARCHAR (500)   NULL,
    [Nature]                      TINYINT         NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    [RetentionConceptId]          INT             NULL,
    [BaseValue]                   DECIMAL (18, 2) CONSTRAINT [DF_LoadMassiveAccountPayableDetail_BaseValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Payments_LoadMassiveAccountPayableDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_AccountPayableConcept] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_LoadMassiveAccountPayable] FOREIGN KEY ([LoadMassiveAccountPayableId]) REFERENCES [Payments].[LoadMassiveAccountPayable] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_RetentionConcept] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base para cálculo de retenciones (DECIMAL 18,2, default 0; base imponible antes de aplicar porcentajes de retención)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención (FK a RetentionConcepts, opcional; impuesto/retención aplicable: IVA, ICA, renta, etc.)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del movimiento (DECIMAL 18,2, monto en pesos del débito o crédito aplicado al concepto)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable del movimiento (TINYINT: 1=Débito/Cargo, 2=Crédito/Abono; indica si aumenta o reduce la obligación)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza (Débito = 1, Crédito = 2)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del concepto (VARCHAR 500, notas adicionales, referencia de factura, período, detalles específicos del servicio)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (FK a CostCenter, opcional; centro que absorbe el gasto contable)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero acreedor (FK a ThirdParty, proveedor/contratista/prestador a quien se adeuda)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuenta por pagar (FK a AccountPayableConcepts, define tipo de obligación: honorarios, servicios, suministros, etc.)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto de cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar cargada masivamente (FK a LoadMassiveAccountPayable, agrupa múltiples detalles)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'LoadMassiveAccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta por pagar cargada masivamente', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'LoadMassiveAccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'LoadMassiveAccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de carga masiva de cuenta por pagar (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las líneas de carga masiva de cuentas por pagar. Cada fila representa un concepto de pago asociado a un tercero dentro de una carga masiva, incluyendo el valor, la naturaleza contable (débito/crédito), el centro de costo y la retención aplicable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'LoadMassiveAccountPayableDetail';
