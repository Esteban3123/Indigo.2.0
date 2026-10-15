CREATE TABLE [Inventory].[DispensingByFunctionalUnit] (
    [Id]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FunctionalUnitId]           INT NOT NULL,
    [PharmaceuticalDispensingId] INT NOT NULL,
    CONSTRAINT [PK_Inventory_DispensingByFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la dispensación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DispensingByFunctionalUnit', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DispensingByFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DispensingByFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

