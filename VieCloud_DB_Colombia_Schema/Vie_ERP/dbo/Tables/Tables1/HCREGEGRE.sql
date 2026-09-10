CREATE TABLE [dbo].[HCREGEGRE] (
    [IDETIPHIS]             CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]             NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]             VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                        NOT NULL,
    [CODCENATE]             CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]             CHAR (10)                                                                        NOT NULL,
    [FECALTPAC]             DATETIME                                                                         NOT NULL,
    [ESTPACEGR]             INT                                                                              NOT NULL,
    [FECMUEPAC]             DATETIME                                                                         NULL,
    [CODCAUMUE]             CHAR (3)                                                                         NULL,
    [NUMCERDEF]             CHAR (40) MASKED WITH (FUNCTION = 'partial(0, "DeathCertificate_Ofuscado", 0)')  NULL,
    [DEPMUNCOD]             CHAR (5)                                                                         NULL,
    [AIPSREMIS]             CHAR (60)                                                                        NULL,
    [REHORASOL]             DATETIME                                                                         NULL,
    [REHORCON]              DATETIME                                                                         NULL,
    [REHORLLE]              DATETIME                                                                         NULL,
    [REMINIVEL]             INT                                                                              NULL,
    [REESPECIA]             CHAR (60)                                                                        NULL,
    [REPERCONF]             CHAR (60)                                                                        NULL,
    [RESERVICI]             CHAR (60)                                                                        NULL,
    [IOBSERVAC]             VARCHAR (MAX)                                                                    NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CTRSALSEG]             BIT                                                                              NULL,
    [INDAUDFOR]             NUMERIC (18)                                                                     NOT NULL,
    [NOMSOLREM]             VARCHAR (150)                                                                    NULL,
    [MOTIVEMI]              CHAR (3)                                                                         NULL,
    [CODDIAGNOCOMPLICACION] CHAR (4)                                                                         NULL,
    CONSTRAINT [PK_HCREGEGRE] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC),
    CONSTRAINT [FK_HCREGEGRE_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCREGEGRE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREGEGRE_DIAGNO_COMPL] FOREIGN KEY ([CODDIAGNOCOMPLICACION]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCREGEGRE_INCAUMUER] FOREIGN KEY ([CODCAUMUE]) REFERENCES [dbo].[INCAUMUER] ([CODCAUMUE]),
    CONSTRAINT [FK_HCREGEGRE_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]),
    CONSTRAINT [FK_HCREGEGRE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCREGEGRE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCREGEGRE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCREGEGRE] NOCHECK CONSTRAINT [FK_HCREGEGRE_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREGEGRE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREGEGRE].[NUMCERDEF]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREGEGRE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCREGEGRE_6_1534628510__K3_K7]
    ON [dbo].[HCREGEGRE]([IPCODPACI] ASC, [FECALTPAC] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCREGEGRE_8_1534628510__K7_K6_K4_K2_K3_8]
    ON [dbo].[HCREGEGRE]([FECALTPAC] ASC, [UFUCODIGO] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC)
    INCLUDE([ESTPACEGR]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCREGEGRE_8_1534628510__K4_7]
    ON [dbo].[HCREGEGRE]([NUMINGRES] ASC)
    INCLUDE([FECALTPAC]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de remisión del paciente; razón clínica o administrativa por la cual se refiere el egreso a otra institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'MOTIVEMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'MOTIVEMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'MOTIVEMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del profesional de salud o autoridad que solicita la remisión del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NOMSOLREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quien solicita la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NOMSOLREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NOMSOLREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo reservado para auditoría y compliance; indicador numérico de trazabilidad del egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (Null=No, True=Sí) que especifica si el egreso ya tiene control de salida y seguimiento registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CTRSALSEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el egreso ya tiene el control de Salida y Seguimiento  Null -> No tiene el control por lo tanto se muestra  True -> Ya tiene el control no se muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CTRSALSEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CTRSALSEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (usuario) que crea y registra el egreso del paciente. FK → INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que Crea el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo administrativo de texto libre (VARCHAR MAX) para observaciones generales, notas clínicas o comentarios del egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de uso administrativo para especificar observaciones generales del Egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IOBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del servicio o especialidad clínica a donde se remite el paciente (servicio remitido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'RESERVICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio Remitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'RESERVICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'RESERVICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del profesional de salud que confirma o acepta la remisión del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REPERCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que Confirma la Remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REPERCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REPERCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad clínica remitida; disciplina médica solicitada (ej: cardiología, neurología, urgencia).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad Remitida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REESPECIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de complejidad de la remisión; 1=Primer nivel, 2=Segundo nivel, 3=Tercer nivel.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REMINIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Remitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REMINIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REMINIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de llegada del paciente a la institución remitida (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORLLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de LLegada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORLLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORLLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación de la remisión por el profesional receptor (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de Confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se solicita la remisión del paciente (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORASOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de Solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORASOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'REHORASOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación de la Institución Prestadora de Salud (IPS) receptora de la remisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la IPS a donde Remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'AIPSREMIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de departamento-municipio donde se ubica la IPS remitida. FK → INMUNICIP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el codigo del Municipio a la IPS donde remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de certificado de defunción del paciente (PII ofuscado); se diligencia solo si ESTPACEGR=3 (Fallecido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del certificado de defuncion del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMCERDEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la causa de muerte según clasificación clínica (CIE-10); FK → INCAUMUER.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del fallecimiento del paciente (NULL si vivo); solo se registra cuando ESTPACEGR=3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'FECMUEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de muerte del paciente, solo si aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'FECMUEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'FECMUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del paciente al egreso: 1=Mejor, 2=Igual o Peor, 3=Fallecido, 4=Remitido, 5=Hospitalización en Casa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'ESTPACEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Paciente al Egreso   1: Mejor  2: Igual o Peor  3: Fallecido  4: Remitido  5: Hospitalizacion en Casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'ESTPACEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'ESTPACEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de alta o egreso del paciente de la institución (DATETIME, obligatorio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'FECALTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Alta del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'FECALTPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'FECALTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (servicio, piso, área) de donde egresa el paciente. FK → INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o entidad prestadora donde ocurre el egreso. FK → ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso/admisión del paciente; clave primaria del egreso. FK → ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, pasaporte, documento de identidad); PII ofuscado. FK → INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de folio o referencia interna del registro de egreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código interno del tipo de historia clínica; identificador de modalidad de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 de diagnóstico de complicación; se registra cuando el médico identifica complicaciones en el egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODDIAGNOCOMPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico de complicación, se diligencia cuando el medico este dando el egreso al paciente y seleccione 1 diagnostico de este tipo.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODDIAGNOCOMPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE', @level2type = N'COLUMN', @level2name = N'CODDIAGNOCOMPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de egreso hospitalario del paciente: guarda el alta, la condición de salida, datos de fallecimiento, remisión a otra institución y el médico tratante al momento del egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGEGRE';
