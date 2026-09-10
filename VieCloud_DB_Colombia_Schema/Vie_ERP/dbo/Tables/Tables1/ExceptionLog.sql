CREATE TABLE [dbo].[ExceptionLog] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [PatientCode]      NVARCHAR (50)  NULL,
    [AdmissionCode]    NVARCHAR (50)  NULL,
    [UniqueKey]        NVARCHAR (200) NULL,
    [Professional]     NVARCHAR (100) NULL,
    [DateCreation]     DATETIME       DEFAULT (getdate()) NULL,
    [ExceptionMessage] NVARCHAR (600) NULL,
    [Product]          NVARCHAR (50)  NULL,
    [Origin]           NVARCHAR (50)  NULL,
    [Folio]            VARCHAR (20)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de errores y excepciones del sistema Indigo Vie Cloud. Guarda los fallos ocurridos durante el procesamiento de operaciones clínicas o administrativas, asociándolos al paciente, ingreso y profesional involucrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o cédula del paciente relacionado con el error, identificación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso, admisión u hospitalización asociado al error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'AdmissionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'AdmissionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código del profesional de salud que ejecutaba la acción cuando ocurrió el error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Professional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Professional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró el error o excepción en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción técnica del error o mensaje de excepción generado por el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'ExceptionMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'ExceptionMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Módulo o producto del sistema (por ejemplo: urgencias, laboratorio, facturación) donde ocurrió el error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Product';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Product';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen o componente del sistema que generó la excepción, fuente del error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Origin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Origin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o referencia del documento, orden o transacción relacionada con el error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExceptionLog', @level2type = N'COLUMN', @level2name = N'Folio';
