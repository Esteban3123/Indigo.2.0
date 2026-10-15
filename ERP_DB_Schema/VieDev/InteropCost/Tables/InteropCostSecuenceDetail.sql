CREATE TABLE [InteropCost].[InteropCostSecuenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SequenseInteropCostId] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseInteropCostD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseInteropCostD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InteropCostSecuenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseInteropCostD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseInteropCostD_SequenseInteropCostC] FOREIGN KEY ([SequenseInteropCostId]) REFERENCES [InteropCost].[InteropCostSecuence] ([Id])
);


GO
ALTER TABLE [InteropCost].[InteropCostSecuenceDetail] NOCHECK CONSTRAINT [FK_InteropCostSecuenceDetail_Sequense];


GO
ALTER TABLE [InteropCost].[InteropCostSecuenceDetail] NOCHECK CONSTRAINT [FK_SequenseInteropCostD_OperatingUnit];


GO
ALTER TABLE [InteropCost].[InteropCostSecuenceDetail] NOCHECK CONSTRAINT [FK_SequenseInteropCostD_SequenseInteropCostC];

