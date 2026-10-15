CREATE TABLE [Contract].[GroupersActivities] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GrouperId]     INT           NOT NULL,
    [AGACTIMEDCode] VARCHAR (5)   NOT NULL,
    [AGACTIMEDName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GroupersActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersActivities_Groupers] FOREIGN KEY ([GrouperId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersActivities] NOCHECK CONSTRAINT [FK_GroupersActivities_Groupers];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Agrupador', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'GrouperId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave principal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'Id';

