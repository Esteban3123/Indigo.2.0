CREATE TABLE [dbo].[Persona] (
    [NumeroIdentificacion]     VARCHAR (50)  NULL,
    [TipoIdentificacion]       CHAR (1)      NULL,
    [NombreCiudadExpedicion]   VARCHAR (200) NULL,
    [FechaExpedicionDocumento] DATE          NULL,
    [TipoLibretaMilitar]       CHAR (1)      NULL,
    [NumLibretaMilitar]        VARCHAR (50)  NULL,
    [PrimerNombre]             VARCHAR (100) NULL,
    [SegundoNombre]            VARCHAR (100) NULL,
    [PrimerApellido]           VARCHAR (100) NULL,
    [SegundoApellido]          VARCHAR (100) NULL,
    [FechaCumpleaños]          DATE          NULL,
    [NombreCiudadNacimiento]   VARCHAR (200) NULL,
    [Genero]                   CHAR (1)      NULL,
    [GrupoSanguineo]           CHAR (1)      NULL,
    [RH]                       CHAR (1)      NULL,
    [EstadoCivil]              CHAR (1)      NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Almacena los datos demográficos y de identificación de personas, incluyendo documento de identidad (número, tipo y ciudad de expedición), libreta militar, nombres completos, fecha de nacimiento, ciudad de nacimiento, género, grupo sanguíneo con factor RH y estado civil. Funciona como catálogo base de personas en el sistema, sin clave primaria definida ni restricciones de integridad explícitas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Persona';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Persona';
GO
