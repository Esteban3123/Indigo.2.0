CREATE TABLE [Inventory].[LoanMerchandiseDevolutionDetail] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDevolutionId] INT NOT NULL,
    [LoanMerchandiseDetaillId]    INT NOT NULL,
    [Quantity]                    INT NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDetail] FOREIGN KEY ([LoanMerchandiseDetaillId]) REFERENCES [Inventory].[LoanMerchandiseDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDevolution] FOREIGN KEY ([LoanMerchandiseDevolutionId]) REFERENCES [Inventory].[LoanMerchandiseDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDetail];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDevolution];

