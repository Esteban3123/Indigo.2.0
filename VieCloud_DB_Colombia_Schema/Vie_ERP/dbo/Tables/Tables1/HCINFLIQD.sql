CREATE TABLE [dbo].[HCINFLIQD] (
    [CODCONCEC]                NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]                CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]                CHAR (20)                                                                        NOT NULL,
    [TIPMEZLIQ]                CHAR (1)                                                                         NOT NULL,
    [METAPLMED]                CHAR (1)                                                                         NOT NULL,
    [ABRPROMEZ]                CHAR (200)                                                                       NULL,
    [CANPROCAL]                INT                                                                              NOT NULL,
    [ESBOLMEDL]                BIT                                                                              NULL,
    [DOSISBOLO]                NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDBOL]                VARCHAR (20)                                                                     NULL,
    [ESLIQINFU]                BIT                                                                              NULL,
    [DOSISINFU]                NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDINF]                VARCHAR (20)                                                                     NULL,
    [FRECUEINF]                INT                                                                              NULL,
    [UNIFREINF]                CHAR (1)                                                                         NULL,
    [FECINIINF]                DATETIME                                                                         NULL,
    [DURACIINF]                CHAR (20)                                                                        NULL,
    [VALDURINF]                INT                                                                              NULL,
    [UNIDURINF]                CHAR (1)                                                                         NULL,
    [FECANTINF]                BIT                                                                              NULL,
    [MOTANTINF]                CHAR (250)                                                                       NULL,
    [CODPRODIL]                CHAR (20)                                                                        NULL,
    [CANPASDIL]                NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDDIL]                VARCHAR (20)                                                                     NULL,
    [ESBOLOMEZ]                BIT                                                                              NULL,
    [DOBOLOMEZ]                NUMERIC (18, 2)                                                                  NULL,
    [UNMEBOMEZ]                VARCHAR (20)                                                                     NULL,
    [VADUBOMEZ]                INT                                                                              NULL,
    [UNDUBOMEZ]                CHAR (1)                                                                         NULL,
    [ESBOLMEDM]                BIT                                                                              NULL,
    [DOSISBOLM]                NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDBOM]                VARCHAR (20)                                                                     NULL,
    [CODVIABOM]                VARCHAR (20)                                                                     NULL,
    [ESMEZINFU]                BIT                                                                              NULL,
    [UNIMEINFM]                CHAR (10)                                                                        NULL,
    [CANMEINFM]                NUMERIC (18, 2)                                                                  NULL,
    [INFTITULA]                BIT                                                                              NULL,
    [UNIMEINFT]                CHAR (10)                                                                        NULL,
    [CANMEINFT]                NUMERIC (18, 2)                                                                  NULL,
    [CANCCHORA]                NUMERIC (18, 2)                                                                  NULL,
    [CANCCHORT]                NUMERIC (18, 2)                                                                  NULL,
    [FECINIINFM]               DATETIME                                                                         NULL,
    [DURACIINFM]               CHAR (20)                                                                        NULL,
    [VALDURINFM]               INT                                                                              NULL,
    [UNIDURINFM]               CHAR (1)                                                                         NULL,
    [FECANTINFM]               BIT                                                                              NULL,
    [MOTANTINFM]               CHAR (250)                                                                       NULL,
    [INDAPLMED]                VARCHAR (MAX)                                                                    NULL,
    [FOLIOINIC]                NCHAR (10)                                                                       NOT NULL,
    [TRATNUEVO]                BIT                                                                              NOT NULL,
    [TRATMODIF]                BIT                                                                              NOT NULL,
    [MEZLIQPAC]                CHAR (500)                                                                       NOT NULL,
    [ADMMEZLIQ]                CHAR (500)                                                                       NOT NULL,
    [JUSTIFICACIONPBS]         VARCHAR (2000)                                                                   NULL,
    [VADUBOLIQUIDOS]           INT                                                                              NULL,
    [UNDUBOLIQUIDOS]           VARCHAR (1)                                                                      NULL,
    [NUMEROAPLICACIONES]       INT                                                                              NULL,
    [VIAADMDIL]                VARCHAR (20)                                                                     NULL,
    [ViaCodeMagistralInfusion] VARCHAR (20)                                                                     NULL,
    CONSTRAINT [PK_HCINFLIQD_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCINFLIQD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFLIQD_HCINFLIQD] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFLIQD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCINFLIQD_IHLISTPRO1] FOREIGN KEY ([CODPRODIL]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCINFLIQD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCVIAADMI_HCINFLIQD] FOREIGN KEY ([VIAADMDIL]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLIQD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLIQD].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCINFLIQD__NUMEFOLIO__IPCODPACI__NUMINGRES__INC__ADMMEZLIQ__FOLIOINIC__INDAPLMED__MEZLIQPAC__TRATMODIF]
    ON [dbo].[HCINFLIQD]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([INDAPLMED], [FOLIOINIC], [TRATMODIF], [MEZLIQPAC], [ADMMEZLIQ]);


