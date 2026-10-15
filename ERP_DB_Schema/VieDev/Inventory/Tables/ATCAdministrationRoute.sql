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

