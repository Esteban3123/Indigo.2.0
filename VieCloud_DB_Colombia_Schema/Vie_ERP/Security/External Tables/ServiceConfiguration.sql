CREATE EXTERNAL TABLE [Security].[ServiceConfiguration] (
    [Id] TINYINT NOT NULL,
    [EntityServiceProtocol] TINYINT NOT NULL,
    [XPOServiceProtocol] TINYINT NOT NULL,
    [DocumentServiceProtocol] TINYINT NOT NULL,
    [IndexingServiceProtocol] TINYINT NOT NULL,
    [EntityServiceURL] VARCHAR (300) NOT NULL,
    [XPOServiceURL] VARCHAR (300) NOT NULL,
    [DocumentServiceURL] VARCHAR (300) NOT NULL,
    [IndexingServiceURL] VARCHAR (300) NOT NULL,
    [NotificationServiceURL] VARCHAR (300) NOT NULL,
    [EHRWebServiceProtocol] TINYINT NOT NULL,
    [EHREntityServiceProtocol] TINYINT NOT NULL,
    [EHRWebServiceURL] VARCHAR (300) NOT NULL,
    [EHREntityServiceURL] VARCHAR (300) NOT NULL,
    [AppFunctionURL] VARCHAR (300) NULL,
    [FunctionKey1] VARCHAR (100) NULL,
    [FunctionKey2] VARCHAR (100) NULL,
    [FunctionKey3] VARCHAR (100) NULL,
    [FunctionKey4] VARCHAR (100) NULL,
    [FunctionKey5] VARCHAR (100) NULL,
    [FunctionKey6] VARCHAR (100) NULL,
    [FunctionKey7] VARCHAR (100) NULL,
    [FunctionKey8] VARCHAR (100) NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'ServiceConfiguration'
    );

GO