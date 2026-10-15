CREATE TABLE [dbo].[HCSOLTRASLADOSINT] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NOT NULL,
    [ESTADO]          INT                                                                              NOT NULL,
    [IDHCMOANULB]     CHAR (4)                                                                         NOT NULL,
    [CODCENATEOR]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGOOR]     CHAR (10)                                                                        NOT NULL,
    [CODCENATEDE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGODE]     CHAR (10)                                                                        NOT NULL,
    [USUARIOSOLICITA] CHAR (20)                                                                        NOT NULL,
    [OBSERVACION]     VARCHAR (MAX)                                                                    NULL,
    [NOVEDAD]         VARCHAR (MAX)                                                                    NULL,
    [FECHAANULA]      DATETIME                                                                         NULL,
    [USUARIOANULA]    CHAR (20)                                                                        NULL,
    [IDHCMOANULA]     CHAR (4)                                                                         NULL,
    [JUSTIFICAANULA]  VARCHAR (MAX)                                                                    NULL,
    [USUARIOVISADO]   CHAR (20)                                                                        NULL,
    [FECHAVISADO]     DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCSOLTRASLADOSINT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_ADCENATEN1] FOREIGN KEY ([CODCENATEDE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_ADCENATEN2] FOREIGN KEY ([CODCENATEOR]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_HCMOANULB] FOREIGN KEY ([IDHCMOANULA]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_HCMOANULB1] FOREIGN KEY ([IDHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_INUNIFUNC] FOREIGN KEY ([UFUCODIGOOR]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_INUNIFUNC1] FOREIGN KEY ([UFUCODIGODE]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_ADCENATEN1];


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_ADCENATEN2];


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_ADINGRESO];


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_HCMOANULB];


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_HCMOANULB1];


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_INPACIENT];


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCSOLTRASLADOSINT] NOCHECK CONSTRAINT [FK_HCSOLTRASLADOSINT_INUNIFUNC1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario del visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la tabla de motivos de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que se llena desde el dashboard de tralados internos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NOVEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que solicita el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOSOLICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'unidad funcional destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro atención destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional orginen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGOOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atención origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la Solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  1 - solicitado   2 - Ambulancia asignada   3 - Traslado Iniciado   4 - Traslado completado   5 - Cancelado (Pasa aqui cuando la solicitud del traslado no tenia movimientos es decir estaba en solicitada)  6  - Registro Visado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la trazabilidad de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ID';

