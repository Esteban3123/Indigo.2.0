CREATE TABLE [dbo].[ADTRIAGEU] (
    [TRIANUMER]        CHAR (20)                                                                        NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [NUMINGRES]        CHAR (10)                                                                        NULL,
    [TRIAFECHA]        DATETIME                                                                         NOT NULL,
    [CODCONCEC]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [TRIAGEDAD]        TINYINT                                                                          NOT NULL,
    [TRIUNIEDA]        TINYINT                                                                          NOT NULL,
    [TRIMOTCON]        VARCHAR (MAX)                                                                    NULL,
    [TRITENSIO]        CHAR (10)                                                                        NOT NULL,
    [TRIFRECUC]        CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRate_Ofuscado", 0)')         NOT NULL,
    [TRIFRECUR]        CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRespiratory_Ofuscado", 0)')  NOT NULL,
    [TRIAGETEM]        CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Temperature_Ofuscado", 0)')       NULL,
    [TRIAGESO2]        CHAR (10)                                                                        NULL,
    [TRIAGEOBS]        VARCHAR (MAX)                                                                    NULL,
    [TRIAGECLA]        TINYINT                                                                          NOT NULL,
    [TRIAGEDIA]        TINYINT                                                                          NULL,
    [TRIAGECOR]        TINYINT                                                                          NULL,
    [TRIAGEACV]        TINYINT                                                                          NULL,
    [TRIAGECON]        TINYINT                                                                          NULL,
    [TRIAGEHIP]        TINYINT                                                                          NULL,
    [TRIAGEPUL]        TINYINT                                                                          NULL,
    [TRIAGEOTR]        TINYINT                                                                          NULL,
    [TRIOTROSD]        CHAR (254)                                                                       NULL,
    [TRIACIRUG]        CHAR (254)                                                                       NULL,
    [TRIALERGI]        CHAR (254)                                                                       NULL,
    [TRIADROGA]        CHAR (254)                                                                       NULL,
    [TRIAEXTEN]        TINYINT                                                                          NULL,
    [TRIAREMIS]        TINYINT                                                                          NULL,
    [TRIACAMIN]        TINYINT                                                                          NULL,
    [TRIAVEHIC]        TINYINT                                                                          NULL,
    [TRIAAMBUL]        TINYINT                                                                          NULL,
    [TRIAPOLIC]        TINYINT                                                                          NULL,
    [TRIACOLLA]        TINYINT                                                                          NULL,
    [TRIATABLA]        TINYINT                                                                          NULL,
    [TRIAFERUL]        TINYINT                                                                          NULL,
    [TRIAOXIGE]        TINYINT                                                                          NULL,
    [TRIAGELEV]        TINYINT                                                                          NULL,
    [TRIAINTUB]        TINYINT                                                                          NULL,
    [TRIAGESNG]        TINYINT                                                                          NULL,
    [TRIAVESIC]        TINYINT                                                                          NULL,
    [TRIATUBTO]        TINYINT                                                                          NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [TRIAHELIC]        TINYINT                                                                          NULL,
    [TRIACCIDE]        TINYINT                                                                          NULL,
    [TRIAGSOAT]        TINYINT                                                                          NULL,
    [TRIAENFER]        TINYINT                                                                          NULL,
    [TRIACAUIN]        TINYINT                                                                          NULL,
    [TRIESTADO]        CHAR (1)                                                                         NULL,
    [TRIACATEG]        CHAR (5)                                                                         NULL,
    [CODPROSA1]        CHAR (20)                                                                        NULL,
    [CODCENATE]        CHAR (10)                                                                        NULL,
    [CODENTIDA]        CHAR (9)                                                                         NULL,
    [TRIHOSREC]        TINYINT                                                                          NULL,
    [TRIPARRES]        TINYINT                                                                          NULL,
    [TRIINFREC]        TINYINT                                                                          NULL,
    [TRICOXHUB]        TINYINT                                                                          NULL,
    [TRIESTCON]        CHAR (50)                                                                        NULL,
    [TRIPESOKG]        CHAR (9)                                                                         NULL,
    [TRIALIALC]        TINYINT                                                                          NULL,
    [TRIPLANIF]        TINYINT                                                                          NULL,
    [TRICARBOM]        TINYINT                                                                          NULL,
    [TRIAGTAXI]        TINYINT                                                                          NULL,
    [TRIFECCON]        DATETIME                                                                         NULL,
    [CODDIAGN1]        CHAR (4)                                                                         NULL,
    [CODDIAGN2]        CHAR (4)                                                                         NULL,
    [TRIPARTRE]        TINYINT                                                                          NULL,
    [TRREMITID]        BIT                                                                              NOT NULL,
    [CODESPECI]        CHAR (3)                                                                         NULL,
    [TRIORIGEN]        CHAR (1)                                                                         NOT NULL,
    [TRIVICONF]        BIT                                                                              NULL,
    [PRIMERLLA]        BIT                                                                              NULL,
    [SEGUNDLLA]        BIT                                                                              NULL,
    [TERCERLLA]        BIT                                                                              NULL,
    [FECPRILLA]        DATETIME                                                                         NULL,
    [FECSEGLLA]        DATETIME                                                                         NULL,
    [FECTERLLA]        DATETIME                                                                         NULL,
    [PROPRILLA]        CHAR (20)                                                                        NULL,
    [PROSEGLLA]        CHAR (20)                                                                        NULL,
    [PROTERLLA]        CHAR (20)                                                                        NULL,
    [INDAUDFOR]        NUMERIC (18)                                                                     NOT NULL,
    [FECHINITR]        DATETIME                                                                         NULL,
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECREVTR]         DATETIME                                                                         NULL,
    [TRITALLA]         CHAR (10)                                                                        NULL,
    [CRITPOTENTRANSMI] INT                                                                              NULL,
    [PregnancyStatus]  INT                                                                              NULL,
    [FetocardiaTriage] INT                                                                              NULL,
    CONSTRAINT [PK_ADTRIAGEU_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADTRIAGEU_ADCONTURG] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADCONTURG] ([CODCONCEC]),
    CONSTRAINT [FK_ADTRIAGEU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADTRIAGEU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADTRIAGEU] NOCHECK CONSTRAINT [FK_ADTRIAGEU_ADCONTURG];


