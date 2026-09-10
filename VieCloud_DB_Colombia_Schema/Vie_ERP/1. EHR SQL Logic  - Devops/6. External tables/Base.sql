IF NOT EXISTS (SELECT 1 FROM sys.external_data_sources WHERE name = 'Src_regulatory')
CREATE EXTERNAL DATA SOURCE [Src_regulatory]
WITH (TYPE = RDBMS, LOCATION = 'ssindigodev.database.windows.net', DATABASE_NAME = 'regulatory', CREDENTIAL = [LoginExternalTablesSEC]);
GO

IF OBJECT_ID('RegulatoryEngine.DecisionTable') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[DecisionTable] (
    [Id] bigint NOT NULL,
    [RegulatoryRuleId] bigint NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2(7) NOT NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'DecisionTable');
GO

IF OBJECT_ID('RegulatoryEngine.DecisionTableCell') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[DecisionTableCell] (
    [Id] bigint NOT NULL,
    [DecisionTableRowId] bigint NOT NULL,
    [DecisionTableColumnId] bigint NOT NULL,
    [Value] nvarchar(500) NOT NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'DecisionTableCell');
GO

IF OBJECT_ID('RegulatoryEngine.DecisionTableColumn') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[DecisionTableColumn] (
    [Id] bigint NOT NULL,
    [DecisionTableId] bigint NOT NULL,
    [ColumnName] nvarchar(200) NOT NULL,
    [ColumnOrder] int NOT NULL,
    [DataType] nvarchar(50) NOT NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'DecisionTableColumn');
GO

IF OBJECT_ID('RegulatoryEngine.DecisionTableRow') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[DecisionTableRow] (
    [Id] bigint NOT NULL,
    [DecisionTableId] bigint NOT NULL,
    [IsAllowed] bit NOT NULL,
    [EffectiveFrom] date NOT NULL,
    [EffectiveTo] date NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'DecisionTableRow');
GO

IF OBJECT_ID('RegulatoryEngine.RegulatoryPack') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[RegulatoryPack] (
    [Id] bigint NOT NULL,
    [Code] nvarchar(100) NOT NULL,
    [Name] nvarchar(300) NOT NULL,
    [JurisdictionCode] nvarchar(20) NOT NULL,
    [Version] nvarchar(50) NOT NULL,
    [EffectiveFrom] date NOT NULL,
    [EffectiveTo] date NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2(7) NOT NULL,
    [UpdatedAt] datetime2(7) NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'RegulatoryPack');
GO

IF OBJECT_ID('RegulatoryEngine.RegulatoryRule') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[RegulatoryRule] (
    [Id] bigint NOT NULL,
    [RegulatoryPackId] bigint NOT NULL,
    [RuleCode] nvarchar(50) NOT NULL,
    [RuleName] nvarchar(300) NOT NULL,
    [EngineClass] nvarchar(500) NOT NULL,
    [BlockingLevel] nvarchar(20) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2(7) NOT NULL,
    [UpdatedAt] datetime2(7) NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'RegulatoryRule');
GO

IF OBJECT_ID('RegulatoryEngine.RuleParameter') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[RuleParameter] (
    [Id] bigint NOT NULL,
    [RegulatoryRuleId] bigint NOT NULL,
    [ParameterName] nvarchar(200) NOT NULL,
    [ParameterValue] nvarchar(MAX) NULL,
    [DataType] nvarchar(50) NOT NULL,
    [CreatedAt] datetime2(7) NOT NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'RuleParameter');
GO

IF OBJECT_ID('RegulatoryEngine.TerminologyValueSet') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[TerminologyValueSet] (
    [Id] bigint NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [JurisdictionCode] nvarchar(20) NULL,
    [Version] nvarchar(50) NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2(7) NOT NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'TerminologyValueSet');
GO

IF OBJECT_ID('RegulatoryEngine.TerminologyValueSetItem') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[TerminologyValueSetItem] (
    [Id] bigint NOT NULL,
    [TerminologyValueSetId] bigint NOT NULL,
    [CodeSystem] nvarchar(200) NOT NULL,
    [Code] nvarchar(200) NOT NULL,
    [DisplayName] nvarchar(1000) NULL,
    [JurisdictionCode] nvarchar(40) NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2(7) NOT NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'TerminologyValueSetItem');
GO

IF OBJECT_ID('RegulatoryEngine.ValidationLog') IS NULL
CREATE EXTERNAL TABLE [RegulatoryEngine].[ValidationLog] (
    [Id] bigint NOT NULL,
    [RegulatoryRuleId] bigint NOT NULL,
    [EntityType] nvarchar(200) NOT NULL,
    [EntityId] bigint NOT NULL,
    [ValidationDate] datetime2(7) NOT NULL,
    [Result] nvarchar(40) NOT NULL,
    [Message] nvarchar(MAX) NULL,
    [ContextJson] nvarchar(MAX) NULL
) WITH (DATA_SOURCE = [Src_regulatory], SCHEMA_NAME = 'RegulatoryEngine', OBJECT_NAME = 'ValidationLog');
GO