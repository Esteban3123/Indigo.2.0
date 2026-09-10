CREATE TABLE [Glosas].[GlosaMovementGlosaConciliation] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaMovementGlosaId]            INT             NOT NULL,
    [ConciliationCId]                 INT             NOT NULL,
    [ResponseHierarchyConciliationId] INT             NULL,
    [ValueAcceptedIPSconciliation]    DECIMAL (18, 2) NOT NULL,
    [ValueAcceptedEAPBconciliation]   DECIMAL (18, 2) NOT NULL,
    [RationaleConciliation]           VARCHAR (MAX)   NULL,
    [RationaleDateConciliation]       DATETIME        NOT NULL,
    [State]                           TINYINT         NOT NULL,
    CONSTRAINT [PK_GlosaMovementGlosaConciliation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMovementGlosaConciliation_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosaMovementGlosa] FOREIGN KEY ([GlosaMovementGlosaId]) REFERENCES [Glosas].[GlosaMovementGlosa] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosasResponseHierarchyConciliation] FOREIGN KEY ([ResponseHierarchyConciliationId]) REFERENCES [Glosas].[GlosasResponseHierarchy] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaMovementGlosaConciliation] NOCHECK CONSTRAINT [FK_GlosaMovementGlosaConciliation_ConciliationC];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosaConciliation] NOCHECK CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosaMovementGlosa];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosaConciliation] NOCHECK CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosasResponseHierarchyConciliation];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GlosaMovementGlosaConciliation]
    ON [Glosas].[GlosaMovementGlosaConciliation]([GlosaMovementGlosaId] ASC, [ConciliationCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la conciliación antes de iniciar el proceso: pendiente, en progreso, completada o rechazada (TINYINT, 0-255). Indica fase actual del ciclo de conciliación de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado antes de iniciar la conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la conciliación (DATETIME). Marca cuándo se documentó la aceptación, rechazo o ajuste de valores entre IPS y EAPB.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o razón de conciliación: motivo de aceptación, rechazo, ajuste de valores o acuerdos entre entidades (VARCHAR MAX). Campo de observaciones PII según política.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'razon de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario aceptado por la EAPB (asegurador) en la conciliación de glosa (DECIMAL 18,2). Monto final acreditado por el plan de salud.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado EAPB en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario aceptado por la IPS (prestador de servicios) en la conciliación de glosa (DECIMAL 18,2). Monto final reconocido por el proveedor de salud.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor aceptado IPS en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la respuesta jerárquica de concepto de aceptación para conciliación (INT, FK a GlosasResponseHierarchy). Liga a categoría de acuerdo predefinida.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ResponseHierarchyConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion del Concepto de Aceptación Jerarquíca para Conciliación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ResponseHierarchyConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ResponseHierarchyConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/documento maestro de conciliación (INT, FK a ConciliationC). Relaciona a la factura o acta de conciliación principal.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ConciliationCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de conciliación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ConciliationCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ConciliationCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del movimiento de glosa asociado (INT, FK a GlosaMovementGlosa). Traza la glosa original (reclamo, objeción o discrepancia de factura).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion del Movimiento de Glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de conciliación de movimiento de glosa (INT IDENTITY, clave primaria). Autonumérico secuencial para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de movimientos de glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de conciliaciones realizadas sobre movimientos de glosa entre la IPS y la EAPB (aseguradora). Guarda los valores acordados, las justificaciones y el estado de cada proceso de conciliación de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation';
