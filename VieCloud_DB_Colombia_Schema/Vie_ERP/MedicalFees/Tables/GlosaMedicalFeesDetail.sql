CREATE TABLE [MedicalFees].[GlosaMedicalFeesDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaMedicalFeesId]         INT             NOT NULL,
    [AccountPayableId]           INT             NOT NULL,
    [GlosaMedicalFeesConceptsId] INT             NOT NULL,
    [UnitValue]                  DECIMAL (18, 2) NOT NULL,
    [Quantity]                   INT             NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Observation]                VARCHAR (300)   NULL,
    [Evaluated]                  BIT             NOT NULL,
    CONSTRAINT [PK_GlosaMedicalFeesDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMedicalFeesDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_GlosaMedicalFeesDetail_GlosaMedicalFees] FOREIGN KEY ([GlosaMedicalFeesId]) REFERENCES [MedicalFees].[GlosaMedicalFees] ([Id]),
    CONSTRAINT [FK_GlosaMedicalFeesDetail_GlosaMedicalFeesConcepts] FOREIGN KEY ([GlosaMedicalFeesConceptsId]) REFERENCES [MedicalFees].[GlosaMedicalFeesConcepts] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de evaluación (BIT). Identifica si el detalle de glosa ha sido revisado, procesado o validado (1=Evaluado, 0=Pendiente).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Evaluated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identifica si el regitro esta evaluado o no', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Evaluated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Evaluated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o justificación de la glosa (VARCHAR 300, nullable). Notas sobre motivo del rechazo, reparo técnico o administrativo.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total glosado (DECIMAL 18,2). Resultado de Quantity × UnitValue. Monto de rechazo, reparo o ajuste.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total Glosado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades glosadas (INT). Número de servicios, procedimientos o actos médicos incluidos en la glosa.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de los valores unitarios Glosado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario glosado (DECIMAL 18,2). Precio unitario del servicio, procedimiento o honorario rechazado o repuesto.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor unitario Glosado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del concepto de glosa aplicado (ej: rechazo, reparo, ajuste). Referencia a MedicalFees.GlosaMedicalFeesConcepts.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla GlosaMedicalFeesConcepts', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuenta por pagar asociada. Referencia a Payments.AccountPayable.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla AccountPayable', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la glosa de honorarios médicos padre. Referencia a MedicalFees.GlosaMedicalFees.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla GlosaMedicalFees', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del detalle de glosa de honorarios médicos. Clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de glosas sobre honorarios médicos: registra cada concepto glosado dentro de una glosa, con los valores unitarios, cantidades, totales y observaciones del proceso de auditoría o revisión de cuentas médicas.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail';
