CREATE TABLE [Glosas].[GlosaSequenceDetail] (
    [Id]               INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseGlosaC] INT    NOT NULL,
    [IdSequense]       INT    NOT NULL,
    [IdOperatingUnit]  INT    NULL,
    [Next]             BIGINT CONSTRAINT [DF_GlosaSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_GlosaSequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaSequenceDetail_GlosaSequence] FOREIGN KEY ([IdSequenseGlosaC]) REFERENCES [Glosas].[GlosaSequence] ([Id]),
    CONSTRAINT [FK_GlosaSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_GlosaSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaSequenceDetail] NOCHECK CONSTRAINT [FK_GlosaSequenceDetail_GlosaSequence];


GO
ALTER TABLE [Glosas].[GlosaSequenceDetail] NOCHECK CONSTRAINT [FK_GlosaSequenceDetail_OperatingUnit];


GO
ALTER TABLE [Glosas].[GlosaSequenceDetail] NOCHECK CONSTRAINT [FK_GlosaSequenceDetail_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número a generar en la secuencia de glosa (BIGINT, default=1). Contador incremental para numeración automática de documentos de glosa/reclamación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Unidad Operativa (Centro de Atención/Sede) asignada a la secuencia de glosa. Solo se completa cuando el ámbito es UO (Unidad Operativa); en caso contrario es nulo. Foreign Key a [Common].[OperatingUnit].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Secuencia Base de numeración (configuración estándar de secuencias del sistema). Foreign Key a [Common].[Sequense].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Cabecera/Encabezado de la Glosa. Foreign Key a [Glosas].[GlosaSequence]. Vincula este detalle con su configuración principal de secuencia de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseGlosaC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseGlosaC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseGlosaC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle de secuencia de glosa (INT, PK, identity 1,1). Clave primaria que identifica cada configuración de numeración para glosas/reclamaciones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de secuencias de glosas: registra el control de numeración correlativa para la generación de radicados o identificadores de glosas, asociando cada secuencia a una unidad operativa o centro de atención específico.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaSequenceDetail';
