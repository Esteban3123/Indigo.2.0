CREATE TABLE [Contract].[Requirement] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequirementTemplateId] INT           NOT NULL,
    [Requirement]           VARCHAR (100) NOT NULL,
    [Process]               VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Requirement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Requirement_RequirementTemplate] FOREIGN KEY ([RequirementTemplateId]) REFERENCES [Contract].[RequirementTemplate] ([Id])
);


GO
ALTER TABLE [Contract].[Requirement] NOCHECK CONSTRAINT [FK_Requirement_RequirementTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tramite que debe hacer', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre del requerimiento  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Requirement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plantilla de requerimiento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'RequirementTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del requerimiento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Requirement', @level2type = N'COLUMN', @level2name = N'Id';

