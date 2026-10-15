CREATE TABLE [Portfolio].[PortfolioSequenceDetail] (
    [Id]                   INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequensePortfolioC] INT    NOT NULL,
    [IdSequense]           INT    NOT NULL,
    [IdOperatingUnit]      INT    NULL,
    [Next]                 BIGINT CONSTRAINT [DF_SequensePortfolioD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequensePortfolioD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequensePortfolioD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequensePortfolioD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequensePortfolioD_SequensePortfolioC] FOREIGN KEY ([IdSequensePortfolioC]) REFERENCES [Portfolio].[PortfolioSequence] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioSequenceDetail] NOCHECK CONSTRAINT [FK_SequensePortfolioD_OperatingUnit];


GO
ALTER TABLE [Portfolio].[PortfolioSequenceDetail] NOCHECK CONSTRAINT [FK_SequensePortfolioD_Sequense];


GO
ALTER TABLE [Portfolio].[PortfolioSequenceDetail] NOCHECK CONSTRAINT [FK_SequensePortfolioD_SequensePortfolioC];

