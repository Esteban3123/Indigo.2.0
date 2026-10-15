CREATE TABLE [dbo].[HCDOCHISTORIASCLINICAS] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO]    DATETIME                                                                         NOT NULL,
    [USUARIOSUBE]      CHAR (20)                                                                        NOT NULL,
    [NOMBREARCHIVO]    VARCHAR (500)                                                                    NOT NULL,
    [EXTENSIONARCHIVO] VARCHAR (5)                                                                      NOT NULL,
    [TIPODOCUMENTO]    CHAR (3)                                                                         NOT NULL,
    [OBSERVACION]      VARCHAR (MAX)                                                                    NOT NULL,
    CONSTRAINT [PK_HCDOCHISTORIASCLINICAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCDOCHISTORIASCLINICAS] NOCHECK CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCDOCHISTORIASCLINICAS] NOCHECK CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension del archivo normalmente son   .PDF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'EXTENSIONARCHIVO';

