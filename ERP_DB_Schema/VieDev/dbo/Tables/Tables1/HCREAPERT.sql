CREATE TABLE [dbo].[HCREAPERT] (
    [NUMCONSEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECPROCES] DATETIME                                                                         NOT NULL,
    [MOTREAPER] CHAR (256)                                                                       NOT NULL,
    CONSTRAINT [PK_HCREAPERT] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_HCREAPERT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCREAPERT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREAPERT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCREAPERT_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCREAPERT_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCREAPERT] NOCHECK CONSTRAINT [FK_HCREAPERT_ADCENATEN];


GO
ALTER TABLE [dbo].[HCREAPERT] NOCHECK CONSTRAINT [FK_HCREAPERT_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREAPERT] NOCHECK CONSTRAINT [FK_HCREAPERT_INPACIENT];


GO
ALTER TABLE [dbo].[HCREAPERT] NOCHECK CONSTRAINT [FK_HCREAPERT_INPROFSAL];


GO
ALTER TABLE [dbo].[HCREAPERT] NOCHECK CONSTRAINT [FK_HCREAPERT_INUNIFUNC];

