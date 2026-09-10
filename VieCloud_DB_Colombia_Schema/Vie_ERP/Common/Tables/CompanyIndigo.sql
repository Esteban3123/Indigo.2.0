CREATE TABLE [Common].[CompanyIndigo] (
    [Id]                  TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [VerificationNumber]  CHAR (1)      NULL,
    [CompanyCode]         CHAR (2)      NOT NULL,
    [CompanyName]         VARCHAR (90)  NOT NULL,
    [City]                VARCHAR (50)  NULL,
    [RepresentationLegal] VARCHAR (50)  NULL,
    [Telephone]           VARCHAR (100) NULL,
    [Address]             VARCHAR (100) NULL,
    [KeyCode]             CHAR (20)     NOT NULL,
    [CompanyNit]          CHAR (15)     NOT NULL,
    [IdentificationType]  CHAR (1)      NULL,
    [Identification]      CHAR (10)     NULL,
    [ProductionCompany]   BIT           NOT NULL,
    [State]               BIT           NOT NULL,
    [Synchronized]        CHAR (1)      NOT NULL,
    [LyncIntegration]     BIT           NULL,
    CONSTRAINT [PK_CompanyIndigo__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_CompanyIndigo__State]
    ON [Common].[CompanyIndigo]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Integración Lync (BIT). Indica si la empresa tiene habilitada la integración con Microsoft Lync para comunicaciones unificadas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'LyncIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Integracion Lync', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'LyncIntegration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'LyncIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de sincronización (CHAR 1). 1=Sincronizado, 2=No sincronizado. Refleja si los datos de la empresa están actualizados en la base de datos central.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/eliminado (BIT). Bandera lógica para marcar empresa como eliminada o inactiva en sincronización de base de datos. No refleja eliminación física.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ambiente (BIT). TRUE=Empresa en producción (datos reales), FALSE=Ambiente de pruebas/testing. Distingue entre datos operativos y de validación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'ProductionCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si la empresa es de produccion = True, o ambiente de pruebas = False', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'ProductionCompany';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'ProductionCompany';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del prestador (CHAR 10). Documento del representante legal o responsable de la empresa prestadora de salud. PII - Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Identificacion del Prestador', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del prestador (CHAR 1). 1=Cédula de ciudadanía (CC), 2=NIT. Especifica el documento del responsable legal.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Identificacion del Prestador  1: CC  2: Nit', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT de la empresa (CHAR 15). Número de identificación tributaria (NIT) único de la institución prestadora de servicios de salud. PII - Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit de la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código serial de la empresa (CHAR 20). Clave única generada para identificar y autenticar la empresa en el sistema Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'KeyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serial Generado para la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'KeyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'KeyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección (VARCHAR 100). Domicilio o ubicación física de la sede principal de la empresa prestadora.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono (VARCHAR 100). Número de contacto telefónico de la empresa o representante legal. Puede contener múltiples números.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Telephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Telephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Telephone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Representación legal (VARCHAR 50). Nombre del representante legal o apoderado autorizado de la empresa prestadora.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'RepresentationLegal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representación Legal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'RepresentationLegal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'RepresentationLegal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad (VARCHAR 50). Municipio o ciudad donde está ubicada la empresa prestadora de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'City';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'City';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'City';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la empresa Indigo (VARCHAR 90). Razón social o nombre comercial de la institución prestadora de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la empresa indigo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la empresa Indigo (CHAR 2). Identificador corto alfanumérico único para la empresa dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Empresa Indigo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de verificación (CHAR 1). Dígito de control o verificación asociado al código de la empresa para validación de integridad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'VerificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Verificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'VerificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'VerificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autonumérico (TINYINT, 1-255). Clave primaria única de la tabla CompanyIndigo. Identificador interno del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de las empresas o instituciones configuradas en el sistema Indigo Vie Cloud. Contiene los datos de identificación, ubicación y configuración de cada compañía (IPS, clínica u organización) que opera en la plataforma.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo';
