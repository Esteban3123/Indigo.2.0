CREATE TABLE [dbo].[SOLARCADJ] (
    [AUTO]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMARCADJ] VARCHAR (100) NOT NULL,
    [AUTOCOTI]  INT           NOT NULL,
    CONSTRAINT [PK_SOLARCADJ] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOLARCADJ_SOLCOTI] FOREIGN KEY ([AUTOCOTI]) REFERENCES [dbo].[SOLCOTI] ([AUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la cotización referenciada; clave foránea que vincula a la tabla SOLCOTI para rastrear presupuestos, cotizaciones de servicios de salud o compras asociadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo adjunto guardado; ruta o referencia de documento, formulario, imagen o anexo relacionado con la cotización (ej: PDF, comprobante, imagen de receta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'NOMARCADJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del archivo guardado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'NOMARCADJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'NOMARCADJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (PK) que identifica de forma unívoca cada registro adjunto de cotización en la tabla SOLARCADJ.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivos adjuntos vinculados a cotizaciones solares o solicitudes de compra; registra cada documento o archivo asociado a una cotización específica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLARCADJ';
