CREATE TABLE [HumanTalent].[PVCandidateAdministrationDetail] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PVCandidateAdministrationId] INT          NOT NULL,
    [Diabled]                     TINYINT      NOT NULL,
    [Detail]                      VARCHAR (80) NULL,
    [CreationUser]                VARCHAR (20) NOT NULL,
    [CreationDate]                DATETIME     NOT NULL,
    [ModificationUser]            VARCHAR (20) NULL,
    [ModificationDate]            DATETIME     NULL,
    CONSTRAINT [PK_PVCandidateAdministrationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVCandidateAdministrationDetail_PVCandidateAdministration] FOREIGN KEY ([PVCandidateAdministrationId]) REFERENCES [HumanTalent].[PVCandidateAdministration] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se modifica/actualiza el registro de detalle administrativo del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación (VARCHAR 20) del usuario que realiza la última modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación/inserción inicial del registro de detalle administrativo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación (VARCHAR 20) del usuario que crea el registro de detalle administrativo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción (VARCHAR 80) del motivo de inhabilitación del candidato; cuando se completa, el estado debe marcarse como Inhabilitado (Disabled=1) y excluir al candidato de cualquier proceso de selección o vinculación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para detallar el motivo. Seleccionando esta opción, el estado del candidato se debe poner como “Inhabilitado” y no debe poder ligarse a ningún proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de estado (TINYINT: 0=Habilitado, 1=Inhabilitado) que indica si el candidato está bloqueado para participar en procesos de selección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Diabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' opción para seleccionar en el caso que el candidato no se deba ligar a un proceso: 0- habilitado, 1- Inhabilidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Diabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Diabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cabecera principal en tabla PVCandidateAdministration; enlaza este detalle con la administración del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'PVCandidateAdministrationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'PVCandidateAdministrationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'PVCandidateAdministrationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) del registro de detalle administrativo del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de administración de candidatos en el módulo de Talento Humano. Registra información complementaria de cada proceso administrativo asociado a un candidato (postulante), incluyendo si el ítem está deshabilitado, el detalle textual y la trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail';
