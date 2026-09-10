CREATE TABLE [dbo].[HCONCOFICH] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMFICHA]      CHAR (4)                                                                         NOT NULL,
    [ESTFICHA]      BIT                                                                              NOT NULL,
    [NUMANTRA]      TINYINT                                                                          NULL,
    [TIPVIVIENDA]   TINYINT                                                                          NULL,
    [REGBENIGNO]    VARCHAR (200)                                                                    NULL,
    [REGMALIGNO]    VARCHAR (200)                                                                    NULL,
    [TIPOTUMOR]     TINYINT                                                                          NULL,
    [FECHINGRES]    DATETIME                                                                         NULL,
    [FECHPRIDIAG]   DATETIME                                                                         NULL,
    [CODENTIDA]     CHAR (9)                                                                         NULL,
    [CODIGOIPS]     CHAR (100)                                                                       NULL,
    [CODHABILI]     CHAR (15)                                                                        NULL,
    [DEPMUNCOD]     CHAR (5)                                                                         NULL,
    [IDTOPOGRAF]    INT                                                                              NULL,
    [CODTOPOGRAF]   VARCHAR (5)                                                                      NULL,
    [IDMORFOLO]     INT                                                                              NULL,
    [CODMORFOL]     VARCHAR (10)                                                                     NULL,
    [HISTOLOGICO]   VARCHAR (200)                                                                    NULL,
    [DIFERENCIACI]  TINYINT                                                                          NULL,
    [CLINICO]       BIT                                                                              NULL,
    [EXPC]          BIT                                                                              NULL,
    [PESP]          BIT                                                                              NULL,
    [CITOL]         BIT                                                                              NULL,
    [HMTT]          BIT                                                                              NULL,
    [HIRIO]         BIT                                                                              NULL,
    [DES]           BIT                                                                              NULL,
    [PATOLOGIA]     VARCHAR (100)                                                                    NULL,
    [T]             TINYINT                                                                          NULL,
    [N]             TINYINT                                                                          NULL,
    [M]             TINYINT                                                                          NULL,
    [ESTCLINICO1]   VARCHAR (5)                                                                      NULL,
    [ESTCLINICO2]   VARCHAR (1)                                                                      NULL,
    [METASTASIS1]   VARCHAR (100)                                                                    NULL,
    [METASTASIS2]   VARCHAR (100)                                                                    NULL,
    [METASTASIS3]   VARCHAR (100)                                                                    NULL,
    [CARCINOMA]     BIT                                                                              NULL,
    [VIH]           BIT                                                                              NULL,
    [EMBARAZO]      BIT                                                                              NULL,
    [LATERALIDAD]   TINYINT                                                                          NULL,
    [SINTRATAMIE]   BIT                                                                              NULL,
    [FECHATRAMITE]  DATETIME                                                                         NULL,
    [CIRUGIA]       BIT                                                                              NULL,
    [RADIOTERAP]    BIT                                                                              NULL,
    [QUMIOTERAP]    BIT                                                                              NULL,
    [PUVATERAPIA]   BIT                                                                              NULL,
    [YODOTERAPIA]   BIT                                                                              NULL,
    [INMUNOTERA]    BIT                                                                              NULL,
    [HORMONOTERA]   BIT                                                                              NULL,
    [TBIOLOGICA]    BIT                                                                              NULL,
    [PALIATIVO]     BIT                                                                              NULL,
    [SINDATO]       BIT                                                                              NULL,
    [OTRO]          BIT                                                                              NULL,
    [OTRODESCRI]    VARCHAR (200)                                                                    NULL,
    [FECHINIPRITRA] DATETIME                                                                         NULL,
    [FECHFINPRITRA] DATETIME                                                                         NULL,
    [RECURRENCIA]   BIT                                                                              NULL,
    [FECHARECURR]   DATETIME                                                                         NULL,
    [UBICARECURRE]  TINYINT                                                                          NULL,
    [FECHULTCMED]   DATETIME                                                                         NULL,
    [ESTVITAL]      TINYINT                                                                          NULL,
    [FECHMUERTE]    DATETIME                                                                         NULL,
    [SITIODEFUNCI]  TINYINT                                                                          NULL,
    [NUMCERDEF]     VARCHAR (40)                                                                     NULL,
    [ESTREGISTRO]   BIT                                                                              NULL,
    [FECHDILIGEN]   DATETIME                                                                         NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    CONSTRAINT [PK_HCONCOFICH] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCONCOFICH_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCONCOFICH_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_HCONCOFICH_HCONCOMORFO] FOREIGN KEY ([IDMORFOLO]) REFERENCES [dbo].[HCONCOMORFO] ([ID]),
    CONSTRAINT [FK_HCONCOFICH_HCONCOTOPO] FOREIGN KEY ([IDTOPOGRAF]) REFERENCES [dbo].[HCONCOTOPO] ([ID]),
    CONSTRAINT [FK_HCONCOFICH_INMUNICIP] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_HCONCOFICH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCONCOFICH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCONCOFICH].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCONCOFICH_1]
    ON [dbo].[HCONCOFICH]([NUMFICHA] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCONCOFICH]
    ON [dbo].[HCONCOFICH]([IPCODPACI] ASC, [NUMFICHA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución prestadora de servicios de salud (IPS) donde se registra la ficha oncológica. FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional, departamento o servicio de oncología responsable del registro. FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacioncon Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diligenciamiento, momento en que se completa y registra la ficha oncológica en el sistema (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHDILIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diligenciamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHDILIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHDILIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro oncológico: 0=Pendiente, 1=Confirmado. Indicador de validación administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  0->Pendiente  1->Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de certificado de defunción, documento oficial emitido por autoridades sanitarias al fallecimiento (VARCHAR 40, datos sensibles).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de certificado de defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio de defunción del paciente: 1=Institución propia, 2=Otra institución, 3=Casa, 4=Otro lugar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SITIODEFUNCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio de Defunción:  1-> Institucion Propia  2-> Otra Institucion  3-> Casa  4-> Otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SITIODEFUNCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SITIODEFUNCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de muerte del paciente, fecha de fallecimiento registrada (DATETIME). Dato crítico para seguimiento vital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Muerte Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado vital del paciente: 1=Vivo, 2=Muerto. Indicador de desenlace clínico en seguimiento oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTVITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Vital del Paciente  1-> Vivo  2-> Muerto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTVITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTVITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última cita médica, último contacto clínico registrado con el paciente (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHULTCMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Última Cita Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHULTCMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHULTCMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación de la recurrencia del tumor: 1=Local, 2=Regional, 3=A distancia. Clasificación de reaparición tumoral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UBICARECURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicación de la recurrencia:  1->Local  2->Regional  3->a Distancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UBICARECURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'UBICARECURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la recurrencia, momento de identificación del rebrote o reaparición del tumor (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHARECURR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la recurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHARECURR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHARECURR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de recurrencia: 1=Sí hay recurrencia, 0=No. Especifica si hay reactivación tumoral post-tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RECURRENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si hay recurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RECURRENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RECURRENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha fin del tratamiento, fecha de conclusión de la terapia oncológica (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHFINPRITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha fin Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHFINPRITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHFINPRITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicio del primer tratamiento, inicio de la primera línea terapéutica oncológica (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINIPRITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio del primer tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINIPRITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINIPRITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tratamiento especificado como ''''Otro'''', campo de texto libre para opciones no categorizadas (VARCHAR 200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRODESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se llena el campo cuando se seleccionó otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRODESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRODESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tratamiento adicional no especificado: 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Otro, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dato faltante o no disponible: 1=Sin dato, 0=Hay dato. Señala ausencia de información.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINDATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si no hay dato, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINDATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINDATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cuidados paliativos: 1=Sí recibe, 0=No. Especifica tratamiento de confort y manejo sintomático.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PALIATIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Paliativo, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PALIATIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PALIATIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de terapia biológica o terapia dirigida: 1=Sí, 0=No. Tratamiento con agentes biológicos oncológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TBIOLOGICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es T Biologica, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TBIOLOGICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TBIOLOGICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hormonoterapia oncológica: 1=Sí recibe, 0=No. Tratamiento hormonal para cánceres sensibles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HORMONOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si tiene Hormonoterapias , 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HORMONOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HORMONOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de inmunoterapia u inmuno-oncología: 1=Sí recibe, 0=No. Tratamiento basado en sistema inmune.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'INMUNOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Inmunoterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'INMUNOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'INMUNOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de yodoterapia radiactiva: 1=Sí recibe, 0=No. Especialmente en cánceres tiroideos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'YODOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Yodoterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'YODOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'YODOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de puvaterapia (radiación ultravioleta A): 1=Sí recibe, 0=No. Modalidad en dermatología oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PUVATERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Puvaterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PUVATERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PUVATERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de quimioterapia: 1=Sí recibe, 0=No. Tratamiento farmacológico sistémico más frecuente en oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'QUMIOTERAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es QuimioTerapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'QUMIOTERAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'QUMIOTERAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de radioterapia o radioterapia externa: 1=Sí recibe, 0=No. Tratamiento con radiación ionizante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RADIOTERAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Radioterapia, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RADIOTERAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'RADIOTERAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cirugía oncológica: 1=Sí, 0=No. Intervención quirúrgica de extirpación tumoral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CIRUGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es Cirugía, 1-> si, 0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CIRUGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CIRUGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del trámite administrativo, fecha de procesamiento o gestión de la solicitud (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHATRAMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHATRAMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHATRAMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de paciente sin tratamiento: 1=Sin tratamiento, 0=Recibe tratamiento. Especifica status terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINTRATAMIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paciente esta sin tratamiento  1-> Si  0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINTRATAMIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'SINTRATAMIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad del tumor: 1=Izquierda, 2=Derecha. Ubicación bilateral o unilateral de la lesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lateralidad  1-> Izquierda  2-> Derecha  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de embarazo: 1=Sí embarazada, 0=No. Dato importante para decisiones terapéuticas oncológicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Embarazo  1-> Si  0-> No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EMBARAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EMBARAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serostatus VIH del paciente: 1=VIH positivo, 0=VIH negativo. Comorbilidad relevante en oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'VIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'VIH  1-> Si  0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'VIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'VIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de carcinomatosis (diseminación maligna): 1=Sí, 0=No. Enfermedad diseminada en peritoneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CARCINOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Carcinomatosis  1-> Si  0-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CARCINOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CARCINOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo 3 de metástasis, ubicación o descripción de tercera localización metastásica (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo 3 de metastasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo 2 de metástasis, ubicación o descripción de segunda localización metastásica (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo 2 de metastasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo 1 de metástasis, ubicación o descripción de primera localización metastásica (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo 1 de metastasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'METASTASIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado clínico 2, clasificación complementaria con opciones: O, A, B, C, 8, 9 (CHAR 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Clínico 2  Opciones:  O  A  B  C  8  9', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado clínico 1, clasificación principal con opciones: O, I, II, III, IV, 8, 9 (CHAR 5).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Clínico 1  opciones:  O  I  II  III  IV  8  9', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTCLINICO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M del clasificador TNM, metástasis: valor 0-3. Componente de estadificación tumoral internacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'M';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'hace parte del campo TNM = N es un numero de 0 a 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'M';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'M';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'N del clasificador TNM, nódulos linfáticos: valor 0-4. Componente de estadificación tumoral internacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'N';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'hace parte del campo TNM = N es un numero de 0 a 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'N';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'N';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'T del clasificador TNM, tumor primario: valor 1-10. Componente de estadificación tumoral internacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'T';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'hace parte del campo TNM = T es un numero de 1 a 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'T';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'T';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de patología, hallazgos histopatológicos y diagnóstico confirmado por anatomía patológica (VARCHAR 100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro de Patologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base diagnóstica válida DX: Opción DESC (Descartado). Indicador booleano de método de confirmación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base Válida DX Opcion DESC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base diagnóstica válida DX: Opción H. Irio (Histología Irio). Indicador booleano de fuente diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HIRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base Válida DX Opcion H. Irio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HIRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HIRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base diagnóstica válida DX: Opción H.MTT (Histología MTT). Indicador booleano de método anatomopatológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HMTT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base Válida DX Opcion H.Mtt', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HMTT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HMTT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base diagnóstica válida DX: Opción Citología. Indicador booleano de diagnóstico citológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CITOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base Válida DX Opcion Citol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CITOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CITOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base diagnóstica válida DX: Opción P. Esp (Psicología Especializada). Indicador booleano de especialidad diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base Válida DX Opcion P. Esp', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'PESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base diagnóstica válida DX: Opción Exp C (Examen Especializado). Indicador booleano de diagnóstico por examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EXPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base Válida DX Opcion Exp C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EXPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'EXPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base diagnóstica válida DX: Opción Clínico. Indicador booleano de diagnóstico clínico puro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CLINICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base Válida DX Opcion Clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CLINICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CLINICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de diferenciación celular del tumor: valor 1-9 (TINYINT). Evalúa agresividad histológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DIFERENCIACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diferenciación, Número de 1 dígito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DIFERENCIACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DIFERENCIACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Histológico, tipo histológico específico del tumor, descripción del tipo celular (VARCHAR 200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HISTOLOGICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Histológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HISTOLOGICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'HISTOLOGICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de morfología tumoral padre, clasificación CIE-O de la morfología (VARCHAR 10). FK a HCONCOMORFO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODMORFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Morfologico Padre a la cual pertenece', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODMORFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODMORFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID relación con tabla de morfologías oncológicas (HCONCOMORFO). Clasificación CIE-O del patrón celular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDMORFOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de morfologicos (HCONCOMORFO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDMORFOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDMORFOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del sitio topográfico padre, clasificación CIE-O de localización tumoral (VARCHAR 5).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODTOPOGRAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del registro Topografico Padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODTOPOGRAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODTOPOGRAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID relación con tabla de topografías (HCONCOTOPO). Identifica sitio anatómico de la lesión primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDTOPOGRAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion con la tabla de Topograficos (HCONCOTOPO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDTOPOGRAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IDTOPOGRAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de departamento y municipio de residencia del paciente, ubicación geográfica (CHAR 5). FK a INMUNICIP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de departamento y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de habilitación de IPS, acreditación sanitaria del centro de atención (CHAR 15).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODHABILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Habilitación de IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODHABILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODHABILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la IPS, identificador único de institución prestadora de servicios de salud (CHAR 100). FK a ADCONTIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la EPS, identificador de entidad promotora de salud/aseguradora (CHAR 9). FK a INENTIDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la EPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del primer diagnóstico de cáncer, fecha de confirmación inicial oncológica (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHPRIDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Primer Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHPRIDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHPRIDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso del paciente, inicio de atención en el centro oncológico (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'FECHINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tumor: 1=Maligno, 2=Benigno. Clasificación fundamental por naturaleza tumoral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de tumor  1-> Maligno  2-> Benigno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPOTUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maligno, datos clínicos y patológicos para neoplasias malignas (VARCHAR 200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGMALIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Maligno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGMALIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGMALIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro benigno, datos clínicos y patológicos para neoplasias benignas (VARCHAR 200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGBENIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Benigno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGBENIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'REGBENIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vivienda: 1=Propia, 2=Familiar, 3=Amigo, 4=Albergue. Condición sociodemográfica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPVIVIENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Vivienda:  1-> Propia  2-> Familiar  3-> Amigo  4-> Albergue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPVIVIENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'TIPVIVIENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de años en antracenos u ocupación: valor TINYINT. Dato epidemiológico de exposición laboral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMANTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Años Trabajados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMANTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMANTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la ficha oncológica: 1=ACTIVO, 0=INACTIVO. Indicador de vigencia del registro clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTFICHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la ficha Oncológica  1-> ACTIVO   0 ->INACTIVO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTFICHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ESTFICHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ficha oncológica, identificador secuencial del registro clínico (CHAR 4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMFICHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ficha Oncológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMFICHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'NUMFICHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente (código cédula, pasaporte, documento), PII enmascarada con Identification_Ofuscado. FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla HCONCOFICH (INT IDENTITY, clave primaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha oncológica del paciente (registro de cáncer): guarda la información clínica y epidemiológica del tumor, incluyendo diagnóstico, topografía, morfología, estadificación TNM, tratamientos recibidos, recurrencia y estado vital. Corresponde al registro individual de cada caso de cáncer por paciente en el sistema de historia clínica oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOFICH';
