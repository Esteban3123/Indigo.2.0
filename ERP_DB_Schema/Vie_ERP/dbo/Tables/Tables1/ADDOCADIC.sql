CREATE TABLE [dbo].[ADDOCADIC] (
    [CODDOCALM]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [FECREGDOC]  DATETIME                                                                         NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]  CHAR (10)                                                                        NULL,
    [CODENTIDA]  CHAR (9)                                                                         NOT NULL,
    [JUSCLISER]  VARCHAR (MAX)                                                                    NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NULL,
    [CODSERIPS]  CHAR (20)                                                                        NULL,
    [NOMANTARC]  CHAR (255)                                                                       NOT NULL,
    [EXTEARCHI]  CHAR (5)                                                                         NOT NULL,
    [OBSGENARC]  CHAR (1000)                                                                      NULL,
    [CODUSUARI]  CHAR (20)                                                                        NOT NULL,
    [TIPDOCTRA]  CHAR (1)                                                                         NOT NULL,
    [CODCONCEC]  NUMERIC (18)                                                                     NULL,
    [DOCAUTSER]  BIT                                                                              NOT NULL,
    [NUMAUTSERV] CHAR (10)                                                                        NULL,
    [FECAUTSERV] DATETIME                                                                         NULL,
    [PORAUTSERV] CHAR (3)                                                                         NULL,
    [SEMAFIPAC]  CHAR (2)                                                                         NULL,
    [SOLVALBON]  BIT                                                                              NULL,
    [VALCUOMOD]  NUMERIC (18)                                                                     NULL,
    [PORCUOMOD]  CHAR (3)                                                                         NULL,
    [VALMAXCUO]  NUMERIC (18)                                                                     NULL,
    [VALCUOCOP]  NUMERIC (18)                                                                     NULL,
    [PORCUOCOP]  CHAR (3)                                                                         NULL,
    [VALMAXCOP]  NUMERIC (18)                                                                     NULL,
    [VALCUOOTR]  NUMERIC (18)                                                                     NULL,
    [PORCUOOTR]  CHAR (3)                                                                         NULL,
    [VALMAXOTR]  NUMERIC (18)                                                                     NULL,
    [INDAUDFOR]  NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADDOCADIC] PRIMARY KEY CLUSTERED ([CODDOCALM] ASC),
    CONSTRAINT [FK_ADDOCADIC_ADAUTSERC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADAUTSERC] ([CODCONCEC]),
    CONSTRAINT [FK_ADDOCADIC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADDOCADIC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADDOCADIC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ADDOCADIC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[ADDOCADIC] NOCHECK CONSTRAINT [FK_ADDOCADIC_ADAUTSERC];


GO
ALTER TABLE [dbo].[ADDOCADIC] NOCHECK CONSTRAINT [FK_ADDOCADIC_ADCENATEN];


GO
ALTER TABLE [dbo].[ADDOCADIC] NOCHECK CONSTRAINT [FK_ADDOCADIC_ADINGRESO];


GO
ALTER TABLE [dbo].[ADDOCADIC] NOCHECK CONSTRAINT [FK_ADDOCADIC_INPACIENT];


GO
ALTER TABLE [dbo].[ADDOCADIC] NOCHECK CONSTRAINT [FK_ADDOCADIC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Maximo Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pesos Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Maximo Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pesos Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Maximo Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCUO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pesos Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reclamo o solicitud de Vale o Bono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SOLVALBON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Semanas de Afiliacion del Paciente al momento de la solicitud de autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SEMAFIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Porcentaje reconocido por la EAPB - Aplica en pagos compartidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORAUTSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se genero la Autorizacion de Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECAUTSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Autorizacion de Servicios emitida por la EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMAUTSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el documento recibido es una Autorizacion de Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'DOCAUTSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de Tabla - Solicitud de Servicios Adicionales: Este campo se diligencia cuando corresponde a un archivo enviado desde una transaccion de solicitud de servicios adicionales, para poder identificar con que transaccion se envio. O cuando corresponde a una autorizacion de servicios enviada por la EAPB, se especifica a que solicitud de la IPS corresonde.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Documento por Transaccion  1: Documento Enviado  2: Documento Recibido  3: Documento Escaneado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'TIPDOCTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones Generales del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'OBSGENARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el nombre del archivo antes de ser renombrado por el consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NOMANTARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion Clinica para la solicitud del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio de Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECREGDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centron de Atencion de donde se genera el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del documento almacenado - Representa el nombre del archivo fisico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODDOCALM';

