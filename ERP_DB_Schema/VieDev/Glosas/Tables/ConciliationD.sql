CREATE TABLE [Glosas].[ConciliationD] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConciliationCId]  INT          NOT NULL,
    [GlosaPortfolioId] INT          NOT NULL,
    [InvoiceNumber]    VARCHAR (50) NOT NULL,
    [State]            CHAR (1)     NOT NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ConciliationD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConciliationD_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id]),
    CONSTRAINT [FK_ConciliationD_GlosaPortfolioGlosada] FOREIGN KEY ([GlosaPortfolioId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id])
);


GO
ALTER TABLE [Glosas].[ConciliationD] NOCHECK CONSTRAINT [FK_ConciliationD_ConciliationC];


GO
ALTER TABLE [Glosas].[ConciliationD] NOCHECK CONSTRAINT [FK_ConciliationD_GlosaPortfolioGlosada];

