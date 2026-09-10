CREATE TABLE [dbo].[RISGRIMAGE] (
    [ID]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]                      VARCHAR (50)  NOT NULL,
    [NOMBRE]                      VARCHAR (100) NOT NULL,
    [ESTADO]                      BIT           CONSTRAINT [DF_RISGRIMAGE_ESTADO] DEFAULT ((1)) NOT NULL,
    [ABREVIACION]                 VARCHAR (50)  NULL,
    [DefineRoomIntrahospitalario] BIT           CONSTRAINT [DF_RISGRIMAGE_DefineRoomIntrahospitalario] DEFAULT ((0)) NULL,
    [DefineRoutingToTheInterface] INT           NULL,
    CONSTRAINT [PK_RISGRIMAGE] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT que define si el grupo imagenología requiere asignación de sala/ámbito intrahospitalario (1=Sí, 0=No), control de localización física del equipo dentro de la institución', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'DefineRoomIntrahospitalario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Definir sala ámbito intrahospitalario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'DefineRoomIntrahospitalario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'DefineRoomIntrahospitalario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviación o acrónimo del grupo imagenología (VARCHAR 50), sigla corta para referencia rápida en formularios e interfaz RIS (ej: TC, RM, RX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ABREVIACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la descripción de la abreviación imagenologias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ABREVIACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ABREVIACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de activación: BIT, 1=Activo/Habilitado para uso en solicitudes de imagen, 0=Inactivo/Deshabilitado, controla disponibilidad del grupo imagenológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 0-->Inactivo, 1-->Activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre completo del grupo de imagenología (VARCHAR 100), denominación de modalidad de imagen (radiología, tomografía, resonancia, ecografía, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion o nombre de Grupo Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del grupo de imagenología (VARCHAR 50), identificador de modalidad de imagen, equivalente a código de servicio radiológico/imagenológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Grupo de Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de la tabla RISGRIMAGE, clave primaria para registro de grupo de imagenología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración INT de enrutamiento de imágenes: 0=Enrutador manual (visualiza en formulario Enrutador de Imágenes Intrahospitalarias), 1=Procesamiento automático en interfaz RIS Indira activa, 2=Procesamiento en VieCloud (Dashboard de Imagenología Tecnólogo directo), define flujo de procesamiento post-captura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'DefineRoutingToTheInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si la modalidad está configurada como "Requiere enrutamiento" se guarda el valor 0, el servicio debe visualizarse en el formulario "Enrutador de imágenes intrahospitalarias", desde donde se realizará el enrutamiento manual correspondiente. 

Si la modalidad está configurada como "Procesamiento en interfaz", guarda el valor 1, el servicio debe enviarse automáticamente a la interfaz RIS Indira activa.

Si la modalidad está configurada como "Procesamiento en VieCloud" se guarda el valor 2, el servicio debe visualizarse directamente en el Dashboard de Imagenología Tecnólogo, sin pasar por la interfaz.

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'DefineRoutingToTheInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE', @level2type = N'COLUMN', @level2name = N'DefineRoutingToTheInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de modalidades o tipos de imágenes diagnósticas (radiología, tomografía, ecografía, resonancia, etc.) utilizadas en el módulo de imágenes del sistema RIS/PACS. Define cada tipo de imagen con su nombre, abreviatura y configuración de enrutamiento hacia interfaces externas o salas intrahospitalarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISGRIMAGE';
