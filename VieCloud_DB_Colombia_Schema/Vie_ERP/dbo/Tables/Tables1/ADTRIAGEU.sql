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
    [FetocardiaTriage] VARCHAR (300)                                                                    NULL,
    CONSTRAINT [PK_ADTRIAGEU_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADTRIAGEU_ADCONTURG] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADCONTURG] ([CODCONCEC]),
    CONSTRAINT [FK_ADTRIAGEU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADTRIAGEU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEU].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEU].[CODCONCEC]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEU].[TRIFRECUC]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEU].[TRIFRECUR]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEU].[TRIAGETEM]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEU].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_ControlTriage]
    ON [dbo].[ADTRIAGEU]([CODCENATE] ASC, [NUMINGRES] ASC);


GO
ALTER INDEX [IX_ControlTriage]
    ON [dbo].[ADTRIAGEU] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ADTRIAGEU]
    ON [dbo].[ADTRIAGEU]([CODCONCEC] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_Triage]
    ON [dbo].[ADTRIAGEU]([NUMINGRES] ASC)
    INCLUDE([TRIAGECLA]);


GO
CREATE NONCLUSTERED INDEX [IDX_ADTRIAGEU_CODCENATE_NUMINGRES_TRIAFECHA]
    ON [dbo].[ADTRIAGEU]([CODCENATE] ASC, [NUMINGRES] ASC, [TRIAFECHA] ASC)
    INCLUDE([TRIANUMER], [CODCONCEC], [TRIAGECLA], [CODPROSAL], [TRIACATEG], [CODESPECI], [TRIORIGEN], [FECHINITR], [IPCODPACI]);


GO
CREATE NONCLUSTERED INDEX [Index_ADTRIAGEU]
    ON [dbo].[ADTRIAGEU]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Interconsultas]
    ON [dbo].[ADTRIAGEU]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADTRIAGEU__IPCODPACI__NUMINGRES__INC__TRIANUMER]
    ON [dbo].[ADTRIAGEU]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([TRIANUMER]);


GO
CREATE NONCLUSTERED INDEX [IX_ADTRIAGEU_TRIANUMER_FECHINITR]
    ON [dbo].[ADTRIAGEU]([TRIANUMER] ASC)
    INCLUDE([FECHINITR]);

GO
CREATE INDEX IX_ADTRIAGEU_RIPS
  ON ADTRIAGEU (NUMINGRES, CODCENATE)
  INCLUDE (IPCODPACI, FECHINITR, TRIAFECHA, TRIAGECLA);


