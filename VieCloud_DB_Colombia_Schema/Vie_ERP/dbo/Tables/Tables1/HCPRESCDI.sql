CREATE TABLE [dbo].[HCPRESCDI] (
    [CODCONCEC]        NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]        CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [CODVIAADM]        CHAR (3)                                                                         NOT NULL,
    [CODFORMED]        VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD]        NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]        CHAR (3)                                                                         NULL,
    [FRECUENCI]        INT                                                                              NULL,
    [UNIFRECUE]        CHAR (1)                                                                         NULL,
    [FECINIDOS]        DATETIME                                                                         NOT NULL,
    [TIPFORMED]        CHAR (1)                                                                         NOT NULL,
    [DURACIDOS]        CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]        INT                                                                              NULL,
    [UNIDURFIJ]        CHAR (1)                                                                         NULL,
    [FECFINDOS]        DATETIME                                                                         NULL,
    [CANPEDPRO]        INT                                                                              NOT NULL,
    [PREFECANT]        BIT                                                                              NOT NULL,
    [MOTPREANT]        CHAR (250)                                                                       NULL,
    [PREESTADO]        INT                                                                              NOT NULL,
    [INDAPLMED]        VARCHAR (MAX)                                                                    NULL,
    [MANEXTPRO]        BIT                                                                              NOT NULL,
    [FOLIOINIC]        NCHAR (10)                                                                       NOT NULL,
    [TRATMODIF]        BIT                                                                              NOT NULL,
    [FORMUMANU]        BIT                                                                              NULL,
    [DESADMINI]        VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR]        NUMERIC (18)                                                                     NOT NULL,
    [DOSISPRFN]        NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMFN]        CHAR (3)                                                                         NULL,
    [OBSJUSMEE]        VARCHAR (MAX)                                                                    NULL,
    [CODJUMEES]        CHAR (3)                                                                         NULL,
    [FORMAPRESCRIBE]   TINYINT                                                                          NULL,
    [NUMERODOSIS]      TINYINT                                                                          NULL,
    [DOSISPROD1]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD2]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD3]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD4]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD5]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD6]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD7]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD8]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD9]       NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD10]      NUMERIC (18, 2)                                                                  NULL,
    [HORA1]            DATETIME                                                                         NULL,
    [HORA2]            DATETIME                                                                         NULL,
    [HORA3]            DATETIME                                                                         NULL,
    [HORA4]            DATETIME                                                                         NULL,
    [HORA5]            DATETIME                                                                         NULL,
    [HORA6]            DATETIME                                                                         NULL,
    [HORA7]            DATETIME                                                                         NULL,
    [HORA8]            DATETIME                                                                         NULL,
    [HORA9]            DATETIME                                                                         NULL,
    [HORA10]           DATETIME                                                                         NULL,
    [JUSTIFICARB]      VARCHAR (500)                                                                    NULL,
    [JUSTIFICACIONPBS] VARCHAR (2000)                                                                   NULL,
    CONSTRAINT [PK_HCPRESCDI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCPRESCDI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCDI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCDI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO


EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (Pos formulario), campo VARCHAR(2000), justificativa para medicamentos en lista de beneficios o excepciones de cobertura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de medicamento con resistencia bacteriana, campo VARCHAR(500), argumento clínico para uso de antibióticos con resistencia detectada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de medicamento con resistencia bacteriana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la décima dosis, DATETIME, timestamp del décimo evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la novena dosis, DATETIME, timestamp del noveno evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la octava dosis, DATETIME, timestamp del octavo evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la séptima dosis, DATETIME, timestamp del séptimo evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la sexta dosis, DATETIME, timestamp del sexto evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la quinta dosis, DATETIME, timestamp del quinto evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la cuarta dosis, DATETIME, timestamp del cuarto evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la tercera dosis, DATETIME, timestamp del tercer evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la segunda dosis, DATETIME, timestamp del segundo evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la primera dosis, DATETIME, timestamp del primer evento de administración de medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'HORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Décima dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la décima administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novena dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la novena administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Octava dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la octava administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Séptima dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la séptima administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexta dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la sexta administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la quinta administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la cuarta administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la tercera administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la segunda administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera dosis del producto, NUMERIC(18,2), cantidad y unidad de medicamento en la primera administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis a administrar, TINYINT, cantidad total de eventos de medicamento planificados en la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de prescripción: 0=Estándar, 1=Personalizada, TINYINT, indica modo de generación de la orden médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de prescripción: 0:Estandar, 1:Personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de justificación de medicamentos especiales, CHAR(3), clasificador para tipos de exceptuación farmacéutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de solicitud de medicamentos especiales, VARCHAR(MAX), texto descriptivo de razones clínicas para fármacos de manejo restringido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la solicitud de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida final post-conversión, CHAR(3), almacena gramos/miligramos/microgramos cuando hay ajustes de equivalencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la Unidad Medida final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis final post-conversión, NUMERIC(18,2), valor recalculado después de transformación entre unidades de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la dosis final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de auditoría, NUMERIC(18), indicador de trazabilidad y revisión de la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administración del medicamento, VARCHAR(MAX), instrucciones detalladas sobre forma de aplicación y preparación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion del medicamento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formulación manual, BIT, bandera indicando si la receta fue escrita manualmente vs. generada automáticamente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulacion Manual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FORMUMANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si medicamento fue modificado en orden médica, BIT, trazador de cambios post-prescripción inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si es Un Medicamento que fue Modificado en la Orden Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'TRATMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'TRATMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del folio inicial donde se prescribió, NCHAR(10), referencia a la receta de origen cuando hay enmiendas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio inicial donde se prescribio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FOLIOINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto corresponde a plan de manejo externo, BIT, distingue medicamentos de atención extramural vs. intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de aplicación de medicamentos, VARCHAR(MAX), instrucciones clínicas específicas sobre modo y contexto de uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del medicamento: 1=Interfaz inventario, 2=Sin interfaz (existencia en kardex), 3=Sin interfaz (falta en módulo), 4=Extramural, INT, clasificación de gestión logística', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Medicamento  1: Realiza Interfase con Inventarios  2: No Realiza Interfase con Inventarios x existencia del producto en el Kardex del Paciente correspondiente a la Centro de Atencion y Unidad Funcional Actual  3: No Realiza Interfase con Inventarios x no existencia del producto en el Kardez del modulo de Inventario.  4: Manejo Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo prescripción anterior, CHAR(250), justificación para cambio o continuidad de medicamento previo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo Prescripcion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'MOTPREANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si prescripción ocurrió en fecha anterior, BIT, marca órdenes retroactivas o enmendadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la Prescripcion ocurrio en una Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'PREFECANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida del producto, INT, número de unidades solicitadas al inventario o farmacia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la dosis, DATETIME, cierre del tratamiento o suspensión planificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad duración fija: 1=Minutos, 2=Horas, 3=Días, CHAR(1), tipo de período para tratamiento prolongado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor duración fija, INT, cantidad numérica de minutos/horas/días de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la dosis, CHAR(20), período total de aplicación del medicamento prescrito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo formulación: 1=Peso, 2=Volumen, 3=Peso-Volumen, 4=Unidad administración, CHAR(1), clasificación de presentación farmacéutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial dosis, DATETIME, inicio del plan de medicación prescrito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FECINIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad frecuencia: 1=Minutos, 2=Horas, 3=Días, CHAR(1), escala temporal para intervalos de administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia, INT, número de veces que se administra medicamento en la unidad de tiempo especificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad de medida, CHAR(3), clasificador para gramos/miligramos/ml/UI/comprimidos, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del producto, NUMERIC(18,2), cantidad del medicamento por cada administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código forma de presentación, VARCHAR(20), clasificador de tipo farmacéutico (tableta, ampolla, jarabe, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía de administración común, CHAR(3), clasificador para vía (oral, IV, IM, subcutánea, tópica, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto, CHAR(20), PK identificador único del medicamento/insumo en el catálogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de salud, VARCHAR(20) MASKED, PII, referencia al prescriptor médico/enfermero (FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional, CHAR(10), FK a unidad de negocio donde se atiende (urgencias, hospitalización, ambulatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención, CHAR(10), FK a sede/hospital/clínica donde se prescribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso, CHAR(10), FK a evento de atención/admisión del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, VARCHAR(25) MASKED, PII (cédula/identificación del paciente), FK a tabla INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio, NCHAR(10), identificador único de la receta/orden médica dentro del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno tipo historia, CHAR(9), clasificador de modalidad de registro (HC digital, electrónica, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo, NUMERIC(18), PK generador secuencial de líneas de prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de prescripciones de medicamentos en historia clínica. Registra cada ítem de un folio de prescripción médica: el producto prescrito, dosis, vía de administración, frecuencia, duración del tratamiento y estado de la prescripción, asociado al paciente, ingreso, centro de atención y profesional de la salud que prescribe.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCDI';
