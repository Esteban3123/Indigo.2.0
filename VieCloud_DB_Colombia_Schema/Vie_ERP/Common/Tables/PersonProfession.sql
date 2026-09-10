CREATE TABLE [Common].[PersonProfession] (
    [Id]            INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdProfession]  TINYINT NOT NULL,
    [PersonId]      INT     NOT NULL,
    [IsSupported]   BIT     NOT NULL,
    [PersonStudyId] INT     NULL,
    [State]         BIT     NOT NULL,
    CONSTRAINT [PK_ProfessionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonProfession_PersonStudy] FOREIGN KEY ([PersonStudyId]) REFERENCES [Common].[PersonStudy] ([Id]),
    CONSTRAINT [FK_ProfessionDetail_Employee] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_ProfessionDetail_Profession] FOREIGN KEY ([IdProfession]) REFERENCES [Payroll].[Profession] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la profesión del empleado: 1=Activo, 0=Inactivo. Indica si el registro de profesión está vigente o desactivado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del estudio/formación académica que respalda o soporta la profesión del empleado. Referencia FK a PersonStudy.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonStudyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del estudio que soporta la profesion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonStudyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonStudyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si la profesión del empleado está respaldada por un estudio formal documentado (1=Sí/Soportado, 0=No/No soportado).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IsSupported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'profesion soportado por un estudio (SI/NO)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IsSupported';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IsSupported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado/persona a quien se asigna la profesión. Referencia FK a Person (empleado, profesional de salud).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la profesión/especialidad asignada al empleado. Referencia FK a Profession en módulo Payroll.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IdProfession';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Profession FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IdProfession';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IdProfession';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la relación profesión-empleado. Clave primaria de PersonProfession.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona a una persona (profesional de la salud) con sus profesiones u ocupaciones registradas, indicando si la profesión está soportada o habilitada y vinculando opcionalmente con su estudio académico correspondiente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession';
