CREATE TABLE [dbo].[INEMPRESU] (
    [INDCODEMP]            CHAR (3)        NOT NULL,
    [INDNOMEMP]            CHAR (90)       NOT NULL,
    [INDCODDGH]            VARCHAR (20)    NOT NULL,
    [INDSERIAL]            CHAR (20)       NOT NULL,
    [INDNITEMP]            CHAR (15)       NOT NULL,
    [INDTIPIDE]            CHAR (1)        NOT NULL,
    [INDNUMIDE]            CHAR (10)       NOT NULL,
    [INDDIGVER]            CHAR (1)        NOT NULL,
    [INDDIREMP]            VARCHAR (150)   NULL,
    [INDTE1EMP]            VARCHAR (15)    NOT NULL,
    [INDTE2EMP]            VARCHAR (15)    NOT NULL,
    [DEPMUNCOD]            CHAR (5)        NOT NULL,
    [LOGOPRINC]            VARBINARY (MAX) NULL,
    [LOGOSECUN]            VARBINARY (MAX) NULL,
    [INDVERDGH]            CHAR (1)        NULL,
    [INDIDERES]            VARCHAR (15)    NOT NULL,
    [INDNOMRES]            VARCHAR (250)   NOT NULL,
    [INDDIRRES]            VARCHAR (150)   NOT NULL,
    [INDCE1RES]            VARCHAR (15)    NOT NULL,
    [INDCE2RES]            VARCHAR (15)    NULL,
    [INDCOERES]            VARCHAR (50)    NOT NULL,
    [INDCOPRES]            VARCHAR (50)    NULL,
    [INDFIRRES]            VARBINARY (MAX) NULL,
    [EMPPRODUC]            BIT             CONSTRAINT [DFX_08D0D4B8] DEFAULT ((0)) NOT NULL,
    [IMPMODNIF]            BIT             NULL,
    [DGHPRIVADO]           BIT             NULL,
    [IDPAIS]               INT             NULL,
    [TOKENMIPRES]          VARCHAR (40)    NULL,
    [BD_DWH]               VARCHAR (100)   NULL,
    [URLINDIRA]            NVARCHAR (150)  NULL,
    [RISINDIRA]            BIT             NULL,
    [MIPRESTEST]           BIT             NULL,
    [BDFUNDACIONAL]        VARCHAR (100)   NULL,
    [RouteLogoReportLeft]  VARCHAR (MAX)   NULL,
    [RouteLogoReportRight] VARCHAR (MAX)   NULL,
    [InitialDateAnnexOne]  DATE            NULL,
    [ActivateSnorlax]      BIT             NULL,
    [IHCE_Activo]          BIT             NULL,
    [IHCE_UrlServidor]     VARCHAR (500)   NULL,
    [IHCE_GrantType]       VARCHAR (500)   NULL,
    [IHCE_ClientId]        VARCHAR (500)   NULL,
    [IHCE_ClientSecret]    VARCHAR (500)   NULL,
    [IHCE_Scope]           VARCHAR (500)   NULL,
    [IHCE_SubsKey]         VARCHAR (500)   NULL,
    [IHCE_TenantId]        VARCHAR (500)   NULL,
    [IHCE_BaseUrl]         VARCHAR (500)   NULL,
    CONSTRAINT [PK_INempresu] PRIMARY KEY CLUSTERED ([INDCODEMP] ASC),
    CONSTRAINT [FK_INEMPRESU_INEMPRESU] FOREIGN KEY ([INDCODEMP]) REFERENCES [dbo].[INEMPRESU] ([INDCODEMP]),
    CONSTRAINT [FK_INEMPRESU_INMUNICIP] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD])
);






GO



GO





GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del Anexo 1; DATE; marca el inicio de vigencia de clausulado normativo en contrato de prestación de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'InitialDateAnnexOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial anexo 1
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'InitialDateAnnexOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'InitialDateAnnexOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del logo para reportes EHR - sección derecha; VARCHAR(MAX); ruta a archivo de imagen corporativo en servidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportRight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta del logo de los reportes del EHR - para la seccion derecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportRight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportRight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del logo para reportes EHR - sección izquierda; VARCHAR(MAX); ruta a archivo de imagen corporativo en servidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportLeft';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta del logo de los reportes del EHR - para la seccion izquierda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportLeft';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportLeft';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de datos fundacional de replicación; VARCHAR(100); nombre de BD para sincronización de datos maestros y cambios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BDFUNDACIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base de datos fundacional de replicacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BDFUNDACIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BDFUNDACIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo prueba MIPRES; BIT; 0=desactivado, 1=servicio test de prescripciones MIPRES habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'MIPRESTEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se usa el servicio de prueba de MIPRES; 0:No, 1:Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'MIPRESTEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'MIPRESTEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Integración Crystal con RIS INDIRA; BIT; 0=no integrado, 1=visor de imágenes médicas INDIRA activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RISINDIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si crystal esta integrado con el RIS de INDIRA.  False -> No esta integrado  True -> Si esta integrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RISINDIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RISINDIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL base visor INDIRA; VARCHAR(150); endpoint del módulo RIS para consulta de imágenes radiológicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'URLINDIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Url base del visor Indira que sera usada en el modulo RIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'URLINDIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'URLINDIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de datos DWH reportes; VARCHAR(100); BD de Data Warehouse para reportes personalizados de historia clínica especializada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BD_DWH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Base de datos de informes DWH  - reportes personalizados de HC especializada y otro infomes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BD_DWH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BD_DWH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token autenticación MIPRES; VARCHAR(40); credencial PII Identification_Ofuscado para conexión a servicio de prescripciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'TOKENMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Token para conexion a MIPRES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'TOKENMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'TOKENMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID país; INT; FK relacional con tabla de países VIE para ubicación geográfica del prestador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me relaciona el ID pais con la tabla de Paises en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión DGH privada; BIT; 0=versión pública, 1=Dinámica Gerencial modo privado/institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DGHPRIVADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si DGH es la version Privada ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DGHPRIVADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DGHPRIVADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Módulo NIFF implementado; BIT; 0=desactivado, 1=módulo de validación de actos de identificación habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IMPMODNIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Implementa modulo Niff  1:si  0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IMPMODNIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IMPMODNIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empresa producción; BIT; 0=ambiente test, 1=ambiente productivo cargado por defecto en login', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'EMPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es la empresa de produccion y se carga por defecto en el login', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'EMPPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'EMPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firma representante legal; VARBINARY(MAX); documento escaneado/imagen de firma PII Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDFIRRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Firma del respresentante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDFIRRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDFIRRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo personal responsable legal; VARCHAR(50); email personal del representante o responsable de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo Electronico Personal del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo empresarial responsable legal; VARCHAR(50); email corporativo del responsable legal/administrativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo Electronico Empresarial del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Celular 2 responsable legal; VARCHAR(15); teléfono celular alterno del representante legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE2RES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono Celular 2 del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE2RES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE2RES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Celular 1 responsable legal; VARCHAR(15); teléfono celular principal del representante legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE1RES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono Celular 1 del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE1RES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE1RES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección responsable legal; VARCHAR(150); domicilio registrado del representante o responsable de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIRRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIRRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIRRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre responsable legal; VARCHAR(250); nombre completo del representante legal o apoderado de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Completo del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula/Identificación responsable legal; VARCHAR(15); número de documento de identidad PII Identification_Ofuscado del representante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDIDERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Identificación del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDIDERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDIDERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión Dinámica Gerencial; CHAR(1); 0=nativo, 1=Fox Pro, 2=.Net; arquitectura de módulo gerencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDVERDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Version de Dinamica Gerencial - 0 :modo nativo - 1:Fox pro -  2:Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDVERDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDVERDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Logo secundario empresa; VARBINARY(MAX); imagen corporativa secundaria para reportes y documentos oficiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este es el logo secundario de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Logo principal empresa; VARBINARY(MAX); imagen corporativa principal para encabezados de reportes y documentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOPRINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este es el logo Principal de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOPRINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOPRINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código depto-municipio; CHAR(5); FK INMUNICIP; ubicación física del centro de atención o sede principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Departamento y la ciudad donde se ubica fisicamente la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono alterno empresa; VARCHAR(15); línea telefónica secundaria de contacto de la institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE2EMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono Alterno de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE2EMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE2EMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono principal empresa; VARCHAR(15); línea telefónica principal de contacto de la institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE1EMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono Principal de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE1EMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE1EMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección empresa; VARCHAR(150); domicilio físico registrado de la sede principal o centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIREMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion Fisica de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIREMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIREMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número verificación; CHAR(1); dígito de chequeo para validación de identificación del prestador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIGVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Verificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIGVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIGVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número identificación prestador; CHAR(10); documento PII Identification_Ofuscado del responsable administrativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNUMIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Identificacion del Prestador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNUMIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNUMIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo identificación prestador; CHAR(1); 1=Cédula Ciudadanía, 2=NIT; documento de identidad de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTIPIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Identificacion del Prestador  1: CC  2: Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTIPIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTIPIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT empresa; CHAR(15); número de identificación tributaria; código único de la institución de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNITEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNITEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNITEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serial empresa; CHAR(20); identificador único generado para interfaz DGH e integración de sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDSERIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serial Generado para la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDSERIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDSERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código empresa DGH; VARCHAR(20); identificador en Dinámica Gerencial para menú Utilidades e interfacing', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Empresa en DGH para interface del menu Utilidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre empresa Indigo; CHAR(90); denominación comercial o razón social del prestador de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la empresa indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código empresa Indigo; CHAR(3); PK; identificador único de la institución/prestador en ERP Indigo Vie Cloud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Empresa Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL servidor IHCE; VARCHAR(500); endpoint base para transmisión y consulta de RDA según MINSALUD 1888-2025', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_UrlServidor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección base del servidor IHCE para transmisión/consulta de RDA. Endpoint objetivo según MSPS/1888-2025.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_UrlServidor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_UrlServidor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grant Type IHCE; VARCHAR(500); identificador Service Bus para transporte de eventos y mensajes en interoperabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_GrantType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta o identificador del Service Bus destinado al transporte de eventos y mensajes para interoperabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_GrantType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_GrantType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Secret cliente IHCE QA; VARCHAR(500); token autenticación PII Identification_Ofuscado para ambiente testing IHCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_ClientSecret';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Token de autenticación para ambiente QA/Testing IHCE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_ClientSecret';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_ClientSecret';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cliente IHCE Prod; VARCHAR(500); token autenticación PII Identification_Ofuscado para ambiente productivo IHCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_ClientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Token de autenticación para ambiente productivo IHCE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_ClientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_ClientId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IHCE habilitado; BIT; 0=interoperabilidad IHCE desactivada, 1=transmisión de RDA y datos clínicos habilitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_Activo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la interoperabilidad IHCE está habilitada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_Activo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_Activo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información maestra de la empresa o institución prestadora de salud registrada en el sistema: razón social, NIT, identificación del representante legal, datos de contacto, logotipos, configuración de entornos de integración y parámetros de conexión a servicios externos (MIPRES, Historia Clínica Electrónica, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador que activa o desactiva un módulo o funcionalidad especial del sistema identificada internamente como ''''Snorlax''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'ActivateSnorlax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'ActivateSnorlax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alcance o permisos (scope) requeridos para la autenticación OAuth con el servicio de Historia Clínica Electrónica (HCE) interoperable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de suscripción (subscription key) para consumir la API de Historia Clínica Electrónica interoperable; credencial de acceso al gateway del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_SubsKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_SubsKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del inquilino (tenant) en la plataforma de Historia Clínica Electrónica interoperable; identifica la organización dentro del servicio de nube.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL base del servicio de Historia Clínica Electrónica interoperable; dirección raíz desde la que se construyen todas las llamadas a la API HCE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_BaseUrl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IHCE_BaseUrl';
