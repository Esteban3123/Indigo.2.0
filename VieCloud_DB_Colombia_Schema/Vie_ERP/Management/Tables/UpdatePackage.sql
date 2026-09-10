CREATE TABLE [Management].[UpdatePackage] (
    [Id]              INT           NOT NULL,
    [NumberVersion]   INT           NOT NULL,
    [PackageVersion]  VARCHAR (12)  NOT NULL,
    [Description]     VARCHAR (MAX) NOT NULL,
    [BuildDate]       DATETIME      NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [PackageFileName] VARCHAR (100) NOT NULL,
    [Status]          BIT           NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del paquete de actualización | 1 = Activo/Disponible | 0 = Inactivo/Deshabilitado. Tipo: BIT (booleano). Indica si la versión está lista para desplegar en el sistema ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado | 1 = Activo | 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo de distribución del paquete de actualización (ej: IndyVie_v2.5.1_Build20240115.zip). Tipo: VARCHAR(100). Referencia al fichero físico almacenado en repositorio de versiones.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageFileName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del archivo del paquete', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageFileName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del paquete en el sistema de gestión de actualizaciones. Tipo: DATETIME. Marca cuándo se registró la versión en Management.UpdatePackage.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de compilación/construcción del paquete (release date). Tipo: DATETIME. Indica cuándo se generó la versión ejecutable del software.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'BuildDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de construcción', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'BuildDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'BuildDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del contenido, cambios, mejoras, parches de seguridad y dependencias del paquete de actualización. Tipo: VARCHAR(MAX). Campo para notas técnicas y changelog de la versión.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del paquete', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de versión del paquete en formato estándar (ej: 2.5.1, 3.0.0). Tipo: VARCHAR(12). Etiqueta semántica de la release (major.minor.patch).', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión del paquete', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'PackageVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial entero de la versión para orden y comparación. Tipo: INT. Correlativo incremental que facilita ordenamiento cronológico de actualizaciones.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'NumberVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NúmeroVersión', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'NumberVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'NumberVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (clave primaria) del registro de paquete de actualización. Tipo: INT. Referencia interna para auditoría y relaciones de FK con otras tablas de deployment.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de paquetes de actualización del sistema Indigo Vie Cloud. Controla las versiones, fechas de compilación y estado de cada paquete de software desplegado o disponible para instalación.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'UpdatePackage';
