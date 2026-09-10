CREATE EXTERNAL TABLE [Security].[UserConfiguration] (
    [Id] BIGINT NOT NULL,
    [UserId] INT NOT NULL,
    [ShowThemeSkinSelector] BIT NOT NULL,
    [ReporteadorActivo] BIT NOT NULL,
    [LightweightVersion] BIT NOT NULL,
    [CustomReportPath] VARCHAR (200) NOT NULL,
    [ActualCity] VARCHAR (50) NOT NULL,
    [LanguageCulture] VARCHAR (20) NOT NULL,
    [DefaultCompany] INT NULL,
    [Dashboard] TINYINT NULL,
    [SideFace] TINYINT NULL,
    [CenterAttention] VARCHAR (10) NULL,
    [FunctionalUnit] VARCHAR (10) NULL,
    [NameCareCenter] VARCHAR (100) NULL,
    [FunctionalUnitName] VARCHAR (60) NULL,
    [TypeFunctionalUnit] INT NULL,
    [RoleCode] VARCHAR (3) NULL,
    [GroupCode] VARCHAR (3) NULL,
    [DefaultConfiguration] BIT NULL,
    [OperatingUnitId] INT NULL,
    [TypeAlertControl] TINYINT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'UserConfiguration'
    );

GO