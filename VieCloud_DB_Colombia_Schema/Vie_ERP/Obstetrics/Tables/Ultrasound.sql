CREATE TABLE [Obstetrics].[Ultrasound] (
    [Id]                                  INT             IDENTITY (1, 1) NOT NULL,
    [IdPerinatalMaternalAssessmentC]      INT             NOT NULL,
    [UltrasoundDate]                      DATETIME        NOT NULL,
    [WeeksAccordingToUltrasound]          DECIMAL (18, 1) NOT NULL,
    [CurrentDate]                         DATETIME        NOT NULL,
    [WeeksToToday]                        DECIMAL (18, 1) NOT NULL,
    [EstimatedDateOfDeliveryByUltrasound] DATETIME        NOT NULL,
    [Percentile]                          DECIMAL (18, 2) NULL,
    [Weight]                              INT             NULL,
    [Size]                                DECIMAL (18, 2) NULL,
    [AmnioticFluidIndex]                  DECIMAL (18, 2) NULL,
    [MaximumVerticalPocket]               DECIMAL (18, 2) NULL,
    [Observations]                        VARCHAR (100)   NULL,
    CONSTRAINT [PK_Ultrasound] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ultrasound_PerinatalMaternalAssessmentC] FOREIGN KEY ([IdPerinatalMaternalAssessmentC]) REFERENCES [Obstetrics].[PerinatalMaternalAssessmentC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas de la ecografía, hallazgos adicionales, notas de evaluación paraclínica (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bolsillo vertical máximo (BVM) en centímetros, medida de profundidad máxima de líquido amniótico para evaluación de volumen (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'MaximumVerticalPocket';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bolsillo vertical máximo (BVM) en cms', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'MaximumVerticalPocket';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'MaximumVerticalPocket';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de líquido amniótico (ILA) en centímetros, suma de bolsillos en cuatro cuadrantes para evaluación cuantitativa de volumen amniótico (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'AmnioticFluidIndex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Índice de líquido amniótico (ILA) en cms', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'AmnioticFluidIndex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'AmnioticFluidIndex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla fetal en centímetros según mediciones biométricas de ecografía (longitud total, DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso fetal estimado en gramos según biometría ecográfica (INT, cálculo por diámetro biparietal, circunferencia abdominal, longitud fémur)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso (gramos)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Percentil de crecimiento fetal según edad gestacional, clasificación de normalidad del desarrollo intrauterino (DECIMAL 18,2, NULL=no determinado)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Percentile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Percentil', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Percentile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Percentile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha probable de parto (FPP) calculada por ecografía, estimación de edad gestacional para predicción de fecha de nacimiento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'EstimatedDateOfDeliveryByUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha probable de parto por ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'EstimatedDateOfDeliveryByUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'EstimatedDateOfDeliveryByUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de embarazo desde fecha actual hasta hoy, diferencia en semanas entre edad gestacional ecográfica y fecha actual (DECIMAL 18,1)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksToToday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas a hoy', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksToToday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksToToday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha actual del registro, timestamp de cuándo se calcula la edad gestacional comparativa (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'CurrentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha actual', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'CurrentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'CurrentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de gestación según ecografía, edad gestacional fetal en semanas determinada por biometría (DECIMAL 18,1)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas según ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de realización de la ecografía obstétrica, fecha del paraclínico de evaluación perinatal (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'UltrasoundDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'UltrasoundDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'UltrasoundDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena datos de ecografía obstétrica de evaluación perinatal materna, incluye biometría fetal, mediciones de líquido amniótico y estimación de edad gestacional y fecha probable de parto', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena los datos de la ecografía de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos de la ecografía de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de ecografía obstétrica.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la evaluación materna perinatal a la que pertenece esta ecografía; vincula el resultado ecográfico con la valoración clínica de la gestante.', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'IdPerinatalMaternalAssessmentC';
