CREATE TABLE [Inventory].[ClosedMonthControl] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Value]         DECIMAL (18, 2) NOT NULL,
    [Nature]        INT             CONSTRAINT [DF__ClosedMon__Natur__19896ECC] DEFAULT ((1)) NOT NULL,
    [CloseMonthId]  INT             NOT NULL,
    [MainAccountId] INT             NOT NULL,
    [ThirdPartyId]  INT             NULL,
    [CostCenterId]  INT             NULL,
    [EntityId]      INT             NOT NULL,
    [EntityName]    VARCHAR (250)   NOT NULL,
    CONSTRAINT [PK_ClosedMonthControl] PRIMARY KEY CLUSTERED ([Id] ASC)
);

