CREATE TABLE [Security].[Containers] (
    [Id]                     INT              IDENTITY (1, 1) NOT NULL,
    [Code]                   VARCHAR (2)      NOT NULL,
    [Name]                   VARCHAR (50)     NOT NULL,
    [FoundationalContainer]  VARCHAR (30)     CONSTRAINT [DF_Containers_FoundationalContainer] DEFAULT ('') NOT NULL,
    [TransactionalContainer] VARCHAR (30)     NOT NULL,
    [DocumentalContainer]    VARCHAR (30)     NOT NULL,
    [VituelContainer]        VARCHAR (30)     NOT NULL,
    [HISContainer]           VARCHAR (30)     NOT NULL,
    [InteropCostContainer]   VARCHAR (30)     NOT NULL,
    [HISIntegration]         TINYINT          CONSTRAINT [DF_Containers_HISIntegration] DEFAULT ((1)) NOT NULL,
    [GlossesIntegration]     TINYINT          CONSTRAINT [DF_Containers_GlossesIntegration] DEFAULT ((1)) NOT NULL,
    [PayrollIntegration]     TINYINT          CONSTRAINT [DF_Containers_PayrollIntegration] DEFAULT ((1)) NOT NULL,
    [CompanyType]            TINYINT          CONSTRAINT [DF_Containers_CompanyType] DEFAULT ((1)) NOT NULL,
    [CompanyNit]             VARCHAR (15)     NOT NULL,
    [RepresentationLegal]    VARCHAR (100)    NOT NULL,
    [City]                   VARCHAR (50)     NOT NULL,
    [Address]                VARCHAR (50)     NOT NULL,
    [Telephone]              VARCHAR (20)     NOT NULL,
    [ProductionCompany]      BIT              NOT NULL,
    [KeyCode]                VARCHAR (20)     NOT NULL,
    [LyncIntegration]        BIT              NOT NULL,
    [Version]                VARCHAR (20)     CONSTRAINT [DF_Containers_Version] DEFAULT ('14.7.2.2') NOT NULL,
    [State]                  BIT              NOT NULL,
    [DispensingIntegration]  TINYINT          CONSTRAINT [DF_Containers_HEONIntegration] DEFAULT ((1)) NOT NULL,
    [HumanTalentIntegration] TINYINT          CONSTRAINT [DF_Containers_HumanTalentIntegration] DEFAULT ((1)) NOT NULL,
    [VerificationDigitNit]   VARCHAR (3)      NOT NULL,
    [ArchitectureType]       TINYINT          NOT NULL,
    [CloudType]              TINYINT          NOT NULL,
    [Multitenant]            BIT              NOT NULL,
    [SuscriptionID]          UNIQUEIDENTIFIER NOT NULL,
    [BranchOffices]          TINYINT          NOT NULL,
    [ServiceConfigurationId] TINYINT          NOT NULL,
    [PublishEvent]           BIT              DEFAULT ((0)) NULL,
    [UrlQueue]               VARCHAR (MAX)    NULL,
    [IsSynchronized]         BIT              DEFAULT ((0)) NULL,
    [HeadersImage]           VARCHAR (100)    NULL,
    [FootImage]              VARCHAR (100)    NULL,
    [DecimalSeparator]       VARCHAR (1)      NULL,
    [AzureFileShareRoute]    VARCHAR (500)    NULL,
    [ArchitectureQueue]      TINYINT          NULL,
    [NameQueue]              VARCHAR (30)     NULL,
    [ClientId]               UNIQUEIDENTIFIER DEFAULT (lower(newid())) NOT NULL,
    [VersionURL]             VARCHAR (1000)   NULL,
    [ExchangeControl]        VARCHAR (1000)   NULL,
    CONSTRAINT [PK_Containers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Containers_ServiceConfiguration] FOREIGN KEY ([ServiceConfigurationId]) REFERENCES [Security].[ServiceConfiguration] ([Id]),
    CONSTRAINT [IX_Containers_Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Containers_ServiceConfigurationId]
    ON [Security].[Containers]([ServiceConfigurationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Empresa', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Empresa', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'FoundationalContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el contenedor el cual fue asignado a la empresa', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'TransactionalContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el contenedor para gestion documental', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'DocumentalContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenedor Virtual', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'VituelContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo Obsoleto usado antes de la unificación de bases de datos', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'HISContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo Obsoleto usado para el uso del modulo de costos conectado a otro ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'InteropCostContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si el ERP tiene integracion con VIE EHR
0 - No se encuentra integrado
1 - ERP Integrado al EHR', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'HISIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de integracion de glosas
1 - Integracion Nativa con VIE Finance 

2 - Integracion con otro ERP
', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'GlossesIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica como es la integraciond e nomina
1 - Integracion Nativa (Con VIE HCM)
2 - Integracion con otro ERP', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'PayrollIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de compañia 
1 - Privada
2 - Publica', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'CompanyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nit de la compañia', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'CompanyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Representante Legal de la compañia', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'RepresentationLegal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad de la compañia', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'City';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direccion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Telephone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si la compañia esta en produccion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'ProductionCompany';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serial Generado para la empresa', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'KeyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si tiene integracion con Lync', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'LyncIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Version de la Aplicacion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del contenedor 
1 - Activo  
0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define el tipo de integracion para la Dispensacion Ambulatoria
0 = No hay integración
1 = Hay integración con Heon
2 = Hay integración entre un Vie EHR y un Vie Farmacy - UDS

Nota: Este parametro esta relacionado con la ruta de servicios que se consume para la consultas de prescripciones segun el modo de integracion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'DispensingIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Integración de Talento Humano
1. Integración con Vie HCM
2. No se integra con  Vie HCM
', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'HumanTalentIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define el tipo de arquitectura en el despliegue
1: On Premise
2: Plataform as service PAAS', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'ArchitectureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nube
1: Nube Publica
2: Nube Dedicada
3: Nube Privada
4: No Aplica', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'CloudType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Si es el grupo de recursos es compartido', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'Multitenant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'especifica el id de la suscripcion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'SuscriptionID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define el numero de sucursales que maneja', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'BranchOffices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla configuración de servicios', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'ServiceConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'0 : No llama evento, 1 : Si Llama evento', @value = N'PublishEvent', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'PublishEvent';


GO
EXECUTE sp_addextendedproperty @name = N'uriFactory para comunicar RabbitMQ', @value = N'PublishEvent', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'UrlQueue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que especifica el caracter separador decimal para el formato numerico-moneda', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'DecimalSeparator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina la arquitectura a utlizar para publicar eventos: 1 => RabbitMQ 2 => Azure Service Bus', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'ArchitectureQueue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el nombre de la cola para utilizar en la publicación de eventos con RabbitMQ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'NameQueue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de los clientes creada principalmente para cargarse en los valores de sesion y ser enviada a el API abra para cargar archivos relacionados a terceros', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'ClientId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enlace de la version en formato zip', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Containers', @level2type = N'COLUMN', @level2name = N'VersionURL';

