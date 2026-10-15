CREATE TABLE [Common].[ConceptGlosasUser] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [ConceptGlosaId] INT          NOT NULL,
    [UserId]         INT          NOT NULL,
    [UserCode]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ConceptGlosasUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptGlosasUser_ConceptGlosas] FOREIGN KEY ([ConceptGlosaId]) REFERENCES [Common].[ConceptGlosas] ([Id])
);


GO
ALTER TABLE [Common].[ConceptGlosasUser] NOCHECK CONSTRAINT [FK_ConceptGlosasUser_ConceptGlosas];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de glosa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'ConceptGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del concepto de glosas usuario.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosasUser', @level2type = N'COLUMN', @level2name = N'Id';

