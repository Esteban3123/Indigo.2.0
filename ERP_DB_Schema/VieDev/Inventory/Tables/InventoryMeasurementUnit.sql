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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de unidad  1- Peso 2- Volumen 3- Unidad Administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'UnitType';

