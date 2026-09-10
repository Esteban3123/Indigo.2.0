CREATE TABLE [Common].[PersonNationality] (
    [Id]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CountryId] INT NOT NULL,
    [PersonId]  INT NOT NULL,
    [State]     BIT NOT NULL,
    CONSTRAINT [PK_PersonNationality] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonNationality_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Common].[Country] ([Id]),
    CONSTRAINT [FK_PersonNationality_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de nacionalidad: 1=Activo, 0=Inactivo. Indica si la relación persona-país está vigente en el sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Registro: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la persona (FK a Common.Person). Referencia al paciente, profesional o usuario cuya nacionalidad se registra.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del país (FK a Common.Country). Referencia a la nación de origen o nacionalidad asignada.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'CountryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del País', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'CountryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'CountryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) de la tabla. Clave primaria que identifica unívocamente cada registro de nacionalidad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nacionalidades asociadas a una persona. Registra el vínculo entre una persona y su país de nacionalidad, permitiendo múltiples nacionalidades por persona con indicador de estado activo o inactivo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonNationality';
