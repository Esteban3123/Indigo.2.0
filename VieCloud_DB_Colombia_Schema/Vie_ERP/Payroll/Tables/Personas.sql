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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla del módulo de nómina que almacena datos personales y demográficos de los individuos vinculados al proceso de pago. Registra identificación (número y tipo de documento, ciudad y fecha de expedición), nombres completos, datos de nacimiento, género, grupo sanguíneo y estado civil. También captura información de libreta militar. Las fechas y códigos categóricos se almacenan como FLOAT, lo que sugiere una importación desde una fuente externa como Excel.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'Personas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'Personas';
GO
