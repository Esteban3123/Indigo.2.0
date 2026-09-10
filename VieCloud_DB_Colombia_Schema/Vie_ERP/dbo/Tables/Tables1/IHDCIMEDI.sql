CREATE TABLE [dbo].[IHDCIMEDI] (
    [CODDCIMED] VARCHAR (20) NOT NULL,
    [DESDCIMED] CHAR (255)   NOT NULL,
    CONSTRAINT [PK_IHDCIMEDI] PRIMARY KEY CLUSTERED ([CODDCIMED] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la Denominación Común Internacional (DCI) del medicamento; nombre genérico estandarizado del fármaco, receta, principio activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'DESDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Denominacion Comun Internacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'DESDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'DESDCIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Denominación Común Internacional (DCI) del medicamento; identificador único del fármaco genérico, clave de búsqueda farmacológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Denominacion Comun Internacion del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de medicamentos o insumos médicos utilizados en la historia clínica. Relaciona el código del ítem con su descripción completa para uso en prescripciones, dispensaciones y registros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHDCIMEDI';
