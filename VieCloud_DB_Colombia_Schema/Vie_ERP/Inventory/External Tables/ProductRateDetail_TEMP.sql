CREATE EXTERNAL TABLE [Inventory].[ProductRateDetail_TEMP] (
    [Id] INT NOT NULL,
    [ProductRateId] INT NOT NULL,
    [ProductId] INT NULL,
    [InitialDate] DATETIME NOT NULL,
    [EndDate] DATETIME NOT NULL,
    [SalesValue] NUMERIC (18, 2) NOT NULL,
    [SalesValueWithSurcharge] NUMERIC (18, 2) NOT NULL,
    [Contracted] BIT NOT NULL,
    [Quoted] BIT NOT NULL,
    [Observations] VARCHAR (MAX) NULL,
    [LiquidationType] TINYINT NOT NULL,
    [RateType] TINYINT NOT NULL,
    [PercentageBasedOn] TINYINT NOT NULL,
    [Percentage] NUMERIC (6, 2) NULL,
    [CupsId] INT NULL,
    [ContractDescriptionId] INT NULL,
    [Status] TINYINT NOT NULL,
    [RateClass] TINYINT NULL,
    [PackageId] INT NULL,
    [DoseType] TINYINT NULL
)
    WITH (
    DATA_SOURCE = [INDIGO999_temp]
    );

