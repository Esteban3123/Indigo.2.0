CREATE TABLE [Contract].[RateManualDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RateManualId]            INT             NOT NULL,
    [IPSServiceId]            INT             NOT NULL,
    [SalesValueWithSurcharge] NUMERIC (18, 2) CONSTRAINT [DF_RateManualDetail_SalesValueWithSurcharge] DEFAULT ((0)) NOT NULL,
    [SalesValue]              NUMERIC (18, 2) CONSTRAINT [DF_RateManualDetail_SalesValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RateManualDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RateManualDetail_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_RateManualDetail_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id])
);


GO
ALTER TABLE [Contract].[RateManualDetail] NOCHECK CONSTRAINT [FK_RateManualDetail_IPSService];


GO
ALTER TABLE [Contract].[RateManualDetail] NOCHECK CONSTRAINT [FK_RateManualDetail_RateManual];

