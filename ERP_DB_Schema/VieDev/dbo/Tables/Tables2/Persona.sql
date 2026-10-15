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

