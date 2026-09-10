CREATE TABLE [dbo].[HCNUTRICIONC] (
    [ID]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NOT NULL,
    [FECHREGIS]            DATE                                                                             NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [PESOPACIENTE]         INT                                                                              NOT NULL,
    [REQUECALORICO]        INT                                                                              NOT NULL,
    [APORTELIQUIDOS]       INT                                                                              NOT NULL,
    [TIEMPOADMINISTRACION] INT                                                                              NOT NULL,
    [VOLUMENNPT]           INT                                                                              NOT NULL,
    [VELOCIDADINFUSION]    INT                                                                              NOT NULL,
    [ORDEREGIST]           INT                                                                              NOT NULL,
    [RESULAGUA]            VARCHAR (4)                                                                      NULL,
    [RESULOSMOLARIDAD]     VARCHAR (4)                                                                      NULL,
    [RESULNITROGENO]       VARCHAR (4)                                                                      NULL,
    [RESULPROTEICAS]       VARCHAR (4)                                                                      NULL,
    [RESULCALCIO]          VARCHAR (4)                                                                      NULL,
    [RESULVOLUMEN]         VARCHAR (4)                                                                      NULL,
    CONSTRAINT [PK_HCNUTRICIONC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTRICIONC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCNUTRICIONC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCNUTRICIONC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCNUTRICIONC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNUTRICIONC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNUTRICIONC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado analítico de volumen (ml) en nutrición parenteral total; tipo VARCHAR(4), almacena valor cuantitativo del volumen administrado en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULVOLUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el resultado Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULVOLUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULVOLUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado analítico de calcio (mg/dl o mmol/l) en nutrición parenteral; tipo VARCHAR(4), parámetro de control metabólico en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULCALCIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el resulatdo Calcio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULCALCIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULCALCIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado analítico de proteínas (g/dl o g/kg) en nutrición parenteral; tipo VARCHAR(4), medida de aporte proteico en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULPROTEICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resultado  Proteicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULPROTEICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULPROTEICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado analítico de nitrógeno (g/día) en nutrición parenteral; tipo VARCHAR(4), parámetro de balance nitrogenado en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULNITROGENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resultado Nitrogeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULNITROGENO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULNITROGENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado analítico de osmolaridad (mOsm/l) en nutrición parenteral; tipo VARCHAR(4), mide concentración osmótica de la solución NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULOSMOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resultado Osmolaridad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULOSMOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULOSMOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado analítico de agua (ml o ml/kg) en nutrición parenteral; tipo VARCHAR(4), parámetro de hidratación en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULAGUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resultado Agua', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULAGUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'RESULAGUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Velocidad de infusión de nutrición parenteral en ml/hora; tipo INT, controla flujo de administración de NPT al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VELOCIDADINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Velocidad de Infusion (EN ml/H)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VELOCIDADINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VELOCIDADINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total de nutrición parenteral total (NPT) en mililitros (ml); tipo INT, cantidad diaria de solución preparada para infusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VOLUMENNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen de la NPT (EN ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VOLUMENNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'VOLUMENNPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de administración de nutrición parenteral en horas; tipo INT, duración del ciclo de infusión de NPT al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'TIEMPOADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de administración (Horas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'TIEMPOADMINISTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'TIEMPOADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte de líquidos en nutrición parenteral en ml/kg de peso corporal; tipo INT, volumen relativo de hidratación en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'APORTELIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aporte de liquidos en la NPT (ml/Kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'APORTELIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'APORTELIQUIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requerimiento calórico en kilocalorías por kilogramo (kcal/kg); tipo INT, necesidad energética diaria del paciente en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'REQUECALORICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Requerimiento Calorico (KCAL/Kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'REQUECALORICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'REQUECALORICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente en kilogramos (kg); tipo INT, parámetro antropométrico base para cálculos de nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'PESOPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'PESOPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'PESOPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud nutricionista o médico prescriptor; tipo VARCHAR(25) con máscara PII, identificación ofuscada del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional de nutrición/dietética donde se registra la NPT; tipo CHAR(10), referencia a centro administrativo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención hospitalario; tipo CHAR(10), referencia a institución donde se administra nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro del evento de nutrición parenteral; tipo DATE, marca temporal de documentación de NPT en historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión hospitalaria del paciente; tipo CHAR(10), referencia FK a episodio de atención en ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento); tipo VARCHAR(25) con máscara PII, identificador único ofuscado del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la tabla; tipo INT IDENTITY(1,1), clave primaria del registro de nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de valoración nutricional parenteral de pacientes hospitalizados. Incluye datos del paciente, parámetros nutricionales como peso, requerimiento calórico, aporte de líquidos, volumen y velocidad de infusión de NPT, y resultados de análisis de componentes como agua, osmolaridad, nitrógeno, proteínas, calcio y volumen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden o secuencia del registro nutricional, permite identificar el turno u orden en que fue ingresado el dato dentro del ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'ORDEREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIONC', @level2type = N'COLUMN', @level2name = N'ORDEREGIST';
