CREATE EXTERNAL TABLE [Report].[ViewListadoUsuarios] (
    [TENAN_ID] SMALLINT NOT NULL,
    [Persona_Id] INT NOT NULL,
    [Codigo_Usuario] VARCHAR (20) NOT NULL,
    [Identification] VARCHAR (15) NOT NULL,
    [Nombre_Completo] VARCHAR (250) NOT NULL,
    [Email] VARCHAR (60) NULL,
    [Tipo Rol] VARCHAR (14) NOT NULL,
    [Rol_Codigo] INT NOT NULL,
    [Rol_Nombre] VARCHAR (60) NULL,
    [Tipo Usuario] VARCHAR (21) NOT NULL,
    [Grupo_Codigo] INT NOT NULL,
    [Grupo_Nombre] VARCHAR (60) NOT NULL,
    [Cargo] VARCHAR (30) NULL,
    [Estado] VARCHAR (8) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Report',
    OBJECT_NAME = N'ViewListadoUsuarios'
    );

