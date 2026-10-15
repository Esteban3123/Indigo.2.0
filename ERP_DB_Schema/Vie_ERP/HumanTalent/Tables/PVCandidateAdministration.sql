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
ALTER TABLE [HumanTalent].[PVCandidateAdministration] NOCHECK CONSTRAINT [FK_PVCandidateAdministration_Candidate];


GO
ALTER TABLE [HumanTalent].[PVCandidateAdministration] NOCHECK CONSTRAINT [FK_PVCandidateAdministration_ExternEntities];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de creación del usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para detallar el motivo. Seleccionando esta opción, el estado del candidato se debe poner como “Inhabilitado” y no debe poder ligarse a ningún proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionDisabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' opción para seleccionar en el caso que el candidato no se deba ligar a un proceso: 0- habilitado, 1- Inhabilidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Disabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion para detallar quien lo refiere, se registra cuando el campo Referred esta en 1', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'DescriptionReferred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'opción para seleccionar en el caso que el candidato esté referenciado: 0: No referenciado, 1- referenciado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Referred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de candidato: 1-Candidato interno, 2-Candidato exgerno ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuente de selección  opciones del maestro “Entidades Externas – Fuentes de selección”', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'SelectionSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación  de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministration', @level2type = N'COLUMN', @level2name = N'Id';

