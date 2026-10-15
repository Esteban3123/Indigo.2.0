CREATE TABLE [dbo].[HCINFPROM] (
    [CODCONCEC]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                CHAR (9)                                                                         NULL,
    [NUMEFOLIO]                NCHAR (10)                                                                       NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREAPRO]                DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [DESHALLAZ]                VARCHAR (MAX)                                                                    NOT NULL,
    [DESCOMPLI]                VARCHAR (3000)                                                                   NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    CONSTRAINT [PK_HCINFPROM] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCINFPROM_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFPROM_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFPROM_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCINFPROM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFPROM_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCINFPROM_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCINFPROM] NOCHECK CONSTRAINT [FK_HCINFPROM_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINFPROM] NOCHECK CONSTRAINT [FK_HCINFPROM_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINFPROM] NOCHECK CONSTRAINT [FK_HCINFPROM_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCINFPROM] NOCHECK CONSTRAINT [FK_HCINFPROM_INPACIENT];


GO
ALTER TABLE [dbo].[HCINFPROM] NOCHECK CONSTRAINT [FK_HCINFPROM_INPROFSAL];


GO
ALTER TABLE [dbo].[HCINFPROM] NOCHECK CONSTRAINT [FK_HCINFPROM_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCINFPROM_IPCODPACI_NUMINGRES_CODSERIPS]
    ON [dbo].[HCINFPROM]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC);

