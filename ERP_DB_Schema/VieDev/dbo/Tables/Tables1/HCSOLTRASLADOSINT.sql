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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  1 - solicitado   2 - Ambulancia asignada   3 - Traslado Iniciado   4 - Traslado completado   5 - Cancelado (Pasa aqui cuando la solicitud del traslado no tenia movimientos es decir estaba en solicitada)  6  - Registro Visado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la trazabilidad de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ID';

