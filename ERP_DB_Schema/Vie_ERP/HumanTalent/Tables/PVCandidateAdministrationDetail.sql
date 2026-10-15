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
ALTER TABLE [HumanTalent].[PVCandidateAdministrationDetail] NOCHECK CONSTRAINT [FK_PVCandidateAdministrationDetail_PVCandidateAdministration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para detallar el motivo. Seleccionando esta opción, el estado del candidato se debe poner como “Inhabilitado” y no debe poder ligarse a ningún proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' opción para seleccionar en el caso que el candidato no se deba ligar a un proceso: 0- habilitado, 1- Inhabilidato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Diabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'PVCandidateAdministrationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVCandidateAdministrationDetail', @level2type = N'COLUMN', @level2name = N'Id';

