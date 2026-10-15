CREATE TABLE [dbo].[InstitutionalDocument] (
    [Id]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]         VARCHAR (20)  NOT NULL,
    [Name]         VARCHAR (150) NOT NULL,
    [DocumentType] VARCHAR (3)   NOT NULL,
    CONSTRAINT [PK_InstitutionalDocument__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InstitutionalDocument__Code]
    ON [dbo].[InstitutionalDocument]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del documento  M - Manuales  P - Procedimientos  O - Programa  C - Caracterizaciones  F - Formatos  I - Instructivos  G - Guias Clinicas  E - Protocolos Atención Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'InstitutionalDocument', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del documento institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'InstitutionalDocument', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del documento institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'InstitutionalDocument', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'InstitutionalDocument', @level2type = N'COLUMN', @level2name = N'Id';

