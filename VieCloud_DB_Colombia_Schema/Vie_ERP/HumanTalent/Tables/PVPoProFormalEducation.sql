CREATE TABLE [HumanTalent].[PVPoProFormalEducation] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId]     INT          NOT NULL,
    [EducationLevelId]      INT          NOT NULL,
    [AcademicInformationId] INT          NOT NULL,
    [Required]              INT          NOT NULL,
    [State]                 INT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK__PVPoProF__3214EC0773CD6E38] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVPoProAcademicInformation] FOREIGN KEY ([AcademicInformationId]) REFERENCES [HumanTalent].[AcademicInformation] ([id]),
    CONSTRAINT [fk_PVPoProEducationLevel] FOREIGN KEY ([EducationLevelId]) REFERENCES [HumanTalent].[EducationLevel] ([Id]),
    CONSTRAINT [fk_PVPoProPositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del requisito de formación académica para el perfil de posición (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del requisito educativo (VARCHAR 20, auditoría, responsable del cambio)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación  de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de requisito de formación académica (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del requisito educativo para el perfil de posición (VARCHAR 20, auditoría, responsable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del requisito educativo: 1=Activo, 2=Suspendido, 3=Terminado, 4=Graduado (INT, estado del requisito, historial académico)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1: Actualmente, 2: Suspendido, 3: Terminado, 4: Graduado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la formación académica es obligatoria o requerida para desempeñar la posición (INT, booleano, requisito obligatorio)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'Required';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requerido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'Required';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'Required';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la información académica específica (carrera, especialidad, certificación, diplomado) vinculada al requisito (FK→AcademicInformation)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de la información académica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'AcademicInformationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de educación requerido (técnico, pregrado, posgrado, especialización, maestría, doctorado) para la posición (FK→EducationLevel)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de nivel de educaciónal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'EducationLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del perfil de posición o cargo que requiere este nivel de formación académica (FK→PositionProfile, requisito de posición)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de perfil de posición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de requisito de formación académica para el perfil de posición (INT IDENTITY, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requisitos de formación académica formal asociados a un perfil de cargo en el módulo de Talento Humano. Registra qué nivel educativo y área de conocimiento se requieren (o se prefieren) para ocupar una posición, junto con su obligatoriedad y estado de vigencia.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProFormalEducation';
