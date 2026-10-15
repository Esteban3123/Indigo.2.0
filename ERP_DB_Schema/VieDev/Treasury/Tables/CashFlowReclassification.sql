CREATE TABLE [Treasury].[CashFlowReclassification] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentDate] DATETIME     NOT NULL,
    [DocumentType] TINYINT      NOT NULL,
    [DocumentCode] VARCHAR (30) NULL,
    [CreationUser] VARCHAR (20) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    CONSTRAINT [PK_CashFlowReclassification] PRIMARY KEY CLUSTERED ([Id] ASC)
);

