CREATE TABLE [dbo].[SOLPROVEE] (
    [PROVAUTO]    NUMERIC (18)  IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PROVENIT]    CHAR (15)     NOT NULL,
    [PROVNOMBRE]  VARCHAR (100) NOT NULL,
    [PROVESTADO]  BIT           NOT NULL,
    [PROVUBICA]   CHAR (2)      NOT NULL,
    [PROVCALPRE]  TINYINT       NULL,
    [PROVCALCALI] TINYINT       NULL,
    [PROVCALTEN]  TINYINT       NULL,
    CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED ([PROVAUTO] ASC),
    CONSTRAINT [FK_SOLPROVEE_INDEPARTA] FOREIGN KEY ([PROVUBICA]) REFERENCES [dbo].[INDEPARTA] ([depcodigo])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación general de desempeño del proveedor (TINYINT, 0-255); puntuación integral de tiempos, entregas y cumplimiento de contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la calificacion del proveedor ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALTEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación de calidad del proveedor (TINYINT, 0-255); puntuación de conformidad, cumplimiento y calidad de productos/servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALCALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la calificacion del proveedor en calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALCALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALCALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación de precios del proveedor (TINYINT, 0-255); puntuación de competitividad económica y propuestas comerciales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la calificacion del proveedor en precios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVCALPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación o departamento del proveedor (CHAR 2, FK→INDEPARTA.depcodigo); referencia geográfica/administrativa del domicilio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVUBICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la ubicacion del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVUBICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVUBICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del proveedor (BIT: 0=inactivo, 1=activo); indicador booleano de disponibilidad para procesos de solicitud/contratación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el estado del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social del proveedor (VARCHAR 100); denominación comercial del tercero o empresa contratada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el nombre del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o número de identificación tributaria del proveedor (CHAR 15); identificación fiscal/legal requerida para contrataciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVENIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el nit del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVENIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVENIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (NUMERIC 18, PK) de la tabla SOLPROVEE; clave primaria para cada proveedor registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene le autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE', @level2type = N'COLUMN', @level2name = N'PROVAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de proveedores de la institución. Guarda la información básica de cada proveedor (nombre, estado, ubicación y calificaciones de precio, calidad y tendencia) para el proceso de solicitud y gestión de compras o suministros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVEE';
