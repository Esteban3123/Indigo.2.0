CREATE TABLE [Admissions].[GroupsERCPrecursoras] (
    [Id]               INT       IDENTITY (1, 1) NOT NULL,
    [GroupCode]        INT       NOT NULL,
    [CreationDate]     DATETIME  NOT NULL,
    [UserCreation]     CHAR (20) NOT NULL,
    [ModificationDate] DATETIME  NULL,
    [UserModification] CHAR (20) NULL,
    CONSTRAINT [PK__GroupsER__3214EC076A87D3D4] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación, CHAR 20) que realizó la última modificación; auditoría de cambios para trazabilidad de actualizaciones en grupo ERC', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro (DATETIME NULL); marca temporal para control de cambios en configuración de grupos', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha cuando se modifica el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación, CHAR 20) que creó el registro; auditoría de creación para rastrear origen de datos en grupo ERC', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el usuario que creo el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME); marca temporal de auditoría para trazabilidad de origen del grupo precursor', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de enfermedades precursoras: 1=Hipertensión Arterial, 2=Diabetes Mellitus, 3=Enfermedad Renal Crónica (ERC); clasificación clínica para estratificación de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'GroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del grupo (1. Hipertensión Arterial, 2. Diabetes Mellitus y 3. Enfermedad Renal Crónica (ERC))', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'GroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'GroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, clave primaria (INT IDENTITY) de cada registro de grupo ERC precursor en el sistema', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el identificador unico de cada registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos ERC (Enfermedad Renal Crónica) precursoras registrados en admisiones. Guarda los grupos de condiciones o factores precursores asociados a ERC, con trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GroupsERCPrecursoras';
