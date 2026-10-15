CREATE TABLE [dbo].[RISTRACAB] (
    [CODCONSEC]        VARCHAR (10)                                                                     NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NOMPACIEN]        VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')           NOT NULL,
    [IPPRIAPEL]        VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')   NOT NULL,
    [IPSEGAPEL]        VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')  NOT NULL,
    [IPFECNACI]        VARCHAR (8) MASKED WITH (FUNCTION = 'default()')                                 NOT NULL,
    [IPSEXOPAC]        VARCHAR (6)                                                                      NOT NULL,
    [TIPMODALI]        VARCHAR (2)                                                                      NOT NULL,
    [CODSERIPS]        CHAR (20)                                                                        NOT NULL,
    [DESSERIPS]        VARCHAR (300)                                                                    NOT NULL,
    [FECHTURNO]        VARCHAR (8)                                                                      NOT NULL,
    [HORATURNO]        VARCHAR (6)                                                                      NOT NULL,
    [INESTADOT]        VARCHAR (15)                                                                     NOT NULL,
    [PROCESADO]        VARCHAR (1)                                                                      NOT NULL,
    [REPSERPAC]        VARCHAR (8000)                                                                   NULL,
    [OBSSERPAC]        VARCHAR (400)                                                                    NULL,
    [FECNACDAT]        DATETIME                                                                         NOT NULL,
    [EXAURGPAC]        BIT                                                                              NOT NULL,
    [NOMSERPAC]        CHAR (30)                                                                        NULL,
    [USUAGESER]        CHAR (20)                                                                        NULL,
    [USUCONSER]        CHAR (20)                                                                        NULL,
    [ACCNUMBER]        INT                                                                              NULL,
    [STUDYINSTANCEUID] VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_ITSYNAPSE] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_RISTRACAB_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_RISTRACAB_RISTRACAB] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[RISTRACAB] NOCHECK CONSTRAINT [FK_RISTRACAB_INPACIENT];


GO
ALTER TABLE [dbo].[RISTRACAB] NOCHECK CONSTRAINT [FK_RISTRACAB_RISTRACAB];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la instancia de estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'STUDYINSTANCEUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el numero ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'ACCNUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el envio al PACS  1: Indica que ha sido guardado por el sistema de agendamiento o por las Ordenes Medicas Intrahospitalarias  2: Indica que el Tecnologo ya lo asigno para la toma del examen y esta a la espera del envio al PACS  3: Indica ya se envio la Orden al PACS  4. Examen Anulado por el Tecnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'PROCESADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Estudio  Ordened: Ordenado - Dashboard Medico Tratante  Scheduled: Agendado - Dashboard Tecnologo  Completed: Completado - Dashboard Tecnologo  Dictated: Dictado - Dashboard Radiologo  Canceled: Anulado por el Tecnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'INESTADOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo del Paciente:  1=Masculino  2=Femenino  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';

