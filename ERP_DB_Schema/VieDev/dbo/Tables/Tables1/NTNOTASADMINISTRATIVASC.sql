CREATE TABLE [dbo].[NTNOTASADMINISTRATIVASC] (
    [ID]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NULL,
    [IDNOTAADMINISTRATIVA] INT                                                                              NOT NULL,
    [FECHACREACION]        DATETIME                                                                         NOT NULL,
    [CODUSUARI]            CHAR (20)                                                                        NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [NOTANOFACTURABLE]     BIT                                                                              NULL,
    CONSTRAINT [PK_NTVALORESCABECERA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTNOTASADMINISTRATIVASC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_NTVALORESCABECERA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_NTVALORESCABECERA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_NTVALORESCABECERA_NTADMINISTRATIVAS] FOREIGN KEY ([IDNOTAADMINISTRATIVA]) REFERENCES [dbo].[NTADMINISTRATIVAS] ([ID])
);


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASC] NOCHECK CONSTRAINT [FK_NTNOTASADMINISTRATIVASC_ADCENATEN];


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASC] NOCHECK CONSTRAINT [FK_NTVALORESCABECERA_ADINGRESO];


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASC] NOCHECK CONSTRAINT [FK_NTVALORESCABECERA_INPACIENT];


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASC] NOCHECK CONSTRAINT [FK_NTVALORESCABECERA_NTADMINISTRATIVAS];


GO
CREATE NONCLUSTERED INDEX [IX_NTNOTASADMINISTRATIVASC_IPCODPACI]
    ON [dbo].[NTNOTASADMINISTRATIVASC]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica que la Nota diligenciada No Facturable   True   False ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASC', @level2type = N'COLUMN', @level2name = N'NOTANOFACTURABLE';

