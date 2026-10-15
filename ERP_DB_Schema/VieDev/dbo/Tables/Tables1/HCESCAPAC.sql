CREATE TABLE [dbo].[HCESCAPAC] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [VALTEMPER] CHAR (10)                                                                        NULL,
    [PTSTEMPER] TINYINT                                                                          NULL,
    [VALPREART] CHAR (10)                                                                        NULL,
    [PTSPREART] TINYINT                                                                          NULL,
    [VALFRECAR] CHAR (10)                                                                        NULL,
    [PTSFRECAR] TINYINT                                                                          NULL,
    [VALFRERES] NCHAR (10)                                                                       NULL,
    [PTSFRERES] TINYINT                                                                          NULL,
    [VALFIOXI2] NCHAR (10)                                                                       NULL,
    [VALAADOX2] NCHAR (10)                                                                       NULL,
    [VALPAOXI2] NCHAR (10)                                                                       NULL,
    [PTSPREOXI] TINYINT                                                                          NULL,
    [VALPHARTE] NCHAR (10)                                                                       NULL,
    [PTSPHARTE] TINYINT                                                                          NULL,
    [VALSODPLA] NCHAR (10)                                                                       NULL,
    [PTSSODPLA] NCHAR (10)                                                                       NULL,
    [VALPOTPLA] NCHAR (10)                                                                       NULL,
    [PTSPOTPLA] TINYINT                                                                          NULL,
    [VALCREATI] NCHAR (10)                                                                       NULL,
    [PTSCREATI] TINYINT                                                                          NULL,
    [VALHEMATO] NCHAR (10)                                                                       NULL,
    [PTSHEMATO] TINYINT                                                                          NULL,
    [VALLEUCOC] NCHAR (10)                                                                       NULL,
    [PTSLEUCOC] TINYINT                                                                          NULL,
    [VALEDAPAC] TINYINT                                                                          NULL,
    [PTSEDAPAC] TINYINT                                                                          NULL,
    [VALENFCRO] NCHAR (10)                                                                       NULL,
    [PTSENFCRO] TINYINT                                                                          NULL,
    [VALGLAGLO] NCHAR (10)                                                                       NULL,
    [PTSGLAGLO] TINYINT                                                                          NULL,
    CONSTRAINT [PK_HCESCAPAC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCESCAPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCAPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCAPAC] NOCHECK CONSTRAINT [FK_HCESCAPAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCAPAC] NOCHECK CONSTRAINT [FK_HCESCAPAC_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCAPAC', @level2type = N'COLUMN', @level2name = N'VALTEMPER';

