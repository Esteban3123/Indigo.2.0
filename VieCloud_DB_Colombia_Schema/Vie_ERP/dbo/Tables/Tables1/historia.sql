CREATE TABLE [dbo].[historia] (
    [ID_COMPANY]                VARCHAR (9)     NULL,
    [CENTRO ATENCION]           CHAR (100)      NOT NULL,
    [IDENTIFICACIÓN]            VARCHAR (25)    NOT NULL,
    [TIPO DE IDENTIFICACION]    VARCHAR (2)     NULL,
    [NOMBRE]                    CHAR (250)      NOT NULL,
    [GENERO]                    VARCHAR (9)     NULL,
    [NUM INGRES]                CHAR (10)       NOT NULL,
    [COBERTURA]                 VARCHAR (12)    NOT NULL,
    [FECHA INICIAL DE ESTANCIA] DATETIME        NOT NULL,
    [FECHA FINAL]               DATETIME        NULL,
    [DIAS DE ESTANCIA]          INT             NULL,
    [DESCRIPCION DE LA CAMA]    VARCHAR (65)    NULL,
    [TIPO DE ESTANCIA]          CHAR (40)       NOT NULL,
    [UNIDAD FUNCIONAL]          CHAR (60)       NOT NULL,
    [ENTIDAD]                   VARCHAR (321)   NOT NULL,
    [FECHA BUSQUEDA]            DATE            NULL,
    [AÑO FECHA BUSQUEDA]        INT             NULL,
    [MES AÑO FECHA BUSQUEDA]    INT             NULL,
    [MES NOMBRE FECHA BUSQUEDA] VARCHAR (10)    NULL,
    [DIA FECHA BUSQUEDA]        NVARCHAR (4000) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda [DIA FECHA BUSQUEDA]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'DIA FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda [MES NOMBRE FECHA BUSQUEDA]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'MES NOMBRE FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda [MES AÑO FECHA BUSQUEDA]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'MES AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda [AÑO FECHA BUSQUEDA]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda [FECHA BUSQUEDA]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'UNIDAD FUNCIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el tipo de estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'TIPO DE ESTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la descripción de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'DESCRIPCION DE LA CAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda los días de estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'DIAS DE ESTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda [fecha final]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'FECHA FINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda la fecha inicial de estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'FECHA INICIAL DE ESTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cobertura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'COBERTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'NUM INGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el genero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NOMBRE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'TIPO DE IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda la identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'IDENTIFICACIÓN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'CENTRO ATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el id de la compañia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'historia', @level2type = N'COLUMN', @level2name = N'ID_COMPANY';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de registro de estancias hospitalarias que almacena la historia de ingresos de pacientes por compañía y centro de atención. Consolida datos demográficos del paciente (identificación, nombre, género), información del ingreso (número de ingreso, cobertura, entidad pagadora, cama asignada, unidad funcional y tipo de estancia) y el período de hospitalización con fechas de inicio y fin y días calculados. Incluye columnas derivadas de una fecha de búsqueda descompuesta en año, mes y día, orientadas a facilitar consultas analíticas o de reporting.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'historia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'historia';
GO
