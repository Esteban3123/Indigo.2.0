CREATE TABLE [Inventory].[ATCAdministrationRoute] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]                 INT NOT NULL,
    [AdministrationRouteId] INT NOT NULL,
    CONSTRAINT [PK_ATCAdministrationRoute] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ATCAdministrationRoute_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_ATCAdministrationRoute_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id])
);


GO
ALTER TABLE [Inventory].[ATCAdministrationRoute] NOCHECK CONSTRAINT [FK_ATCAdministrationRoute_AdministrationRoute];


GO
ALTER TABLE [Inventory].[ATCAdministrationRoute] NOCHECK CONSTRAINT [FK_ATCAdministrationRoute_ATC];


GO
CREATE NONCLUSTERED INDEX [IX_ATCAdministrationRoute_ATCId]
    ON [Inventory].[ATCAdministrationRoute]([ATCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de vía de administración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'Id';

