CREATE TABLE [dbo].[INTERCABE] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_INTERCABE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_IPCODPACI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_NUMINGRES_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES])
);


GO
ALTER TABLE [dbo].[INTERCABE] NOCHECK CONSTRAINT [FK_IPCODPACI_INPACIENT];


GO
ALTER TABLE [dbo].[INTERCABE] NOCHECK CONSTRAINT [FK_NUMINGRES_ADINGRESO];

