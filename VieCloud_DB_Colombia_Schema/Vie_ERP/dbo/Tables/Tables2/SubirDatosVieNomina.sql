CREATE TABLE [dbo].[SubirDatosVieNomina] (
    [UnidadFuncional]       VARCHAR (50)  NULL,
    [CodigoUnidadFuncional] VARCHAR (50)  NULL,
    [Grupo]                 VARCHAR (3)   NULL,
    [CodigoCargo]           VARCHAR (10)  NULL,
    [Cargo]                 VARCHAR (50)  NULL,
    [Salario]               NUMERIC (18)  NULL,
    [PrimerApellido]        VARCHAR (50)  NULL,
    [SegundoApellido]       VARCHAR (50)  NULL,
    [PrimerNombre]          VARCHAR (50)  NULL,
    [SegundoNombre]         VARCHAR (50)  NULL,
    [Nombres]               VARCHAR (200) NULL,
    [Cedula]                VARCHAR (50)  NULL,
    [LugarExpedicion]       VARCHAR (100) NULL,
    [FechaExpedicion]       DATE          NULL,
    [LugarNacimiento]       VARCHAR (100) NULL,
    [Cumpleanos]            DATE          NULL,
    [Genero]                VARCHAR (50)  NULL,
    [NumeroLibretaMilitar]  VARCHAR (20)  NULL,
    [GrupoSanguineo]        VARCHAR (3)   NULL,
    [RH]                    VARCHAR (20)  NULL,
    [FechaContrato]         DATE          NULL,
    [FechaTerminacion]      DATE          NULL,
    [EPS]                   VARCHAR (50)  NULL,
    [AFP]                   VARCHAR (50)  NULL,
    [EstadoCivil]           VARCHAR (50)  NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de carga temporal (staging) utilizada para importar datos de empleados desde una fuente externa de nómina (VIE). Almacena información personal, contractual y de seguridad social de cada trabajador, incluyendo unidad funcional, cargo, salario, documento de identidad, fechas de contrato, EPS y AFP. Sirve como paso previo a la integración con el sistema principal de nómina o recursos humanos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirDatosVieNomina';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirDatosVieNomina';
GO
