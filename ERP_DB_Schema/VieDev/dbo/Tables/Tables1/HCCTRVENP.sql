CREATE TABLE [dbo].[HCCTRVENP] (
    [CONSECUTI]             NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]             VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                        NOT NULL,
    [CODCENATE]             CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]             CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [ESCATHEPA]             BIT                                                                              NOT NULL,
    [NOMCATETE]             CHAR (60)                                                                        NULL,
    [NUMCATETE]             CHAR (10)                                                                        NULL,
    [NOMVENUTI]             CHAR (60)                                                                        NOT NULL,
    [EXTREMIDA]             CHAR (2)                                                                         NULL,
    [ESTACTIVA]             BIT                                                                              NOT NULL,
    [FECHAINIC]             DATETIME                                                                         NOT NULL,
    [FECHAFINA]             DATETIME                                                                         NULL,
    [MOTITERMI]             VARCHAR (300)                                                                    NULL,
    [CODPROACT]             CHAR (20)                                                                        NULL,
    [FECREGINI]             DATETIME                                                                         NOT NULL,
    [FECREGFIN]             DATETIME                                                                         NULL,
    [CatheterTypeId]        INT                                                                              NULL,
    [NumberOfVenipuncture]  INT                                                                              NULL,
    [DiscontinuationReason] INT                                                                              NULL,
    [CODCENATESUS]          CHAR (10)                                                                        NULL,
    [UFUCODIGOSUS]          CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCCTRVENP] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCCTRVENP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRVENP_ADCENATEN1] FOREIGN KEY ([CODCENATESUS]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRVENP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRVENP_CatheterType] FOREIGN KEY ([CatheterTypeId]) REFERENCES [dbo].[CatheterType] ([Id]),
    CONSTRAINT [FK_HCCTRVENP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRVENP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRVENP_INPROFSAL1] FOREIGN KEY ([CODPROACT]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRVENP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCCTRVENP_INUNIFUNC1] FOREIGN KEY ([UFUCODIGOSUS]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_ADCENATEN1];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_CatheterType];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INPROFSAL1];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCCTRVENP] NOCHECK CONSTRAINT [FK_HCCTRVENP_INUNIFUNC1];


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRVENP__IPCODPACI__NUMINGRES__NOMVENUTI]
    ON [dbo].[HCCTRVENP]([IPCODPACI] ASC, [NUMINGRES] ASC, [NOMVENUTI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Sistema al crear  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECREGINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Venopuncion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'FECHAINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'la Vena esta activa:  Esta Siendo usada en ese Momento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVENP', @level2type = N'COLUMN', @level2name = N'ESTACTIVA';

