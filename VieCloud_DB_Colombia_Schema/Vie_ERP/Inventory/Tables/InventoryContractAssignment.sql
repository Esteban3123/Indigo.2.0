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



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, registro o modificación de la cesión de contrato. Registra el instante exacto en que se creó, actualizó o modificó el documento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se anuló o canceló la cesión del contrato de inventario. Nulo si no ha sido anulado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la anulación o cancelación de la cesión contractual. Nulo si el registro no fue anulado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se confirmó oficialmente la cesión del contrato. Nulo si aún no fue confirmada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que confirmó la validez y aceptación de la cesión contractual. Nulo si no confirmado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio o actualización realizado a la cesión del contrato. Nulo si no fue modificado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación o actualización de la cesión contractual. Nulo si sin cambios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó o registró inicialmente la cesión del contrato en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que originalmente creó o registró la cesión del contrato en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual (TINYINT) de la cesión: 1=Registrado (pendiente), 2=Confirmado (aprobado), 3=Anulado (cancelado). Controla el flujo de la asignación contractual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 300) del detalle, motivo o notas de la cesión o transferencia del contrato entre proveedores.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la cesión del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) a línea de distribución del proveedor cesionario (receptor). Referencia [Common].[SuppliersDistributionLines].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineAssigneeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la linea de distribucion del proveedor cesionario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineAssigneeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineAssigneeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) al proveedor cesionario o receptor de la asignación contractual. Referencia [Common].[Supplier].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierAssigneeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor cesionario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierAssigneeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierAssigneeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) a línea de distribución del proveedor cedente (origen). Referencia [Common].[SuppliersDistributionLines].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineTransferorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la linea de distribucion del proveedor cedente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineTransferorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineTransferorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) al proveedor cedente u origen de la asignación contractual. Referencia [Common].[Supplier].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierTransferorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor cedente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierTransferorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'SupplierTransferorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) del contrato de inventario que se cede o transfiere. Referencia [Inventory].[InventoryContract].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato a ceder', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) del documento de cesión o transferencia contractual. Registra la fecha oficial del acto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) identificador de la cesión o transferencia contractual. Equivalente a número o referencia del documento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la cesión de contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) de la unidad operativa, centro de atención o establecimiento donde se registra la cesión. Referencia [Common].[OperatingUnit].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de la cesión de contrato. Clave primaria que registra cada asignación contractual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de asignaciones de contratos de inventario entre proveedores cedentes y proveedores cesionarios dentro de una unidad operativa. Permite trazar el traspaso o reasignación de contratos de suministro, incluyendo el estado del documento y la trazabilidad de creación, confirmación y anulación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAssignment';
