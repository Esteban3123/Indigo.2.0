CREATE TABLE [dbo].[HCHOJMEZINF] (
    [Id]             INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IDHCINFLIQC]    NUMERIC (18)                                                                     NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [FECHAPLICACION] DATE                                                                             NOT NULL,
    [HORA]           DATETIME                                                                         NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [VALORINFUSION]  NUMERIC (18, 2)                                                                  NULL,
    CONSTRAINT [PK_HCHOJMEZINF] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CODCENATE_ADCENATENs] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CODCONCEC_HCINFLIQCc] FOREIGN KEY ([IDHCINFLIQC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_CODPROSAL_INPROFSALl] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_IPCODPACI_INPACIENTt] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_NUMINGRES_NUMINGRESs] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_UFUCODIGO_INUNIFUNCc] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHOJMEZINF].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHOJMEZINF].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario o cantidad de la infusión administrada por hora. Numeric(18,2), puede ser nulo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'VALORINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Infusione por la hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'VALORINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'VALORINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud (médico, enfermero, farmacéutico) que administra la infusión. VARCHAR(25), PII ofuscado, referencia a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo profesional de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora exacta de aplicación/administración de la mezcla infusión en formato DATETIME (HH:MM:SS). Registra franja horaria: mañana (00:00-11:00), tarde (12:00-17:00), noche (18:00-23:00).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'HORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha:    Mañana  00:00   01:00   02:00   03:00   04:00   05:00   06:00   07:00   08:00   09:00   10:00   11:00      Tarde:  12:00   13:00   14:00   15:00   16:00   17:00     Noche  18:00   19:00   20:00   21:00   22:00   23:00   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'HORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'HORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aplicación o administración de la mezcla infusión al paciente. DATE, sin hora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'FECHAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de aplicacion de esa mezcla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'FECHAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'FECHAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se aplica la infusión (UCI, sala de hospitalización, urgencias, etc). CHAR(10), referencia a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución o sede donde se realiza la infusión. CHAR(10), referencia a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o episodio del paciente a la institución. CHAR(10), referencia a ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula, documento o identificación del paciente (PII). VARCHAR(25), ofuscado, referencia a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cedula paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la orden de mezcla infusión relacionada. Numeric(18), llave foránea a HCINFLIQC (tabla maestra de órdenes/prescripciones).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de la orden de mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo identity) del registro de aplicación de infusión. INT, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de infusiones aplicadas a pacientes durante su ingreso hospitalario. Guarda el detalle de cada aplicación de infusión: quién la recibió, cuándo, en qué centro y unidad funcional, y el valor de la infusión administrada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZINF';
