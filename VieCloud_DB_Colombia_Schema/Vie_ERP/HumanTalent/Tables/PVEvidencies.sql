CREATE TABLE [HumanTalent].[PVEvidencies] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId] INT          NOT NULL,
    [EvidenciesId]      INT          NOT NULL,
    [FrequencyId]       INT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    CONSTRAINT [PK__PVEviden__3214EC0748D7CA94] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVEEvidencies] FOREIGN KEY ([EvidenciesId]) REFERENCES [StaffPick].[Evidencies] ([Id]),
    CONSTRAINT [fk_PVEPositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de evidencia en perfil de posición (DATETIME, puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro (VARCHAR 20, puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de asociación entre evidencia y perfil de posición (DATETIME)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de evidencia en perfil (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de frecuencia de evaluación o revisión de la evidencia requerida', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'FrequencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de la frecuencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'FrequencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'FrequencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la evidencia requerida, FK a tabla Evidencies (certificaciones, capacitaciones, documentos)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'EvidenciesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de las Evidencias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'EvidenciesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'EvidenciesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del perfil de posición, FK a PositionProfile (vincula evidencia a cargo/rol)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de perfil de la posición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id llave primaria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las evidencias asociadas a cada perfil de cargo en la gestión de talento humano, incluyendo la frecuencia con que se requiere cada evidencia y el seguimiento de auditoría de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies';
