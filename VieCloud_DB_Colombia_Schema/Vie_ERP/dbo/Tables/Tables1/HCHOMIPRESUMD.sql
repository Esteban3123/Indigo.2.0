CREATE TABLE [dbo].[HCHOMIPRESUMD] (
    [ID]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]     VARCHAR (5)  NOT NULL,
    [DESCRIPCION]   VARCHAR (70) NOT NULL,
    [UNIMEDPRINACT] VARCHAR (30) NOT NULL,
    [CODINDIGO]     VARCHAR (20) NOT NULL,
    [ESTADO]        BIT          NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESUMD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOMIPRESUMD_INUNIMEDI] FOREIGN KEY ([CODINDIGO]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_HCHOMIPRESUMD_CODINDIGO]
    ON [dbo].[HCHOMIPRESUMD]([CODINDIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro en MIPRES (activo/inactivo); bit booleano que indica si la unidad de medida está habilitada en el sistema de prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida en Indigo Vie Cloud (FK a INUNIMEDI.CODUNIMED); identificador único del catálogo de unidades de medida del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODINDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de unidad de medida en Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODINDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODINDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del principio activo del medicamento (mg, ml, unidad, etc.); especifica la dosis y concentración del fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'UNIMEDPRINACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del pricipio activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'UNIMEDPRINACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'UNIMEDPRINACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la unidad de medida para dosis y presentación del medicamento; texto de búsqueda para identificar la forma farmacéutica (tableta, cápsula, ampolla, jarabe).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de unidad de medida dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida de dosis en MIPRES (Sistema de Información de Precios de Medicamentos); identificador externo para interoperabilidad con normativa de prescripción electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de unidad de medida dosis mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la tabla HCHOMIPRESUMD; clave primaria para referencias internas del ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de  la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de ítems MIPRES (Medicamentos, Insumos, Procedimientos y Servicios no cubiertos por el plan de beneficios). Relaciona cada código MIPRES con su descripción, unidad de medida y el código equivalente en Indigo, para gestionar prescripciones y autorizaciones externas al POS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD';
