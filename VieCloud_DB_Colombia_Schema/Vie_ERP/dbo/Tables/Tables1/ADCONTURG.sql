CREATE TABLE [dbo].[ADCONTURG] (
    [CODCONCEC]                    CHAR (20)                                                                        NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPFECLLEGA]                   DATETIME                                                                         NOT NULL,
    [CONESTADO]                    INT                                                                              NOT NULL,
    [IPPRIAPEL]                    CHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NOT NULL,
    [IPSEGAPEL]                    CHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NOT NULL,
    [IPPRINOMB]                    CHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NOT NULL,
    [IPSEGNOMB]                    CHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NOT NULL,
    [IPNOMCOMP]                    CHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [CODENTIDA]                    CHAR (9)                                                                         NOT NULL,
    [CODCENATE]                    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                    CHAR (10)                                                                        NOT NULL,
    [CODTIPPAC]                    INT                                                                              NOT NULL,
    [PRIMERLLA]                    BIT                                                                              NULL,
    [SEGUNDLLA]                    BIT                                                                              NULL,
    [TERCERLLA]                    BIT                                                                              NULL,
    [FECPRILLA]                    DATETIME                                                                         NULL,
    [FECSEGLLA]                    DATETIME                                                                         NULL,
    [FECTERLLA]                    DATETIME                                                                         NULL,
    [OBVAUSENT]                    VARCHAR (300)                                                                    NULL,
    [PROAUSENT]                    CHAR (20)                                                                        NULL,
    [PROPRILLA]                    CHAR (20)                                                                        NULL,
    [PROSEGLLA]                    CHAR (20)                                                                        NULL,
    [PROTERLLA]                    CHAR (20)                                                                        NULL,
    [FECAUSENT]                    DATETIME                                                                         NULL,
    [INDAUDFOR]                    NUMERIC (18)                                                                     NOT NULL,
    [CODUSUARI]                    CHAR (20)                                                                        NULL,
    [CODCONTRA]                    CHAR (6)                                                                         NULL,
    [CODPANATE]                    CHAR (2)                                                                         NULL,
    [GENCAREGROUP]                 INT                                                                              NULL,
    [GENCONENTITY]                 INT                                                                              NULL,
    [DateCallOnePreTriage]         DATETIME                                                                         NULL,
    [DateCallTwoPreTriage]         DATETIME                                                                         NULL,
    [DateCallThreePreTriage]       DATETIME                                                                         NULL,
    [PhysicianFirstCallPreTriage]  CHAR (20)                                                                        NULL,
    [PhysicianSecondCallPreTriage] NCHAR (10)                                                                       NULL,
    [PhysicianThirdCallPreTriage]  NCHAR (10)                                                                       NULL,
    CONSTRAINT [PK_ADCONTURG] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADCONTURG].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADCONTURG].[IPPRIAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADCONTURG].[IPSEGAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADCONTURG].[IPPRINOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADCONTURG].[IPSEGNOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADCONTURG].[IPNOMCOMP]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');




GO
CREATE NONCLUSTERED INDEX [IX_ListadoTriage]
    ON [dbo].[ADCONTURG]([CONESTADO] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC)
    INCLUDE([CODCONCEC], [CODENTIDA], [CODTIPPAC], [IPCODPACI], [IPFECLLEGA], [IPNOMCOMP]);


GO
CREATE NONCLUSTERED INDEX [IX_ADCONTURG_CONESTADO_IPFECLLEGA_INC_CODCONCEC_IPCODPACI_UFUCODIGO]
    ON [dbo].[ADCONTURG]([CONESTADO] ASC, [IPFECLLEGA] ASC)
    INCLUDE([CODCONCEC], [IPCODPACI], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADCONTURG_CODCENATE_CONESTADO_UFUCODIGO_CODENTIDA_CODTIPPAC_DateCallOnePreTriage_IPCODPACI_IPFECLLEGA_IPNOMCOMP]
    ON [dbo].[ADCONTURG]([CODCENATE] ASC, [CONESTADO] ASC, [UFUCODIGO] ASC)
    INCLUDE([CODENTIDA], [CODTIPPAC], [DateCallOnePreTriage], [IPCODPACI], [IPFECLLEGA], [IPNOMCOMP]);


GO
ALTER INDEX [IX_ADCONTURG_CODCENATE_CONESTADO_UFUCODIGO_CODENTIDA_CODTIPPAC_DateCallOnePreTriage_IPCODPACI_IPFECLLEGA_IPNOMCOMP]
    ON [dbo].[ADCONTURG] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_IPCODPACI]
    ON [dbo].[ADCONTURG]([IPCODPACI] ASC, [CONESTADO] ASC);


GO
ALTER INDEX [IX_IPCODPACI]
    ON [dbo].[ADCONTURG] DISABLE;


GO
CREATE NONCLUSTERED INDEX [ConceptosUrgencias]
    ON [dbo].[ADCONTURG]([IPCODPACI] ASC, [IPFECLLEGA] ASC)
    INCLUDE([CODCONCEC], [IPNOMCOMP]);


