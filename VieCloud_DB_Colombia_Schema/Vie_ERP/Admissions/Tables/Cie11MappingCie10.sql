CREATE TABLE [Admissions].[Cie11MappingCie10] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [Cie11MappingId] INT      NOT NULL,
    [CODDIAGNO]      CHAR (4) NOT NULL,
    CONSTRAINT [PK_Cie11MappingCie10] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cie11Mapping_Cie11MappingId] FOREIGN KEY ([Cie11MappingId]) REFERENCES [Admissions].[Cie11Mapping] ([Id]),
    CONSTRAINT [FK_Indiagnos_coddiagno] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 asociado, referenciado desde la tabla INDIAGNOS.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11MappingCie10', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro en la tabla Admissions.Cie11Mapping (cabecera del mapeo CIE-11).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11MappingCie10', @level2type = N'COLUMN', @level2name = N'Cie11MappingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de mapeo entre CIE-11 y CIE-10.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11MappingCie10', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre diagnósticos CIE-11 y diagnósticos CIE-10 asociados.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11MappingCie10';

