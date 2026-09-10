CREATE TABLE [Security].[ApplicationSettings] (
    [Id]                   SMALLINT      IDENTITY (1, 1) NOT NULL,
    [ContainerId]          INT           NOT NULL,
    [CacheServerName]      VARCHAR (50)  NULL,
    [CachePort]            VARCHAR (10)  NULL,
    [NombreCache]          VARCHAR (50)  CONSTRAINT [DF_ApplicationSettings_NombreCache] DEFAULT ('IndigoCrystal') NOT NULL,
    [DuraccionCacheCorta]  SMALLINT      CONSTRAINT [DF_ApplicationSettings_DuraccionCacheCorta] DEFAULT ((10)) NOT NULL,
    [DuraccionCacheMedia]  SMALLINT      CONSTRAINT [DF_ApplicationSettings_DuraccionCacheMedia] DEFAULT ((60)) NOT NULL,
    [DuraccionCacheLarga]  SMALLINT      CONSTRAINT [DF_ApplicationSettings_DuraccionCacheLarga] DEFAULT ((180)) NOT NULL,
    [ServidorMongo]        VARCHAR (50)  NULL,
    [BaseDatosMongo]       VARCHAR (50)  NULL,
    [CachedEnable]         BIT           CONSTRAINT [DF_ApplicationSettings_CachedEnable] DEFAULT ((0)) NOT NULL,
    [NewServices]          BIT           CONSTRAINT [DF_ApplicationSettings_NewServices] DEFAULT ((0)) NOT NULL,
    [ChangeMessageBox]     VARCHAR (1)   CONSTRAINT [DF_ApplicationSettings_ChangeMessageBox] DEFAULT ('0') NOT NULL,
    [UrlServiceDispensing] VARCHAR (300) NULL,
    CONSTRAINT [PK_ApplicationSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationSettings_Containers] FOREIGN KEY ([ContainerId]) REFERENCES [Security].[Containers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationSettings_ContainerId]
    ON [Security].[ApplicationSettings]([ContainerId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena  la configuración de la aplicación de una compañia

Nota: Contiene objetos obsoletos que se dejaran de usar al cambio por RedisCache
', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de container', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'ContainerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servidor o ip donde se almacena la cache para EHR - Obsoleto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'CacheServerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puerto usado para el manejo de la cache para EHR - Obsoleto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'CachePort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la cache para EHR - Obsoleto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'NombreCache';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos para la vigencia del dato en la cache - Obsoleto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'DuraccionCacheCorta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos para la vigencia del dato en la cache - Obsoleto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'DuraccionCacheMedia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos para la vigencia del dato en la cache - Obsoleto', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'DuraccionCacheLarga';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servidor o ip del servidor mongo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'ServidorMongo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la base de datos en el servidor mongo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'BaseDatosMongo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se habilita o no cache para XPO en ERP. True:Si, False:No ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'CachedEnable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se crea o no una nueva instancia de cache para XPO en ERP. True:Si, False:No', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'NewServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se visualizan los mensajes del EHR en la parte inferior derecha como en ERP. 0:Normal, 1:Como en ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'ChangeMessageBox';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de dispensación. - Funcional especifico para los frm de Dispensación automatica y manual realizados para FarmaQx', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ApplicationSettings', @level2type = N'COLUMN', @level2name = N'UrlServiceDispensing';

