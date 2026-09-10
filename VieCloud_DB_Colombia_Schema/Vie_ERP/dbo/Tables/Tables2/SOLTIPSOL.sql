CREATE TABLE [dbo].[SOLTIPSOL] (
    [AUTO]     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODTISOL] VARCHAR (3)  NOT NULL,
    [DESTISOL] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_SOLTIPSOL] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del tipo de solicitud (VARCHAR 60). Ej: urgencia, consulta, examen, procedimiento, receta, RIPS, facturación. Campo de búsqueda para clasificar solicitudes en el ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'DESTISOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el nombre del tipo de solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'DESTISOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'DESTISOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tipo de solicitud (VARCHAR 3). Identificador corto alfanumérico que clasifica la solicitud. Referencia para filtros y reportes de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'CODTISOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CONTIENE EL CODIGO DEL TIPO DE SOLICITU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'CODTISOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'CODTISOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria de la tabla, generado automáticamente por SQL Server. No utilizar para lógica de negocio, solo para integridad referencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de tipos de solicitud médica o administrativa. Define las categorías posibles que puede tener una solicitud (por ejemplo: urgente, programada, interconsulta, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPSOL';
