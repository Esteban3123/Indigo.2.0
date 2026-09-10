CREATE TABLE [dbo].[HCPARTGRA] (
    [IDPARTGRA]     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECPARTOG]     INT                                                                              NOT NULL,
    [FECREGIST]     DATETIME                                                                         NULL,
    [TENSSARTE]     VARCHAR (10)                                                                     NULL,
    [FRECUCARD]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [SMFRERESP]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [TEMPERATU]     FLOAT (53) MASKED WITH (FUNCTION = 'default()')                                  NULL,
    [AUFRECUEN]     VARCHAR (10) MASKED WITH (FUNCTION = 'partial(0, "MaternalSigns_Ofuscado", 0)')  NULL,
    [AUINTESID]     INT                                                                              NULL,
    [AUDURACIO]     INT                                                                              NULL,
    [BFFETOCAR]     INT                                                                              NULL,
    [BFDESACEL]     INT                                                                              NULL,
    [BFMOVFETA]     INT                                                                              NULL,
    [TVDILATAC]     INT                                                                              NULL,
    [TVBORRAMI]     NUMERIC (18)                                                                     NULL,
    [TVESTACIO]     INT                                                                              NULL,
    [TVMEMBRAN]     INT                                                                              NULL,
    [TVLIQANMI]     INT                                                                              NULL,
    [TVVARPOSI]     INT                                                                              NULL,
    [OBSERVACI]     VARCHAR (500)                                                                    NULL,
    [FECINIREG]     DATETIME                                                                         NULL,
    [PARPELVIS]     CHAR (1)                                                                         NULL,
    [ESTREGPAR]     BIT                                                                              NULL,
    [PARIDAD]       TINYINT                                                                          NULL,
    [POSMATERNA]    TINYINT                                                                          NULL,
    [IDENTIFICADOR] BIT                                                                              NULL,
    CONSTRAINT [PK_HCPARTGRA] PRIMARY KEY CLUSTERED ([IDPARTGRA] ASC),
    CONSTRAINT [FK_HCPARTGRA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARTGRA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPARTGRA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPARTGRA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPARTGRA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPARTGRA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPARTGRA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPARTGRA].[FRECUCARD]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPARTGRA].[SMFRERESP]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPARTGRA].[TEMPERATU]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPARTGRA].[AUFRECUEN]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
