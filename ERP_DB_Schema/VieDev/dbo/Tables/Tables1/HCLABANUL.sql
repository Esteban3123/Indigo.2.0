CREATE TABLE [dbo].[HCLABANUL] (
    [CODCONSEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [CODMOTANU] CHAR (4)                                                                         NULL,
    [OBSERVACI] CHAR (80)                                                                        NULL,
    [FECANULAB] DATETIME                                                                         NOT NULL,
    [USUANULAB] CHAR (20)                                                                        NOT NULL,
    [INFENFANU] BIT                                                                              NOT NULL,
    [TIPOLABO]  TINYINT                                                                          NULL,
    CONSTRAINT [PK_HCLABANUL] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCLABANUL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCLABANUL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCLABANUL_HCMOANULB] FOREIGN KEY ([CODMOTANU]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCLABANUL_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCLABANUL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCLABANUL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCLABANUL] NOCHECK CONSTRAINT [FK_HCLABANUL_ADCENATEN];


GO
ALTER TABLE [dbo].[HCLABANUL] NOCHECK CONSTRAINT [FK_HCLABANUL_ADINGRESO];


GO
ALTER TABLE [dbo].[HCLABANUL] NOCHECK CONSTRAINT [FK_HCLABANUL_HCMOANULB];


GO
ALTER TABLE [dbo].[HCLABANUL] NOCHECK CONSTRAINT [FK_HCLABANUL_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCLABANUL] NOCHECK CONSTRAINT [FK_HCLABANUL_INPACIENT];


GO
ALTER TABLE [dbo].[HCLABANUL] NOCHECK CONSTRAINT [FK_HCLABANUL_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCLABANUL__NUMEFOLIO__NUMINGRES__IPCODPACI__CODCENATE__UFUCODIGO__CODSERIPS]
    ON [dbo].[HCLABANUL]([NUMEFOLIO] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC, [CODSERIPS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el tipo de laboratorio anulado:  1: Intrahospitalario  2: Ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLABANUL', @level2type = N'COLUMN', @level2name = N'TIPOLABO';

