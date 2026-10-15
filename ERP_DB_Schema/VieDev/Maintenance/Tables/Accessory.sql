CREATE TABLE [Maintenance].[Accessory] (
    [Id]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]      VARCHAR (20)  NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [State]     BIT           NOT NULL,
    [TimeStamp] ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Accessory__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Accessory__Code]
    ON [Maintenance].[Accessory]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Accessory__State]
    ON [Maintenance].[Accessory]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de concurrencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Accessory', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Accesorio 1- Activo 0 - Inactivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Accessory', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Accessory', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Accessory', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de accesorios.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Accessory', @level2type = N'COLUMN', @level2name = N'Id';

