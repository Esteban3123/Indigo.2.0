CREATE TABLE [dbo].[HCSESHEMSV] (
    [ID]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCSESHEMID]    INT             NOT NULL,
    [TIPO]          TINYINT         NOT NULL,
    [TA]            VARCHAR (7)     NOT NULL,
    [FC]            INT             NOT NULL,
    [PA]            NUMERIC (18, 1) NOT NULL,
    [SO2]           CHAR (10)       NOT NULL,
    [PV]            NUMERIC (18, 2) NOT NULL,
    [PTM]           NUMERIC (18, 2) NOT NULL,
    [FLUBOM]        NUMERIC (18, 2) NOT NULL,
    [CONDUCTIVIDAD] NUMERIC (18, 2) NOT NULL,
    [FECREGISTRO]   DATETIME        NOT NULL,
    [USUREGISTRO]   CHAR (20)       NOT NULL,
    [OBSERVACION]   VARCHAR (3000)  NULL,
    [FR]            NUMERIC (18)    NULL,
    [TEMP]          NUMERIC (18)    NULL,
    CONSTRAINT [PK_HCSESHEMSV] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSESHEMSV_HCSESHEM] FOREIGN KEY ([HCSESHEMID]) REFERENCES [dbo].[HCSESHEM] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura corporal del paciente en grados Celsius (°C); signo vital monitorizado durante hemodiálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia respiratoria; número de respiraciones por minuto (rpm) durante sesión de hemodiálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas y observaciones adicionales del profesional de salud durante la sesión; texto libre hasta 3000 caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/profesional de salud (enfermero, técnico) que registró los signos vitales; Char(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro de signos vitales; DateTime, auditoría de cuándo se capturó el dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Feha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conductividad del dializado; unidad µs/cm (microsiemens por centímetro), rango 1-40; parámetro de calidad del líquido de diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'CONDUCTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conductividad  Unidad de medida µs/cm  de 1-40  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'CONDUCTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'CONDUCTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flujo de bomba de sangre; unidad ml/min (mililitros por minuto), rango 1-999; velocidad de circulación extracorpórea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FLUBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Flujo de Bomba   Unidad de medida ml/min   de 1-999  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FLUBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FLUBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión transmembrana; unidad mmHg, rango 1-999; gradiente de presión en membrana filtrante durante hemodiálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PTM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presión Transmembrana  Unidad de Medida = mmHg  de 1-999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PTM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PTM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión venosa; unidad mmHg, rango 0-28; presión en línea venosa del acceso vascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presión Venosa  Unidad de medida = mmHg  0-28', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saturación de oxígeno en sangre (SpO2); porcentaje (%) monitorizado en tiempo real durante sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'SO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saturación de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'SO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'SO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión arterial sistólica/diastólica (mmHg); signo vital crítico durante hemodiálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia cardiaca; número de latidos por minuto (lpm); signo vital monitorizado continuamente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tensión arterial sistólica y diastólica; formato VARCHAR(7) ej. ''''120/80''''; medida de presión sanguínea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tensión Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro vital: 1=Al iniciar sesión, 2=Monitoreo periódico, 3=Al finalizar sesión; clasificación temporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Registro: 1-> Al iniciar, 2-> Monitoreo, 3-> Al Finalizar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la bolsa de sangre/sesión de hemodiálisis padre; clave foránea a tabla HCSESHEM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'HCSESHEMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de bolsa de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'HCSESHEMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'HCSESHEMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (Identity INT); clave primaria de la tabla de signos vitales de hemodiálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de signos vitales y parámetros hemodinámicos durante sesiones de hemodiálisis o procedimientos de terapia de reemplazo renal. Cada fila corresponde a una medición tomada en un momento específico dentro de una sesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV';
