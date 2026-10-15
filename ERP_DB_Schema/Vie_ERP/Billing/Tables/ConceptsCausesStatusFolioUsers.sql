CREATE TABLE [Billing].[ConceptsCausesStatusFolioUsers] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptsCausesStatusFolioId] INT           NOT NULL,
    [UserId]                      INT           NOT NULL,
    [UserCode]                    VARCHAR (20)  NOT NULL,
    [FullNameUser]                VARCHAR (200) NULL,
    CONSTRAINT [PK_ConceptsCausesStatusFolioUsers_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptsCausesStatusFolioUsers_ConceptsCausesStatusFolio] FOREIGN KEY ([ConceptsCausesStatusFolioId]) REFERENCES [Billing].[ConceptsCausesStatusFolio] ([Id])
);


GO
ALTER TABLE [Billing].[ConceptsCausesStatusFolioUsers] NOCHECK CONSTRAINT [FK_ConceptsCausesStatusFolioUsers_ConceptsCausesStatusFolio];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el nombre completo del usuario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'FullNameUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla Conceptos Causas de Estado Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'ConceptsCausesStatusFolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla Conceptos Causas de Estado Folio Usuarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ConceptsCausesStatusFolioUsers', @level2type = N'COLUMN', @level2name = N'Id';

