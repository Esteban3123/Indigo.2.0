CREATE TABLE [Common].[FilePerson] (
    [Id]           UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [IdPerson]     INT              NOT NULL,
    [Photo]        VARBINARY (MAX)  NULL,
    [Signature]    VARBINARY (MAX)  NULL,
    [TimeStamp]    ROWVERSION       NOT NULL,
    [State]        BIT              NOT NULL,
    [Synchronized] CHAR (1)         NOT NULL,
    CONSTRAINT [PK_FilesPerson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilesPerson_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sincronización con base de datos remota (CHAR 1: ''''1'''' sincronizado, ''''0'''' pendiente de sincronización). Control de estado de replicación de archivos de persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de eliminación lógica para sincronización de base de datos (BIT: 1=activo, 0=eliminado). Marca registros para purga en réplicas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de control de concurrencia y versión optimista (TIMESTAMP). Previene conflictos en actualizaciones simultáneas de foto y firma.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Concurrencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firma digital del profesional de salud (VARBINARY MAX, PII-Ofuscado). Imagen rasterizada de la rúbrica para documentos clínicos y administrativos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Signature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Firma del Profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Signature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Signature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fotografía del profesional de la salud o paciente (VARBINARY MAX, PII-Ofuscado). Imagen de identificación para trazabilidad en atención y autenticación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Photo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto del Profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Photo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Photo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la persona (INT, FK→Common.Person). Vincula registros de foto y firma con datos demográficos del profesional o paciente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id persona ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'IdPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único global de archivo de persona (UNIQUEIDENTIFIER ROWGUIDCOL). Clave primaria para replicación y sincronización distribuida de documentos multimedia.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivos binarios de imagen (foto y firma) asociados a personas registradas en el sistema; permite almacenar y recuperar la fotografía y firma digital de pacientes, profesionales u otros individuos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson';
