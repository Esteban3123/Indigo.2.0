CREATE TABLE [dbo].[ADREGNOTA] (
    [CODDOCALM] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [OBSGENARC] CHAR (255)                                                                       NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADREGNOTA] PRIMARY KEY CLUSTERED ([CODDOCALM] ASC),
    CONSTRAINT [FK_ADREGNOTA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADREGNOTA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADREGNOTA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADREGNOTA] NOCHECK CONSTRAINT [FK_ADREGNOTA_ADCENATEN];


GO
ALTER TABLE [dbo].[ADREGNOTA] NOCHECK CONSTRAINT [FK_ADREGNOTA_ADINGRESO];


GO
ALTER TABLE [dbo].[ADREGNOTA] NOCHECK CONSTRAINT [FK_ADREGNOTA_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADREGNOTA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';

