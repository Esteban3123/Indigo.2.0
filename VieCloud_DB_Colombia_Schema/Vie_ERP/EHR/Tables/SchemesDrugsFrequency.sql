CREATE TABLE [EHR].[SchemesDrugsFrequency] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesID]      INT             NOT NULL,
    [SchemesDrugsID] INT             NOT NULL,
    [Day]            INT             NOT NULL,
    [Dose1]          NUMERIC (18, 2) NULL,
    [Dose2]          NUMERIC (18, 2) NULL,
    [Dose3]          NUMERIC (18, 2) NULL,
    [Dose4]          NUMERIC (18, 2) NULL,
    [Dose5]          NUMERIC (18, 2) NULL,
    [Hour1]          DATETIME        NULL,
    [Hour2]          DATETIME        NULL,
    [Hour3]          DATETIME        NULL,
    [Hour4]          DATETIME        NULL,
    [Hour5]          DATETIME        NULL,
    CONSTRAINT [PK_SchemesDrugsFrequency] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de administración del medicamento quinta dosis (DATETIME). Momento programado para el quinto horario de toma del fármaco en el esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la hora 5', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de administración del medicamento cuarta dosis (DATETIME). Momento programado para el cuarto horario de toma del fármaco en el esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la hora 4', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de administración del medicamento tercera dosis (DATETIME). Momento programado para el tercer horario de toma del fármaco en el esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la hora 3', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de administración del medicamento segunda dosis (DATETIME). Momento programado para el segundo horario de toma del fármaco en el esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la hora 2', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de administración del medicamento primera dosis (DATETIME). Momento programado para el primer horario de toma del fármaco en el esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la hora 1', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Hour1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis quinta del medicamento (NUMERIC 18,2). Cantidad en unidades de la quinta administración del fármaco en el día de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la dosis 5 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis cuarta del medicamento (NUMERIC 18,2). Cantidad en unidades de la cuarta administración del fármaco en el día de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la dosis 4  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis tercera del medicamento (NUMERIC 18,2). Cantidad en unidades de la tercera administración del fármaco en el día de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la dosis 3 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis segunda del medicamento (NUMERIC 18,2). Cantidad en unidades de la segunda administración del fármaco en el día de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la dosis 2 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis primera del medicamento (NUMERIC 18,2). Cantidad en unidades de la primera administración del fármaco en el día de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo la dosis 1 ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Dose1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día de la frecuencia medicamentosa (INT). Número del día dentro del esquema terapéutico o plan de tratamiento farmacológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo el dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación esquema-medicamento (FK → EHR.SchemesDrugs). Enlaza medicamento específico con su posología en el esquema de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesDrugsID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del esquema X medicamento (EHR.SchemesDrugs)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesDrugsID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesDrugsID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema terapéutico (FK → EHR.Schemes). Referencia al plan de medicación, tratamiento o protocolo farmacológico al que pertenece esta frecuencia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'SchemesID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de frecuencia medicamentosa (INT IDENTITY). Clave primaria y consecutivo de cada registro de dosis y horario del medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración de medicamentos dentro de un esquema farmacológico: registra, por día y por medicamento del esquema, las dosis y los horarios exactos en que debe administrarse cada dosis (hasta 5 tomas diarias). Se usa en la programación de esquemas de medicación para pacientes hospitalizados o en tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesDrugsFrequency';
