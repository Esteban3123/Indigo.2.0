CREATE TABLE [Inventory].[InventoryContractAssignment] (
    [Id]                                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                      INT           NOT NULL,
    [Code]                                 VARCHAR (20)  NOT NULL,
    [DocumentDate]                         DATETIME      NOT NULL,
    [ContractId]                           INT           NOT NULL,
    [SupplierTransferorId]                 INT           NOT NULL,
    [SupplierDistributionLineTransferorId] INT           NOT NULL,
    [SupplierAssigneeId]                   INT           NOT NULL,
    [SupplierDistributionLineAssigneeId]   INT           NOT NULL,
    [Description]                          VARCHAR (300) NOT NULL,
    [Status]                               TINYINT       NOT NULL,
    [CreationUser]                         VARCHAR (20)  NOT NULL,
    [CreationDate]                         DATETIME      NOT NULL,
    [ModificationUser]                     VARCHAR (20)  NULL,
    [ModificationDate]                     DATETIME      NULL,
    [ConfirmationUser]                     VARCHAR (20)  NULL,
    [ConfirmationDate]                     DATETIME      NULL,
    [AnnulmentUser]                        VARCHAR (20)  NULL,
    [AnnulmentDate]                        DATETIME      NULL,
    [TimeStamp]                            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_InventoryContractAssignment__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContractAssignment_InventoryContract] FOREIGN KEY ([ContractId]) REFERENCES [Inventory].[InventoryContract] ([Id]),
    CONSTRAINT [FK_InventoryContractAssignment_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryContractAssignment_SupplierAssignee] FOREIGN KEY ([SupplierAssigneeId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InventoryContractAssignment_SuppliersDistributionLinesAssignee] FOREIGN KEY ([SupplierDistributionLineAssigneeId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_InventoryContractAssignment_SuppliersDistributionLinesTransferor] FOREIGN KEY ([SupplierDistributionLineTransferorId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_InventoryContractAssignment_SupplierTransferor] FOREIGN KEY ([SupplierTransferorId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryContractAssignment] NOCHECK CONSTRAINT [FK_InventoryContractAssignment_InventoryContract];


GO
ALTER TABLE [Inventory].[InventoryContractAssignment] NOCHECK CONSTRAINT [FK_InventoryContractAssignment_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventoryContractAssignment] NOCHECK CONSTRAINT [FK_InventoryContractAssignment_SupplierAssignee];


GO
ALTER TABLE [Inventory].[InventoryContractAssignment] NOCHECK CONSTRAINT [FK_InventoryContractAssignment_SuppliersDistributionLinesAssignee];


GO
ALTER TABLE [Inventory].[InventoryContractAssignment] NOCHECK CONSTRAINT [FK_InventoryContractAssignment_SuppliersDistributionLinesTransferor];


GO
ALTER TABLE [Inventory].[InventoryContractAssignment] NOCHECK CONSTRAINT [FK_InventoryContractAssignment_SupplierTransferor];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la cesión del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion del proveedor cesionario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineAssigneeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor cesionario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierAssigneeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion del proveedor cedente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineTransferorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor cedente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierTransferorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato a ceder', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la cesión de contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Id';