CREATE NONCLUSTERED INDEX [IX_HCPARTGRA__IPCODPACI]
    ON [dbo].[HCPARTGRA]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT que marca qué registros del partograma fueron completados. Por paciente se generan 15 registros; este campo identifica cuáles fueron diligenciados (1=completado, 0=no completado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDENTIFICADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En esta tabla se crean por paciente siempre 15 registros, este campo es para identificar que registros se diligenciaron de todos esos 10 registros.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDENTIFICADOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDENTIFICADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición corporal de la gestante durante el parto: 1=Horizontal (acostada), 2=Vertical (sentada, cuclillas). Registro obstétrico de partograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'POSMATERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posición De la Materna  1: Horizontal  2: Vertical  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'POSMATERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'POSMATERNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de la madre según número de partos previos: 1=Nulípara (primer parto), 2=Multípara (partos anteriores). Estado reproductivo materno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paridad de la materna  1: Nulípara  2: Multípara', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit que indica el estado o validación del registro de partograma: 1=activo/válido, 0=inactivo/anulado. Control de integridad del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'ESTREGPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro de partograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'ESTREGPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'ESTREGPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de la pelvis materna para parto vaginal: 1=Adecuada, 2=Dudosa, 3=No adecuada. Hallazgo pelviano crítico para vía del parto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARPELVIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pelvis  1:Adecuada  2:Dudosa  3:No Adecuada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARPELVIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'PARPELVIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del servicio de parto o atención obstétrica (DATETIME). Marca el comienzo del registro de partograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECINIREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se inicia el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECINIREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECINIREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 500) para anotaciones clínicas por hora o evento durante el partograma. Notas de seguimiento materno-fetal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones por Hora o Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del tacto vaginal: variedad o posición de la presentación fetal (anterior, transversa, posterior). Exploración ginecológica intraparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVVARPOSI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tacto Vaginal/Variedad de Posicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVVARPOSI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVVARPOSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hallazgo del tacto vaginal respecto al líquido amniótico: presencia, color, características (claro, teñido). Evaluación de bienestar fetal indirecto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVLIQANMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tacto Vaginal/Liquido Amniotico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVLIQANMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVLIQANMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de las membranas ovulares al tacto vaginal: íntegras, rotas, con prolapso. Registro de integridad corioamniótica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVMEMBRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tacto Vaginal/Membranas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVMEMBRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVMEMBRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida del tacto vaginal: posición del polo fetal respecto a las espinas isquiáticas (-5 a +5 cm). Estadio del descenso fetal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVESTACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tacto Vaginal/Estacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVESTACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVESTACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o grado de borramiento cervical (0-100%) al tacto vaginal. Indica maduración y adelgazamiento del cuello uterino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVBORRAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tacto Vaginal/Borramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVBORRAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVBORRAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dilatación cervical en centímetros (0-10 cm) medida al tacto vaginal. Progreso del trabajo de parto obstétrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVDILATAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tacto Vaginal/Dilatacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVDILATAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TVDILATAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bienestar fetal: registro de movimientos fetales presentes durante la hora/período. Evaluación clínica de vitalidad fetal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFMOVFETA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bienestar Fetal/Movimiento Fetal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFMOVFETA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFMOVFETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bienestar fetal: presencia y tipo de desaceleraciones en frecuencia cardiaca fetal (variable, tardía, prolongada). Indicador de bienestar fetal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFDESACEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bienestar Fetal/Desaceleracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFDESACEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFDESACEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bienestar fetal: frecuencia cardiaca fetal en latidos por minuto (120-160 lpm basal). Signo vital fetal clave en monitoreo intraparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFFETOCAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bienestar Fetal/Fetocardia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFFETOCAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'BFFETOCAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad uterina: duración en segundos de cada contracción uterina durante el período. Intensidad temporal de dinámica uterina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUDURACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad Uterina/Duracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUDURACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUDURACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad uterina: intensidad de la contracción (débil, moderada, fuerte) o en mmHg. Fuerza de contracciones en trabajo de parto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUINTESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad Uterina/Intensidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUINTESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUINTESID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad uterina: frecuencia de contracciones por 10 minutos (VARCHAR 10). Medida de regularidad del trabajo de parto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUFRECUEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad Uterina/Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUFRECUEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'AUFRECUEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos maternos: temperatura corporal en °C (FLOAT). Vital materno, señal de infección o complicaciones obstétricas. PII Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TEMPERATU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos Maternos/Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TEMPERATU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TEMPERATU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos maternos: frecuencia respiratoria en respiraciones por minuto. Vital materno, monitoreo de estabilidad respiratoria. Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'SMFRERESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos Maternos/Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'SMFRERESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'SMFRERESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos maternos: frecuencia cardiaca materna en latidos por minuto. Vital materno, indicador de estabilidad hemodinámica. Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FRECUCARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos Maternos/Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FRECUCARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FRECUCARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos maternos: tensión/presión arterial sistólica/diastólica (ej: 120/80). Vital materno crítico en monitoreo intraparto. Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TENSSARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos Maternos/Tesion Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TENSSARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'TENSSARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro del partograma (DATETIME). Timestamp de cuándo se documentó la observación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hora o hora secuencial del partograma (INT, 1-15 típicamente). Identificador temporal del período observado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECPARTOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECPARTOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'FECPARTOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermera) que realiza/valida el registro. FK a INPROFSAL. Responsable clínico. PII Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (sala de parto, labor y parto, obstétrica). FK a INUNIFUNC. Área donde se atiende el parto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (hospital, clínica, IPS). FK a ADCENATEN. Institución prestadora del servicio obstétrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso hospitalario del paciente-gestante. FK a ADINGRESO. Enlaza con el episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente gestante (cédula, documento, carné). FK a INPACIENT. PII Ofuscado. Buscar: cédula, paciente, madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY INT) del registro de partograma. Clave primaria secuencial de la tabla HCPARTGRA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDPARTGRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDPARTGRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA', @level2type = N'COLUMN', @level2name = N'IDPARTGRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del partograma obstétrico: seguimiento clínico del trabajo de parto de una paciente durante su ingreso, incluyendo signos vitales maternos, actividad uterina, frecuencia cardíaca fetal, dilatación cervical y estado de membranas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARTGRA';
