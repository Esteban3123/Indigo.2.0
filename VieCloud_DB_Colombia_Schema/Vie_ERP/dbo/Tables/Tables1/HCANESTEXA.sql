CREATE TABLE [dbo].[HCANESTEXA] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]     CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]     CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [EXAECG]        VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ResultECG_Ofuscado", 0)')     NULL,
    [EXAHEMATO]     VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "ResultHemato_Ofuscado", 0)')   NULL,
    [EXABUN]        VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXACREATINI]   VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAGLICEMI]    VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAALBUMI]     VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAHEMOG]      VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAPROTROM]    VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXATROMBO]     VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXARXTORAX]    VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')        NULL,
    [EXAOTROS]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')        NULL,
    [RIEMALLAM]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [RIEINTUBACION] BIT                                                                              NULL,
    [RIEASA]        INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [RIEOTROS]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [FECREGSIS]     DATETIME                                                                         CONSTRAINT [DF_HCANESTEXA_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [VIAAREA]       VARCHAR (400)                                                                    NULL,
    CONSTRAINT [PK_HCANESTEXA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCANESTEXA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANESTEXA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANESTEXA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANESTEXA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANESTEXA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXAECG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXAHEMATO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXABUN]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXACREATINI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXAGLICEMI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXAALBUMI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXAHEMOG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXAPROTROM]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXATROMBO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXARXTORAX]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[EXAOTROS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[RIEMALLAM]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[RIEASA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTEXA].[RIEOTROS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía aérea, descripción de permeabilidad y manejo de la vía respiratoria durante anestesia (VARCHAR 400)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'VIAAREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vía area', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'VIAAREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'VIAAREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación/registro del evento anestésico en el sistema (DATETIME, auto-poblado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros riesgos anestésicos adicionales no clasificados en ASA, Mallampati o intubación (VARCHAR MAX, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Riesgos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Riesgo anestésico según clasificación ASA (1-5: Sano, comorbilidad leve, comorbilidad severa, peligro vida, moribundo) (INT, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Riesgo Anestesico - Clasificación ASA: 1, 2, 3, 4, 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Riesgo anestésico de intubación difícil, indicador binario sí/no durante procedimiento (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEINTUBACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Riesgos anestesicos - Intubación difícil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEINTUBACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEINTUBACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Riesgo anestésico según escala Mallampati (1-5: visualización epiglotis completa a no visible) para predicción de vía aérea difícil (INT, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEMALLAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Riesgos anestesicos - clasificación Mallampati: 1, 2, 3, 4, 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEMALLAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'RIEMALLAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros exámenes complementarios realizados pre-anestésicos no especificados en campos anteriores (VARCHAR MAX, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'11.0 Otros Exámenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAOTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radiografía de tórax, examen radiológico pulmonar pre-anestésico (VARCHAR MAX, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXARXTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10.0 Rx de Torax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXARXTORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXARXTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo parcial de tromboplastina (TTP), prueba de coagulación en laboratorio pre-anestésico (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXATROMBO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'9.0 Tiempo parcial de Tromboplastina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXATROMBO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXATROMBO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de protrombina (TP), prueba de función hepática y coagulación en laboratorio pre-anestésico (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAPROTROM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'8.0 Tiempo de Protrombina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAPROTROM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAPROTROM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemoglobina, concentración de pigmento respiratorio en sangre para evaluar anemia pre-quirúrgica (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAHEMOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'7.0 Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAHEMOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAHEMOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Albúmina sérica, proteína plasmática indicador de estado nutricional y función hepática pre-anestésico (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAALBUMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'6.0 Albumina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAALBUMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAALBUMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Glicemia, nivel de glucosa en sangre pre-anestésico (riesgo hiperglicemia/hipoglicemia) (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAGLICEMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'5.0 Glicemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAGLICEMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAGLICEMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creatinina sérica, marcador de función renal pre-anestésico para dosificación de medicamentos (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXACREATINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'4.0 Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXACREATINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXACREATINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BUN (nitrógeno ureico en sangre), evaluación de función renal pre-anestésica (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXABUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'3.0 BUN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXABUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXABUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hematocrito, porcentaje de glóbulos rojos en sangre para evaluar anemia pre-quirúrgica (VARCHAR 15, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Examen Hematocrito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAHEMATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Electrocardiograma (ECG), registro de actividad cardíaca pre-anestésico (VARCHAR MAX, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAECG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Examen ECG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAECG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAECG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (anestesiólogo/a) que atiende el procedimiento, referencia a INPROFSAL (VARCHAR 25, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (quirófano, sala de procedimiento, UCI) donde se realiza la anestesia, referencia a INUNIFUNC (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución donde se realiza el procedimiento anestésico, referencia a ADCENATEN (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario/atención asociado al procedimiento anestésico, referencia a ADINGRESO (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula/identificación/documento de identidad), referencia a INPACIENT (VARCHAR 25, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio/secuencia del registro anestésico dentro de la historia clínica (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica/módulo anestesiología (CHAR 9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incrementado de la tabla HCANESTEXA (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AUTONUMERICO DE LA TABLA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exámenes preoperatorios y valoración de riesgo anestésico registrados en la historia clínica. Contiene los resultados de laboratorio (hematología, creatinina, glicemia, entre otros), electrocardiograma, rayos X de tórax y la clasificación de riesgo anestésico (escala ASA, riesgo de mallampati e intubación difícil) asociados a un paciente y su ingreso, previo a una intervención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA';
