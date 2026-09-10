CREATE TABLE [dbo].[HCRENFICH] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMFICHAR]    VARCHAR (15)                                                                     NOT NULL,
    [ESTFICHAR]    INT                                                                              NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [FECREGISTRO]  DATETIME                                                                         NOT NULL,
    [FECINGUREN]   DATETIME                                                                         NULL,
    [ERC]          TINYINT                                                                          NULL,
    [ETILOGIA]     TINYINT                                                                          NULL,
    [ESTADIO]      TINYINT                                                                          NULL,
    [FECDIAGNO]    DATETIME                                                                         NULL,
    [PROGATERC]    TINYINT                                                                          NULL,
    [PROGATERCDES] VARCHAR (50)                                                                     NULL,
    [RECIBETRR]    TINYINT                                                                          NULL,
    [RECIBETRRDES] VARCHAR (50)                                                                     NULL,
    [TRRHEMODIA]   TINYINT                                                                          NULL,
    [TRRDIAPERI]   TINYINT                                                                          NULL,
    [TRRTERNODIA]  TINYINT                                                                          NULL,
    [MODINITRR]    TINYINT                                                                          NULL,
    [FECHINITRR]   DATETIME                                                                         NULL,
    [TFGINITRR]    FLOAT (53)                                                                       NULL,
    [RECTRAREN]    TINYINT                                                                          NULL,
    [IPSREATRA]    VARCHAR (50)                                                                     NULL,
    [INDITRANS]    TINYINT                                                                          NULL,
    [FECINGLIES]   DATETIME                                                                         NULL,
    [IPSLISESPE]   VARCHAR (50)                                                                     NULL,
    [HTA]          TINYINT                                                                          NULL,
    [FECDIAGHTA]   DATETIME                                                                         NULL,
    [RECIBEIECA]   TINYINT                                                                          NULL,
    [RECIBEARA]    TINYINT                                                                          NULL,
    [DM]           TINYINT                                                                          NULL,
    [FECDIAGDM]    DATETIME                                                                         NULL,
    [HEPATIB]      TINYINT                                                                          NULL,
    [FECDIAGHB]    DATETIME                                                                         NULL,
    [HEPATIC]      TINYINT                                                                          NULL,
    [FECDIAGHC]    DATETIME                                                                         NULL,
    [VACUHEPA]     TINYINT                                                                          NULL,
    [ITCANACT]     TINYINT                                                                          NULL,
    [ITINFCRO]     TINYINT                                                                          NULL,
    [ITNOTRANS]    TINYINT                                                                          NULL,
    [ITESPVIDA]    TINYINT                                                                          NULL,
    [ITPOTILIM]    TINYINT                                                                          NULL,
    [ITENFCARD]    TINYINT                                                                          NULL,
    [ITINFEVIH]    TINYINT                                                                          NULL,
    [ITINFEVHC]    TINYINT                                                                          NULL,
    [ITENFINMU]    TINYINT                                                                          NULL,
    [ITENFPULM]    TINYINT                                                                          NULL,
    [ITOTRENFC]    TINYINT                                                                          NULL,
    [PERITOINFE]   NUMERIC (18)                                                                     NULL,
    [FECHAINSERC]  DATETIME                                                                         NULL,
    [MOTIVOTRR]    VARCHAR (500)                                                                    NULL,
    [NUMEFOLIO]    NCHAR (10)                                                                       NULL,
    CONSTRAINT [PK_HCRENFICH] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRENFICH_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRENFICH_HCRENFICH] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCRENFICH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCRENFICH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRENFICH].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10) que vincula la ficha renal a Historia de Ingreso completada; se registra exclusivamente en unidades funcionales de tipo Renal para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero folio que nos indica que la ficha renal ya se le hizo una Historia de Ingreso, se registra solo en las unidades Funcionales de Tipo Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo clínico o administrativo (VARCHAR 500) que justifica la indicación de terapia de remplazo renal; razón de inicio TRR, diálisis, hemodiálisis o trasplante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'MOTIVOTRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'MOTIVOTRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'MOTIVOTRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de inserción del registro en la base de datos; marca de auditoría de creación del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECHAINSERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me va a guardar la fecha de la inserción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECHAINSERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECHAINSERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peritonitis infecciosa (NUMERIC 18); 98=No aplica (paciente sin diálisis peritoneal en últimos 12 meses), 1=Sí, 2=No; complicación infecciosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PERITOINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peritonitis Infecciosa:  98 = No Aplica (El usuario no ha estado en diálisis periotoneal en ningún momento en los últimos 12 meses)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PERITOINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PERITOINFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Otras enfermedades crónicas (TINYINT); 1=Sí, 2=No; factor de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITOTRENFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Otras enfermedades crónicas (11) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITOTRENFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITOTRENFC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Enfermedad pulmonar crónica (TINYINT); 1=Sí, 2=No; patología respiratoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFPULM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Enfermedad pulmonar crónica (10) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFPULM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFPULM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Enfermedad inmunológica activa (TINYINT); 1=Sí, 2=No; inmunosupresión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFINMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Enfermedad inmunológica activa (9) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFINMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFINMU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Infección por VHC, Hepatitis C (TINYINT); 1=Sí, 2=No; serostatus.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFEVHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Infección por el VHC (8) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFEVHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFEVHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Infección por VIH (TINYINT); 1=Sí, 2=No; serostatus.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFEVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Infección por el VIH (7) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFEVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFEVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Enfermedad cardiaca, cerebrovascular o vascular periférica (TINYINT); 1=Sí, 2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFCARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Enfermedad cardiaca, cerebrovascular o vascular periférica (6) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFCARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITENFCARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Potenciales limitaciones al autocuidado y adherencia post-trasplante (TINYINT); 1=Sí, 2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITPOTILIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Potenciales limitaciones al autocuidado y adherencia al tratamiento post trasplante (5) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITPOTILIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITPOTILIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Esperanza de vida ≤6 meses (TINYINT); 1=Sí, 2=No; pronóstico vital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITESPVIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Esperanza de vida menor o igual a 6 meses (4) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITESPVIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITESPVIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Paciente NO ha expresado deseo de trasplantarse (TINYINT); 1=Sí, 2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITNOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: NO ha manifestado su deseo de trasplantarse (3) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITNOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITNOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Infección crónica o activa no tratada/controlada (TINYINT); 1=Sí, 2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Infección crónica o activa no tratada o no controlada (2) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITINFCRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Cáncer activo (TINYINT, prioridad 1); 1=Sí, 2=No; oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITCANACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Cancer Activo (1)   1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITCANACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ITCANACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vacunación contra Hepatitis B (TINYINT); 98=No aplica, 1=Esquema completo, 2=Esquema incompleto, 3=No vacunado, 99=Sin dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'VACUHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna Hepatitis:  98=No Aplica  1= Si, esquema completo  2= Si, esquema incompleto   3= No  99= Sin dato.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'VACUHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'VACUHEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de Hepatitis C (DATETIME); 1845-01-01=No aplica, 1800-01-01=Desconocida; marca temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico Hepatitos C:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de Hepatitis C (TINYINT); 1=Sí, 2=No; serostatus VHC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HEPATIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hepatitis C:  i. 1= Si  ii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HEPATIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HEPATIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de Hepatitis B (DATETIME); 1845-01-01=No aplica, 1800-01-01=Desconocida; marca temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico Hepatitis B:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de Hepatitis B (TINYINT); 1=Sí, 2=No; serostatus VHB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HEPATIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hepatitis B:  i. 1= Si  ii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HEPATIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HEPATIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de Diabetes Mellitus (DATETIME); 1845-01-01=No aplica, 1800-01-01=Desconocida; comorbilidad ERC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico DM:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diabetes Mellitus (TINYINT); 1=Sí, 2=No; factor etiológico frecuente de ERC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'DM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diabetes Mellitus:  1 = Si  2 = No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'DM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'DM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recibe antagonista de receptor de angiotensina II en plan terapéutico (TINYINT); 98=No aplica, 1=Sí, 2=No formulado, 3=No adherencia, 99=Sin dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBEARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe ARA II:  98 =No Aplica  1. 1=Si  2. 2=No, No fue formulado dentro del plan terapéutico  3. 3=No, Aunque fue formulado dentro del plan terapéutico  4. 99=Sin Dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBEARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBEARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recibe inhibidor de enzima convertidora de angiotensina en plan terapéutico (TINYINT); 98=No aplica, 1=Sí, 2=No formulado, 3=No adherencia, 99=Sin dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBEIECA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe IECA:  98 =No Aplica  1. 1=Si  2. 2=No, No fue formulado dentro del plan terapéutico  3. 3=No, Aunque fue formulado dentro del plan terapéutico  4. 99=Sin Dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBEIECA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBEIECA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de Hipertensión arterial (DATETIME); 1845-01-01=No aplica, 1800-01-01=Desconocida; comorbilidad ERC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico HTA:  1845-01-01= No Aplic  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGHTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hipertensión arterial (TINYINT); 1=Sí, 2=No; factor etiológico frecuente de ERC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hipertención arterial:  i. 1 = Si  ii. 2 = No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'HTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de IPS en lista de espera para trasplante renal (VARCHAR 50); 98=No aplica; referencia a prestador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPSLISESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IPS Lista de espera:  98= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPSLISESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPSLISESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a lista de espera para trasplante renal (DATETIME); 1845-01-01=No aplica, 1800-01-01=Desconocida; hito trasplante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECINGLIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso a lista de espera:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECINGLIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECINGLIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de trasplante renal (TINYINT); 1=No contraindicado, 2=Contraindicado; soporta múltiples contraindicaciones simultáneas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'INDITRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante:  i. 1= No Contraindicado  ii. 2= Contraindicado: Las siguientes opciones son de múltiple selección  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'INDITRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'INDITRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de IPS que realizó trasplante renal al paciente (VARCHAR 50); 98=No aplica, 99=Sin dato; prestador quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPSREATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IPS realizó transplante:  98=No Aplica  99= Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPSREATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPSREATRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recepción de trasplante renal (TINYINT); 1=Sí funcional, 3=Sí no funcional, 5=No; estado actual del injerto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECTRAREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibio transplante renal:  i. 1=Si y está Funcional: Inhabilitar si en “Recibe TRR”  seleccionó una opción diferente a “2”  ii. 3=Si y no está funcional  iii. 5=No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECTRAREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECTRAREN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de filtración glomerular al iniciar terapia remplazo renal crónica por primera vez (FLOAT, mL/min/1.73m²); 98=No aplica, 99=Sin dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TFGINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TFG al iniciar la terapia de remplazo renal de forma cronica por primer vez:  98=No Aplica  99= Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TFGINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TFGINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de terapia remplazo renal crónica (DATETIME); 1845-01-01=No aplica, 1800-01-01=Desconocida; hito TRR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECHINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de terapia de remplazo renal:  1845-01-01 = No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECHINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECHINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de inicio primera terapia remplazo renal (TINYINT); 1=Urgencia, 2=Programada, 3=Desconocido, 4=No diálisis, 98=No aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'MODINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de inicio primera terapia de remplazo renal:  i. 1= Urgencia  ii. 2= Programada  iii. 3= Desconocido  iv. 4= Primera TRR diferente a Diálisis  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'MODINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'MODINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recibe terapia remplazo renal no dialítica (TINYINT); 1=Sí, 98=No aplica; modalidad TRR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRTERNODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal: Si, Terapia No Dialítica  98=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRTERNODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRTERNODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recibe diálisis peritoneal (TINYINT); 1=Manual, 2=Automatizada, 98=No aplica; modalidad TRR peritoneal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRDIAPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal: Si, Diálisis Peritoneal  1. 1= Manual  2. 2= Automatizada  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRDIAPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRDIAPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recibe hemodiálisis (TINYINT); 1=Vía fístula, 2=Vía catéter, 98=No aplica; acceso vascular TRR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRHEMODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal: Si, Hemodiálisis  1. 1= Fístula  2. 2= Catéter  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRHEMODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'TRRHEMODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la modalidad de terapia remplazo renal recibida (VARCHAR 50); corresponde a selección codificada RECIBETRR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBETRRDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal,  se guarda la descripcion correspondiente  la selección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBETRRDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBETRRDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de terapia remplazo renal recibida (TINYINT); 1=Hemodiálisis, 1=Diálisis peritoneal, 1=Otra TRR, 2=No ha recibido primera TRR, 98=No aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBETRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal,  se guarda el codigo correspondiente  la selección:  98=No Aplica  i. 1= Si, HemodiálisisH   ii. 1= Si, Diálisis Peritoneal   iii. 1= Si, Terapia   iv. 2= No ha recibido por primera vez TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBETRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'RECIBETRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del programa de atención para enfermedad renal crónica (VARCHAR 50); corresponde a selección codificada PROGATERC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PROGATERCDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Programa de atención de ERC, se guarda la descripción correspondiente a la selección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PROGATERCDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PROGATERCDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Programa de atención para ERC (TINYINT); 1=Nefroprotección, 1=Prediálisis, 2=No, 98=No aplica; estrategia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PROGATERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Programa de atención de ERC, se guarda el codigo correspondiente  la selección:  98=No Aplica  i. 1= Si, Nefroprotección  ii. 1= Si, Prediálisis  iii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PROGATERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'PROGATERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de enfermedad renal crónica (DATETIME); 1845-01-01=No aplica, 1800-01-01=Desconocida; hito ERC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnositco:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadío de enfermedad renal crónica KDIGO (TINYINT); 1-5=KDIGO G1-G5, 98=Sin ERC, 99=Desconocido; clasificación funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estadío:  98= No Tiene ERC  99= Desconocido  1  2  3  4  5  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiología, causa de enfermedad renal crónica (TINYINT); 1=HTA/DM, 2=Autoinmune, 3=Obstructiva, 4=Poliquística, 5=Otras, 98=Sin ERC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ETILOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Etilogía:  98= No Tiene ERC  i. 1 = HTA o DM  ii. 2 = Autoinmune  iii. 3 = Nefropatía Obstructiva  iv. 4 = Enfermedad Poliquística  v. 5 = otras  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ETILOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ETILOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enfermedad renal crónica, diagnóstico (TINYINT); 0=No, 1=Sí, 2=Indeterminado E1-E2, 3=No estudiado; estado diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad renal cronica:  i. 0 = No.   ii. 1 = Si  iii. 2 = Indeterminado entre estadios 1, 2 o sin ERC  iv. 3 = El usuario no ha sido estudiado para ERC    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a unidad funcional renal (DATETIME NULL); marca temporal de atención en servicio nefrología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECINGUREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso a unidad Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECINGUREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECINGUREN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de la ficha renal en el sistema (DATETIME); marca de creación del documento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro De La Ficha Renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional asignada (CHAR 10, FK a INUNIFUNC); referencia a servicio, área o centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención de salud (CHAR 10, FK a ADCENATEN); institución, hospital o clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la ficha renal (INT); 1=Activo, 2=Inactivo; validez y vigencia del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ESTFICHAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la ficha renal.    1=Activo  2=Inactivo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ESTFICHAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ESTFICHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ficha renal (VARCHAR 15); identificador de documento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'NUMFICHAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ficha Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'NUMFICHAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'NUMFICHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, MASKED, PII, FK a INPACIENT); identificación única, cédula, documento de identidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Códgio del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, clave primaria autoincremental (INT IDENTITY); consecutivo técnico de tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de seguimiento nefrológico del paciente con Enfermedad Renal Crónica (ERC). Registra el historial clínico renal de cada paciente: estadio de la enfermedad, etiología, terapias de reemplazo renal (hemodiálisis, diálisis peritoneal, trasplante), comorbilidades asociadas (HTA, diabetes, hepatitis) y criterios de inclusión en lista de espera para trasplante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRENFICH';
