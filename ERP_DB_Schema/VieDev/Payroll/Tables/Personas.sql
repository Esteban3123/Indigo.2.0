CREATE TABLE [Payroll].[Personas] (
    [NumeroIdentificacion]     VARCHAR (20)   NULL,
    [TipoIdentificacion]       FLOAT (53)     NULL,
    [NombreCiudadExpDocumento] NVARCHAR (255) NULL,
    [IdCiudad_Common]          INT            NULL,
    [FechaExpedicionDocumento] FLOAT (53)     NULL,
    [TipoLibretaMilitar]       FLOAT (53)     NULL,
    [NumeroLibretaMilitar]     FLOAT (53)     NULL,
    [PrimerNombre]             NVARCHAR (255) NULL,
    [SegundoNombre]            NVARCHAR (255) NULL,
    [PrimerApellido]           NVARCHAR (255) NULL,
    [SegundoApellido]          NVARCHAR (255) NULL,
    [FechaCumpleanos]          FLOAT (53)     NULL,
    [NombreCiudadNacimiento]   NVARCHAR (255) NULL,
    [IdCiudad_Naci]            INT            NULL,
    [Genero]                   FLOAT (53)     NULL,
    [GrupoSanguineo]           FLOAT (53)     NULL,
    [RH]                       FLOAT (53)     NULL,
    [EstadoCivil]              FLOAT (53)     NULL
);

