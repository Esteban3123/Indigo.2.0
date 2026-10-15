CREATE TABLE [dbo].[ADAUTEVEN] (
    [CODREGUNI]  NUMERIC (10)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODENTIDA]  CHAR (9)                                                                         NOT NULL,
    [FECREGEVE]  DATETIME                                                                         NOT NULL,
    [TIPREPENT]  CHAR (1)                                                                         NOT NULL,
    [NUMTELCON]  CHAR (12)                                                                        NULL,
    [NUMEXTTEL]  CHAR (6)                                                                         NULL,
    [FECHORINI]  DATETIME                                                                         NULL,
    [FECHORFIN]  DATETIME                                                                         NULL,
    [NOMCONENT]  CHAR (80)                                                                        NULL,
    [CARCONENT]  CHAR (50)                                                                        NULL,
    [NUMFAXCON]  CHAR (12)                                                                        NULL,
    [NUMEXTFAX]  CHAR (6)                                                                         NULL,
    [NUMINTENV]  CHAR (1)                                                                         NULL,
    [FECENVFAX]  DATETIME                                                                         NULL,
    [URLSERWEB]  CHAR (150)                                                                       NULL,
    [FECREGWEB]  DATETIME                                                                         NULL,
    [CODDOCALM]  NUMERIC (18)                                                                     NULL,
    [NUMVALDER]  CHAR (20)                                                                        NULL,
    [COMGENREG]  VARCHAR (2000)                                                                   NULL,
    [CODUSUARI]  CHAR (20)                                                                        NOT NULL,
    [CANSERAUT]  TINYINT                                                                          NULL,
    [ESTADO]     INT                                                                              NULL,
    [ADAUTSERID] INT                                                                              NULL,
    [TIPOTRAZA]  INT                                                                              NULL,
    [PACIENTNOT] BIT                                                                              NULL,
    [INFOPACI]   VARCHAR (4000)                                                                   NULL,
    CONSTRAINT [PK_ADAUTEVEN] PRIMARY KEY CLUSTERED ([CODREGUNI] ASC),
    CONSTRAINT [FK_ADAUTEVEN_ADDOCADIC] FOREIGN KEY ([CODDOCALM]) REFERENCES [dbo].[ADDOCADIC] ([CODDOCALM]),
    CONSTRAINT [FK_ADAUTEVEN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADAUTEVEN_INENTIDAD] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTIDAD] ([CODENTIDA]),
    CONSTRAINT [FK_ADAUTEVEN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADAUTEVEN] NOCHECK CONSTRAINT [FK_ADAUTEVEN_ADDOCADIC];


GO
ALTER TABLE [dbo].[ADAUTEVEN] NOCHECK CONSTRAINT [FK_ADAUTEVEN_ADINGRESO];


GO
ALTER TABLE [dbo].[ADAUTEVEN] NOCHECK CONSTRAINT [FK_ADAUTEVEN_INENTIDAD];


GO
ALTER TABLE [dbo].[ADAUTEVEN] NOCHECK CONSTRAINT [FK_ADAUTEVEN_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTEVEN__IPCODPACI__NUMINGRES]
    ON [dbo].[ADAUTEVEN]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTEVEN__ADAUTSERID__INC__TIPOTRAZA]
    ON [dbo].[ADAUTEVEN]([ADAUTSERID] ASC)
    INCLUDE([TIPOTRAZA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente notificado   Si: True-1  No: False-2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'PACIENTNOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el tipo de trazabilidad que agregó el evento:  1. Atención inicial de urgencias  2. Autorización de servicios  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPOTRAZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autorizado = 1  No Autorizado = 2  Pedniente de Autorización = 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Reporte Entidad:  1 - Llamada Telefonica  2 - Envio de FAX  3 - Registro Pagina Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPREPENT';

