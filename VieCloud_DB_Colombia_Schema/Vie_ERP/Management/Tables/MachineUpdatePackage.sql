CREATE TABLE [Management].[MachineUpdatePackage] (
    [Id]              INT      NOT NULL,
    [IdMachine]       INT      NOT NULL,
    [IdUpdatePackage] INT      NOT NULL,
    [UpgradeDate]     DATETIME NULL,
    [UpgradedDate]    DATETIME NULL,
    [Upgraded]        BIT      NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de actualización completada (BIT: 1=Sí actualizado, 0=No actualizado). Bandera de estado que refleja si el paquete de actualización fue aplicado exitosamente a la máquina.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Upgraded';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualizado | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Upgraded';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Upgraded';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se completó la actualización del paquete en la máquina (DATETIME). Timestamp de finalización del proceso de upgrade.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora programada o iniciada para la actualización del paquete en la máquina (DATETIME). Timestamp de inicio o agendamiento del proceso de upgrade.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradeDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradeDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'UpgradeDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del paquete de actualización (INT). Clave foránea que referencia la versión o lote de software a aplicar en la máquina.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdUpdatePackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Paquete de actualización', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdUpdatePackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdUpdatePackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la máquina o equipo (INT). Clave foránea que referencia el dispositivo o servidor que recibe la actualización.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdMachine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la máquina', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdMachine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'IdMachine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la relación máquina-paquete (INT PRIMARY KEY). Clave primaria que registra cada evento de actualización.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de paquetes de actualización asignados a cada máquina o equipo del sistema. Permite hacer seguimiento del estado de las actualizaciones de software por dispositivo, incluyendo si ya fueron aplicadas y en qué fechas.', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Management', @level1type = N'TABLE', @level1name = N'MachineUpdatePackage';
