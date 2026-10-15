CREATE TABLE [dbo].[CALREPORTE] (
    [ID]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ESTADO]              INT                                                                              NOT NULL,
    [TIPORERPORTE]        INT                                                                              NOT NULL,
    [CLASEREPORTE]        INT                                                                              NOT NULL,
    [FECHAOCURRENC]       DATE                                                                             NOT NULL,
    [FECHAREPORTE]        DATETIME                                                                         NOT NULL,
    [CODCENATE]           CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]           CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]           CHAR (10)                                                                        NULL,
    [TIPOIDENTIFICA]      INT                                                                              NOT NULL,
    [NOMBRECOMP]          VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NOT NULL,
    [FECNACIMIENTO]       DATE MASKED WITH (FUNCTION = 'default()')                                        NOT NULL,
    [CODENTIDA]           CHAR (9)                                                                         NULL,
    [CODTIPPAC]           INT                                                                              NULL,
    [SEXO]                INT                                                                              NOT NULL,
    [DIRECCION]           VARCHAR (100) MASKED WITH (FUNCTION = 'default()')                               NOT NULL,
    [TELEFONO]            VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')          NOT NULL,
    [IDTIPO]              INT                                                                              NOT NULL,
    [CLASIFICACION]       INT                                                                              NULL,
    [IDNIVELDANO]         INT                                                                              NULL,
    [DESCRIPCION]         VARCHAR (MAX)                                                                    NULL,
    [ACCIONCORREC]        VARCHAR (MAX)                                                                    NOT NULL,
    [REPORTEANONIMO]      INT                                                                              NOT NULL,
    [USUARIORE]           CHAR (20)                                                                        NULL,
    [UFUCODIGORE]         CHAR (10)                                                                        NULL,
    [TIPOESTADO]          INT                                                                              CONSTRAINT [DF_TIPOESTADO] DEFAULT ((1)) NOT NULL,
    [FECHACREACION]       DATETIME                                                                         NULL,
    [USUARIOCREACION]     CHAR (20)                                                                        NULL,
    [FECHAMODIFICACION]   DATETIME                                                                         NULL,
    [USUARIOMODIFICACION] CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_CALREPORTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALREPORTE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CALREPORTE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CALREPORTE_CALNIVELDANO] FOREIGN KEY ([IDNIVELDANO]) REFERENCES [dbo].[CALNIVELDANO] ([ID]),
    CONSTRAINT [FK_CALREPORTE_CALTIPOCLASE] FOREIGN KEY ([IDTIPO]) REFERENCES [dbo].[CALTIPOCLASE] ([ID]),
    CONSTRAINT [FK_CALREPORTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CALREPORTE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CALREPORTE_INUNIFUNC1] FOREIGN KEY ([UFUCODIGORE]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CALREPORTE_SEGusuaru1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_CALREPORTE_SEGusuaru2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_ADCENATEN];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_ADINGRESO];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_CALNIVELDANO];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_CALTIPOCLASE];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_INPACIENT];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_INUNIFUNC];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_INUNIFUNC1];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_SEGusuaru1];


GO
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_SEGusuaru2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPOESTADO:    1=Reportado    2=Descartado    3=Modificado    4=Visado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reporte animo   1 = si   2 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'REPORTEANONIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 =  Evento adverso   2 =  Incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo   1 = Hombre   2 = Mujer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Paciente-Población: 1: Maternas 2: Menores de 5 Años 3:   Adultos Mayores 4: Discapacitados 5: Poblacion General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de identificacion  1 = CC  2 = CE  3 = TI  4 = RC  5 = PA  6 = AS  7 = MS  8 = NU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOIDENTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase del reporte   1=Voluntario  2=Busqueda activa  3=Identificado por terceros  4=Identificado por terceros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASEREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de reporte   1 = Acciones Inseguras  2 = Situaciones clínicas inesperadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPORERPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ESTADO';

