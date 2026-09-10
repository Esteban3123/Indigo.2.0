CREATE TABLE [dbo].[INEMPRESU] (
    [INDCODEMP]                       CHAR (3)        NOT NULL,
    [INDNOMEMP]                       CHAR (90)       NOT NULL,
    [INDCODDGH]                       VARCHAR (20)    NOT NULL,
    [INDSERIAL]                       CHAR (20)       NOT NULL,
    [INDNITEMP]                       CHAR (15)       NOT NULL,
    [INDTIPIDE]                       CHAR (1)        NOT NULL,
    [INDNUMIDE]                       CHAR (10)       NOT NULL,
    [INDDIGVER]                       CHAR (1)        NOT NULL,
    [INDDIREMP]                       VARCHAR (150)   NULL,
    [INDTE1EMP]                       VARCHAR (15)    NOT NULL,
    [INDTE2EMP]                       VARCHAR (15)    NOT NULL,
    [DEPMUNCOD]                       CHAR (5)        NOT NULL,
    [LOGOPRINC]                       VARBINARY (MAX) NULL,
    [LOGOSECUN]                       VARBINARY (MAX) NULL,
    [INDVERDGH]                       CHAR (1)        NULL,
    [INDIDERES]                       VARCHAR (15)    NOT NULL,
    [INDNOMRES]                       VARCHAR (250)   NOT NULL,
    [INDDIRRES]                       VARCHAR (150)   NOT NULL,
    [INDCE1RES]                       VARCHAR (15)    NOT NULL,
    [INDCE2RES]                       VARCHAR (15)    NULL,
    [INDCOERES]                       VARCHAR (50)    NOT NULL,
    [INDCOPRES]                       VARCHAR (50)    NULL,
    [INDFIRRES]                       VARBINARY (MAX) NULL,
    [EMPPRODUC]                       BIT             CONSTRAINT [DFX_08D0D4B8] DEFAULT ((0)) NOT NULL,
    [IMPMODNIF]                       BIT             NULL,
    [DGHPRIVADO]                      BIT             NULL,
    [IDPAIS]                          INT             NULL,
    [TOKENMIPRES]                     VARCHAR (40)    NULL,
    [BD_DWH]                          VARCHAR (100)   NULL,
    [URLINDIRA]                       NVARCHAR (150)  NULL,
    [RISINDIRA]                       BIT             NULL,
    [MIPRESTEST]                      BIT             NULL,
    [BDFUNDACIONAL]                   VARCHAR (100)   NULL,
    [ParticularCareGroupId]           INT             NULL,
    [ParticularHealthAdministratorId] INT             NULL,
    [RouteLogoReportRight]            VARCHAR (150)   NULL,
    [RouteLogoReportLeft]             VARCHAR (150)   NULL,
    CONSTRAINT [PK_INempresu] PRIMARY KEY CLUSTERED ([INDCODEMP] ASC),
    CONSTRAINT [FK_INEMPRESU_INEMPRESU] FOREIGN KEY ([INDCODEMP]) REFERENCES [dbo].[INEMPRESU] ([INDCODEMP])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Empresas INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Empresa Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la empresa indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Empresa en DGH para interface del menu Utilidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCODDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serial Generado para la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDSERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nit de la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNITEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Identificacion del Prestador
1: CC
2: Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTIPIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de Identificacion del Prestador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNUMIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de Verificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIGVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direccion Fisica de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIREMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono Principal de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE1EMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono Alterno de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDTE2EMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Departamento y la ciudad donde se ubica fisicamente la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Este es el logo Principal de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOPRINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Este es el logo secundario de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'LOGOSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Version de Dinamica Gerencial - 0 :modo nativo - 1:Fox pro -  2:Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDVERDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de Identificación del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDIDERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Completo del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDNOMRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Direccion del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDDIRRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono Celular 1 del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE1RES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono Celular 2 del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCE2RES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo Electronico Empresarial del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOERES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo Electronico Personal del Responsable Legal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDCOPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firma del respresentante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'INDFIRRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si es la empresa de produccion y se carga por defecto en el login', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'EMPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Implementa modulo Niff  1:si  0:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IMPMODNIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define si DGH es la version Privada ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'DGHPRIVADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que me relaciona el ID pais con la tabla de Paises en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token para conexion a MIPRES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'TOKENMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la Base de datos de informes DWH  - reportes personalizados de HC especializada y otro infomes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BD_DWH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Url base del visor Indira que sera usada en el modulo RIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'URLINDIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina si crystal esta integrado con el RIS de INDIRA.
False -> No esta integrado
True -> Si esta integrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RISINDIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se usa el servicio de prueba de MIPRES; 0:No, 1:Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'MIPRESTEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de datos fundacional de replicacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'BDFUNDACIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de atencion particular por default para la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'ParticularCareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad administradora de salud particular por default para la empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'ParticularHealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del logo de los reportes del EHR - para la seccion derecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportRight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del logo de los reportes del EHR - para la seccion izquierda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INEMPRESU', @level2type = N'COLUMN', @level2name = N'RouteLogoReportLeft';

