CREATE TABLE [Inventory].[InventoryMeasurementUnit] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)    NOT NULL,
    [Name]                   VARCHAR (100)   NOT NULL,
    [Abbreviation]           VARCHAR (10)    NOT NULL,
    [UnitType]               TINYINT         NOT NULL,
    [AllowEditCostValue]     BIT             CONSTRAINT [DF_InventoryMeasurementUnit_AllowEditCostValue] DEFAULT ((0)) NOT NULL,
    [CostValue]              DECIMAL (18, 2) CONSTRAINT [DF_InventoryMeasurementUnit_CostValue] DEFAULT ((0)) NOT NULL,
    [CrystalMeasurementUnit] VARCHAR (20)    NULL,
    [Status]                 BIT             NOT NULL,
    [CreationUser]           VARCHAR (20)    CONSTRAINT [DF_UnitMeasure_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]           DATETIME        CONSTRAINT [DF_UnitMeasure_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    [RequiresStandardCode]   BIT             DEFAULT ((0)) NOT NULL,
    [StandardCode]           VARCHAR (5)     NULL,
    CONSTRAINT [PK_InventoryMeasurementUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_STANDARD_CODE] CHECK ([RequiresStandardCode]=case when coalesce([StandardCode],'')<>'' then (1) else (0) end)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InventoryMeasurementUnit__Code]
    ON [Inventory].[InventoryMeasurementUnit]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida de cristal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CrystalMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Punto de valor o el valor del costo para la unidad de medida del sistema de costos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si se permite la modificación de los puntos de valor para la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'AllowEditCostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de unidad  1- Peso 2- Volumen 3- Unidad Administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abreviación de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Abbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';

