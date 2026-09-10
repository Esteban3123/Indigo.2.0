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
ALTER TABLE [dbo].[CALREPORTE] NOCHECK CONSTRAINT [FK_CALREPORTE_CALTIPOCLASE];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALREPORTE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALREPORTE].[NOMBRECOMP]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALREPORTE].[FECNACIMIENTO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALREPORTE].[DIRECCION]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CALREPORTE].[TELEFONO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro de reporte de calidad (FK → SEGusuaru.CODUSUARI), CHAR(20), auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de reporte de calidad, DATETIME, trazabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó originalmente el registro de reporte de calidad (FK → SEGusuaru.CODUSUARI), CHAR(20), auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de reporte de calidad, DATETIME, trazabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación del Registo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del reporte (1=Reportado, 2=Descartado, 3=Modificado, 4=Visado), INT, catalogo de estados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TIPOESTADO:    1=Reportado    2=Descartado    3=Modificado    4=Visado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional que reporta o revisa el evento adverso/incidente (FK → INUNIFUNC.UFUCODIGO), CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGORE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registra o reporta el evento adverso, incidente, acción insegura o situación clínica inesperada, CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIORE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'USUARIORE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de anonimato del reporte (1=Sí anónimo, 2=No anónimo), INT, confidencialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'REPORTEANONIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reporte animo   1 = si   2 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'REPORTEANONIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'REPORTEANONIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción correctiva inmediata ejecutada en respuesta al evento adverso o incidente detectado, VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ACCIONCORREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Accion correctiva inmediata', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ACCIONCORREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ACCIONCORREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del evento adverso, incidente, acción insegura o situación clínica inesperada ocurrido, VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de severidad/daño del evento (FK → CALNIVELDANO.ID), INT, clasificación de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IDNIVELDANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de nivel de daño que relaciona con tabla CALNIVELDANO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IDNIVELDANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IDNIVELDANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de evento reportado (1=Evento adverso, 2=Incidente), INT, categorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 =  Evento adverso   2 =  Incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de situación clínica inesperada o acción insegura (FK → CALTIPOCLASE.ID), INT, clasificación clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IDTIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de situación clinica inesperada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IDTIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IDTIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico del paciente involucrado en el reporte, VARCHAR(30), PII ofuscado, contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección del domicilio del paciente involucrado, VARCHAR(100), PII ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género biológico del paciente (1=Hombre, 2=Mujer), INT, demografía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo   1 = Hombre   2 = Mujer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de población/tipo de paciente (1=Maternas, 2=Menores 5 años, 3=Adultos mayores, 4=Discapacitados, 5=Población general), INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Paciente-Población: 1: Maternas 2: Menores de 5 Años 3:   Adultos Mayores 4: Discapacitados 5: Poblacion General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora o institución a la que pertenece el paciente, CHAR(9), FK implícita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente involucrado en el reporte, DATE, PII ofuscada, edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECNACIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECNACIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECNACIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente involucrado en el evento adverso o incidente, VARCHAR(100), PII ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'NOMBRECOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'NOMBRECOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'NOMBRECOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad del paciente (1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU), INT, identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de identificacion  1 = CC  2 = CE  3 = TI  4 = RC  5 = PA  6 = AS  7 = MS  8 = NU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPOIDENTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente en centro de atención (FK → ADINGRESO.NUMINGRES), CHAR(10), nullable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente o equivalente a cédula/identificación/documento (PII ofuscado), VARCHAR(25), Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional donde ocurrió el evento (FK → INUNIFUNC.UFUCODIGO), CHAR(10), localización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se reporta el evento (FK → ADCENATEN.CODCENATE), CHAR(10), institución', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registra o reporta el evento adverso, incidente o acción insegura, DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Reporte:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que ocurrió el evento adverso, incidente o acción insegura reportado, DATE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ocurrencia:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase o origen del reporte (1=Voluntario, 2=Búsqueda activa, 3=Identificado por terceros, 4=Identificado por terceros), INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASEREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase del reporte   1=Voluntario  2=Busqueda activa  3=Identificado por terceros  4=Identificado por terceros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASEREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'CLASEREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo general de reporte (1=Acciones inseguras, 2=Situaciones clínicas inesperadas), INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPORERPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de reporte   1 = Acciones Inseguras  2 = Situaciones clínicas inesperadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPORERPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'TIPORERPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado interno del registro de reporte de calidad, INT, gestión de estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del registro de reporte de calidad/evento adverso (PK), INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de reportes de eventos adversos, incidentes o fallas de calidad asistencial ocurridos en la institución. Permite hacer seguimiento a situaciones de seguridad del paciente, incluyendo el tipo de evento, nivel de daño, acciones correctivas y estado del reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALREPORTE';
