CREATE TABLE [HumanTalent].[PVPoProProfesionalExperience] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId] INT           NOT NULL,
    [Required]          INT           NOT NULL,
    [PositionsId]       INT           NOT NULL,
    [Amount]            INT           NOT NULL,
    [AreaId]            INT           NOT NULL,
    [Require]           INT           NOT NULL,
    [Sector]            INT           NOT NULL,
    [CreationUser]      VARCHAR (20)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [Description]       VARCHAR (500) NULL,
    CONSTRAINT [PK__PVPoProP__3214EC0763CC2DB8] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVPoProProfePositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id]),
    CONSTRAINT [FK_PVPoProProfesionalExperience_Area] FOREIGN KEY ([AreaId]) REFERENCES [HumanTalent].[Area] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la experiencia profesional requerida para el perfil de posición, incluyendo competencias y requisitos específicos. VARCHAR(500).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de experiencia profesional. DATETIME, auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro de experiencia. VARCHAR(20), trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de experiencia profesional requerida. DATETIME, auditoría de trazabilidad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de experiencia profesional. VARCHAR(20), responsable inicial.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sector económico o industria (salud, tecnología, administrativo, etc.) al cual aplica la experiencia. INT, clasificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Sector';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sector', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Sector';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Sector';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de requisito obligatorio (1=Sí, 0=No) para la experiencia en este sector. INT, bandera de obligatoriedad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Require';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requerir', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Require';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Require';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única del área organizacional asociada a la experiencia profesional requerida. INT, FK a [HumanTalent].[Area].', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'AreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del área', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'AreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'AreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad, años de experiencia o número de posiciones requeridas para este perfil. INT, métrica cuantitativa.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Amount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Amount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de las posiciones/cargos (vacantes o roles) vinculadas a esta experiencia. INT, referencia a posiciones.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion De posiciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de carácter obligatorio (1=Requerido, 0=Deseable) de la experiencia para el perfil. INT, bandera de requisito.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Required';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requerido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Required';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Required';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única del perfil de posición al cual pertenece esta experiencia profesional. INT, FK a [HumanTalent].[PositionProfile].', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de la posicion del  perfil ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'PositionProfileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único primario (clave) del registro de experiencia profesional. INT IDENTITY, PK.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Experiencia profesional requerida por perfil de cargo en talento humano. Registra cuántas personas se necesitan, en qué área, sector y cargo, junto con los requisitos de experiencia para cada perfil de posición.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProProfesionalExperience';
