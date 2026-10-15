CREATE TABLE [MixingStation].[WorkingArea] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT           NOT NULL,
    [Code]              VARCHAR (3)   NOT NULL,
    [Description]       VARCHAR (20)  NOT NULL,
    [Observation]       VARCHAR (500) NOT NULL,
    [Status]            BIT           NOT NULL,
    CONSTRAINT [PK_WorkingArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkingArea_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [IX_WorkingArea] UNIQUE NONCLUSTERED ([CMConfigurationId] ASC, [Code] ASC)
);


GO
ALTER TABLE [MixingStation].[WorkingArea] NOCHECK CONSTRAINT [FK_WorkingArea_CMConfiguration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo  estado ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'WorkingArea', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Observación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'WorkingArea', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Descripción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'WorkingArea', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'WorkingArea', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'WorkingArea', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'WorkingArea', @level2type = N'COLUMN', @level2name = N'Id';

