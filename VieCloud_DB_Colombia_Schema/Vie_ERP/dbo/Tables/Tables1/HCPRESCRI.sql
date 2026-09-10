CREATE TABLE [dbo].[HCPRESCRI] (
    [IDETIPHIS]                   CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                   CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                   CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]                   CHAR (20)                                                                        NOT NULL,
    [CODVIAADM]                   VARCHAR (20)                                                                     NOT NULL,
    [CODFORMED]                   VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD]                   NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]                   VARCHAR (20)                                                                     NULL,
    [FRECUENCI]                   INT                                                                              NULL,
    [UNIFRECUE]                   CHAR (1)                                                                         NULL,
    [FECINIDOS]                   DATETIME                                                                         NOT NULL,
    [TIPFORMED]                   CHAR (1)                                                                         NOT NULL,
    [DURACIDOS]                   CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]                   INT                                                                              NULL,
    [UNIDURFIJ]                   CHAR (1)                                                                         NULL,
    [FECFINDOS]                   DATETIME                                                                         NULL,
    [PREFECANT]                   BIT                                                                              NOT NULL,
    [MOTPREANT]                   CHAR (250)                                                                       NULL,
    [PREESTADO]                   INT                                                                              NOT NULL,
    [CODCONCEC]                   NUMERIC (18)                                                                     NOT NULL,
    [CODDIAGNO]                   CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [INDAPLMED]                   VARCHAR (MAX)                                                                    NULL,
    [MOTSUSMED]                   VARCHAR (2000)                                                                   NULL,
    [NIVRIEPAC]                   CHAR (1)                                                                         NULL,
    [OBSNIVRIE]                   VARCHAR (1000)                                                                   NULL,
    [MANEXTPRO]                   BIT                                                                              NOT NULL,
    [CANPEDPRO]                   INT                                                                              NOT NULL,
    [NUMFOLSUS]                   CHAR (10)                                                                        NULL,
    [TOTPROUNI]                   NUMERIC (18, 2)                                                                  NULL,
    [MEDPENAGE]                   BIT                                                                              NULL,
    [FORMUMANU]                   BIT                                                                              NULL,
    [DESADMINI]                   VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR]                   NUMERIC (18)                                                                     NOT NULL,
    [DOSISPRFN]                   NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMFN]                   CHAR (3)                                                                         NULL,
    [OBSJUSMEE]                   VARCHAR (500)                                                                    NULL,
    [CODJUMEES]                   CHAR (3)                                                                         NULL,
    [FORMAPRESCRIBE]              TINYINT                                                                          NULL,
    [NUMERODOSIS]                 TINYINT                                                                          NULL,
    [DOSISPROD1]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD2]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD3]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD4]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD5]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD6]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD7]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD8]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD9]                  NUMERIC (18, 2)                                                                  NULL,
    [DOSISPROD10]                 NUMERIC (18, 2)                                                                  NULL,
    [HORA1]                       DATETIME                                                                         NULL,
    [HORA2]                       DATETIME                                                                         NULL,
    [HORA3]                       DATETIME                                                                         NULL,
    [HORA4]                       DATETIME                                                                         NULL,
    [HORA5]                       DATETIME                                                                         NULL,
    [HORA6]                       DATETIME                                                                         NULL,
    [HORA7]                       DATETIME                                                                         NULL,
    [HORA8]                       DATETIME                                                                         NULL,
    [HORA9]                       DATETIME                                                                         NULL,
    [HORA10]                      DATETIME                                                                         NULL,
    [JUSTIFICARB]                 VARCHAR (500)                                                                    NULL,
    [JUSTIFICACIONPBS]            VARCHAR (2000)                                                                   NULL,
    [ReasonDiscontinuationOfDrug] INT                                                                              NULL,
    CONSTRAINT [PK_HCPRESCRI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [CODPRODUC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCPRESCRI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRESCRI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESCRI_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCPRESCRI_IHFORMEDI] FOREIGN KEY ([CODFORMED]) REFERENCES [dbo].[IHFORMEDI] ([CODFORMED]),
    CONSTRAINT [FK_HCPRESCRI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCPRESCRI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESCRI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPRESCRI_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRI].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRI_IPCODPACI_NUMEFOLIO_NUMINGRES]
    ON [dbo].[HCPRESCRI]([IPCODPACI] ASC, [NUMEFOLIO] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón de descontinuación del medicamento (INT): 1=Riesgos y reacciones adversas farmacológicas, 2=Otra razón o motivo. Motivo de suspensión de la terapia farmacológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razon descontinuacion de medicamento 1. Riesgos y reacciones adversas de medicamentos 2. Otra Razón o motivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (VARCHAR 2000): argumento médico para autorización de medicamento en lista de beneficios, protocolos clínicos, criterios de restricción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de medicamento con resistencia bacteriana (VARCHAR 500): fundamento clínico para prescripción de antibióticos ante patógenos resistentes, uso de antimicrobianos especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de medicamento con resistencia bacteriana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la décima dosis (DATETIME): timestamp de administración del medicamento en la décima toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la novena dosis (DATETIME): timestamp de administración del medicamento en la novena toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la octava dosis (DATETIME): timestamp de administración del medicamento en la octava toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la séptima dosis (DATETIME): timestamp de administración del medicamento en la séptima toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la sexta dosis (DATETIME): timestamp de administración del medicamento en la sexta toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la quinta dosis (DATETIME): timestamp de administración del medicamento en la quinta toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la cuarta dosis (DATETIME): timestamp de administración del medicamento en la cuarta toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la tercera dosis (DATETIME): timestamp de administración del medicamento en la tercera toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la segunda dosis (DATETIME): timestamp de administración del medicamento en la segunda toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de la primera dosis (DATETIME): timestamp de administración del medicamento en la primera toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'HORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Décima dosis (NUMERIC 18,2): cantidad de medicamento en la décima administración, cantidad de toma 10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novena dosis (NUMERIC 18,2): cantidad de medicamento en la novena administración, cantidad de toma 9.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Octava dosis (NUMERIC 18,2): cantidad de medicamento en la octava administración, cantidad de toma 8.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Séptima dosis (NUMERIC 18,2): cantidad de medicamento en la séptima administración, cantidad de toma 7.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexta dosis (NUMERIC 18,2): cantidad de medicamento en la sexta administración, cantidad de toma 6.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta dosis (NUMERIC 18,2): cantidad de medicamento en la quinta administración, cantidad de toma 5.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta dosis (NUMERIC 18,2): cantidad de medicamento en la cuarta administración, cantidad de toma 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera dosis (NUMERIC 18,2): cantidad de medicamento en la tercera administración, cantidad de toma 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda dosis (NUMERIC 18,2): cantidad de medicamento en la segunda administración, cantidad de toma 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera dosis (NUMERIC 18,2): cantidad de medicamento en la primera administración, cantidad de toma 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis (TINYINT): cantidad total de tomas programadas del medicamento, número de administraciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de prescripción (TINYINT): 0=Estándar (esquema común), 1=Personalizada (adaptada al paciente), tipo de formulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de prescripción: 0:Estandar, 1:Personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de justificación de medicamento especial (CHAR 3): referencia al catálogo de justificaciones para medicamentos de alto costo o restringidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de meticamento especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de solicitud de medicamentos especiales (VARCHAR 500): argumentación clínica para aprobación de medicamentos no formulario, medicamentos de manejo especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la solicitud de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida final (CHAR 3): unidad de medicamento después de conversiones (g, mg, mcg, UI), unidad normalizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la Unidad Medida final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis final después de conversión (NUMERIC 18,2): cantidad de medicamento normalizada tras conversión de unidades de medida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la dosis final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control auditoría (NUMERIC 18): indicador de registro auditado, trazabilidad de cambios en prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administración del medicamento - dosis manual (VARCHAR MAX): instrucciones específicas de aplicación, notas de administración manual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion del medicamento (Dosis Manual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formulación manual (BIT): 1=formulación manualmente ingresada, 0=formulación estándar del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulacion Manual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FORMUMANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento pendiente de agendar (BIT): 1=medicamento pendiente en hoja de medicamentos, 0=ya agendado, estado de agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento Pendiente de Agendar en la Hoja de Medicamentos:  True => pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del producto por unidad (NUMERIC 18,2): cantidad total de medicamento dispensado por unidad terapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del producto por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de suspensión (CHAR 10): referencia al folio donde se suspendió el medicamento, trazabilidad de cambio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio desde Donde se Suspendio el Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida del producto (INT): cantidad solicitada de medicamento en la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto en plan de manejo externo (BIT): 1=sí, medicamentos entregados por tercero externo, 0=no, distribución interna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de niveles de riesgo (VARCHAR 1000): notas sobre riesgo farmacológico, contraindicaciones, advertencias del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'OBSNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de los niveles de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'OBSNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'OBSNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de riesgo del paciente (CHAR 1): clasificación de riesgo farmacológico: bajo, medio, alto frente al medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NIVRIEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de riesgo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NIVRIEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NIVRIEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de suspensión del medicamento (VARCHAR 2000): razón clínica de discontinuación, cambio de terapia, efectos adversos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de suspesion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de aplicación del medicamento (VARCHAR MAX): condiciones clínicas, síntomas, diagnósticos indicados para el medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CHAR 4, DiagnosticCode_Ofuscado): código CIE-10 principal justificante de la prescripción, diagnóstico principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico principal o razon principal por la solicitud del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de orden médica (NUMERIC 18): identificador único de la orden médica que origina la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de la Orden Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del medicamento (INT): 1=Iniciado, 2=Ciclo completado, 3=Descontinuado (modificación), 4=Suspendido, 5=Manejo externo, 6=Sin existencia, 7=Terminado por salida. Estado de la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Medicamento  1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez  2: Ciclo Completado  3: Tratamiento descontinuado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia  4: Tratamiento Suspendido: Cuando el Medicamento es Suspendido  5: Plan de Manejo Externo: Cuando los Medicamentos son entregados por un Tercero  6: Medicamentos Solicitados sin Existencia Actual en el Kardex.  7: Tratamiento Terminado por Salida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo prescripción anterior (CHAR 250): razón de cambio de prescripción previa, justificación de modificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo Prescripcion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'MOTPREANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prescripción fecha anterior (BIT): 1=prescripción con fecha anterior a registro actual, indicador de retroactividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prescripcion Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'PREFECANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de dosis (DATETIME): fecha de término del tratamiento, última toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de duración fija (CHAR 1): 1=Minutos, 2=Horas, 3=Días, unidad temporal de la duración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de duración fija (INT): cantidad numérica de la duración del tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la dosis (CHAR 20): período total de tratamiento, intervalo terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulación del medicamento (CHAR 1): 1=Peso, 2=Volumen, 3=Peso-Volumen, 4=Unidad de administración, presentación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial dosis (DATETIME): fecha de inicio del tratamiento, primera toma programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FECINIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de frecuencia (CHAR 1): 1=Minutos, 2=Horas, 3=Días, unidad temporal de repetición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia (INT): número de veces que se repite la administración en la unidad temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida (VARCHAR 20, FK INUNIMEDI): unidad de cuantificación del medicamento (mg, ml, UI, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis (NUMERIC 18,2): cantidad de medicamento por administración, cantidad de toma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de forma de presentación (VARCHAR 20, FK IHFORMEDI): formato del medicamento (tableta, inyectable, jarabe, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de vía de administración (VARCHAR 20, FK HCVIAADMI): ruta de aplicación (oral, IV, IM, subcutánea, tópica, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto (CHAR 20): identificador del medicamento, producto farmacológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (VARCHAR 25, Identification_Ofuscado): identificación del prescriptor, médico que ordena.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10, FK INUNIFUNC): servicio clínico que prescribe (urgencias, hospitalización, consulta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK ADCENATEN): institución de salud donde se prescribe, sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso (CHAR 10, FK ADINGRESO): identificador del episodio de atención, entrada del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, Identification_Ofuscado): identificación del paciente, cédula, documento PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (CHAR 10): identificador secuencial único de la prescripción en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno tipo historia (CHAR 9): identificador del tipo de registro de historia clínica electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prescripciones médicas de medicamentos realizadas durante un ingreso hospitalario. Registra cada fórmula o receta médica con su producto, dosis, vía de administración, frecuencia, duración y estado, vinculando al paciente, profesional de salud y diagnóstico asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRI';
