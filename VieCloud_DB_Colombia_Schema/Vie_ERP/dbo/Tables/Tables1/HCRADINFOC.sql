CREATE TABLE [dbo].[HCRADINFOC] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHFOLIO]     DATETIME                                                                         NOT NULL,
    [CODDIAGNO]     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [ESTADIO1]      CHAR (10)                                                                        NOT NULL,
    [ESTADIO2]      CHAR (10)                                                                        NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CONCOMITAN]    INT                                                                              NOT NULL,
    [CODCENATE]     INT                                                                              NULL,
    [CODIGOIPS]     CHAR (100)                                                                       NULL,
    [TIPORADIOTERA] INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCRADINFOC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADINFOC_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_HCRADINFOC_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCRADINFOC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCRADINFOC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRADINFOC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRADINFOC].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCRADINFOC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de radioterapia aplicada: 1=Radioterapia Externa, 2=Braquiterapia. INT, clasificación de modalidad terapéutica oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'TIPORADIOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Radioterapia 1-Radioterapia Externa 2 - Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'TIPORADIOTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'TIPORADIOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del servicio o IPS prestador. CHAR(100), referencia FK a ADCONTIPS. Búsqueda: institución, prestador, centro de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código servicio Ips.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del centro de atención: 1=Centro de Atención, 2=IPS. INT nullable. Tipo de entidad proveedora del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Centro atención 2 - Ips', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de concomitancia terapéutica: 1=Sí (tratamiento simultáneo), 2=No. INT obligatorio. Búsqueda: terapia combinada, concurrente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CONCOMITAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene Concomitancia  1-Si  2-No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CONCOMITAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CONCOMITAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud responsable. CHAR(20) PII ofuscado, FK a INPROFSAL. Médico, oncólogo, radiólogo terapéuta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Profesional Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio clínico secundario o complementario del tumor. CHAR(10) nullable. Clasificación TNM: 0, I, II, III, IV.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estadio 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio clínico principal del tumor: 0→0, I→1, II→2, III→3, IV→4. CHAR(10), clasificación TNM del diagnóstico oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estadio 1  0->0  I->1  II->2  III>3  IV->4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico principal de la historia clínica. CHAR(4) ofuscado, FK a INDIAGNOS. CIE-10, diagnóstico de cáncer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Diagnóstico Principal de la Hc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del folio o registro de radioterapia. DATETIME obligatorio. Fecha de atención, consulta oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'FECHFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'FECHFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'FECHFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del paciente (cédula, documento, equivalente). VARCHAR(25) PII ofuscado, FK a INPACIENT. Búsqueda: paciente, persona.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Identificaión Paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, clave primaria secuencial de la tabla HCRADINFOC. INT IDENTITY, consecutivo registros radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de información de radioterapia oncológica por paciente. Guarda los folios de tratamiento con diagnóstico, estadio tumoral, profesional tratante, centro de atención y tipo de radioterapia aplicada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC';
