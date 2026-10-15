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

