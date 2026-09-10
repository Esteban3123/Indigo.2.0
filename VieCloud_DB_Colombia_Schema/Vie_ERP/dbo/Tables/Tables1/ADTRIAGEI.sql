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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEI].[CODCONCEC]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEI].[TRIFRECUC]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEI].[TRIFRECUR]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEI].[TRIAGETEM]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADTRIAGEI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de parto reciente (1=Sí). TINYINT. Marca si el paciente ha tenido parto en período reciente, relevante para antecedentes obstétricos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPARTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPARTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPARTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en taxi (1=Sí). TINYINT. Registro del medio de transporte particular utilizado para arribar a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGTAXI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'llegada en Taxi 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGTAXI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGTAXI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en carro de bomberos (1=Sí). TINYINT. Medio de transporte de emergencia para arribar a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRICARBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Carro de Bomberos 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRICARBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRICARBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de atención planificada (1=Sí). TINYINT. Identifica si la consulta fue programada o espontánea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Planifica 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de consulta externa HUB (1=Sí). TINYINT. Referencia a consulta del nivel superior de red de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRICOXHUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consulta Externa HUB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRICOXHUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRICOXHUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de infarto reciente (1=Sí). TINYINT. Antecedente de evento cardiovascular agudo en período reciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIINFREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Infarto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIINFREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIINFREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de parto reciente (1=Sí). TINYINT. Antecedente obstétrico relevante en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPARRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPARRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPARRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hospitalización reciente (1=Sí). TINYINT. Antecedente de ingreso previo a centro de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIHOSREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hospitalizacion Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIHOSREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIHOSREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo por enfermedad general (1=Sí). TINYINT. Marca llegada a urgencias por patología médica sin traumatismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAENFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Enfermedad General 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAENFER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAENFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo por SOAT/accidente asegurado (1=Sí). TINYINT. Registro de siniestro de tránsito con cobertura obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGSOAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias S.O.A.T. 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGSOAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGSOAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arribo por accidente laboral (1=Sí). TINYINT. Lesión o evento ocupacional cubierto por ARL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACCIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Accidente Laboral 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACCIDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACCIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en helicóptero (1=Sí). TINYINT. Transporte aéreo de emergencia para arribar a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAHELIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Helicoptero 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAHELIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAHELIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tubo torácico presente al arribo (1=Sí). TINYINT. Procedimiento de drenaje torácico realizado antes de llegada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIATUBTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Tubo Tórax 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIATUBTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIATUBTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sonda vesical presente al arribo (1=Sí). TINYINT. Dispositivo de drenaje urinario previo a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAVESIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias S. Vesical 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAVESIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAVESIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sonda nasogástrica presente al arribo (1=Sí). TINYINT. Dispositivo de nutrición/descompresión gástrica previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGESNG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias SNG 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGESNG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGESNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de intubación traqueal presente al arribo (1=Sí). TINYINT. Manejo de vía aérea avanzada previo a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAINTUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Intubación Traqueal 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAINTUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAINTUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de levantamiento/movilización asistida al arribo (1=Sí). TINYINT. Medio de transporte o apoyo movilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGELEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias LEV 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGELEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGELEV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de oxígeno en uso al arribo (1=Sí). TINYINT. Soporte respiratorio previo a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAOXIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Oxígeno 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAOXIGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAOXIGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de férulas en extremidades al arribo (1=Sí). TINYINT. Inmovilización de miembros previo a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAFERUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Férulas extrem. 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAFERUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAFERUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tabla espinal presente al arribo (1=Sí). TINYINT. Inmovilización de columna vertebral previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIATABLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Tabla espinal 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIATABLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIATABLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de collar cervical presente al arribo (1=Sí). TINYINT. Protección cervical por sospecha de lesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACOLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Collar Cervical 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACOLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACOLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de policía acompañante al arribo (1=Sí). TINYINT. Presencia de autoridad en caso de delito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAPOLIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Policía 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAPOLIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAPOLIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en ambulancia (1=Sí). TINYINT. Transporte médico profesional para arribar a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAAMBUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Ambulancia 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAAMBUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAAMBUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada en vehículo particular (1=Sí). TINYINT. Auto privado utilizado para arribar a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAVEHIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Vehiculo Particular 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAVEHIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAVEHIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de llegada caminando (1=Sí). TINYINT. Paciente autónomo, sin medio de transporte mecánico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACAMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Caminando 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACAMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACAMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de remisión desde otro centro (1=Sí). TINYINT. Referencia de institución de salud previa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Remisión 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAREMIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAREMIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de consulta espontánea/no remitida (1=Sí). TINYINT. Demanda de atención autogenerada por paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAEXTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arribo a urgencias Consulta expontánea 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAEXTEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAEXTEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de drogas, medicamentos o sustancias consumidas. VARCHAR(254). Antecedente toxicológico para evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIADROGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Drogas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIADROGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIADROGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de alergias a medicamentos, alimentos o sustancias. VARCHAR(254). Alergias conocidas del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIALERGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alergias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIALERGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIALERGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de antecedentes quirúrgicos previos. VARCHAR(254). Cirugías realizadas relevantes al cuadro clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cirujias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otros antecedentes personales relevantes. VARCHAR(254). Información complementaria de contexto clínico-social.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIOTROSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de los Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIOTROSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIOTROSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de otros antecedentes relevantes (1=Sí). TINYINT. Marca presencia de antecedentes no clasificados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros antecedentes 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad pulmonar crónica (1=Sí). TINYINT. Antecedente de EPOC, asma, fibrosis u otra patología respiratoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEPUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEPUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEPUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hipertensión arterial (1=Sí). TINYINT. Antecedente de presión arterial elevada diagnosticada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEHIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HiperTension Arterial 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEHIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEHIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de convulsiones/epilepsia (1=Sí). TINYINT. Antecedente de trastorno convulsivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convulsiones 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de accidente cerebrovascular previo (1=Sí). TINYINT. Antecedente de ACV, infarto cerebral o hemorrágico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEACV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'A.C.V', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEACV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEACV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad coronaria (1=Sí). TINYINT. Antecedente de cardiopatía isquémica, infarto miocardio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Coronaria 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diabetes mellitus (1=Sí). TINYINT. Antecedente de desorden glucémico tipo 1 o 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'diabetes 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de auditoría del registro. NUMERIC(18). Identificador único para trazabilidad en auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de iniciación del registro de triage. DATETIME. Timestamp del comienzo de la evaluación urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECHINITR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Iniciacion del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECHINITR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECHINITR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del profesional que realizó el tercer llamado/seguimiento. CHAR(20) (PII ofuscado). Médico o enfermero responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza el tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del profesional que realizó el segundo llamado/seguimiento. CHAR(20) (PII ofuscado). Médico o enfermero responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza el segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del profesional que realizó el primer llamado/seguimiento. CHAR(20) (PII ofuscado). Médico o enfermero responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza el primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PROPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del tercer llamado/seguimiento de triage. DATETIME. Timestamp del tercer contacto clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Tercer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del segundo llamado/seguimiento de triage. DATETIME. Timestamp del segundo contacto clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Segundo Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del primer llamado/seguimiento de triage. DATETIME. Timestamp del primer contacto clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Primer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'FECPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del tercer llamado (1=Sí). BIT. Marca tercera re-evaluación en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TERCERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del segundo llamado (1=Sí). BIT. Marca segunda re-evaluación en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización del primer llamado (1=Sí). BIT. Marca primera re-evaluación en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de víctima de conflicto armado (1=Sí). BIT. Marca si el paciente es víctima de conflicto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIVICONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paciente es victima del conflicto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIVICONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIVICONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de derivación: 1=Medicina General, 2=Medicina Especializada. CHAR(1). Clasificación de nivel de complejidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen para los pacientes de triage 1-Pacientes Medicina General 2- Medicina Especializada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica requerida. CHAR(3). Referencia a tabla de especialidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de paciente remitido a especialización (1=Sí). BIT. Marca derivación a especialista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRREMITID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el paciente es remitido (especializacion)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRREMITID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRREMITID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico secundario (CIE-10). CHAR(4). Segunda impresión diagnóstica en triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODDIAGN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODDIAGN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODDIAGN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico principal (CIE-10). CHAR(4). Impresión diagnóstica primaria en triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODDIAGN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODDIAGN1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODDIAGN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de atención/valoración urgencias. DATETIME. Timestamp de evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFECCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFECCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFECCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aliento alcohólico detectado (1=Sí). TINYINT. Hallazgo de intoxicación etílica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIALIALC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aliento a Alcohol 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIALIALC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIALIALC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso corporal del paciente en kilogramos. CHAR(7). Medida antropométrica para cálculo de dosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPESOKG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso en KG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPESOKG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIPESOKG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de conciencia del paciente (ej: alerta, somnolencia, coma). CHAR(50). Descripción neurológica basal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIESTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Conciencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIESTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIESTCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad administradora de salud (EPS/ARL/asegurador). CHAR(9). Referencia a pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/IPS donde se realiza triage. CHAR(10). Ubicación física de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del médico que realiza atención inicial. CHAR(20). Profesional responsable de valoración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODPROSA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que atiende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODPROSA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODPROSA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría de triage (combinación de criterios clínicos). CHAR(5). Clasificación de urgencia compleja.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACATEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Categoria de Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACATEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACATEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de triage (A=Activo, I=Inactivo, etc). CHAR(1). Bandera de vigencia del triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa del ingreso: 1=Heridos combate, 2=Enfermedad ocupacional, 3=Enfermedad general adulto, 4=Pediatría, 5=Odontología, 6=Accidente tránsito, 7=Catástrofe, 8=Quemados, 9=Maternidad. TINYINT. Clasificación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del médico responsable (similar a CODPROSA1). CHAR(20). Profesional de la salud tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de urgencia: 1=Emergencia, 2=Urgencia Médica, 3=Urgencia Diferida, 4=No Urgente. TINYINT. Nivel de prioridad asistencial (ESI/SALT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion 1=Emergencia 2=Urgencia Medica 3=Urgencia Diferida 4=No Urgente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas amplias del triage. VARCHAR(4000). Descripción detallada del cuadro clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEOBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEOBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEOBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala de Glasgow o saturación O2 (según contexto). CHAR(10). Valor numérico de función neurológica o respiratoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGESO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Escala de Glasswood', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGESO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGESO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura corporal en grados Celsius (PII ofuscado). CHAR(10). Signo vital termométrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGETEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGETEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGETEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia respiratoria en respiraciones por minuto (PII ofuscado). CHAR(10). Signo vital pulmonar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFRECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia cardíaca en latidos por minuto (PII ofuscado). CHAR(10). Signo vital cardiovascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFRECUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFRECUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIFRECUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tensión arterial sistólica/diastólica en mmHg. CHAR(10). Signo vital hemodinámico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRITENSIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tension Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRITENSIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRITENSIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de consulta o síntoma principal referido. VARCHAR(MAX). Descripción de la causa de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIMOTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIMOTCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIMOTCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de edad: 1=años, 2=meses, 3=días. TINYINT. Designa escala temporal de TRIAGEDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIUNIEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de la Edad 1=años 2= meses 3= dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIUNIEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIUNIEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del paciente en la unidad especificada. TINYINT. Valor numérico de edad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAGEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente (cédula/documento, PII ofuscado). CHAR(20) (FK a ADCONTURG). Identificación única del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de realización del triage. DATETIME. Timestamp de entrada a urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIAFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario (FK a ADINGRESO). CHAR(10). Identificador del episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente en tabla INPACIENT (PII ofuscado). VARCHAR(25) (FK a INPACIENT). Clave externa de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo único del triage. CHAR(20) (PK). Identificador secuencial del registro de triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIANUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Triage  Nota: Codigo Consecutivo 00000002', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIANUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI', @level2type = N'COLUMN', @level2name = N'TRIANUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de triage de urgencias: evaluación inicial del paciente al llegar a urgencias, incluyendo signos vitales, motivo de consulta, clasificación de prioridad, antecedentes, dispositivos utilizados en traslado y datos del profesional que realizó la valoración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEI';
