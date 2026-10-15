CREATE TABLE [Security].[PermissionCompanyInt] (
    [Id]                     INT NOT NULL,
    [IdUser]                 INT NOT NULL,
    [IdContainer]            INT NOT NULL,
    [IdOperatingUnitDefault] INT NOT NULL,
    [Permission]             BIT NOT NULL,
    [Administrator]          BIT NULL
);

