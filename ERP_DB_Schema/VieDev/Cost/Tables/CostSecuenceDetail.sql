CREATE TABLE [Cost].[CostSecuenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SequenseInteropCostId] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_CostInteropCostSecuenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CostInteropCostSecuenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostInteropCostSecuenceDetail_CostInteropCostSecuence] FOREIGN KEY ([SequenseInteropCostId]) REFERENCES [Cost].[CostSecuence] ([Id]),
    CONSTRAINT [FK_CostInteropCostSecuenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CostInteropCostSecuenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Cost].[CostSecuenceDetail] NOCHECK CONSTRAINT [FK_CostInteropCostSecuenceDetail_CostInteropCostSecuence];


GO
ALTER TABLE [Cost].[CostSecuenceDetail] NOCHECK CONSTRAINT [FK_CostInteropCostSecuenceDetail_OperatingUnit];


GO
ALTER TABLE [Cost].[CostSecuenceDetail] NOCHECK CONSTRAINT [FK_CostInteropCostSecuenceDetail_Sequense];

