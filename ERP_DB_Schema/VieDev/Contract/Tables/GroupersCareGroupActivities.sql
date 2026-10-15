CREATE TABLE [Contract].[GroupersCareGroupActivities] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupersCareGroupId] INT           NOT NULL,
    [AGACTIMEDCode]       VARCHAR (5)   NOT NULL,
    [AGACTIMEDName]       VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GroupersCareGroupActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroupActivities_GroupersCareGroup] FOREIGN KEY ([GroupersCareGroupId]) REFERENCES [Contract].[GroupersCareGroup] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersCareGroupActivities] NOCHECK CONSTRAINT [FK_GroupersCareGroupActivities_GroupersCareGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la actividad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de los grupos de CareGroup.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'Id';