GO
ALTER INDEX [IX_HCINFLIQD__NUMEFOLIO__IPCODPACI__NUMINGRES__INC__ADMMEZLIQ__FOLIOINIC__INDAPLMED__MEZLIQPAC__TRATMODIF]
    ON [dbo].[HCINFLIQD] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCINFLIQD_IPCODPACI_NUMINGRES_CANPROCAL]
    ON [dbo].[HCINFLIQD]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CANPROCAL], [CODCONCEC], [CODPRODUC], [NUMEFOLIO], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de vía de administración para mezcla magistral (VARCHAR 20). Referencia a catálogo de vías: IV, IM, SC, tópica, oral, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ViaCodeMagistralInfusion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla magistral - codigo via administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ViaCodeMagistralInfusion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ViaCodeMagistralInfusion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de administración del diluyente o mezcla (VARCHAR 20, FK HCVIAADMI). Ruta de infusión: intravenosa, intramuscular, subcutánea, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VIAADMDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la via administración de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VIAADMDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VIAADMDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de aplicaciones o dosis del medicamento a administrar (INT). Cantidad total de veces que se aplicará el tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Aplicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para duración del bolo en líquidos (VARCHAR 1): 1=Minutos, 2=Horas, 3=Días. Define escala temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNDUBOLIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de la Duración del Bolo en Liquidos  1: Minutos  2: Horas    3: Dias     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNDUBOLIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNDUBOLIQUIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de duración del bolo medicamento en líquidos (INT). Combinado con UNDUBOLIQUIDOS para tiempo total infusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VADUBOLIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duración en tiempo del bolo medicamento en liquidos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VADUBOLIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VADUBOLIQUIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (Política de Beneficios en Salud) para prescripción de medicamento (VARCHAR 2000). Sustento regulatorio y clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de administración: instrucciones clínicas sobre cómo aplicar la mezcla o líquido (CHAR 500). Pasos, precauciones, dilución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la mezcla o líquido prescrito (CHAR 500). Composición, ingredientes, presentación para paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si registro fue modificado en folio actual (BIT). Flag de auditoría: 1=Modificado, 0=No modificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el Registro es modificado en el folio actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TRATMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si registro es nuevo en folio actual (BIT). Flag de control: 1=Nuevo registro, 0=Existente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TRATNUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el Registro es nuevo en el folio actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TRATNUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TRATNUEVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio inicial donde se prescribió el tratamiento (NCHAR 10). Referencia a documento de origen primera prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio inicial donde se prescribio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de administración del medicamento (VARCHAR MAX). Instrucciones clínicas detalladas, diluciones, velocidad, frecuencia, monitoreo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo por el que se pausó infusión anterior de mezcla (CHAR 250). Razón: reacción adversa, fin de tratamiento, cambio orden médica, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MOTANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Motivo Infusion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MOTANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MOTANTINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si infusión de mezcla ocurrió en fecha anterior (BIT). Flag: 1=Sí pausada antes, 0=No pausada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Indica si la infusion ocurrio en una Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECANTINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECANTINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad temporal de duración fija infusión mezcla (CHAR 1): 1=Minutos, 2=Horas, 3=Días. Escala para VALDURINFM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad del Valor de la duracion fija de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIDURINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico duración fija infusión mezcla (INT). Tiempo total en unidades especificadas por UNIDURINFM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VALDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'mezcla - Valor de la duracion fija de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VALDURINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VALDURINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de duración de dosis infusión mezcla (CHAR 20). Categoría: continua, intermitente, única, fraccionada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DURACIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Tipo Duracion de la Dosis de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DURACIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DURACIINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicio infusión mezcla (DATETIME). Registro temporal de cuándo comienza administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECINIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Fecha Inicial de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECINIINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECINIINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad calculada de cc/hora titulable mezcla (NUMERIC 18,2). Volumen variable titulable por respuesta clínica paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANCCHORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad Calculada de CC por Hora Titulable  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANCCHORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANCCHORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad calculada de cc/hora mezcla (NUMERIC 18,2). Velocidad fija de infusión en mililitros por hora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANCCHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad Calculada de CC por Hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANCCHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANCCHORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad unidad medida infusión mezcla titulable (NUMERIC 18,2). Dosis variable ajustable según evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad de la unidad de medida infusion titulable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANMEINFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida infusión mezcla titulable (CHAR 10): mcg/Kg/min, mcg/min, UI/hr, mEq/hr. Escala potencia medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida de la infusion de la mezcla titulable :  mcg/Kg/min  mcg/min  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEINFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEINFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infusión titulable: ajustable según respuesta clínica (BIT). Flag: 1=Sí titulable, 0=No titulable, dosis fija.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'INFTITULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Infusion Titulable hasta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'INFTITULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'INFTITULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad unidad medida infusión mezcla (NUMERIC 18,2). Dosis fija de medicamento en unidades específicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad de la unidad de medida infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANMEINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida infusión mezcla (CHAR 10): mcg/Kg/min, mcg/min, UI/hr, mEq/hr. Especifica potencia o concentración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida de la infusion de la mezcla:  mcg/Kg/min  mcg/min  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEINFM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEINFM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es mezcla infusión (BIT). Flag: 1=Método infusión mezcla, 0=Otro método. Identifica tipo administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESMEZINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Es Mezcla Infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESMEZINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESMEZINFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía administración bolo mezcla (VARCHAR 20). Referencia catálogo vías para bolos: IV rápido, IM, SC, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODVIABOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Codigo de la Via de Administracion Bolo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODVIABOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODVIABOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida bolo medicamento mezcla (VARCHAR 20). Escala: mg, mcg, UI, mEq, ml, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis bolo medicamento mezcla (NUMERIC 18,2). Cantidad a aplicar en cada bolo de medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISBOLM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Dosis Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISBOLM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISBOLM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es bolo medicamento mezcla (BIT). Flag: 1=Contiene bolo medicamento, 0=No contiene.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLMEDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Es Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLMEDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLMEDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad duración fija bolo mezcla (CHAR 1): 1=Minutos, 2=Horas, 3=Días. Escala temporal para VADUBOMEZ.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNDUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad del Valor de la duracion fija del bolo mezcla:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNDUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNDUBOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor duración fija bolo mezcla (INT). Tiempo aplicación bolo en unidades especificadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VADUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Valor de la duracion fija de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VADUBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VADUBOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida bolo mezcla (VARCHAR 20). Escala: mg, ml, UI, mEq, unidades farmacéuticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNMEBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla  - Unidad de Medida Bolo Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNMEBOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNMEBOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis bolo mezcla (NUMERIC 18,2). Cantidad específica de mezcla en cada aplicación bolo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Dosis Bolo mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOBOLOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es bolo mezcla (BIT). Flag: 1=Incluye bolo mezcla, 0=No incluye. Identifica método infusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Es Bolo Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLOMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLOMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida diluyente mezcla (VARCHAR 20). Escala: ml, L, solución normal, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida del Diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pasar diluyente mezcla (NUMERIC 18,2). Volumen diluyente necesario para preparar mezcla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANPASDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad a pasar del diluyente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANPASDIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANPASDIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código producto diluyente mezcla (CHAR 20, FK IHLISTPRO). Identifica líquido diluyente: suero fisiológico, dextrosa, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Codigo del Producto (Diluyente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPRODIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo infusión anterior líquido (CHAR 250). Razón pausa: reacción, flebitis, fin tratamiento, orden médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MOTANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Motivo Infusion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MOTANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'MOTANTINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica infusión líquido ocurrió fecha anterior (BIT). Flag: 1=Pausada previamente, 0=No pausada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Indica si la infusion ocurrio en una Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECANTINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECANTINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad duración fija infusión líquido (CHAR 1): 1=Minutos, 2=Horas, 3=Días. Escala para VALDURINF.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad del Valor de la duracion fija de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIDURINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor duración fija infusión líquido (INT). Tiempo total administración en unidades especificadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VALDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Valor de la duracion fija de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VALDURINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'VALDURINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duracion dosis infusión líquido (CHAR 20). Tipo: continua, intermitente, única, fraccionada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DURACIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Duracion de la Dosis de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DURACIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DURACIINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial infusión líquido (DATETIME). Inicio administración registro temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECINIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Fecha Inicial de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECINIINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FECINIINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad frecuencia infusión líquido (CHAR 1): 1=Minutos, 2=Horas, 3=Días. Escala para FRECUEINF.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIFREINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad del Valor de la Frecuencia de la infusion:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIFREINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIFREINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia infusión líquido (INT). Intervalo repetición administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FRECUEINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Frecuencia de la infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FRECUEINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'FRECUEINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida bolo medicamento líquido (VARCHAR 20). Escala: mg, mcg, UI, ml, mEq.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad de Medida Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis infusión líquido (NUMERIC 18,2). Cantidad medicamento por aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Dosis Infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISINFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es líquido infusión (BIT). Flag: 1=Método infusión líquido, 0=Otro método.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESLIQINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Es Liquido Infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESLIQINFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESLIQINFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad medida bolo medicamento líquido (VARCHAR 20). Escala: mg, mcg, UI, ml, mEq, cc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Unidad de Medida Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UNIMEDBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis bolo líquido (NUMERIC 18,2). Cantidad medicamento por administración bolo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISBOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Dosis Bolo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISBOLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'DOSISBOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es bolo medicamento líquido (BIT). Flag: 1=Contiene bolo, 0=No contiene.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLMEDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Es Bolo Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLMEDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ESBOLMEDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad inicial producto calculada (INT). Volumen o cantidad total calculada para preparación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Inicial de producto calculada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANPROCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CANPROCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura producto mezcla (CHAR 200). Código corto visualización en prescripciones: ej. D5W, SF 0.9%, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método aplicación medicamento (CHAR 1): 1=Bolo Mezcla, 2=Infusión Mezcla, 3=Bolo Medicamento Mezcla, 4=Infusión Líquido, 5=Bolo Medicamento Líquido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo orden médica (CHAR 1): 1=Mezcla Continua, 2=Líquido, 3=Mezcla Frecuencia, 4=Mezcla Magistral. Clasificación prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Medica:  1. Mezcla Continua  2. Liquido   3. Mezcla Frecuencia   4. Mezcla Magistral      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código producto por tipo registro (CHAR 20, FK IHLISTPRO): Bolo=Diluyente, Infusión=Diluyente, Medicamento=Medicamento, Líquido=Líquido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto por Tipo de Registro   1: Bolo Mezcla - Especifica el Diluyente  2: Infusion - Especifica el Diluyente  3: Bolo Medicamento - Especifica el Medicamento  4: Liquido - Especifica el Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional salud (CHAR 20, Identification_Ofuscado, FK). Identificación médico o profesional prescriptor PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional (CHAR 10). Departamento o servicio donde se prescribe: urgencia, UCI, piso, ambulatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro atención (CHAR 10). Institución o sede: hospital, clínica, centro de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso (CHAR 10). Identificador de episodio de atención o admisión paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código paciente (VARCHAR 25, Identification_Ofuscado, FK INPACIENT). Cédula/identificación/documento paciente PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número folio (NCHAR 10). Identificador documento prescripción médica o historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno tipo historia (CHAR 9). Clasificación tipo documento clínico: HCE, urgencia, consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo (NUMERIC 18, PK, FK HCINFLIQC). Identificador único registro línea detalle orden medicamentosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de líquidos e infusiones ordenados en historia clínica: mezclas magistrales, bolos, diluciones y parámetros de infusión continua para cada paciente hospitalizado. Centraliza la preparación farmacéutica, dosis, frecuencias, vías de administración y motivos de suspensión de terapias intravenosas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQD';
