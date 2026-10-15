CREATE TABLE [dbo].[HCLISTREC] (
    [CODCONSEC]  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC] INT                                                                              NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NULL,
    [FECHRESPE]  DATETIME                                                                         NOT NULL,
    [CODUSUCRE]  CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_HCLISTREC] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCLISTREC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCLISTREC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCLISTREC] NOCHECK CONSTRAINT [FK_HCLISTREC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCLISTREC] NOCHECK CONSTRAINT [FK_HCLISTREC_INPACIENT];

