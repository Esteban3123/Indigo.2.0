CREATE TABLE [dbo].[HCINFLIDI] (
    [CODCONCEC]        NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]        CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [TIPMEZLIQ]        CHAR (1)                                                                         NOT NULL,
    [METAPLMED]        CHAR (1)                                                                         NOT NULL,
    [ABRPROMEZ]        CHAR (200)                                                                       NULL,
    [CANPROCAL]        INT                                                                              NOT NULL,
    [ESBOLMEDL]        BIT                                                                              NULL,
    [DOSISBOLO]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDBOL]        CHAR (3)                                                                         NULL,
    [ESLIQINFU]        BIT                                                                              NULL,
    [DOSISINFU]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDINF]        CHAR (3)                                                                         NULL,
    [FRECUEINF]        INT                                                                              NULL,
    [UNIFREINF]        CHAR (1)                                                                         NULL,
    [FECINIINF]        DATETIME                                                                         NULL,
    [DURACIINF]        CHAR (20)                                                                        NULL,
    [VALDURINF]        INT                                                                              NULL,
    [UNIDURINF]        CHAR (1)                                                                         NULL,
    [FECANTINF]        BIT                                                                              NULL,
    [MOTANTINF]        CHAR (250)                                                                       NULL,
    [CODPRODIL]        CHAR (20)                                                                        NULL,
    [CANPASDIL]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDDIL]        CHAR (3)                                                                         NULL,
    [ESBOLOMEZ]        BIT                                                                              NULL,
    [DOBOLOMEZ]        NUMERIC (18, 2)                                                                  NULL,
    [UNMEBOMEZ]        CHAR (3)                                                                         NULL,
    [VADUBOMEZ]        INT                                                                              NULL,
    [UNDUBOMEZ]        CHAR (1)                                                                         NULL,
    [ESBOLMEDM]        BIT                                                                              NULL,
    [DOSISBOLM]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDBOM]        CHAR (3)                                                                         NULL,
    [CODVIABOM]        CHAR (2)                                                                         NULL,
    [ESMEZINFU]        BIT                                                                              NULL,
    [UNIMEINFM]        CHAR (10)                                                                        NULL,
    [CANMEINFM]        NUMERIC (18, 2)                                                                  NULL,
    [INFTITULA]        BIT                                                                              NULL,
    [UNIMEINFT]        CHAR (10)                                                                        NULL,
    [CANMEINFT]        NUMERIC (18, 2)                                                                  NULL,
    [CANCCHORA]        NUMERIC (18, 2)                                                                  NULL,
    [CANCCHORT]        NUMERIC (18, 2)                                                                  NULL,
    [FECINIINFM]       DATETIME                                                                         NULL,
    [DURACIINFM]       CHAR (20)                                                                        NULL,
    [VALDURINFM]       INT                                                                              NULL,
    [UNIDURINFM]       CHAR (1)                                                                         NULL,
    [FECANTINFM]       BIT                                                                              NULL,
    [MOTANTINFM]       CHAR (250)                                                                       NULL,
    [INDAPLMED]        VARCHAR (MAX)                                                                    NULL,
    [FOLIOINIC]        NCHAR (10)                                                                       NOT NULL,
    [TRATNUEVO]        BIT                                                                              NOT NULL,
    [TRATMODIF]        BIT                                                                              NOT NULL,
    [MEZLIQPAC]        CHAR (500)                                                                       NOT NULL,
    [ADMMEZLIQ]        CHAR (500)                                                                       NOT NULL,
    [JUSTIFICACIONPBS] VARCHAR (2000)                                                                   NULL,
    [VIAADMDIL]        VARCHAR (20)                                                                     NULL,
    CONSTRAINT [PK_HCINFLIDI_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCINFLIDI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFLIDI_HCINFLIDI] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLICI] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFLIDI_HCVIAADMI] FOREIGN KEY ([VIAADMDIL]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_HCINFLIDI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLIDI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLIDI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del diluyente o mezcla (intravenosa, intramuscular, subcutánea, etc.); FK a HCVIAADMI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VIAADMDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la via de administración de mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VIAADMDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VIAADMDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (Política de Medicamentos del Sistema); texto de amparo regulatorio de prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administración de la mezcla o líquido: descripción del procedimiento de aplicación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción clínica de la mezcla o líquido prescrito al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el registro fue modificado en el folio actual; bandera de cambio de prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el Registro es modificado en el folio actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TRATMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el registro es nuevo en el folio actual; bandera de prescripción inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TRATNUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el Registro es nuevo en el folio actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TRATNUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TRATNUEVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del folio inicial donde se prescribió la mezcla o medicamento; referencia histórica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio inicial donde se prescribio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de administración: instrucciones clínicas de aplicación y precauciones (TEXT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Motivo por el cual se suspendió o retrasó la infusión anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MOTANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Motivo Infusion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MOTANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MOTANTINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Indicador (BIT) si la infusión ocurrió en fecha anterior; bandera retrospectiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Indica si la infusion ocurrio en una Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECANTINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Unidad de duración fija de infusión: 1=Minutos, 2=Horas, 3=Días.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad del Valor de la duracion fija de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Valor numérico de la duración fija de la infusión (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VALDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'mezcla - Valor de la duracion fija de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VALDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VALDURINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Tipo o descriptor de duración de la dosis infusional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DURACIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Tipo Duracion de la Dosis de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DURACIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DURACIINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Fecha y hora inicial de inicio de la infusión (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECINIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Fecha Inicial de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECINIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECINIINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Cantidad calculada de CC/ml por hora titulable (variable según respuesta clínica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANCCHORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad Calculada de CC por Hora Titulable  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANCCHORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANCCHORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Cantidad calculada de CC/ml por hora fija de infusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANCCHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad Calculada de CC por Hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANCCHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANCCHORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Cantidad de la unidad de medida en infusión titulable (mcg/Kg/min, mcg/min, UI/hr, mEq/hr).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad de la unidad de medida infusion titulable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANMEINFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Unidad de medida infusión titulable: mcg/Kg/min, mcg/min, UI/hr, mEq/hr.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida de la infusion de la mezcla titulable :  mcg/Kg/min  mcg/min  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la infusión es titulable (ajustable según parámetros clínicos).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'INFTITULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Infusion Titulable hasta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'INFTITULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'INFTITULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Cantidad de la unidad de medida en infusión fija.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad de la unidad de medida infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANMEINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Unidad de medida infusión: mcg/Kg/min, mcg/min, UI/hr, mEq/hr.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida de la infusion de la mezcla:  mcg/Kg/min  mcg/min  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Indicador (BIT) si es infusión de mezcla (vs bolo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESMEZINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Es Mezcla Infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESMEZINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESMEZINFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Código de vía de administración bolo (referencia a catálogo de vías).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODVIABOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Codigo de la Via de Administracion Bolo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODVIABOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODVIABOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Unidad de medida del bolo medicamento (mg, mcg, mL, UI, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Dosis en bolo del medicamento dentro de la mezcla (NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISBOLM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Dosis Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISBOLM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISBOLM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Indicador (BIT) si hay bolo medicamento en la mezcla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLMEDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Es Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLMEDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLMEDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Unidad duración bolo mezcla: 1=Minutos, 2=Horas, 3=Días.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNDUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad del Valor de la duracion fija del bolo mezcla:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNDUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNDUBOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Valor duración fija del bolo mezcla (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VADUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Valor de la duracion fija de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VADUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VADUBOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Unidad de medida bolo mezcla (mg, mcg, mL, UI, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNMEBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla  - Unidad de Medida Bolo Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNMEBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNMEBOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Dosis en bolo de la mezcla (NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Dosis Bolo mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOBOLOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Indicador (BIT) si hay bolo mezcla en la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Es Bolo Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Unidad de medida del diluyente (mL, L, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida del Diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Cantidad a pasar del diluyente (volumen, NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANPASDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad a pasar del diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANPASDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANPASDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - Código del producto diluyente (solución fisiológica, dextrosa, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Codigo del Producto (Diluyente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPRODIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Motivo de suspensión o retraso de infusión anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MOTANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Motivo Infusion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MOTANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'MOTANTINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Indicador (BIT) si la infusión líquida ocurrió en fecha anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Indica si la infusion ocurrio en una Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECANTINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Unidad duración infusión: 1=Minutos, 2=Horas, 3=Días.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad del Valor de la duracion fija de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIDURINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Valor duración fija infusión (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VALDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Valor de la duracion fija de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VALDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'VALDURINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Tipo o descriptor de duración de la dosis infusional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DURACIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Duracion de la Dosis de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DURACIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DURACIINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Fecha y hora inicial de infusión (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECINIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Fecha Inicial de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECINIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FECINIINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Unidad frecuencia infusión: 1=Minutos, 2=Horas, 3=Días.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIFREINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad del Valor de la Frecuencia de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIFREINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIFREINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Frecuencia de infusión (INT), intervalo de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FRECUEINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Frecuencia de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FRECUEINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'FRECUEINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Unidad de medida infusión medicamento (mg, mcg, mL, UI, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad de Medida Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Dosis infusión del medicamento o solución (NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Dosis Infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISINFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Indicador (BIT) si hay infusión de líquido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESLIQINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Es Liquido Infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESLIQINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESLIQINFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Unidad de medida bolo medicamento (mg, mcg, mL, UI, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad de Medida Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UNIMEDBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Dosis en bolo del medicamento (NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISBOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Dosis Bolo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISBOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'DOSISBOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líquido - Indicador (BIT) si hay bolo medicamento en líquido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLMEDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Es Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLMEDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ESBOLMEDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad inicial de producto calculada para la prescripción (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Inicial de producto calculada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CANPROCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura o alias del producto para visualización en mezclas (VARCHAR 200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de aplicación de medicamento: 1=Bolo Mezcla, 2=Infusión Mezcla, 3=Bolo Med Mezcla, 4=Infusión Líquido, 5=Bolo Med Líquido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden médica: 1=Mezcla farmacéutica, 2=Líquido (solución).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Medica:  1. Mezcla  2. Liquido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto según tipo: 1=Diluyente (bolo mezcla), 2=Diluyente (infusión), 3=Medicamento (bolo), 4=Líquido (solución).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto por Tipo de Registro   1: Bolo Mezcla - Especifica el Diluyente  2: Infusion - Especifica el Diluyente  3: Bolo Medicamento - Especifica el Medicamento  4: Liquido - Especifica el Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que prescribió (médico, enfermera especialista); PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se prescribió (UCI, urgencias, piso, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (institución, sede, hospital).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso del paciente; FK a ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento); PII ofuscado; FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (registro secuencial de la prescripción o atención clínica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica (CHAR 9).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo, identificador único del registro (PK); FK a HCINFLICI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de líquidos de infusión e instrucciones de mezcla para medicamentos administrados por vía intravenosa en historia clínica. Contiene la configuración de bolos, infusiones, diluyentes y mezclas líquidas prescritas a un paciente durante un ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIDI';
