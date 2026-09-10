CREATE TABLE [HumanTalent].[PVCandidateAdministration] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CandidateId]         INT          NOT NULL,
    [SelectionSourceId]   INT          NULL,
    [Type]                INT          NULL,
    [Referred]            TINYINT      NOT NULL,
    [DescriptionReferred] VARCHAR (80) NULL,
    [Disabled]            TINYINT      NOT NULL,
    [DescriptionDisabled] VARCHAR (80) NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    CONSTRAINT [PK_PVCandidateAdministration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVCandidateAdministration_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [HumanTalent].[Candidate] ([Id]),
    CONSTRAINT [FK_PVCandidateAdministration_ExternEntities] FOREIGN KEY ([SelectionSourceId]) REFERENCES [StaffPick].[ExternEntities] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de administración del candidato (DATETIME, NULL si no se ha modificado)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o usuario del sistema que realizó la última modificación del registro (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de administración del candidato (DATETIME, requerido)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o usuario del sistema que creó el registro de administración (VARCHAR 20, requerido, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de creación del usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle o justificación del motivo de inhabilitación del candidato; cuando se activa, el estado pasa a Inhabilitado y no puede vincularse a procesos de selección (VARCHAR 80, justificación PII)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionDisabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para detallar el motivo. Seleccionando esta opción, el estado del candidato se debe poner como “Inhabilitado” y no debe poder ligarse a ningún proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionDisabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionDisabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de habilitación del candidato para procesos de selección: 0=Habilitado/Activo, 1=Inhabilitado/Bloqueado (TINYINT, booleano)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Disabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' opción para seleccionar en el caso que el candidato no se deba ligar a un proceso: 0- habilitado, 1- Inhabilidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Disabled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Disabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de quién refiere o recomienda al candidato; se registra cuando el campo Referred=1 (VARCHAR 80, referente externo)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionReferred';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion para detallar quien lo refiere, se registra cuando el campo Referred esta en 1', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionReferred';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionReferred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de referencia o recomendación externa del candidato: 0=No referenciado, 1=Referenciado/Recomendado (TINYINT, booleano)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Referred';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'opción para seleccionar en el caso que el candidato esté referenciado: 0: No referenciado, 1- referenciado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Referred';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Referred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de candidato en proceso de selección: 1=Candidato interno (empleado), 2=Candidato externo (tercero)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de candidato: 1-Candidato interno, 2-Candidato exgerno ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la fuente o canal de selección del candidato (FK a StaffPick.ExternEntities); referencia maestro de Entidades Externas', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'SelectionSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuente de selección  opciones del maestro “Entidades Externas – Fuentes de selección”', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'SelectionSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'SelectionSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del candidato (FK a HumanTalent.Candidate); llave de relación con datos demográficos y CV del candidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CandidateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de administración del candidato (INT IDENTITY, auditoría, trazabilidad)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación  de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información administrativa de los candidatos en el proceso de selección de talento humano: fuente de reclutamiento, tipo de vinculación, si fue referido y si tiene alguna discapacidad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration';
