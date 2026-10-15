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
CREATE NONCLUSTERED INDEX [IX_RISTRACAB]
    ON [dbo].[RISTRACAB]([PROCESADO] ASC, [INESTADOT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la instancia de estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'STUDYINSTANCEUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el numero ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'ACCNUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUCONSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'USUAGESER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del servidor PACS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMSERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el examen es de urgencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'EXAURGPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECNACDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones Generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'OBSSERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reporte del Radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'REPSERPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el envio al PACS  1: Indica que ha sido guardado por el sistema de agendamiento o por las Ordenes Medicas Intrahospitalarias  2: Indica que el Tecnologo ya lo asigno para la toma del examen y esta a la espera del envio al PACS  3: Indica ya se envio la Orden al PACS  4. Examen Anulado por el Tecnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'PROCESADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Estudio  Ordened: Ordenado - Dashboard Medico Tratante  Scheduled: Agendado - Dashboard Tecnologo  Completed: Completado - Dashboard Tecnologo  Dictated: Dictado - Dashboard Radiologo  Canceled: Anulado por el Tecnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'INESTADOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Turno Formato: HHMMSS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'HORATURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Turno Formato: YYYYMMDD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'FECHTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'DESSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad del Estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'TIPMODALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo del Paciente:  1=Masculino  2=Femenino  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'NOMPACIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISTRACAB', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

