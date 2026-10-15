CREATE TABLE [Cost].[CostSecuence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_CostInteropCostSecuence_IsManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_CostInteropCostSecuence_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_CostInteropCostSecuence_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_CostInteropCostSecuence_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_CostInteropCostSecuence] PRIMARY KEY CLUSTERED ([Id] ASC)
);

