CREATE TABLE [dbo].[ADTRIAGEI] (
    [TRIANUMER] CHAR (20)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [NUMINGRES] CHAR (10)                                                                        NULL,
    [TRIAFECHA] DATETIME                                                                         NOT NULL,
    [CODCONCEC] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [TRIAGEDAD] TINYINT                                                                          NOT NULL,
    [TRIUNIEDA] TINYINT                                                                          NOT NULL,
    [TRIMOTCON] VARCHAR (MAX)                                                                    NOT NULL,
    [TRITENSIO] CHAR (10)                                                                        NOT NULL,
    [TRIFRECUC] CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRate_Ofuscado", 0)')         NOT NULL,
    [TRIFRECUR] CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRespiratory_Ofuscado", 0)')  NOT NULL,
    [TRIAGETEM] CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Temperature_Ofuscado", 0)')       NOT NULL,
    [TRIAGESO2] CHAR (10)                                                                        NOT NULL,
    [TRIAGEOBS] VARCHAR (4000)                                                                   NOT NULL,
    [TRIAGECLA] TINYINT                                                                          NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [TRIACAUIN] TINYINT                                                                          NULL,
    [TRIESTADO] CHAR (1)                                                                         NULL,
    [TRIACATEG] CHAR (5)                                                                         NULL,
    [CODPROSA1] CHAR (20)                                                                        NULL,
    [CODCENATE] CHAR (10)                                                                        NULL,
    [CODENTIDA] CHAR (9)                                                                         NULL,
    [TRIESTCON] CHAR (50)                                                                        NULL,
    [TRIPESOKG] CHAR (7)                                                                         NULL,
    [TRIALIALC] TINYINT                                                                          NULL,
    [TRIFECCON] DATETIME                                                                         NULL,
    [CODDIAGN1] CHAR (4)                                                                         NULL,
    [CODDIAGN2] CHAR (4)                                                                         NULL,
    [TRREMITID] BIT                                                                              NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NULL,
    [TRIORIGEN] CHAR (1)                                                                         NOT NULL,
    [TRIVICONF] BIT                                                                              NULL,
    [PRIMERLLA] BIT                                                                              NULL,
    [SEGUNDLLA] BIT                                                                              NULL,
    [TERCERLLA] BIT                                                                              NULL,
    [FECPRILLA] DATETIME                                                                         NULL,
    [FECSEGLLA] DATETIME                                                                         NULL,
    [FECTERLLA] DATETIME                                                                         NULL,
    [PROPRILLA] CHAR (20)                                                                        NULL,
    [PROSEGLLA] CHAR (20)                                                                        NULL,
    [PROTERLLA] CHAR (20)                                                                        NULL,
    [FECHINITR] DATETIME                                                                         NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [TRIAGEDIA] TINYINT                                                                          NULL,
    [TRIAGECOR] TINYINT                                                                          NULL,
    [TRIAGEACV] TINYINT                                                                          NULL,
    [TRIAGECON] TINYINT                                                                          NULL,
    [TRIAGEHIP] TINYINT                                                                          NULL,
    [TRIAGEPUL] TINYINT                                                                          NULL,
    [TRIAGEOTR] TINYINT                                                                          NULL,
    [TRIOTROSD] CHAR (254)                                                                       NULL,
    [TRIACIRUG] CHAR (254)                                                                       NULL,
    [TRIALERGI] CHAR (254)                                                                       NULL,
    [TRIADROGA] CHAR (254)                                                                       NULL,
    [TRIAEXTEN] TINYINT                                                                          NULL,
    [TRIAREMIS] TINYINT                                                                          NULL,
    [TRIACAMIN] TINYINT                                                                          NULL,
    [TRIAVEHIC] TINYINT                                                                          NULL,
    [TRIAAMBUL] TINYINT                                                                          NULL,
    [TRIAPOLIC] TINYINT                                                                          NULL,
    [TRIACOLLA] TINYINT                                                                          NULL,
    [TRIATABLA] TINYINT                                                                          NULL,
    [TRIAFERUL] TINYINT                                                                          NULL,
    [TRIAOXIGE] TINYINT                                                                          NULL,
    [TRIAGELEV] TINYINT                                                                          NULL,
    [TRIAINTUB] TINYINT                                                                          NULL,
    [TRIAGESNG] TINYINT                                                                          NULL,
    [TRIAVESIC] TINYINT                                                                          NULL,
    [TRIATUBTO] TINYINT                                                                          NULL,
    [TRIAHELIC] TINYINT                                                                          NULL,
    [TRIACCIDE] TINYINT                                                                          NULL,
    [TRIAGSOAT] TINYINT                                                                          NULL,
    [TRIAENFER] TINYINT                                                                          NULL,
    [TRIHOSREC] TINYINT                                                                          NULL,
    [TRIPARRES] TINYINT                                                                          NULL,
    [TRIINFREC] TINYINT                                                                          NULL,
    [TRICOXHUB] TINYINT                                                                          NULL,
    [TRIPLANIF] TINYINT                                                                          NULL,
    [TRICARBOM] TINYINT                                                                          NULL,
    [TRIAGTAXI] TINYINT                                                                          NULL,
    [TRIPARTRE] TINYINT                                                                          NULL,
    CONSTRAINT [PK_ADTRIAGEI] PRIMARY KEY CLUSTERED ([TRIANUMER] ASC),
    CONSTRAINT [FK_ADTRIAGEI_ADCONTURG] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADCONTURG] ([CODCONCEC]),
    CONSTRAINT [FK_ADTRIAGEI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADTRIAGEI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADTRIAGEI] NOCHECK CONSTRAINT [FK_ADTRIAGEI_ADCONTURG];


GO
ALTER TABLE [dbo].[ADTRIAGEI] NOCHECK CONSTRAINT [FK_ADTRIAGEI_ADINGRESO];


GO
ALTER TABLE [dbo].[ADTRIAGEI] NOCHECK CONSTRAINT [FK_ADTRIAGEI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Triage  Nota: Codigo Consecutivo 00000002', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIANUMER';

