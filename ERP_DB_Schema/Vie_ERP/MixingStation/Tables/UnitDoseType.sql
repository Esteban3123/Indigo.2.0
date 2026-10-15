CREATE TABLE [MixingStation].[UnitDoseType] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Description]         VARCHAR (MAX) NULL,
    [MSClass]             INT           NOT NULL,
    [State]               BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_UnitDoseType_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_UnitDoseType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [InventoryGroupId]    INT           NULL,
    [InventorySubGroupId] INT           NULL,
    [IVACodeId]           INT           NULL,
    [BillingGroupId]      INT           NULL,
    [Prefix]              VARCHAR (4)   CONSTRAINT [DF__UnitDoseT__Pefix__1DEBCF31] DEFAULT ('NNNN') NOT NULL,
    CONSTRAINT [PK_UnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnitDoseType_BillingGroup] FOREIGN KEY ([BillingGroupId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_UnitDoseType_GeneralLedgerIVA] FOREIGN KEY ([IVACodeId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_UnitDoseType_ProductGroup] FOREIGN KEY ([InventoryGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_UnitDoseType_ProductSubGroup] FOREIGN KEY ([InventorySubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id])
);


GO
ALTER TABLE [MixingStation].[UnitDoseType] NOCHECK CONSTRAINT [FK_UnitDoseType_BillingGroup];


GO
ALTER TABLE [MixingStation].[UnitDoseType] NOCHECK CONSTRAINT [FK_UnitDoseType_GeneralLedgerIVA];


GO
ALTER TABLE [MixingStation].[UnitDoseType] NOCHECK CONSTRAINT [FK_UnitDoseType_ProductGroup];


GO
ALTER TABLE [MixingStation].[UnitDoseType] NOCHECK CONSTRAINT [FK_UnitDoseType_ProductSubGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo de  4 caracteres', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Prefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id del grupo de facturación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ID de código IVA', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'IVACodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ID de subgrupo de inventario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventorySubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de grupo de inventario', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'InventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'2 - NPT , 
3 - Antibioticoterapia,
4 - Citostático, 
5 - Reempaque, 
7 - Reenvase (Obsoleta),
9 - Magistral,  
10 - Otros estériles.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'MSClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'UnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';

