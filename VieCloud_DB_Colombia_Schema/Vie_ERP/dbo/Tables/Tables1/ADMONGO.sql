CREATE TABLE [dbo].[ADMONGO] (
    [ID]                INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COLECCIONMONGO]    VARCHAR (100)  NOT NULL,
    [NOMBRE]            VARCHAR (100)  NOT NULL,
    [MESANO]            VARCHAR (50)   NOT NULL,
    [ESTADO]            BIT            NOT NULL,
    [USUARIO]           CHAR (20)      NOT NULL,
    [FECHACREACION]     DATETIME       NOT NULL,
    [USUARIOMOD]        CHAR (20)      NULL,
    [FECHAMODIFICACION] DATETIME       NULL,
    [OTRASCOLUMNAS]     VARCHAR (1000) NULL,
    CONSTRAINT [PK_ADMONGO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo VARCHAR(1000) para almacenar columnas adicionales, datos flexibles o propiedades extras de la colección MongoDB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'OTRASCOLUMNAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar las columnas adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'OTRASCOLUMNAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'OTRASCOLUMNAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de la última modificación o actualización del registro en la base de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario CHAR(20) que realizó la última modificación o cambio del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'USUARIOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de cargue o ingreso inicial del registro a la base de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de cargue de la BD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario CHAR(20) que realizó el cargue o ingreso inicial del registro a la base de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizo el cargue de BD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'USUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado BIT del registro: 1 = Activo, 0 = Inactivo; controla disponibilidad y visibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1 = Activo   0 =  Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período VARCHAR(50) de cargue expresado en mes y año (ej: 202401, 01/2024) para agrupación temporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'MESANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes y año de cargue de la BD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'MESANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'MESANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre VARCHAR(100) del registro o colección, etiqueta identificable del objeto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre VARCHAR(100) de la colección en base de datos MongoDB vinculada al registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'COLECCIONMONGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Coleccion de base de datos de Mongo DB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'COLECCIONMONGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'COLECCIONMONGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico INT IDENTITY(1,1), clave primaria autonumérica de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de colecciones MongoDB integradas con el sistema: almacena el mapeo entre colecciones de base de datos NoSQL y su configuración de sincronización, incluyendo el período (mes/año), estado activo/inactivo y auditoría de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMONGO';
