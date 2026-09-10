CREATE TABLE [Payroll].[ManualConceptsDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ManualConceptId]       INT             NOT NULL,
    [PayrollDateLiquidated] DATE            NOT NULL,
    [Value]                 DECIMAL (18, 2) NULL,
    [State]                 TINYINT         NOT NULL,
    CONSTRAINT [PK_ManualConceptsDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManualConceptsDetail_ManualConcepts] FOREIGN KEY ([ManualConceptId]) REFERENCES [Payroll].[ManualConcepts] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_ManualConceptsDetail__ManualConceptId]
    ON [Payroll].[ManualConceptsDetail]([ManualConceptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del concepto manual: 1=Esperando pago, 2=Pagado. Indica si el abono/descuento ha sido liquidado en nómina o está pendiente de procesamiento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Esperando Pago, 2 - Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pagado del concepto manual en pesos. Importe monetario del abono, descuento o ajuste aplicado al empleado (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nómina en que se liquida el concepto manual. Período de pago asociado al procesamiento del abono o descuento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'PayrollDateLiquidated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Nómina con que se Liquida', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'PayrollDateLiquidated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'PayrollDateLiquidated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto manual (FK a Payroll.ManualConcepts). Referencia al tipo de abono, descuento o ajuste configurado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'ManualConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto Manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'ManualConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'ManualConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la fila de detalle del concepto manual (INT IDENTITY, PK).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos manuales de nómina: registra cada línea individual de un concepto manual aplicado en un proceso de liquidación de nómina, con su valor y estado de procesamiento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail';
