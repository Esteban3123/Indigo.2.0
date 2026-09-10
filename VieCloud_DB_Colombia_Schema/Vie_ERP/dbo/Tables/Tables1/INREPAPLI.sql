CREATE TABLE [dbo].[INREPAPLI] (
    [NUMCONSEC] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMREPARC] VARCHAR (50)    NOT NULL,
    [NOMREPVIS] VARCHAR (255)   NOT NULL,
    [DESCREPOR] VARCHAR (255)   NOT NULL,
    [INDMODULO] CHAR (2)        NULL,
    [ESMODIFIC] BIT             NOT NULL,
    [BUFFER]    VARBINARY (MAX) NULL,
    CONSTRAINT [PK_INREPORCO] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_INREPORCO_SEGmodulu] FOREIGN KEY ([INDMODULO]) REFERENCES [dbo].[SEGmodulu] ([indmodulo]),
    CONSTRAINT [FK_INREPUSUA_SEGmodulu] FOREIGN KEY ([INDMODULO]) REFERENCES [dbo].[SEGmodulu] ([indmodulo])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_INREPAPLI]
    ON [dbo].[INREPAPLI]([NOMREPARC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento binario de datos serializados del reporte (VARBINARY MAX), usado para cache o metadata interna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'BUFFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Buffer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'BUFFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'BUFFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que controla si el diseño/estructura del reporte puede ser modificado por usuarios autorizados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'ESMODIFIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite Modificar el Diseño del Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'ESMODIFIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'ESMODIFIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del módulo funcional en INDIGO VIE (ej: facturación, RIPS, urgencias, laboratorio); FK a SEGmodulu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'INDMODULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'INDMODULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'INDMODULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual amplia del propósito, contenido y casos de uso del reporte aplicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'DESCREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'DESCREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'DESCREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre lógico visible del reporte en la interfaz de usuario; etiqueta de presentación para búsqueda y selección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPVIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Logico del Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPVIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPVIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo físico que almacena el diseño del reporte, sin extensión (.rpt, .rdl); ruta en servidor de reportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Fisico (Sin extension)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NOMREPARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico secuencial único interno para cada definición de reporte; clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de reportes y aplicativos disponibles en el sistema, que registra los informes configurados por módulo, incluyendo su nombre técnico, nombre visible para el usuario, descripción y el archivo binario del reporte (por ejemplo, archivos RDLC o similares).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPAPLI';
