CREATE TABLE [dbo].[CHPARAMET] (
    [CODCENATE]                                CHAR (10)    NOT NULL,
    [REQAPRSOL]                                BIT          NOT NULL,
    [PERSOLRES]                                BIT          NOT NULL,
    [HORMINSOL]                                TINYINT      NOT NULL,
    [PERHOSSIN]                                BIT          NOT NULL,
    [NUMDIAMAX]                                TINYINT      NOT NULL,
    [NUMHORMIN]                                TINYINT      NOT NULL,
    [FECPARINI]                                DATETIME     NOT NULL,
    [TIEMTABLE]                                INT          NOT NULL,
    [HORRESALE]                                TINYINT      NULL,
    [EXICOINFC]                                BIT          NULL,
    [INDAUDFOR]                                NUMERIC (18) NOT NULL,
    [ACTINTERF]                                CHAR (1)     NOT NULL,
    [GENHORACORTE]                             TIME (0)     CONSTRAINT [DF__CHPARAMET__GENHO__16A44564] DEFAULT ('23:59:59') NOT NULL,
    [DASHLIMPIEZA]                             BIT          NULL,
    [BreakfastStartTime]                       TIME (0)     NULL,
    [BreakfastEndTime]                         TIME (0)     NULL,
    [LunchStartTime]                           TIME (0)     NULL,
    [LunchEndTime]                             TIME (0)     NULL,
    [DinnerStartTime]                          TIME (0)     NULL,
    [DinnerEndTime]                            TIME (0)     NULL,
    [MorningSupplementStartTime]               TIME (0)     NULL,
    [MorningSupplementEndTime]                 TIME (0)     NULL,
    [AfternoonSupplementStartTime]             TIME (0)     NULL,
    [AfternoonSupplementEndTime]               TIME (0)     NULL,
    [OtherMealStartTime]                       TIME (0)     NULL,
    [OtherMealEndTime]                         TIME (0)     NULL,
    [SaveDischargeWithAllLegalizedMedications] BIT          CONSTRAINT [DF__CHPARAMET__SaveD__45D06EB0] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CHPARAMET] PRIMARY KEY CLUSTERED ([CODCENATE] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para guardar descarga/egreso con todos los medicamentos legalizados. 1=Sí, 0=No. Aplica a recetas y medicamentos autorizados en alta hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'SaveDischargeWithAllLegalizedMedications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guardar Descarga ConTodos LosMedicamentos Legalizados  1 = true    o = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'SaveDischargeWithAllLegalizedMedications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'SaveDischargeWithAllLegalizedMedications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización (TIME) de otras comidas. Define el rango horario para servicios de alimentación adicionales en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Final de otra Comida ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio (TIME) de otras comidas. Define el rango horario para servicios de alimentación adicionales en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial de otra Comida ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización (TIME) del suplemento alimentario de la tarde. Aplica a servicio de refrigerio/complemento vespertino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Final suplemento en la tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio (TIME) del suplemento alimentario de la tarde. Aplica a servicio de refrigerio/complemento vespertino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial de la Comida de Suplemento de la tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización (TIME) del suplemento alimentario de la mañana. Aplica a servicio de refrigerio/complemento matutino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Final  suplemento en la mañana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio (TIME) del suplemento alimentario de la mañana. Aplica a servicio de refrigerio/complemento matutino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial de la Comida de Suplemento de la mañana ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización (TIME) de la cena. Define cierre del servicio de alimentación vespertino/nocturno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Fianl de la  cena', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio (TIME) de la cena. Define apertura del servicio de alimentación vespertino/nocturno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicio cena', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización (TIME) del almuerzo. Define cierre del servicio de alimentación diurno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora final almuerzo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio (TIME) del almuerzo. Define apertura del servicio de alimentación diurno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de Inicio del almuerzo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización (TIME) del desayuno. Define cierre del servicio de alimentación matutino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultima hora del desayuno.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastEndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio (TIME) del desayuno. Define apertura del servicio de alimentación matutino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial del desayuno  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para activar el Dashboard de limpieza y desinfección. 1=Activo, 0=Inactivo. Gestiona seguimiento de procesos de asepsia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DASHLIMPIEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para indicar si se va implementar el DashBoard de limpieza y desinfección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DASHLIMPIEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DASHLIMPIEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de corte (TIME, formato HH:MM:SS, default 23:59:59) para liquidación y cierre de estancias/hospedaje diarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'GENHORACORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aqui va la hora de corte para la liquidación de estancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'GENHORACORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'GENHORACORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (CHAR 1) para activar interfaz de homologación de camas en hospitalización. 0=No, 1=Sí. Valida asignación de camillas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'ACTINTERF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se activa la interfaz de homologacion de camas para validar en hospitalizacion   0: No  1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'ACTINTERF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'ACTINTERF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo reservado (NUMERIC 18) para control y rastreo de auditoría en parametrización del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, OBSOLETO desde 06-01-2024 por PBI 17646 Azure DevOps) que requería consentimiento informado para cirugía. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'EXICOINFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'01-06-2024 Apartir de la fecha, este campo se marca obsoleto por el PBI: 17646 en Azure Devops.       Exigir Consentimiento Informado Cirugia   1:Si;  0:No;    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'EXICOINFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'EXICOINFC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de horas mínimas (TINYINT) para disparo de alertas en reservas de camillas/camas. Aplica a unidad de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORRESALE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Horas Minimas Para Disparar Alertas de las Reservas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORRESALE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORRESALE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo en segundos (INT) para rotación y refresco de información en tableros/dashboards del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'TIEMTABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en segundos para rotacion de la informacion en el Tablero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'TIEMTABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'TIEMTABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de arranque, inicialización o vigencia del sistema en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'FECPARINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Arranque del Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'FECPARINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'FECPARINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de horas mínimas (TINYINT) requeridas para confirmar y validar reservas de camillas/camas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de horas minimo para confirmar Reservas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días máximo (TINYINT) permitido para asignar y mantener reservas de camillas/camas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMDIAMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Dias maximo para asignar Reservas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMDIAMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMDIAMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que permite dejar pacientes en observación sin asignar camilla en urgencias. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERHOSSIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite dejar a pacientes en observacion sin asignar camillas, solo aplica a la Unidad de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERHOSSIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERHOSSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de horas mínimas (TINYINT) requeridas desde ingreso para solicitar dietas. Aplica solo a pacientes en observación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el numero de horas minima para solicitar dietas, aplica solo a pacientes en Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que permite emitir solicitudes de dietas para camas reservadas confirmadas. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite realizar solicitudes de dietas de camas reservadas confirmadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERSOLRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que requiere aprobación previa para emitir solicitudes de dieta. 1=Requiere, 0=No requiere.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'REQAPRSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requiere aprobacion para emitir las solicitudes de dieta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'REQAPRSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'REQAPRSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, clave primaria). Identifica el punto de ingreso del paciente, unidad funcional o entidad prestadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración del módulo de camas y hospitalización por centro de atención. Define reglas operativas como tiempos mínimos, aprobaciones requeridas, horarios de corte y franjas de alimentación para la gestión de pacientes hospitalizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET';
