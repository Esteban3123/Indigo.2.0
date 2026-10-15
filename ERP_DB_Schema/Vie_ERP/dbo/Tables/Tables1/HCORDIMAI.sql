CREATE TABLE [dbo].[HCORDIMAI] (
    [IDETIPHIS]  CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                          NOT NULL,
    [CODCENATE]  CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                          NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED]  DATETIME                                                                           NOT NULL,
    [CODSERIPS]  CHAR (20)                                                                          NOT NULL,
    [CANSERIPS]  INT                                                                                NOT NULL,
    [OBSSERIPS]  CHAR (250)                                                                         NULL,
    [PRISERIPS]  CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS]  CHAR (1)                                                                           NULL,
    [MANEXTPRO]  BIT                                                                                NOT NULL,
    [CODDIAGNO]  CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NULL,
    [INTERPRET]  VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]  CHAR (20)                                                                          NULL,
    [NUMFOLINT]  NCHAR (10)                                                                         NULL,
    [SERREAINT]  BIT                                                                                NOT NULL,
    [INDAUDFOR]  NUMERIC (18)                                                                       NOT NULL,
    [ESTALEIMG]  BIT                                                                                NOT NULL,
    [FECRECEXA]  DATETIME                                                                           NULL,
    [NOMARCIMG]  CHAR (250)                                                                         NULL,
    [CONCURRE]   ROWVERSION                                                                         NULL,
    [USURECEXA]  CHAR (20)                                                                          NULL,
    [REALINOTIF] BIT                                                                                NOT NULL,
    [NOMRESULT]  VARCHAR (100)                                                                      NULL,
    [SERTRANSC]  BIT                                                                                NULL,
    [SERVALMED]  BIT                                                                                NULL,
    [CODUSUTRA]  CHAR (20)                                                                          NULL,
    [CODPROVAL]  CHAR (20)                                                                          NULL,
    [FECTRASER]  DATETIME                                                                           NULL,
    [FECVALSER]  DATETIME                                                                           NULL,
    [ESTTRASER]  BIT                                                                                NULL,
    [USUOCUREG]  CHAR (20)                                                                          NULL,
    CONSTRAINT [PK_HCORDIMAI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCORDIMAI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDIMAI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCORDIMAI] NOCHECK CONSTRAINT [FK_HCORDIMAI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDIMAI] NOCHECK CONSTRAINT [FK_HCORDIMAI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que ocupa el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'USUOCUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Transcripcion del Servicio de Imagenologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'ESTTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Validacion de la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'FECVALSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Transcripcion del la Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'FECTRASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional que valida la Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CODPROVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que Transcribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CODUSUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si la Transcripcion Fue ya Validada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'SERVALMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si el Servicio ya se le realizo la Lectura por el Tecnologo o Radiologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'SERTRANSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'NOMRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica si ya realizo la sincronizacion con ristracab y ristraded', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'REALINOTIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'NOMARCIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'FECRECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'ESTALEIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Servicio Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este Estudio Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Estudio Realizado  3: Imagen Procesada  4: Estudio Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDIMAI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

