CREATE TABLE [Inventory].[InventorySequenceDetail] (
    [Id]                  INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventorySequenceId] INT         NOT NULL,
    [IdSequense]          INT         NOT NULL,
    [IdOperatingUnit]     INT         NULL,
    [Next]                BIGINT      CONSTRAINT [DF_InventorySequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    [Prefix]              VARCHAR (4) NULL,
    [Type]                TINYINT     NULL,
    CONSTRAINT [PK_InventorySequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventorySequenceDetail_InventorySequence] FOREIGN KEY ([InventorySequenceId]) REFERENCES [Inventory].[InventorySequence] ([Id]),
    CONSTRAINT [FK_InventorySequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventorySequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Inventory].[InventorySequenceDetail] NOCHECK CONSTRAINT [FK_InventorySequenceDetail_InventorySequence];


GO
ALTER TABLE [Inventory].[InventorySequenceDetail] NOCHECK CONSTRAINT [FK_InventorySequenceDetail_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventorySequenceDetail] NOCHECK CONSTRAINT [FK_InventorySequenceDetail_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos Adicionales', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo usado en la resolución de Facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'InventorySequenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

