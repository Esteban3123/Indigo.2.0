CREATE TABLE [dbo].[HCESCAPAC] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [VALTEMPER] CHAR (10)                                                                        NULL,
    [PTSTEMPER] TINYINT                                                                          NULL,
    [VALPREART] CHAR (10)                                                                        NULL,
    [PTSPREART] TINYINT                                                                          NULL,
    [VALFRECAR] CHAR (10)                                                                        NULL,
    [PTSFRECAR] TINYINT                                                                          NULL,
    [VALFRERES] NCHAR (10)                                                                       NULL,
    [PTSFRERES] TINYINT                                                                          NULL,
    [VALFIOXI2] NCHAR (10)                                                                       NULL,
    [VALAADOX2] NCHAR (10)                                                                       NULL,
    [VALPAOXI2] NCHAR (10)                                                                       NULL,
    [PTSPREOXI] TINYINT                                                                          NULL,
    [VALPHARTE] NCHAR (10)                                                                       NULL,
    [PTSPHARTE] TINYINT                                                                          NULL,
    [VALSODPLA] NCHAR (10)                                                                       NULL,
    [PTSSODPLA] NCHAR (10)                                                                       NULL,
    [VALPOTPLA] NCHAR (10)                                                                       NULL,
    [PTSPOTPLA] TINYINT                                                                          NULL,
    [VALCREATI] NCHAR (10)                                                                       NULL,
    [PTSCREATI] TINYINT                                                                          NULL,
    [VALHEMATO] NCHAR (10)                                                                       NULL,
    [PTSHEMATO] TINYINT                                                                          NULL,
    [VALLEUCOC] NCHAR (10)                                                                       NULL,
    [PTSLEUCOC] TINYINT                                                                          NULL,
    [VALEDAPAC] TINYINT                                                                          NULL,
    [PTSEDAPAC] TINYINT                                                                          NULL,
    [VALENFCRO] NCHAR (10)                                                                       NULL,
    [PTSENFCRO] TINYINT                                                                          NULL,
    [VALGLAGLO] NCHAR (10)                                                                       NULL,
    [PTSGLAGLO] TINYINT                                                                          NULL,
    CONSTRAINT [PK_HCESCAPAC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCESCAPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCAPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCAPAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCAPAC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de temperatura corporal en grados (°C), medición vital del paciente durante la escala de Glasgow o evaluación de conciencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALTEMPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALTEMPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALTEMPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del sistema cuando se documentó la escala de capacidad (DATETIME), trazabilidad de la atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero, especialista) que realizó la evaluación; PII enmascarado con Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se evalúa la capacidad del paciente (urgencias, hospitalización, UCI, consulta externa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución o sitio donde se registra el examen o procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso del paciente a la institución; vinculado a ADINGRESO, identifica el evento de admisión o atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente (cédula, documento, identificación); PII enmascarado con Identification_Ofuscado; FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial único (IDENTITY) del registro de escala de capacidad (Glasgow u otra evaluación neurológica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala de capacidad o gravedad clínica del paciente (posiblemente escala APACHE o similar de cuidados intensivos). Registra los valores medidos de signos vitales, parámetros de laboratorio y condición clínica del paciente, junto con el puntaje asignado a cada parámetro para calcular un índice de severidad o riesgo durante un ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de temperatura corporal del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSTEMPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSTEMPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor medido de la presión arterial del paciente (sistólica/diastólica), signo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPREART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPREART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de presión arterial del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPREART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPREART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor medido de la frecuencia cardíaca del paciente (latidos por minuto), pulso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALFRECAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALFRECAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de frecuencia cardíaca del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSFRECAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSFRECAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor medido de la frecuencia respiratoria del paciente (respiraciones por minuto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALFRERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALFRERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de frecuencia respiratoria del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSFRERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSFRERES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la fracción inspirada de oxígeno (FiO2) suministrada al paciente, porcentaje de oxígeno en la mezcla respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALFIOXI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALFIOXI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del gradiente alveolo-arterial de oxígeno (A-aDO2), diferencia de presión de oxígeno entre alvéolos y sangre arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALAADOX2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALAADOX2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la presión arterial de oxígeno (PaO2) del paciente, oximetría arterial en mmHg', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPAOXI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPAOXI2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada a los parámetros de oxigenación (FiO2, gradiente A-aDO2 o PaO2) del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPREOXI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPREOXI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del pH arterial del paciente, medida del equilibrio ácido-base en sangre arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPHARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPHARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de pH arterial del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPHARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPHARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del sodio plasmático del paciente (natremia), electrolito en sangre en mEq/L', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALSODPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALSODPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de sodio plasmático del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSSODPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSSODPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del potasio plasmático del paciente (kalemia), electrolito en sangre en mEq/L', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPOTPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALPOTPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de potasio plasmático del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPOTPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSPOTPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la creatinina sérica del paciente, indicador de función renal en mg/dL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALCREATI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALCREATI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de creatinina del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSCREATI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSCREATI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del hematocrito del paciente, porcentaje de glóbulos rojos en sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de hematocrito del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSHEMATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del recuento de leucocitos (glóbulos blancos) del paciente, células por mm³ o miles/μL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALLEUCOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALLEUCOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de leucocitos del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSLEUCOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSLEUCOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la edad del paciente al momento del registro, utilizada como factor en el cálculo de la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALEDAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALEDAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada a la edad del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSEDAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSEDAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o indicador de enfermedades crónicas del paciente (comorbilidades), factor de severidad adicional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALENFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALENFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada a las enfermedades crónicas del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSENFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSENFCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la escala de Glasgow del paciente, evaluación del nivel de consciencia (puntuación ocular, verbal y motora)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALGLAGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALGLAGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje o puntuación asignada al valor de la escala de Glasgow del paciente según la escala de severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSGLAGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'PTSGLAGLO';
