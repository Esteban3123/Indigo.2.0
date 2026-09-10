CREATE TABLE [dbo].[HCFICRENFOL] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO]    NCHAR (10)                                                                       NOT NULL,
    [FECINGUREN]   DATETIME                                                                         NULL,
    [FECREGISTRO]  DATETIME                                                                         NOT NULL,
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
    CONSTRAINT [PK_HCFICRENFOL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICRENFOL_HCFICRENFOL] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFICRENFOL].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de terapia de reemplazo renal (TRR), razón clínica de inicio de diálisis o trasplante renal. VARCHAR(500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MOTIVOTRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MOTIVOTRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MOTIVOTRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inserción/creación del registro de ficha renal en el sistema. DATETIME, auditoría de ingreso de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHAINSERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me va a guardar la fecha de la inserción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHAINSERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHAINSERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peritonitis infecciosa en paciente con diálisis peritoneal. Valores: 98=No aplica (sin diálisis peritoneal últimos 12 meses). NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PERITOINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peritonitis Infecciosa:  98 = No Aplica (El usuario no ha estado en diálisis periotoneal en ningún momento en los últimos 12 meses)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PERITOINFE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PERITOINFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Otras enfermedades crónicas (11). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITOTRENFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Otras enfermedades crónicas (11) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITOTRENFC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITOTRENFC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Enfermedad pulmonar crónica (10). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFPULM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Enfermedad pulmonar crónica (10) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFPULM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFPULM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Enfermedad inmunológica activa (9). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFINMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Enfermedad inmunológica activa (9) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFINMU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFINMU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Infección por VHC (Hepatitis C) (8). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Infección por el VHC (8) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Infección por VIH (7). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Infección por el VIH (7) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Enfermedad cardiaca, cerebrovascular o vascular periférica (6). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFCARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Enfermedad cardiaca, cerebrovascular o vascular periférica (6) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFCARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFCARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Limitaciones potenciales al autocuidado y adherencia post-trasplante (5). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITPOTILIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Potenciales limitaciones al autocuidado y adherencia al tratamiento post trasplante (5) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITPOTILIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITPOTILIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Esperanza de vida ≤6 meses (4). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITESPVIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Esperanza de vida menor o igual a 6 meses (4) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITESPVIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITESPVIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Paciente no ha manifestado deseo de trasplantarse (3). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITNOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: NO ha manifestado su deseo de trasplantarse (3) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITNOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITNOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Infección crónica o activa no tratada/controlada (2). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Infección crónica o activa no tratada o no controlada (2) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFCRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contraindicación para trasplante renal: Cáncer activo (1). 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITCANACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante: Cancer Activo (1)   1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITCANACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITCANACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vacunación contra hepatitis B. Valores: 98=No aplica, 1=Esquema completo, 2=Esquema incompleto, 3=No vacunado, 99=Sin dato. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'VACUHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna Hepatitis:  98=No Aplica  1= Si, esquema completo  2= Si, esquema incompleto   3= No  99= Sin dato.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'VACUHEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'VACUHEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de hepatitis C. Valores especiales: 1845-01-01=No aplica, 1800-01-01=Desconocida. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico Hepatitos C:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de hepatitis C en paciente renal crónico. 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hepatitis C:  i. 1= Si  ii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de hepatitis B. Valores especiales: 1845-01-01=No aplica, 1800-01-01=Desconocida. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico Hepatitis B:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de hepatitis B en paciente renal crónico. 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hepatitis B:  i. 1= Si  ii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de diabetes mellitus. Valores especiales: 1845-01-01=No aplica, 1800-01-01=Desconocida. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico DM:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de diabetes mellitus en paciente renal. 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'DM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diabetes Mellitus:  1 = Si  2 = No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'DM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'DM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recepción de antagonista de receptor de angiotensina II (ARA II) en plan terapéutico. 98=No aplica, 1=Sí, 2=No (no formulado), 3=No (formulado pero no recibe), 99=Sin dato. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe ARA II:  98 =No Aplica  1. 1=Si  2. 2=No, No fue formulado dentro del plan terapéutico  3. 3=No, Aunque fue formulado dentro del plan terapéutico  4. 99=Sin Dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recepción de inhibidor de enzima convertidora de angiotensina (IECA) en plan terapéutico. 98=No aplica, 1=Sí, 2=No (no formulado), 3=No (formulado pero no recibe), 99=Sin dato. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEIECA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe IECA:  98 =No Aplica  1. 1=Si  2. 2=No, No fue formulado dentro del plan terapéutico  3. 3=No, Aunque fue formulado dentro del plan terapéutico  4. 99=Sin Dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEIECA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEIECA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de hipertensión arterial sistémica. Valores especiales: 1845-01-01=No aplica, 1800-01-01=Desconocida. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnostico HTA:  1845-01-01= No Aplic  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de hipertensión arterial sistémica en paciente renal. 1=Sí, 2=No. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hipertención arterial:  i. 1 = Si  ii. 2 = No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Institución prestadora de salud (IPS) que gestiona lista de espera para trasplante. 98=No aplica. VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSLISESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IPS Lista de espera:  98= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSLISESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSLISESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a lista de espera para trasplante renal. Valores especiales: 1845-01-01=No aplica, 1800-01-01=Desconocida. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGLIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso a lista de espera:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGLIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGLIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación de trasplante renal: 1=No contraindicado, 2=Contraindicado (selección múltiple de causas). TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'INDITRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación de transplante:  i. 1= No Contraindicado  ii. 2= Contraindicado: Las siguientes opciones son de múltiple selección  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'INDITRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'INDITRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Institución prestadora de salud que realizó el trasplante renal al paciente. 98=No aplica, 99=Sin dato. VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSREATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IPS realizó transplante:  98=No Aplica  99= Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSREATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSREATRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recepción de trasplante renal. 1=Sí y funcional, 3=Sí pero no funcional, 5=No. Inhabilitar si TRR es diferente a ''''2''''. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECTRAREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibio transplante renal:  i. 1=Si y está Funcional: Inhabilitar si en “Recibe TRR”  seleccionó una opción diferente a “2”  ii. 3=Si y no está funcional  iii. 5=No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECTRAREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECTRAREN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de filtración glomerular (TFG) estimada al iniciar terapia de reemplazo renal crónica por primera vez (ml/min). 98=No aplica, 99=Sin dato. FLOAT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TFGINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TFG al iniciar la terapia de remplazo renal de forma cronica por primer vez:  98=No Aplica  99= Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TFGINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TFGINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de terapia de reemplazo renal crónica (diálisis o trasplante). Valores especiales: 1845-01-01=No aplica, 1800-01-01=Desconocida. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de terapia de remplazo renal:  1845-01-01 = No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de inicio de primera terapia de reemplazo renal. 1=Urgencia, 2=Programada, 3=Desconocido, 4=Primera TRR no dialítica, 98=No aplica. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MODINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de inicio primera terapia de remplazo renal:  i. 1= Urgencia  ii. 2= Programada  iii. 3= Desconocido  iv. 4= Primera TRR diferente a Diálisis  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MODINITRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MODINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: Paciente recibe terapia de reemplazo renal no dialítica (trasplante, hemofiltración). 98=No aplica. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRTERNODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal: Si, Terapia No Dialítica  98=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRTERNODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRTERNODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: Paciente recibe diálisis peritoneal. 1=Manual, 2=Automatizada, 98=No aplica. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRDIAPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal: Si, Diálisis Peritoneal  1. 1= Manual  2. 2= Automatizada  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRDIAPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRDIAPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador: Paciente recibe hemodiálisis. 1=Fístula arteriovenosa, 2=Catéter venoso central, 98=No aplica. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRHEMODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal: Si, Hemodiálisis  1. 1= Fístula  2. 2= Catéter  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRHEMODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRHEMODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la terapia de reemplazo renal recibida (hemodiálisis, diálisis peritoneal, trasplante, ninguna). VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRRDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal,  se guarda la descripcion correspondiente  la selección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRRDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRRDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de terapia de reemplazo renal recibida. 1=Hemodiálisis, 11=Diálisis peritoneal, 111=Terapia no dialítica, 2=Nunca recibió TRR, 98=No aplica. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibe terapia de remplazo renal,  se guarda el codigo correspondiente  la selección:  98=No Aplica  i. 1= Si, HemodiálisisH   ii. 11= Si, Diálisis Peritoneal   iii. 111= Si, Terapia No Dialítica  iv. 2= No ha recibido por primera vez TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del programa de atención en enfermedad renal crónica (ERC) seleccionado (nefroprotección, prediálisis, ninguno). VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERCDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Programa de atención de ERC, se guarda la descripción correspondiente a la selección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERCDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERCDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de programa de atención ERC. 1=Nefroprotección, 11=Prediálisis, 2=No, 98=No aplica. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Programa de atención de ERC, se guarda el codigo correspondiente  la selección:  98=No Aplica  i. 1= Si, Nefroprotección  ii. 11= Si, Prediálisis  iii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de enfermedad renal crónica (ERC). Valores especiales: 1845-01-01=No aplica, 1800-01-01=Desconocida. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnositco:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio de enfermedad renal crónica (KDIGO). Valores: 1-5 (estadios), 98=No tiene ERC, 99=Desconocido. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estadío:  98= No Tiene ERC  99= Desconocido  1  2  3  4  5  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiología/causa de enfermedad renal crónica. 1=HTA o DM, 2=Autoinmune, 3=Nefropatía obstructiva, 4=Enfermedad poliquística renal, 5=Otras, 98=Sin ERC. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ETILOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Etilogía:  98= No Tiene ERC  i. 1 = HTA o DM  ii. 2 = Autoinmune  iii. 3 = Nefropatía Obstructiva  iv. 4 = Enfermedad Poliquística  v. 5 = otras  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ETILOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ETILOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de enfermedad renal crónica (ERC). 0=No, 1=Sí, 2=Indeterminado entre estadios 1-2 o sin ERC, 3=No estudiado para ERC. TINYINT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad renal cronica:  i. 0 = No.   ii. 1 = Si  iii. 2 = Indeterminado entre estadios 1, 2 o sin ERC  iv. 3 = El usuario no ha sido estudiado para ERC    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación/registro de la ficha renal en sistema. DATETIME, campo auditoría obligatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro De La Ficha Renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso o primer contacto del paciente con unidad funcional de nefrología/riñón. DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGUREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso a unidad Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGUREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGUREN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de historia clínica de ingreso renal. Registra que el paciente inició atención en unidades funcionales de tipo renal. NCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero folio que nos indica que la ficha renal ya se le hizo una Historia de Ingreso, se registra solo en las unidades Funcionales de Tipo Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación única del paciente (cédula, documento de identidad, identificación clínica). PII-Ofuscado. VARCHAR(25), FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Códgio del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de registro de ficha renal. IDENTITY(1,1). INT, clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica de enfermedad renal crónica (ERC) por paciente. Registra el seguimiento nefrológico: estadio de la enfermedad, etiología, terapia de reemplazo renal (diálisis, hemodiálisis, trasplante), comorbilidades asociadas (hipertensión, diabetes, hepatitis) y criterios de inclusión o contraindicaciones para trasplante renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL';
