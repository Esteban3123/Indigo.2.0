CREATE TABLE [EHR].[HCORDINSUMOS] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]     INT       NOT NULL,
    [IDHCORDQUIMIO] INT       NOT NULL,
    [CODIGOINSUMO]  CHAR (20) NOT NULL,
    [CICLO]         INT       NOT NULL,
    [DIA]           INT       NOT NULL,
    [CANTIDAD]      INT       NOT NULL,
    CONSTRAINT [PK_HCORDINSUMOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDINSUMOS_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCORDINSUMOS_IHLISTPRO] FOREIGN KEY ([CODIGOINSUMO]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDINSUMOS_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [EHR].[HCORDINSUMOS] NOCHECK CONSTRAINT [FK_HCORDINSUMOS_HCORDQUIMIO];




GO
ALTER TABLE [EHR].[HCORDINSUMOS] NOCHECK CONSTRAINT [FK_HCORDINSUMOS_HCORDQUIMIO];


GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCORDINSUMOS_CICLO_IDHCORDQUIMIO_CODIGOINSUMO]
    ON [EHR].[HCORDINSUMOS]([CICLO] ASC, [IDHCORDQUIMIO] ASC, [CODIGOINSUMO] ASC)
    INCLUDE([CANTIDAD], [DIA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de insumo (unidades, dosis, volumen) prescrito en el ciclo y día específico de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Insumo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día del ciclo de quimioterapia en que se administra el insumo (INT, rango 1-N según protocolo)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'DIA', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ciclo de quimioterapia; identifica cada etapa repetida del tratamiento oncológico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'N° del Ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del insumo/producto (FK a IHLISTPRO.CODPRODUC); fármaco, solución o material usado en quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CODIGOINSUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Insumo relacion con la tabla de IHLISTRPO', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CODIGOINSUMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'CODIGOINSUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de quimioterapia padre (FK a HCORDQUIMIO.ID); vincula insumo a la orden clínica completa', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la cabecera de ordenamiento de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema parametrizado (FK a Schemes.Id); pauta de tratamiento oncológico y dosificación', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el Esquema parametrizado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK INT IDENTITY); clave primaria autoincrementada de la línea de insumo en orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Insumos o materiales asociados a órdenes de quimioterapia, especificando el ciclo, día y cantidad de cada insumo requerido en el tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDINSUMOS';
