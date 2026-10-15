CREATE TABLE [Billing].[AccountBillingEntityCapitated] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]         VARCHAR (20) NOT NULL,
    [DocumentDate] DATETIME     NOT NULL,
    [CareGroupId]  INT          NOT NULL,
    [InitialDate]  DATETIME     NOT NULL,
    [EndDate]      DATETIME     NOT NULL,
    CONSTRAINT [PK_AccountBillingEntityCapitated__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

