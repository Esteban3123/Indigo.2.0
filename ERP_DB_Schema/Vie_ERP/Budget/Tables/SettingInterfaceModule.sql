CREATE TABLE [Budget].[SettingInterfaceModule] (
    [Id]       INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ItemType] TINYINT    NOT NULL,
    [Process]  NCHAR (10) NULL,
    CONSTRAINT [PK_SettingInterfaceModule] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tramite que debe hacer', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo del registro  1 - Ingreso  2 - Gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingInterfaceModule', @level2type = N'COLUMN', @level2name = N'Id';

