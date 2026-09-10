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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADDOCADIC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18). Identificador numérico del registro de auditoría para trazabilidad de documentos almacenados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo otros (NUMERIC 18). Monto máximo en pesos permitido por concepto de otros gastos o servicios no clasificados en cuota moderadora o copago.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Maximo Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje otros (CHAR 3). Porcentaje de participación del usuario en otros gastos o servicios adicionales según contrato con EAPB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pesos otros (NUMERIC 18). Monto en pesos a cargo del paciente por concepto de otros servicios o gastos no estándar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pesos Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOOTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOOTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo copago (NUMERIC 18). Monto máximo en pesos que el paciente debe pagar como copago según su plan de beneficios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Maximo Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje copago (CHAR 3). Porcentaje fijo de participación del paciente en el copago de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pesos copago (NUMERIC 18). Monto en pesos calculado como copago a cargo del paciente en esta transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pesos Copago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo cuota moderadora (NUMERIC 18). Límite máximo en pesos de cuota moderadora permitido por EAPB/contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCUO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Maximo Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCUO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALMAXCUO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje cuota moderadora (CHAR 3). Porcentaje de participación del paciente en cuota moderadora por servicios no urgentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORCUOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pesos cuota moderadora (NUMERIC 18). Monto en pesos de cuota moderadora a cargo del paciente en esta solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pesos Cuota Moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'VALCUOMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reclamo o solicitud de vale o bono (BIT). Indicador si el documento es una solicitud de revalidación, reclamo de vale, bono o comprobante de pago.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SOLVALBON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reclamo o solicitud de Vale o Bono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SOLVALBON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SOLVALBON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de afiliación del paciente (CHAR 2). Número de semanas que el paciente llevaba afiliado a la EAPB al momento de solicitar la autorización de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SEMAFIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas de Afiliacion del Paciente al momento de la solicitud de autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SEMAFIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'SEMAFIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje reconocido EAPB (CHAR 3). Porcentaje de costo reconocido y pagado por la EAPB en servicios de pago compartido con usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORAUTSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Porcentaje reconocido por la EAPB - Aplica en pagos compartidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORAUTSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'PORAUTSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha generación autorización (DATETIME). Fecha y hora en que la EAPB emitió y registró la autorización de servicios en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECAUTSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se genero la Autorizacion de Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECAUTSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECAUTSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número autorización de servicios (CHAR 10). Código único de autorización emitido por EAPB/plan de beneficios para validar servicios solicitados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMAUTSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Autorizacion de Servicios emitida por la EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMAUTSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMAUTSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento es autorización de servicios (BIT). Indicador booleano: 1=documento recibido es autorización de servicios de EAPB, 0=otro tipo de documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'DOCAUTSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el documento recibido es una Autorizacion de Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'DOCAUTSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'DOCAUTSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno solicitud servicios (NUMERIC 18, FK→ADAUTSERC). Identificador de la solicitud adicional de servicios o autorización relacionada; vincula con transacción origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno de Tabla - Solicitud de Servicios Adicionales: Este campo se diligencia cuando corresponde a un archivo enviado desde una transaccion de solicitud de servicios adicionales, para poder identificar con que transaccion se envio. O cuando corresponde a una autorizacion de servicios enviada por la EAPB, se especifica a que solicitud de la IPS corresonde.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo documento por transacción (CHAR 1). Clasificación: 1=Enviado por IPS, 2=Recibido de EAPB, 3=Escaneado/Digitalizado en centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'TIPDOCTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Documento por Transaccion  1: Documento Enviado  2: Documento Recibido  3: Documento Escaneado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'TIPDOCTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'TIPDOCTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (CHAR 20). Identificador del operario, profesional de salud o administrativo que registró el documento en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales archivo (CHAR 1000). Comentarios o notas administrativas sobre el documento, estado, validaciones o incidencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'OBSGENARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones Generales del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'OBSGENARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'OBSGENARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión archivo (CHAR 5). Formato del archivo guardado: pdf, jpg, png, docx, tif, etc. Tipo de documento digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extension del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre archivo original (CHAR 255). Denominación del archivo antes de ser renombrado por el sistema con consecutivo de almacenamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NOMANTARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre del archivo antes de ser renombrado por el consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NOMANTARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NOMANTARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código RIPS procedimiento/servicio (CHAR 20). Código único del procedimiento, servicio o examen según clasificación de facturación RIPS nacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional (CHAR 10, FK→INUNIFUNC). Identificador del departamento, sala, urgencia, consulta o unidad donde se solicita el servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica servicio (VARCHAR MAX). Fundamentación médica, diagnóstico, síntomas o condición clínica que justifica la solicitud del servicio o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion Clinica para la solicitud del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'JUSCLISER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código entidad (CHAR 9, FK→INENTIDAD). Identificador de la aseguradora, EAPB, empresa administradora o entidad responsable del pago de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número folio historia clínica (CHAR 10). Identificador del folio o número de expediente en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio de Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso (CHAR 10, FK→ADINGRESO). Identificador único del episodio de atención, hospitalización, urgencia o consulta del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII-MASKED). Cédula, identificación, documento o número de afiliación único del paciente. Ofuscado para privacidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha registro documento (DATETIME). Fecha y hora en que se registró el documento en el sistema de almacenamiento documental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECREGDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECREGDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'FECREGDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de atención (CHAR 10, FK→ADCENATEN). Identificador de la IPS, clínica, hospital, sede o punto de servicio que genera el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion de donde se genera el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código documento almacenado (NUMERIC 18, PK IDENTITY). Identificador único autoincremental del archivo físico; representa el nombre renombrado en repositorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del documento almacenado - Representa el nombre del archivo fisico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'CODDOCALM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos adicionales adjuntos a un ingreso o atención del paciente, incluyendo autorizaciones de servicios, soportes de copagos, cuotas moderadoras y archivos digitalizados asociados al episodio asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC';
