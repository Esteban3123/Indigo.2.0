CREATE TABLE [Inventory].[InventoryAdjustment] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [OperatingUnitId]     INT           NOT NULL,
    [DocumentDate]        DATETIME      NOT NULL,
    [AdjustmentType]      TINYINT       NOT NULL,
    [AdjustmentConceptId] INT           NULL,
    [InventoryControlId]  INT           NULL,
    [WarehouseId]         INT           NULL,
    [ThirdPartyId]        INT           NULL,
    [Description]         VARCHAR (500) NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [AdmissionNumber]     CHAR (10)     NULL,
    CONSTRAINT [PK_InventoryAdjustment__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryAdjustment_AdjustmentConcept] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_InventoryControl] FOREIGN KEY ([InventoryControlId]) REFERENCES [Inventory].[InventoryControl] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_AdjustmentConcept];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_InventoryControl];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_ThirdParty];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_Warehouse];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InventoryAdjustment__Code]
    ON [Inventory].[InventoryAdjustment]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de admisión', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-Registrado 2-Confirmado 3-Anulado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero del ajuste del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen de los cuales tiene permiso el usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del control de inventarios, este campo solo se llena si el tipo del ajuste es de tipo Inventario Fisico    Los registros de control de inventarios se tienen que filtrar por el tipo de documentos "Control de Inventarios"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'InventoryControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de ajuste que puede ser de entrada o a salida o puede ser nulo si el Tipo de ajuste es Inventario Fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de ajuste  1 - Entrada  2 - Salida  3 - Inventario Fisico  4 - Ajuste de Inventario Custodia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'AdjustmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la unidad operativa donde se creo el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del ajuste del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'Id';

