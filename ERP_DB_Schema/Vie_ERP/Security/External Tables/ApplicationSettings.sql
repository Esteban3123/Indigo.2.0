CREATE EXTERNAL TABLE [Security].[ApplicationSettings] (
    [Id] SMALLINT NOT NULL,
    [ContainerId] INT NOT NULL,
    [CacheServerName] VARCHAR (50) NULL,
    [CachePort] VARCHAR (10) NULL,
    [NombreCache] VARCHAR (50) NOT NULL,
    [DuraccionCacheCorta] SMALLINT NOT NULL,
    [DuraccionCacheMedia] SMALLINT NOT NULL,
    [DuraccionCacheLarga] SMALLINT NOT NULL,
    [ServidorMongo] VARCHAR (50) NULL,
    [BaseDatosMongo] VARCHAR (50) NULL,
    [CachedEnable] BIT NOT NULL,
    [NewServices] BIT NOT NULL,
    [ChangeMessageBox] VARCHAR (1) NOT NULL,
    [UrlServiceDispensing] VARCHAR (300) NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'ApplicationSettings'
    );

GO