GO
ALTER TABLE [dbo].[ADTRIAGEU] NOCHECK CONSTRAINT [FK_ADTRIAGEU_ADINGRESO];


GO
ALTER TABLE [dbo].[ADTRIAGEU] NOCHECK CONSTRAINT [FK_ADTRIAGEU_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADTRIAGEU]
    ON [dbo].[ADTRIAGEU]([CODCONCEC] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_ADTRIAGEU_CODCENATE_NUMINGRES_TRIAFECHA]
    ON [dbo].[ADTRIAGEU]([CODCENATE] ASC, [NUMINGRES] ASC, [TRIAFECHA] ASC)
    INCLUDE([TRIANUMER], [CODCONCEC], [TRIAGECLA], [CODPROSAL], [TRIACATEG], [CODESPECI], [TRIORIGEN], [FECHINITR]);


GO
CREATE NONCLUSTERED INDEX [IX_ADTRIAGEU__IPCODPACI__NUMINGRES__INC__TRIANUMER]
    ON [dbo].[ADTRIAGEU]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([TRIANUMER]);


GO
CREATE NONCLUSTERED INDEX [IX_ADTRIAGEU_TRIANUMER_FECHINITR]
    ON [dbo].[ADTRIAGEU]([TRIANUMER] ASC)
    INCLUDE([FECHINITR]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fetocardia triage : en esta columna se almacenara el dato ingresado desde el formulario triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FetocardiaTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'en estado de embarazo : en este campo se almacenara los valores   1 (True) --> Si  2 (False) --> No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PregnancyStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterios para enfermedades potencialmente transmisibles:   1 - Si    2 - No   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CRITPOTENTRANSMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad  10. Accidente Laboral  11. Cirugia Programada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Triage  Nota: Codigo Consecutivo 00000002', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIANUMER';

