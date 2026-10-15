CREATE TABLE [Maintenance].[MaintenancesProtocolActivities] (
    [Id]   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code] VARCHAR (20)  NOT NULL,
    [Name] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_MaintenancesProtocolActivities__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_MaintenancesProtocolActivities__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave principal', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancesProtocolActivities', @level2type = N'COLUMN', @level2name = N'Id';

