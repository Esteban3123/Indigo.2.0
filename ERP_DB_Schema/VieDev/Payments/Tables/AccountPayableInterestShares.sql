CREATE TABLE [Payments].[AccountPayableInterestShares] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayableShares] INT             NOT NULL,
    [DateInterestShares]     DATETIME        NOT NULL,
    [Value]                  DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AccountPayableInterestShares] PRIMARY KEY CLUSTERED ([Id] ASC)
);

