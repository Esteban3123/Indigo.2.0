CREATE TABLE [Management].[MachineUpdatePackage] (
    [Id]              INT      NOT NULL,
    [IdMachine]       INT      NOT NULL,
    [IdUpdatePackage] INT      NOT NULL,
    [UpgradeDate]     DATETIME NULL,
    [UpgradedDate]    DATETIME NULL,
    [Upgraded]        BIT      NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualizado | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Upgraded';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradeDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Paquete de actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdUpdatePackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la máquina', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdMachine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';

