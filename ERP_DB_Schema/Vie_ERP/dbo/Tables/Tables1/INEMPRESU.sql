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
    CONSTRAINT [PK_INempresu] PRIMARY KEY CLUSTERED ([INDCODEMP] ASC),
    CONSTRAINT [FK_INEMPRESU_INEMPRESU] FOREIGN KEY ([INDCODEMP]) REFERENCES [dbo].[INEMPRESU] ([INDCODEMP]),
    CONSTRAINT [FK_INEMPRESU_INMUNICIP] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD])
);


GO
ALTER TABLE [dbo].[INEMPRESU] NOCHECK CONSTRAINT [FK_INEMPRESU_INEMPRESU];


GO
ALTER TABLE [dbo].[INEMPRESU] NOCHECK CONSTRAINT [FK_INEMPRESU_INMUNICIP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial anexo 1
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'InitialDateAnnexOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta del logo de los reportes del EHR - para la seccion derecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportRight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta del logo de los reportes del EHR - para la seccion izquierda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportLeft';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base de datos fundacional de replicacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BDFUNDACIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se usa el servicio de prueba de MIPRES; 0:No, 1:Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'MIPRESTEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si crystal esta integrado con el RIS de INDIRA.  False -> No esta integrado  True -> Si esta integrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RISINDIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Url base del visor Indira que sera usada en el modulo RIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'URLINDIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Base de datos de informes DWH  - reportes personalizados de HC especializada y otro infomes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BD_DWH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Token para conexion a MIPRES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'TOKENMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me relaciona el ID pais con la tabla de Paises en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si DGH es la version Privada ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DGHPRIVADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Implementa modulo Niff  1:si  0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IMPMODNIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es la empresa de produccion y se carga por defecto en el login', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'EMPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Firma del respresentante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDFIRRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo Electronico Personal del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo Electronico Empresarial del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono Celular 2 del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE2RES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono Celular 1 del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE1RES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIRRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Completo del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Identificación del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDIDERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Version de Dinamica Gerencial - 0 :modo nativo - 1:Fox pro -  2:Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDVERDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este es el logo secundario de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este es el logo Principal de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOPRINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Departamento y la ciudad donde se ubica fisicamente la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono Alterno de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE2EMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono Principal de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE1EMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion Fisica de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIREMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Verificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIGVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Identificacion del Prestador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNUMIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificacion del Prestador  1: CC  2: Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTIPIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNITEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Serial Generado para la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDSERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Empresa en DGH para interface del menu Utilidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la empresa indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Empresa Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODEMP';

