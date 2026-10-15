CREATE TABLE [dbo].[ADLOGTRAN] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODTIPTRA] CHAR (1)                                                                         NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCONCET] NUMERIC (18)                                                                     NOT NULL,
    [NUMINFORM] CHAR (10)                                                                        NOT NULL,
    [FECINFORM] DATETIME                                                                         NOT NULL,
    [FECINITRA] DATETIME                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [ESTTRANSA] CHAR (1)                                                                         NOT NULL,
    [OBSGENTRA] CHAR (255)                                                                       NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADLOGTRAN] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODTIPTRA] ASC),
    CONSTRAINT [FK_ADLOGTRAN_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADLOGTRAN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADLOGTRAN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADLOGTRAN] NOCHECK CONSTRAINT [FK_ADLOGTRAN_ADCENATEN];


GO
ALTER TABLE [dbo].[ADLOGTRAN] NOCHECK CONSTRAINT [FK_ADLOGTRAN_ADINGRESO];


GO
ALTER TABLE [dbo].[ADLOGTRAN] NOCHECK CONSTRAINT [FK_ADLOGTRAN_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion General de la Transaccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'OBSGENTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Transaccion:  1: En Proceso de Envio  2: Envio 1 Fallido  3: Envio 2 Fallido  4: Envio 3 Fallido  5: Envio DTL Fallido  6: Envio DTD Fallido  7: Envio Satisfactorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio de Transaccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'FECINITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'FECINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Transaccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de Tabla Origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'CODCONCET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Transaccion Realizada  1: Inconsistencias en Bases de Datos  2: Informe de la Atencion Inicial de Urgencias  3: Solicitud de Autorizacion de Servicios  4: Otros Envios - Soportes Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'CODTIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADLOGTRAN', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

