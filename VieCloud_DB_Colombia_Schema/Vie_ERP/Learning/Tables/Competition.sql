CREATE TABLE [Learning].[Competition] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [Name]              VARCHAR (80)  NOT NULL,
    [Type]              TINYINT       NOT NULL,
    [State]             BIT           NOT NULL,
    [CreationUser]      VARCHAR (20)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [CompetitionRoleId] INT           NULL,
    [Description]       VARCHAR (300) NULL,
    CONSTRAINT [PK_Competition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ__Competit__A25C5AA7E2C1260C] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción (VARCHAR 300, nullable), detalle textual ampliado de la competencia incluyendo alcance, criterios de desempeño, contexto de aplicación en procesos de salud', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id rol de competencia (INT, FK nullable), referencia a relación entre competencia y roles de profesionales de salud; vincula competencia a perfiles de puestos', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Rol de competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha modificación (DATETIME, nullable), timestamp de último cambio realizado a la competencia, trazabilidad de actualizaciones', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario modificación (VARCHAR 20, nullable), identificación del usuario que realizó cambios posteriores a la competencia registrada', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha creación (DATETIME), timestamp de registro inicial de la competencia, auditoría de cuándo se definió la habilidad o destreza', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creación (VARCHAR 20), identificación del usuario que registró la competencia en el sistema de gestión', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT): 1=Activo, 0=Inactivo; indica si la competencia está vigente y disponible para asignación a roles y profesionales de salud', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de competencia (TINYINT): 1=Institucionales, 2=Específicas; clasificación que diferencia competencias transversales de la organización versus competencias técnicas de especialidad', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Competencias: 1. Institucionales - 2. Específicas', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de competencia (VARCHAR 80), denominación descriptiva de la habilidad, destreza o competencia profesional requerida para roles en la organización', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de competencia (VARCHAR 20, UNIQUE), identificador único alfanumérico para búsqueda y referencia rápida de competencias institucionales o específicas', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de competencia (INT, PK), clave primaria secuencial para registro de habilidades, destrezas o competencias profesionales en el sistema', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Competencia', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de competencias o habilidades definidas para el módulo de aprendizaje y gestión del talento. Registra cada competencia con su código, nombre, tipo, estado activo/inactivo y el rol asociado al que aplica.', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Learning', @level1type = N'TABLE', @level1name = N'Competition';
