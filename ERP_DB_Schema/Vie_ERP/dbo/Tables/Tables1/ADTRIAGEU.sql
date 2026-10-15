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
CREATE NONCLUSTERED INDEX [IX_ControlTriage]
    ON [dbo].[ADTRIAGEU]([CODCENATE] ASC, [NUMINGRES] ASC);


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
    INCLUDE([TRIANUMER], [CODCONCEC], [TRIAGECLA], [CODPROSAL], [TRIACATEG], [CODESPECI], [TRIORIGEN], [FECHINITR]);


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fetocardia triage : en esta columna se almacenara el dato ingresado desde el formulario triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FetocardiaTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'en estado de embarazo : en este campo se almacenara los valores   1 (True) --> Si  2 (False) --> No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PregnancyStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterios para enfermedades potencialmente transmisibles:   1 - Si    2 - No   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CRITPOTENTRANSMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla en CM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Revaloracion del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECREVTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Iniciacion del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECHINITR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza el tercer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza el segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza el primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PROPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Tercer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECTERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Segundo Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECSEGLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Primer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'FECPRILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercer Llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TERCERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'SEGUNDLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer llamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'PRIMERLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el paciente es victima del conflicto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIVICONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen para los pacientes de triage 1-Pacientes Medicina General 2- Medicina Especializada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si el paciente es remitido (especializacion)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRREMITID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODDIAGN1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFECCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'llegada en Taxi 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGTAXI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Carro de Bomberos 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICARBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Planifica 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aliento a Alcohol 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALIALC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso en KG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPESOKG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de Conciencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consulta Externa HUB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRICOXHUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Infarto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIINFREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parto Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIPARRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hospitalizacion Reciente 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIHOSREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Medico que atiende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Categoria de Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad  10. Accidente Laboral  11. Cirugia Programada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAUIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Enfermedad General 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAENFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias S.O.A.T. 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGSOAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Accidente Laboral 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACCIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Helicoptero 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAHELIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Tubo Tórax 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATUBTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias S. Vesical 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVESIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias SNG 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Intubación Traqueal 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAINTUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias LEV 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGELEV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Oxígeno 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAOXIGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Férulas extrem. 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFERUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Tabla espinal 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIATABLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Collar Cervical 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACOLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Policía 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAPOLIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Ambulancia 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAAMBUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Vehiculo Particular 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAVEHIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Caminando 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACAMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Remisión 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAREMIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Arribo a urgencias Consulta expontánea 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAEXTEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Drogas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIADROGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alergias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIALERGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cirujias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIACIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de los Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIOTROSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros antecedentes 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad Pulmonar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEPUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HiperTension Arterial 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEHIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Convulsiones 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'A.C.V', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEACV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad Coronaria 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'diabetes 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion 1=Emergencia 2=Urgencia Medica 3=Urgencia Diferida 4=No Urgente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEOBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Escala de Glasswood', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGESO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGETEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIFRECUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tension Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRITENSIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIMOTCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de la Edad 1=años 2= meses 3= dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIUNIEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAGEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIAFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Triage  Nota: Codigo Consecutivo 00000002', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTRIAGEU', @level2type = N'COLUMN', @level2name = N'TRIANUMER';

