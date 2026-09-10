CREATE TABLE [MedicalFees].[GlosaMedicalFeesEvaluation] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaMedicalFeesDetailId] INT             NOT NULL,
    [AcceptedValueProv]        DECIMAL (18, 2) NOT NULL,
    [RaisedValue]              DECIMAL (18, 2) NOT NULL,
    [PendingValue]             DECIMAL (18, 2) NOT NULL,
    [Observation]              VARCHAR (300)   NULL,
    CONSTRAINT [PK_GlosaMedicalFeesEvaluation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMedicalFeesEvaluation_GlosaMedicalFeesDetail] FOREIGN KEY ([GlosaMedicalFeesDetailId]) REFERENCES [MedicalFees].[GlosaMedicalFeesDetail] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación, comentario o nota adicional sobre la evaluación de glosa de honorarios médicos (VARCHAR 300, máx 300 caracteres)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pendiente de resolución en glosa de honorarios, monto que queda por definir o liquidar (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'PendingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pendiente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'PendingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'PendingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor levantado, objetado o reclamado en glosa de honorarios médicos, monto en disputa (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'RaisedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Levantado ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'RaisedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'RaisedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aceptado por proveedor, monto reconocido o aprobado en evaluación de glosa (DECIMAL 18,2, FK a GlosaMedicalFeesDetail)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'AcceptedValueProv';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Aceptado Proveedor', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'AcceptedValueProv';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'AcceptedValueProv';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la glosa de honorarios (detalle), relación FK a tabla GlosaMedicalFeesDetail, clave foránea (INT)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla GlosaMedicalFeesDetail', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de evaluación de glosa de honorarios médicos, clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluaciones o respuestas a los detalles de glosas médicas, registrando los valores aceptados por el prestador, los valores radicados en objeción y los valores pendientes de resolución, junto con observaciones del proceso de conciliación de glosas.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation';
