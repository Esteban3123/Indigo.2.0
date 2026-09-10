CREATE TABLE [dbo].[HCSESHEM] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NULL,
    [NUMSILLA]        VARCHAR (20)                                                                     NULL,
    [PESOSECO]        NUMERIC (18, 2)                                                                  NULL,
    [PESOPREDIALISIS] NUMERIC (18, 2)                                                                  NULL,
    [ACCESOVASCULAR]  TINYINT                                                                          NULL,
    [FILTRO]          NUMERIC (18, 2)                                                                  NULL,
    [ULTRAFILTRACION] NUMERIC (18, 2)                                                                  NULL,
    [HEPARINA]        NUMERIC (18, 2)                                                                  NULL,
    [NOTENFINIID]     INT                                                                              NULL,
    [FECINISES]       DATETIME                                                                         NULL,
    [USUINISES]       CHAR (20)                                                                        NULL,
    [PESOPOST]        NUMERIC (18, 2)                                                                  NULL,
    [VOLSANGTRA]      NUMERIC (18, 2)                                                                  NULL,
    [NOTENFFINID]     INT                                                                              NULL,
    [FECFINSES]       DATETIME                                                                         NULL,
    [USUFINSES]       CHAR (20)                                                                        NULL,
    [FECCREREG]       DATETIME                                                                         NULL,
    [FECMODREG]       DATETIME                                                                         NULL,
    [HORASES]         INT                                                                              NULL,
    [MINSES]          INT                                                                              NULL,
    [ESTADO]          INT                                                                              NULL,
    [HOMGLU]          INT                                                                              NULL,
    [NUMEROFISTULA]   INT                                                                              NULL,
    CONSTRAINT [PK_HCSESHEM] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSESHEM_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSESHEM_HCCTRNOTE] FOREIGN KEY ([NOTENFFINID]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_HCSESHEM_HCCTRNOTE1] FOREIGN KEY ([NOTENFINIID]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_HCSESHEM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCSESHEM].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de fístulas arteriovenosas utilizadas en la sesión de hemodiálisis (INT, 0-N)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMEROFISTULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la cantidad de Fistula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMEROFISTULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMEROFISTULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de glucosa medida en sangre durante la sesión, glucometría (INT, unidad mg/dL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HOMGLU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la cantidad de Glucomatria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HOMGLU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HOMGLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la sesión de hemodiálisis: 1=Proceso, 2=Finalizada (INT, 1-2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Sesion 1 - Proceso 2- Finalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos de la sesión de hemodiálisis (INT, 0-1440)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'MINSES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Minutos de Sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'MINSES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'MINSES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en horas de la sesión de hemodiálisis (INT, 0-24)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HORASES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora  de Sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HORASES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HORASES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de sesión (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECMODREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECMODREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECMODREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de sesión (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario profesional de salud que cierra/finaliza la sesión (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUFINSES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que finaliza la sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUFINSES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUFINSES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización de la sesión de hemodiálisis (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECFINSES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de fin de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECFINSES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECFINSES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nota de enfermería de cierre/fin de sesión en HCCTRNOTE (FK INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFFINID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la nota de enfermería de fin de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFFINID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFFINID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen de sangre tratada/ultrafiltrada durante la sesión en ml (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'VOLSANGTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bolsa de sangre tratada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'VOLSANGTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'VOLSANGTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente al finalizar la sesión de hemodiálisis en kg (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPOST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'peso post', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPOST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario profesional de salud que inicia la sesión (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUINISES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que inicia la sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUINISES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'USUINISES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la sesión de hemodiálisis (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECINISES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECINISES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FECINISES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nota de enfermería de apertura/inicio de sesión en HCCTRNOTE (FK INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFINIID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la nota de enfermería de inicio de sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFINIID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NOTENFINIID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de heparina administrada en la sesión en unidades internacionales (UI), rango 0-10000 (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HEPARINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Heparina  Unidad: ui  desde 0 -10000', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HEPARINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'HEPARINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen de ultrafiltración realizada en ml, rango 0-9999 (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ULTRAFILTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultra Filtración   Unidad:ml  desde 0 - 9999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ULTRAFILTRACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ULTRAFILTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo/número de filtro dialítico utilizado, valor decimal 0-5 (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FILTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Filtro, decimal desde 0 hasta 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FILTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'FILTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de acceso vascular para hemodiálisis: 1=Catéter, 2=Fístula arteriovenosa (TINYINT, 1-2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ACCESOVASCULAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Acceso Vascular:  1-> Cateter  2-> Fístula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ACCESOVASCULAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ACCESOVASCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente antes de iniciar la sesión en kg o gr según edad (<3 meses), máx 3 dígitos (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPREDIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pero predialisis  "validación de peso en gr o kr segun edad, <= 3 meses gr" 3 digitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPREDIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOPREDIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso seco ideal del paciente desde historia clínica, no modificable durante sesión (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOSECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso seco, se trae de la historia clínica y no se permite modificar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOSECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'PESOSECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador de la silla/puesto de hemodiálisis donde se atiende al paciente (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMSILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de silla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMSILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMSILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional de nefrología/hemodiálisis donde se realiza la sesión (CHAR 10, FK a UNIDADES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/clínica donde se realiza la sesión de hemodiálisis (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso/atención del paciente, enlaza a ADINGRESO (CHAR 10, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, identificación, documento PII ofuscado), enlaza a INPACIENT (VARCHAR 25, FK, Identification_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de sesión de hemodiálisis (INT, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de sesiones de hemodiálisis por paciente e ingreso. Guarda los parámetros clínicos y técnicos de cada sesión: pesos, acceso vascular, filtro, ultrafiltración, heparina, duración y notas de enfermería de inicio y fin.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEM';
