CREATE TABLE [dbo].[HCNOSERFA] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC] DATETIME                                                                         NOT NULL,
    [OBSERVACI] VARCHAR (MAX)                                                                    NULL,
    [CONSFOLIO] CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCNOSERFA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCNOSERFA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCNOSERFA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCNOSERFA] NOCHECK CONSTRAINT [FK_HCNOSERFA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCNOSERFA] NOCHECK CONSTRAINT [FK_HCNOSERFA_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCNOSERFA__IPCODPACI]
    ON [dbo].[HCNOSERFA]([IPCODPACI] ASC);

