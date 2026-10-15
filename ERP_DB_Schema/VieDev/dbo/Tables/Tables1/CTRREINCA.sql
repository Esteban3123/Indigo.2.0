CREATE TABLE [dbo].[CTRREINCA] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [FECCONTRO] DATETIME                                                                         NOT NULL,
    [CODUSUCRE] CHAR (20)                                                                        NOT NULL,
    [SENTENCIA] VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_CTRREINCA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_CTRREINCA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CTRREINCA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[CTRREINCA] NOCHECK CONSTRAINT [FK_CTRREINCA_ADINGRESO];


GO
ALTER TABLE [dbo].[CTRREINCA] NOCHECK CONSTRAINT [FK_CTRREINCA_INPACIENT];

