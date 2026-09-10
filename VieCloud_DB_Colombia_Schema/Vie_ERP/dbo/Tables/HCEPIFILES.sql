CREATE TABLE [dbo].[HCEPIFILES] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [Base64Data]            NVARCHAR (MAX) NULL,
    [PacienteId]            NVARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME2 (7)  DEFAULT (sysdatetime()) NOT NULL,
    [PacienteDocumento]     VARCHAR (50)   NULL,
    [PacienteTipoDocumento] VARCHAR (100)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Almacena archivos codificados en Base64 asociados a pacientes dentro del módulo de historia clínica electrónica (HCE). Cada registro vincula el contenido del archivo con un paciente identificado por su ID y documento (incluyendo tipo de documento), registrando automáticamente la fecha y hora de creación mediante `sysdatetime()`.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCEPIFILES';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCEPIFILES';
GO
