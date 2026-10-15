CREATE TABLE [Glosas].[Contract_Public] (
    [ContractCode]       VARCHAR (10) NOT NULL,
    [PlanCode]           VARCHAR (10) NOT NULL,
    [AccountNotRadicate] VARCHAR (20) NULL,
    [AccountRadicate]    VARCHAR (20) NULL,
    CONSTRAINT [PK_Contract_Public] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [PlanCode] ASC)
);

