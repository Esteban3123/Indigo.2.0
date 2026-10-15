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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Documento por Transaccion  1: Documento Enviado  2: Documento Recibido  3: Documento Escaneado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDOCADIC', @level2type = N'COLUMN', @level2name = N'TIPDOCTRA';