GO
CREATE NONCLUSTERED INDEX [IDX_ADCONTURG_CONESTADO_CODENTIDA_CODCENATE_UFUCODIGO_Includes]
    ON [dbo].[ADCONTURG]([CONESTADO] ASC, [CODENTIDA] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC)
    INCLUDE([IPCODPACI], [IPFECLLEGA], [IPNOMCOMP], [CODTIPPAC], [PRIMERLLA], [SEGUNDLLA], [TERCERLLA]);


GO
CREATE NONCLUSTERED INDEX [IX_ADCONTURG_CODCENATE_CONESTADO_UFUCODIGO_CODENTIDA_CODTIPPAC_IPCODPACI_IPFECLLEGA_IPNOMCOMP_PRIMERLLA_SEGUNDLLA_TERCERLLA]
    ON [dbo].[ADCONTURG]([CODCENATE] ASC, [CONESTADO] ASC, [UFUCODIGO] ASC)
    INCLUDE([CODENTIDA], [CODTIPPAC], [IPCODPACI], [IPFECLLEGA], [IPNOMCOMP], [PRIMERLLA], [SEGUNDLLA], [TERCERLLA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del médico o profesional de salud que realizó la tercera llamada de pretriage (NCHAR 10, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianThirdCallPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  MédicoTercera Llamada  PreTriaje  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianThirdCallPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianThirdCallPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del médico o profesional de salud que realizó la segunda llamada de pretriage (NCHAR 10, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianSecondCallPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Médico Segunda Llamada  PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianSecondCallPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianSecondCallPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del médico o profesional de salud que realizó la primera llamada de pretriage (CHAR 20, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianFirstCallPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Médico Primera Llamada  PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianFirstCallPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianFirstCallPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la tercera llamada de pretriage al paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallThreePreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha Llamada Tres   PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallThreePreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallThreePreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la segunda llamada de pretriage al paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallTwoPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha Llamada Dos PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallTwoPreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallTwoPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la primera llamada de pretriage al paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallOnePreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha del primer llamdo del triague', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallOnePreTriage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallOnePreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad administradora de salud (EPS/EAPB) sincronizado desde HIS con Indigo Vie Cloud (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo de la entidad Administradora de salud la cual se llena cuando el HIS se interfaza con Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de atención o programa de salud sincronizado desde HIS con Indigo Vie Cloud (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del grupos de atencion el cual se llena cuando el HIS se interfaza con Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios o cobertura del paciente ante la entidad (CHAR 2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plan de Beneficio del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato celebrado entre la EAPB y la IPS (CHAR 6)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato de la EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que creó el registro en el sistema (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y control para trazabilidad del registro (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se marcó al paciente como ausente a la atención (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que se marco como ausente el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realizó el tercer llamado (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realizo el tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realizó el segundo llamado (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realizo el segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realizó el primer llamado (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realizo el primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que marcó la ausencia del paciente (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que marca como ausente el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o motivo documentado de la ausencia del paciente (VARCHAR 300)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la ausencia del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del tercer llamado al paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Tercer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECTERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del segundo llamado al paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Segundo Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del primer llamado al paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Primer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECPRILLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: 1 si se completó el tercer llamado de urgencias (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'TERCERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'TERCERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: 1 si se completó el segundo llamado de urgencias (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: 1 si se completó el primer llamado de urgencias (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de categoría poblacional: 1=Maternas, 2=Menores 5 años, 3=Adultos mayores, 4=Discapacitados, 5=Población general (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Paciente-Población:  1: Maternas  2: Menores de 5 Años  3: Adultos Mayores  4: Discapacitados  5: Poblacion General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se atiende el paciente (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o IPS donde llega el paciente (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad o EPS a la que está afiliado el paciente (CHAR 9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente: apellidos y nombres (CHAR 250, PII Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Completo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente (CHAR 20, PII Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente (CHAR 20, PII Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente (CHAR 20, PII Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente (CHAR 20, PII Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del control de arribo a urgencias: 1=Sin atender, 2=Ausente en triage, 3=Clasificado sin ingreso, 4=Clasificado con ingreso, 5=Atendido, 6=Ausente en atención inicial, 7=Anulado por error, 8=No atendido por clasificación III-IV sin autorización (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Control de Arribo a Urgencias:  1: Sin Atender  2: Ausente en Clasificacion TRIAGE  3: Clasificado sin Ingreso  4: Clasificado con Ingreso  5: Atendido  6: Ausente en Atencion Inicial Urgencias  7: Anulado por error de Parametrizacion  8: No atendido por Clasificacion III o IV sin Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de arribo a urgencias; NULL si es atendido sin pasar por triage (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPFECLLEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de arribo a Urgencias:  se deja NULL cuando es atendido en urgencias sin pasar por triage, se evalua el triage en la Historia Inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPFECLLEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPFECLLEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o número de identificación del paciente; equivalente a cédula/documento (VARCHAR 25, PII Ofuscado, FK Usuario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo único del registro de control de urgencias (CHAR 20, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de contactos y llamados en urgencias: guarda el control de las convocatorias realizadas a pacientes que esperan atención en urgencias, incluyendo hasta tres intentos de llamado, el profesional que llamó en cada intento, la hora de llegada del paciente y el estado del contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG';
