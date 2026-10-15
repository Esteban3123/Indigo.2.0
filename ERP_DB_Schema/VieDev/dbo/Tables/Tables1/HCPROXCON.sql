CREATE TABLE [dbo].[HCPROXCON] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [NUMPROCON] INT                                                                              NOT NULL,
    [UNDMEDPRO] CHAR (1)                                                                         NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_HCPROXCON] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPROXCON_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPROXCON_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCPROXCON] NOCHECK CONSTRAINT [FK_HCPROXCON_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPROXCON] NOCHECK CONSTRAINT [FK_HCPROXCON_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del proximo control  1. Dias  2. Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPROXCON', @level2type = N'COLUMN', @level2name = N'UNDMEDPRO';

