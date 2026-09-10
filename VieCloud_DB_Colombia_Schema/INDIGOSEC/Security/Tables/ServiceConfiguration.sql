CREATE TABLE [Security].[ServiceConfiguration] (
    [Id]                       TINYINT       IDENTITY (1, 1) NOT NULL,
    [EntityServiceProtocol]    TINYINT       NOT NULL,
    [XPOServiceProtocol]       TINYINT       NOT NULL,
    [DocumentServiceProtocol]  TINYINT       NOT NULL,
    [IndexingServiceProtocol]  TINYINT       NOT NULL,
    [EntityServiceURL]         VARCHAR (300) NOT NULL,
    [XPOServiceURL]            VARCHAR (300) NOT NULL,
    [DocumentServiceURL]       VARCHAR (300) NOT NULL,
    [IndexingServiceURL]       VARCHAR (300) NOT NULL,
    [NotificationServiceURL]   VARCHAR (300) NOT NULL,
    [EHRWebServiceProtocol]    TINYINT       NOT NULL,
    [EHREntityServiceProtocol] TINYINT       NOT NULL,
    [EHRWebServiceURL]         VARCHAR (300) NOT NULL,
    [EHREntityServiceURL]      VARCHAR (300) NOT NULL,
    [AppFunctionURL]           VARCHAR (300) NULL,
    [FunctionKey1]             VARCHAR (100) NULL,
    [FunctionKey2]             VARCHAR (100) NULL,
    [FunctionKey3]             VARCHAR (100) NULL,
    [FunctionKey4]             VARCHAR (100) NULL,
    [FunctionKey5]             VARCHAR (100) NULL,
    [FunctionKey6]             VARCHAR (100) NULL,
    [FunctionKey7]             VARCHAR (100) NULL,
    [FunctionKey8]             VARCHAR (100) NULL,
    [FunctionKey10]            VARCHAR (100) NULL,
    [RegulatoryServiceURL]     VARCHAR (300) NULL,
    CONSTRAINT [PK_ServiceConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla para almacenar la configuración de los servicios de Indigo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del configuracion de servicios', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de entidades del ERP. 0:basicHttp, 1:wsHttp, 2:netTcp, 3:Ninguno, 6:basicHttps', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'EntityServiceProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del Servicio de XPO del ERP. 0:basicHttp, 2:netTcp, 3:Ninguno, 6:basicHttps', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'XPOServiceProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de sistema documental del ERP. 0:basicHttp, 1:wsHttp, 2:netTcp, 3:Ninguno, 6:basicHttps', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'DocumentServiceProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de indexación del ERP. 0:basicHttp, 1:wsHttp, 2:netTcp, 3:Ninguno, 6:basicHttps', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'IndexingServiceProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de entidades del ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'EntityServiceURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del Servicio de XPO del ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'XPOServiceURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de sistema documental del ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'DocumentServiceURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de indexación del ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'IndexingServiceURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de notificación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'NotificationServiceURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo de los servicios web del EHR. 0:basicHttp, 1:netTcp, 2:Ninguno,  3:basicHttps', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'EHRWebServiceProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Protocolo del servicio de XPO del EHR. 0:basicHttp, 1:netTcp, 2:Ninguno,  3:basicHttps', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'EHREntityServiceProtocol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de los servicios web del EHR', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'EHRWebServiceURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del servicio de XPO del EHR', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'EHREntityServiceURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de aplicación de funciones', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'AppFunctionURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetPerfilUbicacion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetProfesional', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function SaveUserConfiguration', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetApplicationSettingsByContainerId', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function LoginUserCompany', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de la azure function GetSuscriptions', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'No se usa, disponible', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'No se usa, disponible', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ServiceConfiguration', @level2type = N'COLUMN', @level2name = N'FunctionKey8';

