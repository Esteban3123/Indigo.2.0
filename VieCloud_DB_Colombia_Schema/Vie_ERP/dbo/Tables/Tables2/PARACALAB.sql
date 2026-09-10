CREATE TABLE [dbo].[PARACALAB] (
    [ID]         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]  CHAR (10)      NOT NULL,
    [CODSERIPS]  CHAR (20)      NOT NULL,
    [CANSERIPS]  INT            NOT NULL,
    [OBSSERIPS]  VARCHAR (2000) NULL,
    [PRISERIPS]  CHAR (1)       NOT NULL,
    [CARGARAUTO] INT            NULL,
    CONSTRAINT [PK_PARACALAB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PARACALAB_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PARACALAB_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de carga automática en historia clínica del recién nacido. INT (0=No, 1=Sí). Determina si el servicio/procedimiento/laboratorio se registra automáticamente en la historia del neonato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CARGARAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargar automaticamente en la hitoria del recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CARGARAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CARGARAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad del servicio solicitado en RIPS. CHAR(1): 1=Urgente/emergencia, 2=Rutina/electiva. Clasifica la urgencia del procedimiento, examen o servicio de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas complementarias del servicio solicitado. VARCHAR(2000). Campo libre para aclaraciones, restricciones, instrucciones especiales o hallazgos relevantes del procedimiento/examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades solicitadas para el servicio, procedimiento o examen. INT. Define el número de veces que se ejecuta o la cantidad de insumos asociados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad solicitada para el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio/procedimiento en nomenclatura RIPS (INCUPSIPS). CHAR(20). Referencia al catálogo de servicios, procedimientos, exámenes de laboratorio o imágenes diagnosticas. FK → [dbo].[INCUPSIPS].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o punto de servicio. CHAR(10). Identifica la sede, clínica o hospital donde se ejecuta el servicio. FK → [dbo].[ADCENATEN].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo) de la parametrización laboratorial por centro. INT IDENTITY(1,1). Clave primaria para auditoría y relaciones internas de la tabla PARACALAB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de calibración de servicios de laboratorio clínico por centro de atención. Define qué servicios (CUPS) están habilitados, su cantidad permitida, prioridad de procesamiento y si se cargan de forma automática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACALAB';
