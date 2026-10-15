CREATE TABLE [dbo].[ADCONTURG] (
    [CODCONCEC]                    CHAR (20)                                                                        NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPFECLLEGA]                   DATETIME                                                                         NOT NULL,
    [CONESTADO]                    INT                                                                              NOT NULL,
    [IPPRIAPEL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NOT NULL,
    [IPSEGAPEL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NOT NULL,
    [IPPRINOMB]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NOT NULL,
    [IPSEGNOMB]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NOT NULL,
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  MédicoTercera Llamada  PreTriaje  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianThirdCallPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Médico Segunda Llamada  PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianSecondCallPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Médico Primera Llamada  PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PhysicianFirstCallPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha Llamada Tres   PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallThreePreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha Llamada Dos PreTriaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallTwoPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del primer llamdo del triague', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'DateCallOnePreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo de la entidad Administradora de salud la cual se llena cuando el HIS se interfaza con Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del grupos de atencion el cual se llena cuando el HIS se interfaza con Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plan de Beneficio del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Contrato de la EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que se marco como ausente el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realizo el tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realizo el segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realizo el primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que marca como ausente el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PROAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de la ausencia del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'OBVAUSENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Tercer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Segundo Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Primer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'FECPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'TERCERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Llamado al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Paciente-Población:  1: Maternas  2: Menores de 5 Años  3: Adultos Mayores  4: Discapacitados  5: Poblacion General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Completo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Control de Arribo a Urgencias:  1: Sin Atender  2: Ausente en Clasificacion TRIAGE  3: Clasificado sin Ingreso  4: Clasificado con Ingreso  5: Atendido  6: Ausente en Atencion Inicial Urgencias  7: Anulado por error de Parametrizacion  8: No atendido por Clasificacion III o IV sin Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de arribo a Urgencias:  se deja NULL cuando es atendido en urgencias sin pasar por triage, se evalua el triage en la Historia Inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPFECLLEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURG', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

