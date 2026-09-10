CREATE TABLE [dbo].[RISTRACAB] (
    [CODCONSEC]        VARCHAR (10)                                                                     NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NOMPACIEN]        VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')           NOT NULL,
    [IPPRIAPEL]        VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')   NOT NULL,
    [IPSEGAPEL]        VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')  NOT NULL,
    [IPFECNACI]        VARCHAR (8) MASKED WITH (FUNCTION = 'default()')                                 NOT NULL,
    [IPSEXOPAC]        VARCHAR (6)                                                                      NOT NULL,
    [TIPMODALI]        VARCHAR (2)                                                                      NOT NULL,
    [CODSERIPS]        CHAR (20)                                                                        NOT NULL,
    [DESSERIPS]        VARCHAR (300)                                                                    NOT NULL,
    [FECHTURNO]        VARCHAR (8)                                                                      NOT NULL,
    [HORATURNO]        VARCHAR (6)                                                                      NOT NULL,
    [INESTADOT]        VARCHAR (15)                                                                     NOT NULL,
    [PROCESADO]        VARCHAR (1)                                                                      NOT NULL,
    [REPSERPAC]        VARCHAR (8000)                                                                   NULL,
    [OBSSERPAC]        VARCHAR (400)                                                                    NULL,
    [FECNACDAT]        DATETIME                                                                         NOT NULL,
    [EXAURGPAC]        BIT                                                                              NOT NULL,
    [NOMSERPAC]        CHAR (30)                                                                        NULL,
    [USUAGESER]        CHAR (20)                                                                        NULL,
    [USUCONSER]        CHAR (20)                                                                        NULL,
    [ACCNUMBER]        INT                                                                              NULL,
    [STUDYINSTANCEUID] VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_ITSYNAPSE] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_RISTRACAB_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_RISTRACAB_RISTRACAB] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RISTRACAB].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RISTRACAB].[NOMPACIEN]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RISTRACAB].[IPPRIAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RISTRACAB].[IPSEGAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RISTRACAB].[IPFECNACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');




GO
CREATE NONCLUSTERED INDEX [IX_RISTRACAB]
    ON [dbo].[RISTRACAB]([PROCESADO] ASC, [INESTADOT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de instancia de estudio DICOM (UID), referencia del examen radiológico en sistema de imagen médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'STUDYINSTANCEUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la instancia de estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'STUDYINSTANCEUID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'STUDYINSTANCEUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de acceso o correlativo de la orden de estudio en el sistema de información radiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'ACCNUMBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el numero ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'ACCNUMBER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'ACCNUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registra o conserva el estudio en sistema, auditoría de ingreso de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUCONSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUCONSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUCONSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de registro o creación del estudio, responsable de la captura inicial del examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUAGESER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUAGESER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUAGESER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servidor PACS (Picture Archiving and Communication System) que almacena imágenes radiológicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del servidor PACS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMSERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de urgencia: 1=Examen urgente, 0=Examen programado o no urgente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'EXAURGPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el examen es de urgencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'EXAURGPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'EXAURGPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (tipo DATETIME), requerida para cálculo de edad y contexto clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECNACDAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECNACDAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECNACDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales del estudio radiológico, notas adicionales del técnico o radiólogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'OBSSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones Generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'OBSSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'OBSSERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte o dictamen radiológico del médico radiólogo, interpretación diagnóstica del examen de imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'REPSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reporte del Radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'REPSERPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'REPSERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de procesamiento: 1=Guardado por agendamiento/órdenes intrahospitalarias, 2=Asignado técnico/espera PACS, 3=Enviado a PACS, 4=Anulado por técnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'PROCESADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el envio al PACS  1: Indica que ha sido guardado por el sistema de agendamiento o por las Ordenes Medicas Intrahospitalarias  2: Indica que el Tecnologo ya lo asigno para la toma del examen y esta a la espera del envio al PACS  3: Indica ya se envio la Orden al PACS  4. Examen Anulado por el Tecnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'PROCESADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'PROCESADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del estudio: Ordered=Ordenado, Scheduled=Agendado, Completed=Completado, Dictated=Dictado, Canceled=Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'INESTADOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Estudio  Ordened: Ordenado - Dashboard Medico Tratante  Scheduled: Agendado - Dashboard Tecnologo  Completed: Completado - Dashboard Tecnologo  Dictated: Dictado - Dashboard Radiologo  Canceled: Anulado por el Tecnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'INESTADOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'INESTADOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora del turno de examen en formato HHMMSS (horas, minutos, segundos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'HORATURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Turno Formato: HHMMSS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'HORATURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'HORATURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del turno de examen en formato YYYYMMDD (año, mes, día)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECHTURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Turno Formato: YYYYMMDD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECHTURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECHTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del procedimiento, servicio o modalidad radiológica solicitada (referencia FK a INCUPSIPS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'DESSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'DESSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'DESSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS/RIPS del procedimiento o servicio radiológico (PK referencia a INCUPSIPS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de imagen: tipo de estudio radiológico (radiografía, tomografía, resonancia, ecografía, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'TIPMODALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad del Estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'TIPMODALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'TIPMODALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del paciente: 1=Masculino, 2=Femenino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo del Paciente:  1=Masculino  2=Femenino  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente en formato VARCHAR (8) YYYYMMDD, enmascarada por PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente, enmascarado por política de privacidad (PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente, enmascarado por política de privacidad (PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente, enmascarado por política de privacidad (PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMPACIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula/identificación/documento), enmascarado, FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único y consecutivo del registro de estudio radiológico, PK de tabla RISTRACAB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cabecera de turnos o citas agendadas para servicios de salud (agenda de atención al paciente). Registra los datos del paciente, el servicio solicitado (CUPS/IPS), la fecha y hora del turno, y el estado del agendamiento, incluyendo información de imágenes diagnósticas (DICOM/RIS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB';