GO
-- Soporta el "último triage por ingreso" (SPCH_ListarPacientesEnObservacion via OUTER APPLY TOP 1 ORDER BY ID DESC).
CREATE NONCLUSTERED INDEX [IX_ADTRIAGEU_NUMINGRES_ID_INC_TRIAGECLA]
    ON [dbo].[ADTRIAGEU] ([NUMINGRES] ASC, [ID] DESC)
    INCLUDE ([TRIAGECLA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con frecuencias cardiacas fetales registradas en triage; observación obstétrica de embarazada, monitoreo fetal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FetocardiaTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena un Json con las fetocardias diligenciadas desde el formulario triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FetocardiaTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FetocardiaTriage';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de embarazo del paciente; valores: 1=Sí (embarazada), 2=No (no embarazada); booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PregnancyStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'en estado de embarazo : en este campo se almacenara los valores   1 (True) --> Si  2 (False) --> No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PregnancyStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PregnancyStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterios de enfermedades potencialmente transmisibles (tuberculosis, COVID, etc.); valores: 1=Sí cumple criterios, 2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CRITPOTENTRANSMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterios para enfermedades potencialmente transmisibles:   1 - Si    2 - No   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CRITPOTENTRANSMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CRITPOTENTRANSMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla del paciente en centímetros (CM); medida antropométrica de estatura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla en CM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de revalorización/reevaluación del triage; seguimiento clínico posterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECREVTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Revaloracion del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECREVTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECREVTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (Identity) de la tabla ADTRIAGEU; clave primaria numérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio/creación del registro de triage; registro de admisión urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECHINITR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Iniciacion del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECHINITR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECHINITR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de control y auditoría interna; trazabilidad de cambios registrales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza el tercer llamado/contacto; seguimiento triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza el tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza el segundo llamado/contacto; seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza el segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza el primer llamado/contacto de triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza el primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del tercer llamado/contacto de seguimiento del paciente en triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Tercer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del segundo llamado/contacto de seguimiento del paciente en triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Segundo Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del primer llamado/contacto de seguimiento del paciente en triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Primer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del tercer llamado; 1=Sí se efectuó contacto de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TERCERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del segundo llamado; 1=Sí se efectuó contacto de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del primer llamado; 1=Sí se efectuó contacto de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si paciente es víctima del conflicto armado; contexto sociopolítico de urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIVICONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paciente es victima del conflicto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIVICONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIVICONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del paciente en triage: 1=Medicina General, 2=Medicina Especializada; tipo atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen para los pacientes de triage 1-Pacientes Medicina General 2- Medicina Especializada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica asignada en triage; cardiología, pediatría, cirugía, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si paciente es remitido a especialista; derivación/referencia desde triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRREMITID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el paciente es remitido (especializacion)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRREMITID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRREMITID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de parto reciente (últimas horas/días); 1=Sí; contexto maternidad/urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico secundario CIE-10 registrado en triage; diagnóstico comorbilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico principal CIE-10 registrado en triage; diagnóstico primario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de atención/consulta del paciente en urgencia; timestamp asistencia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFECCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFECCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFECCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en taxi a urgencias; 1=Sí; tipo transporte llegada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGTAXI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'llegada en Taxi 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGTAXI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGTAXI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en carro de bomberos; 1=Sí; transporte emergencia asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICARBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Carro de Bomberos 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICARBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICARBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de atención planificada; 1=Sí; tipo ingreso urgencia (programado vs emergencia).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Planifica 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aliento alcohólico al ingreso; 1=Sí; hallazgo examen físico triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALIALC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aliento a Alcohol 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALIALC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALIALC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente en kilogramos (KG); medida antropométrica vital para dosificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPESOKG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso en KG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPESOKG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPESOKG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de conciencia del paciente (alerta, somnoliento, inconsciente, confuso); evaluación neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Conciencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de consulta externa en HUB (centro de referencia); 1=Sí; vía acceso urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICOXHUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta Externa HUB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICOXHUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICOXHUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de infarto reciente; 1=Sí; antecedente cardiovascular crítico en urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIINFREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Infarto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIINFREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIINFREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de parto/puerperio reciente; 1=Sí; contexto obstétrico urgencia postparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hospitalización reciente; 1=Sí; antecedente de internación previa urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIHOSREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hospitalizacion Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIHOSREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIHOSREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de entidad administradora (EPS/asegurador); 9 dígitos; responsable cobertura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de centro de atención; identificador del sitio físico donde se efectúa triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del médico/profesional que atiende; cédula/identificación profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que atiende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría/clasificación de triage; emergencia, urgencia, etc.; nivel priorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Categoria de Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de triage (activo, cancelado, procesado); 1 carácter de estado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa del ingreso: 1=Heridos combate, 2=Enfermedad profesional, 3=General adulto, 4=Pediátrico, 5=Odontología, 6=Accidente tránsito, 7=Catástrofe, 8=Quemados, 9=Maternidad, 10=Laboral, 11=Cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad  10. Accidente Laboral  11. Cirugia Programada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo por enfermedad general; 1=Sí; motivo consulta urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAENFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Enfermedad General 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAENFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAENFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo por SOAT (seguro accidentes tránsito); 1=Sí; tipo ingreso urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGSOAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias S.O.A.T. 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGSOAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGSOAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo por accidente laboral/trauma; 1=Sí; motivo asistencia urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACCIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Accidente Laboral 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACCIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACCIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo en helicóptero; 1=Sí; transporte aéreo emergencia crítica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAHELIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Helicoptero 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAHELIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAHELIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del médico/profesional responsable atención; PII/identificación profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tubo de tórax al arribo; 1=Sí; dispositivo drenaje pleural presente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATUBTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Tubo Tórax 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATUBTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATUBTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sonda vesical al arribo; 1=Sí; cateterismo urinario realizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVESIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias S. Vesical 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVESIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVESIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sonda nasogástrica (SNG) al arribo; 1=Sí; tubo gastrointestinal presente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESNG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias SNG 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESNG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de intubación traqueal al arribo; 1=Sí; vía aérea asegurada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAINTUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Intubación Traqueal 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAINTUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAINTUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de levantador/dispositivo de movilización al arribo; 1=Sí.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGELEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias LEV 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGELEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGELEV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de oxígeno/terapia respiratoria al arribo; 1=Sí; soporte respiratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAOXIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Oxígeno 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAOXIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAOXIGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de férulas extremidad al arribo; 1=Sí; inmovilización ortopédica presente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFERUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Férulas extrem. 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFERUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFERUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tabla espinal al arribo; 1=Sí; inmovilización columna vertebral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATABLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Tabla espinal 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATABLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATABLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de collar cervical al arribo; 1=Sí; protección cervical presente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACOLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Collar Cervical 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACOLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACOLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de acompañamiento por policía; 1=Sí; custodia/reporte legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAPOLIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Policía 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAPOLIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAPOLIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en ambulancia; 1=Sí; transporte paramédico urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAAMBUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Ambulancia 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAAMBUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAAMBUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en vehículo particular/propio; 1=Sí; transporte civil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVEHIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Vehiculo Particular 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVEHIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVEHIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada caminando (paciente deambulante); 1=Sí; sin transporte asistido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Caminando 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo por remisión/referencia de otro centro; 1=Sí; derivación urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Remisión 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAREMIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de consulta espontánea/por solicitud directa; 1=Sí; no referido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAEXTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Consulta expontánea 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAEXTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAEXTEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de drogas/medicamentos en antecedentes; alergias medicamentosas, consumo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIADROGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Drogas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIADROGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIADROGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de alergias reportadas; antecedentes alérgicos relevantes urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALERGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alergias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALERGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALERGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de cirugías previas; antecedentes quirúrgicos paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cirujias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otros antecedentes relevantes no especificados; historia clínica general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIOTROSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de los Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIOTROSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIOTROSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de otros antecedentes presentes; 1=Sí; flag para registro en TRIOTROSD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros antecedentes 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad pulmonar; 1=Sí; antecedente respiratorio (EPOC, asma, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEPUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEPUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEPUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hipertensión arterial; 1=Sí; antecedente cardiovascular crónico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEHIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HiperTension Arterial 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEHIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEHIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de convulsiones/epilepsia; 1=Sí; antecedente neurológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convulsiones 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de accidente cerebrovascular (ACV/ictus) previo; 1=Sí; antecedente vascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEACV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'A.C.V', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEACV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEACV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad coronaria; 1=Sí; antecedente isquémico cardiaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Coronaria 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diabetes mellitus; 1=Sí; antecedente endocrinológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'diabetes 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación/nivel de triage: 1=Emergencia, 2=Urgencia médica, 3=Urgencia diferida, 4=No urgente; priorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion 1=Emergencia 2=Urgencia Medica 3=Urgencia Diferida 4=No Urgente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas del triage; hallazgos relevantes, notas clínicas adicionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saturación de oxígeno o Escala de Glasgow (nivel de conciencia); signo vital neurológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Escala de Glasswood', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura corporal del paciente; signo vital en grados Celsius.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGETEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGETEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGETEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia respiratoria del paciente; respiraciones por minuto (RPM); signo vital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia cardíaca/pulso del paciente; latidos por minuto (LPM); signo vital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tensión/presión arterial del paciente; sistólica/diastólica en mmHg; signo vital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITENSIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tension Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITENSIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITENSIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la consulta/presentación clínica; razón principal ingreso urgencia paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIMOTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIMOTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIMOTCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de edad: 1=Años, 2=Meses, 3=Días; tipo edad especialmente pediátrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIUNIEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de la Edad 1=años 2= meses 3= dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIUNIEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIUNIEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del paciente en unidad especificada (años/meses/días); dato demográfico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente; cédula/documento PII ofuscado; FK a ADCONTURG.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del triage; timestamp creación atención urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente; identificador de hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; cédula/identificación PII ofuscado; sinónimo CODCONCEC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número/consecutivo del registro de triage; ID secuencial único (ej: 00000002).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIANUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Triage  Nota: Codigo Consecutivo 00000002', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIANUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIANUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de triage de urgencias: captura la valoración inicial del paciente al llegar a urgencias, incluyendo signos vitales, clasificación de prioridad, antecedentes, medios de transporte, dispositivos utilizados y datos del profesional que realizó la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU';
