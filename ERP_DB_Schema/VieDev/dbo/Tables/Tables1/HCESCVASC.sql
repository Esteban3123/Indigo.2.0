CREATE TABLE [dbo].[HCESCVASC] (
    [CODCONSEC]     NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]     DATETIME                                                                         NOT NULL,
    [NUMEFOLIO]     NCHAR (10)                                                                       NULL,
    [CODESPECI]     CHAR (3)                                                                         NULL,
    [CONSECTRIAGEU] CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCESCVASC] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCESCVASC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES])
);


GO
ALTER TABLE [dbo].[HCESCVASC] NOCHECK CONSTRAINT [FK_HCESCVASC_ADINGRESO];


GO
CREATE NONCLUSTERED INDEX [HCESCVASC_IPCODPACI_NUMINGRES]
    ON [dbo].[HCESCVASC]([IPCODPACI] ASC, [NUMINGRES] ASC);

