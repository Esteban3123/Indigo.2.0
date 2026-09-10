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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última fecha de actualización del registro de egresos hospitalarios (DATETIME). Marca cuándo se sincronizó o modificó por última vez el dato en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ULT_ACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la ultima fecha actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ULT_ACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ULT_ACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiqueta de mes para búsqueda y filtrado (NVARCHAR). Formato legible del mes asociado a la fecha de búsqueda (ej: ''''Enero'''', ''''Febrero'''').', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES_LABEL_BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el label busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES_LABEL_BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES_LABEL_BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día numérico de la fecha de búsqueda (INT). Componente día extraído para análisis temporal y filtros por día específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIA FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el dia, fehca de la busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIA FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIA FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del mes de la fecha de búsqueda (VARCHAR). Componente textual del mes para reportes y agrupaciones temporales legibles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES NOMBRE FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el mes, nombre, fecha de la busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES NOMBRE FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES NOMBRE FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes y año de la fecha de búsqueda (INT). Combinación numérica para análisis de tendencias mensuales (ej: 202401).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES AÑO FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el mes, año, fecha de la busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES AÑO FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MES AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de la fecha de búsqueda (INT). Componente anual para filtros y análisis por período anual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'AÑO FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda año fecha busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'AÑO FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'AÑO FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de búsqueda o generación del reporte de egreso (DATE). Referencia temporal del procedimiento de búsqueda o auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha busqueda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA BUSQUEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA BUSQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de registros o eventos asociados (INT). Número de egresos, camas, o movimientos contabilizados en el período.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional de donde egresó el paciente (CHAR). Área, piso o departamento clínico que realizó el alta (UCI, Medicina General, Cirugía, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD DE EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la unidad de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD DE EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cama hospitalaria del egreso (CHAR). Ubicación física donde se registró el alta médica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CAMA DE EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la cama de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CAMA DE EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CAMA DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico clínico al momento del egreso (VARCHAR). Descripción del estado de salud y enfermedad principal al dar alta al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el diagnóstico del egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 del diagnóstico de egreso (CHAR). Clasificación internacional de enfermedades para diagnóstico al alta (ej: E119, I10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el CIE 10 de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico clínico al momento del ingreso (VARCHAR). Motivo principal de la hospitalización y estado de salud inicial del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el diagnóstico del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO DE INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 del diagnóstico de ingreso (CHAR). Clasificación internacional de enfermedades para diagnóstico al ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el CIE 10 de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CIE10 INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad médica del profesional responsable (CHAR). Área clínica: Cardiología, Pediatría, Cirugía, Psiquiatría, Medicina Interna, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del médico, enfermero o profesional de salud responsable (CHAR). Identificación del personal que atendió al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PROFESIONAL DE LA SALUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre del profesional de la salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PROFESIONAL DE LA SALUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PROFESIONAL DE LA SALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición del paciente al momento del egreso (VARCHAR). Vivo, fallecido, referencia, alta voluntaria, otras condiciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESTADO AL EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el estado al egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESTADO AL EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ESTADO AL EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días de hospitalización (INT). Duración de la estancia desde ingreso hasta egreso (cálculo de estadía).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guardan los días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida del paciente del hospital (DATETIME). Momento en que se oficializa la liberación de la cama y cierre de la hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA EGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA EGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que el médico autoriza el alta (DATETIME). Instante de la autorización clínica para que el paciente se retire de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ALTA MEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de alta médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ALTA MEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ALTA MEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso del paciente a hospitalización (DATETIME). Inicio oficial del período de estancia en camas de internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA HOSPITALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de la hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA HOSPITALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA HOSPITALIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de admisión o recepción del paciente (DATETIME). Momento cuando llega a urgencias, consulta o programa de hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ADMISION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la admisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ADMISION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA ADMISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código único de ingreso (CHAR). Identificador de la hospitalización, vinculado a facturación y expediente clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Municipio de residencia del paciente (CHAR). Localidad, ciudad o municipio del domicilio del afiliado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código DANE o administrativo del municipio (CHAR). Identificador numérico estándar para búsquedas y reportes geográficos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO MUNICIPIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el código del municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO MUNICIPIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO MUNICIPIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento o provincia de residencia (CHAR). División territorial de origen del paciente (Cundinamarca, Antioquia, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DEPARTAMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre del departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DEPARTAMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DEPARTAMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código DANE del departamento (CHAR). Identificador numérico de la región administrativa para análisis geográficos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO DEPARTEMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el codigo del departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO DEPARTEMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO DEPARTEMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono celular de contacto (VARCHAR). Celular del paciente para comunicaciones y seguimiento post-egreso (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO MOVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el número movil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO MOVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO MOVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono fijo de contacto (VARCHAR). Línea fija residencial del paciente para notificaciones (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO FIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el número fijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO FIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TELEFONO FIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Domicilio completo del paciente (VARCHAR). Calle, número, barrio y ciudad de residencia (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género biológico del paciente (VARCHAR). M=Masculino, F=Femenino. Necesario para estadísticas y protocolos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el sexo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del paciente en años (INT). Edad al momento de la hospitalización, usada en protocolos y análisis epidemiológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (DATETIME). Referencia para cálculo de edad y análisis demográficos (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA NACIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA NACIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'FECHA NACIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre y apellidos completos del paciente (VARCHAR). Identificación completa legible: nombres y dos apellidos (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'NOMBRE COMPLETO PACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre completo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'NOMBRE COMPLETO PACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'NOMBRE COMPLETO PACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente (VARCHAR). Apellido paterno o materno complementario (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO APELLIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el segundo apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO APELLIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO APELLIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente (VARCHAR). Apellido principal para identificación del afiliado (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER APELLIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER APELLIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER APELLIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente (VARCHAR). Nombre complementario de identificación civil (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el segundo nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'SEGUNDO NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente (VARCHAR). Nombre principal en el documento de identidad (PII sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el primer nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'PRIMER NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del paciente (VARCHAR). Cédula, pasaporte o documento único de identidad (PII – Identification_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación en texto (VARCHAR). Cédula de Ciudadanía, Pasaporte, Tarjeta de Identidad, Cédula Extranjera, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DESCRIPCION IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la descripción de la identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DESCRIPCION IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'DESCRIPCION IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código abreviado del tipo de identificación (VARCHAR). CC, PA, TI, CE, etc. Acrónimos para filtros y codificación RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el acronimo del tipo de identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Régimen de afiliación en salud (VARCHAR). Contributivo, Subsidiado, Especial, Vinculado. Base para facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'RÉGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el regimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'RÉGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'RÉGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del grupo o categoría de atención (VARCHAR). Urgencias, Hospitalización, Consulta Externa, Procedimientos especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'GRUPO DE ATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el grupo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'GRUPO DE ATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'GRUPO DE ATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la aseguradora o EPS del paciente (VARCHAR). Nombre completo del ente gestor de la salud (POS, Plan Complementario).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora (VARCHAR). Identificador único de la EPS/ARS para facturación y validación RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO ENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el código de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO ENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CODIGO ENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o categoría de estancia hospitalaria (VARCHAR). Cuidados generales, UCI, cuidados intermedios, observación. Determina costos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO ESTANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo de instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO ESTANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO ESTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad clínica o funcional de hospitalización (VARCHAR). Área de internación: Medicina Interna, Cirugía, Pediatría, Maternidad, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD FUNCIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD FUNCIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'UNIDAD FUNCIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación o tipo de la unidad de atención (VARCHAR). Piso, Centro de Atención Especializado, Sala de Operaciones, Urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo de unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'TIPO UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro o institución de atención donde se atendió (CHAR). Nombre de la clínica, hospital o IPS que realizó la hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CENTRO ATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CENTRO ATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'CENTRO ATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la empresa o compañía operadora (VARCHAR). Tenant o entidad organizacional propietaria del registro en Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ID_COMPANY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el ID de la compañia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ID_COMPANY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP', @level2type = N'COLUMN', @level2name = N'ID_COMPANY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ingresos y egresos hospitalarios: guarda la información completa de cada hospitalización de un paciente, incluyendo datos demográficos, entidad aseguradora, fechas de admisión y egreso, diagnósticos CIE-10, profesional tratante y estado al momento del alta. Sirve para análisis de ocupación, estancias, reportes RIPS hospitalarios y seguimiento de pacientes hospitalizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GRESOSHOSP';
