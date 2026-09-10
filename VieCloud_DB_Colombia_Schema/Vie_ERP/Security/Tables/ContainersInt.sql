CREATE TABLE [Security].[ContainersInt] (
    [Id]                     INT              NOT NULL,
    [Code]                   VARCHAR (3)      NOT NULL,
    [Name]                   VARCHAR (100)    NOT NULL,
    [FoundationalContainer]  VARCHAR (30)     NOT NULL,
    [TransactionalContainer] VARCHAR (30)     NOT NULL,
    [DocumentalContainer]    VARCHAR (30)     NOT NULL,
    [VituelContainer]        VARCHAR (30)     NOT NULL,
    [HISContainer]           VARCHAR (30)     NOT NULL,
    [InteropCostContainer]   VARCHAR (30)     NOT NULL,
    [HISIntegration]         TINYINT          NOT NULL,
    [GlossesIntegration]     TINYINT          NOT NULL,
    [PayrollIntegration]     TINYINT          NOT NULL,
    [CompanyType]            TINYINT          NOT NULL,
    [CompanyNit]             VARCHAR (15)     NOT NULL,
    [RepresentationLegal]    VARCHAR (100)    NOT NULL,
    [City]                   VARCHAR (50)     NOT NULL,
    [Address]                VARCHAR (50)     NOT NULL,
    [Telephone]              VARCHAR (20)     NOT NULL,
    [ProductionCompany]      BIT              NOT NULL,
    [KeyCode]                VARCHAR (20)     NOT NULL,
    [LyncIntegration]        BIT              NOT NULL,
    [Version]                VARCHAR (20)     NOT NULL,
    [State]                  BIT              NOT NULL,
    [DispensingIntegration]  TINYINT          NOT NULL,
    [HumanTalentIntegration] TINYINT          NOT NULL,
    [VerificationDigitNit]   VARCHAR (3)      NOT NULL,
    [ArchitectureType]       TINYINT          NOT NULL,
    [CloudType]              TINYINT          NOT NULL,
    [Multitenant]            BIT              NOT NULL,
    [SuscriptionID]          UNIQUEIDENTIFIER NOT NULL,
    [BranchOffices]          TINYINT          NOT NULL,
    [ServiceConfigurationId] TINYINT          NOT NULL,
    [PublishEvent]           BIT              NULL,
    [UrlQueue]               VARCHAR (MAX)    NULL,
    [IsSynchronized]         BIT              NULL,
    [FootImage]              VARCHAR (100)    NULL,
    [DecimalSeparator]       VARCHAR (1)      NULL,
    [AzureFileShareRoute]    VARCHAR (500)    NULL,
    [ArchitectureQueue]      TINYINT          NULL,
    [NameQueue]              VARCHAR (30)     NULL,
    [ClientId]               UNIQUEIDENTIFIER NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de empresas o instituciones (clientes) configuradas en la plataforma Indigo Vie Cloud, incluyendo sus datos legales, integraciones activas, tipo de arquitectura y parámetros de infraestructura en la nube.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único de la empresa o institución registrada.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código corto (hasta 3 caracteres) que identifica la empresa dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social de la empresa o institución de salud.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de infraestructura destinado a los módulos fundacionales o base del sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'FoundationalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'FoundationalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de infraestructura para los módulos transaccionales (facturación, atenciones, etc.).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'TransactionalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'TransactionalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de infraestructura para la gestión documental (documentos clínicos, archivos, etc.).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'DocumentalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'DocumentalContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de infraestructura para los módulos de signos vitales u otros servicios virtuales.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'VituelContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'VituelContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de infraestructura del HIS (Hospital Information System / Sistema de Información Hospitalaria).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'HISContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'HISContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contenedor de infraestructura para interoperabilidad y costos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'InteropCostContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'InteropCostContainer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa tiene activa la integración con el HIS (Sistema de Información Hospitalaria). 0=No, valores mayores=Sí/tipo.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'HISIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'HISIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa tiene activa la integración con el módulo de glosas (devoluciones y objeciones de facturación). 0=No, valores mayores=Sí/tipo.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'GlossesIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'GlossesIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa tiene activa la integración con el módulo de nómina o liquidación de personal. 0=No, valores mayores=Sí/tipo.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'PayrollIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'PayrollIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de empresa o institución (por ejemplo: IPS, EPS, clínica, hospital, laboratorio). Código numérico.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'CompanyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'CompanyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT de la empresa o institución (número de identificación tributaria), sin dígito de verificación.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'CompanyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'CompanyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del representante legal de la empresa o institución.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'RepresentationLegal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'RepresentationLegal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad donde está ubicada la sede principal de la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'City';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'City';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física de la sede principal de la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto de la empresa o institución.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Telephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Telephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa opera en ambiente de producción real (true) o es un entorno de pruebas (false).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ProductionCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ProductionCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o llave de activación/licencia asignada a la empresa en la plataforma.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'KeyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'KeyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa tiene activa la integración con Microsoft Lync / Skype for Business para comunicaciones.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'LyncIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'LyncIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del sistema o módulo instalado/configurado para esta empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Version';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Version';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo de la empresa en la plataforma (true=activa, false=inactiva).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa tiene activa la integración con el módulo de dispensación de medicamentos. 0=No, valores mayores=Sí/tipo.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'DispensingIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'DispensingIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa tiene activa la integración con el módulo de talento humano o recursos humanos. 0=No, valores mayores=Sí/tipo.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'HumanTalentIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'HumanTalentIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dígito de verificación del NIT de la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'VerificationDigitNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'VerificationDigitNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de arquitectura tecnológica utilizada por la empresa (por ejemplo: monolítica, microservicios). Código numérico.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ArchitectureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ArchitectureType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nube en la que opera la empresa (por ejemplo: pública, privada, híbrida). Código numérico.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'CloudType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'CloudType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa opera en modalidad multitenant (varios clientes comparten infraestructura) o tenant dedicado.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Multitenant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'Multitenant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (GUID) de la suscripción de la empresa en la plataforma o en el proveedor de nube (Azure).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'SuscriptionID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'SuscriptionID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de sedes o sucursales habilitadas para la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'BranchOffices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'BranchOffices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del perfil o configuración de servicios asignado a la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ServiceConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ServiceConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la empresa tiene habilitada la publicación de eventos hacia colas de mensajería (integración por eventos).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'PublishEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'PublishEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL o cadena de conexión de la cola de mensajería (Azure Service Bus u otro) utilizada por la empresa para integración de eventos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'UrlQueue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'UrlQueue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la configuración de la empresa ha sido sincronizada correctamente con los servicios de la plataforma.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'IsSynchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'IsSynchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta o nombre del archivo de imagen que se usa como pie de página en documentos e informes de la empresa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'FootImage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'FootImage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Carácter separador decimal utilizado en los reportes y documentos de la empresa (por ejemplo: ''''.'''' o '''','''').', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'DecimalSeparator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'DecimalSeparator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del recurso compartido de archivos en Azure File Share asignado a la empresa para almacenamiento de documentos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'AzureFileShareRoute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'AzureFileShareRoute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o versión de arquitectura de cola de mensajería configurada para la empresa. Código numérico.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ArchitectureQueue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ArchitectureQueue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la cola de mensajería asignada a la empresa para la integración de eventos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'NameQueue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'NameQueue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (GUID) del cliente en el sistema de autenticación o gestión de identidades (por ejemplo: Azure AD).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ClientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ContainersInt', @level2type = N'COLUMN', @level2name = N'ClientId';
