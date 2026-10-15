CREATE TABLE [dbo].[GRESOSHOSP] (
    [ID_COMPANY]                 VARCHAR (9)     NULL,
    [CENTRO ATENCION]            CHAR (100)      NOT NULL,
    [TIPO UNIDAD]                VARCHAR (35)    NULL,
    [UNIDAD FUNCIONAL]           VARCHAR (60)    NULL,
    [TIPO ESTANCIA]              VARCHAR (18)    NOT NULL,
    [CODIGO ENTIDAD]             VARCHAR (20)    NOT NULL,
    [ENTIDAD]                    VARCHAR (300)   NULL,
    [GRUPO DE ATENCION]          VARCHAR (100)   NULL,
    [RÉGIMEN]                    VARCHAR (28)    NOT NULL,
    [TIPO IDENTIFICACION]        VARCHAR (2)     NULL,
    [DESCRIPCION IDENTIFICACION] VARCHAR (31)    NULL,
    [IDENTIFICACION]             VARCHAR (25)    NOT NULL,
    [PRIMER NOMBRE]              VARCHAR (20)    NULL,
    [SEGUNDO NOMBRE]             VARCHAR (20)    NULL,
    [PRIMER APELLIDO]            VARCHAR (20)    NULL,
    [SEGUNDO APELLIDO]           VARCHAR (20)    NULL,
    [NOMBRE COMPLETO PACIENTE]   VARCHAR (250)   NULL,
    [FECHA NACIMIENTO]           DATETIME        NOT NULL,
    [EDAD]                       INT             NULL,
    [SEXO]                       VARCHAR (1)     NOT NULL,
    [DIRECCION]                  VARCHAR (MAX)   NOT NULL,
    [TELEFONO FIJO]              VARCHAR (MAX)   NOT NULL,
    [TELEFONO MOVIL]             VARCHAR (MAX)   NOT NULL,
    [CODIGO DEPARTEMENTO]        CHAR (2)        NULL,
    [DEPARTAMENTO]               CHAR (40)       NULL,
    [CODIGO MUNICIPIO]           CHAR (5)        NULL,
    [MUNICIPIO]                  CHAR (40)       NULL,
    [INGRESO]                    CHAR (10)       NOT NULL,
    [FECHA ADMISION]             DATETIME        NULL,
    [FECHA HOSPITALIZACION]      DATETIME        NOT NULL,
    [FECHA ALTA MEDICA]          DATETIME        NOT NULL,
    [FECHA EGRESO]               DATETIME        NULL,
    [DIAS]                       INT             NULL,
    [ESTADO AL EGRESO]           VARCHAR (23)    NULL,
    [PROFESIONAL DE LA SALUD]    CHAR (60)       NULL,
    [ESPECIALIDAD]               CHAR (60)       NULL,
    [CIE10 INGRESO]              CHAR (4)        NULL,
    [DIAGNOSTICO DE INGRESO]     VARCHAR (350)   NULL,
    [CIE10 EGRESO]               CHAR (4)        NULL,
    [DIAGNOSTICO DE EGRESO]      VARCHAR (350)   NULL,
    [CAMA DE EGRESO]             CHAR (10)       NULL,
    [UNIDAD DE EGRESO]           CHAR (60)       NULL,
    [CANTIDAD]                   INT             NOT NULL,
    [FECHA BUSQUEDA]             DATE            NULL,
    [AÑO FECHA BUSQUEDA]         INT             NULL,
    [MES AÑO FECHA BUSQUEDA]     INT             NULL,
    [MES NOMBRE FECHA BUSQUEDA]  VARCHAR (10)    NULL,
    [DIA FECHA BUSQUEDA]         INT             NULL,
    [MES_LABEL_BUSQUEDA]         NVARCHAR (4000) NOT NULL,
    [ULT_ACTUAL]                 DATETIME        NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la ultima fecha actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ULT_ACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el label busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES_LABEL_BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el dia, fehca de la busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIA FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el mes, nombre, fecha de la busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES NOMBRE FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el mes, año, fecha de la busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda año fecha busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidad de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cama de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CAMA DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el diagnóstico del egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el CIE 10 de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el diagnóstico del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el CIE 10 de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre del profesional de la salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PROFESIONAL DE LA SALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el estado al egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESTADO AL EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guardan los días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de alta médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ALTA MEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de la hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA HOSPITALIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la admisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ADMISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código del municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO MUNICIPIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre del departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DEPARTAMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el codigo del departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO DEPARTEMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número movil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO MOVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número fijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO FIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el sexo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA NACIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre completo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'NOMBRE COMPLETO PACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el segundo apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO APELLIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER APELLIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el segundo nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el primer nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la descripción de la identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DESCRIPCION IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el acronimo del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el regimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'RÉGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'GRUPO DE ATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el código de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO ESTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD FUNCIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CENTRO ATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ID de la compañia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ID_COMPANY';

