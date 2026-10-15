CREATE TABLE [dbo].[HCHEMSIVI] (
    [CONSECUTI]  NUMERIC (18)                                                             NOT NULL,
    [REGREGIST]  DATETIME                                                                 NOT NULL,
    [VALORPSPD]  CHAR (10)                                                                NOT NULL,
    [VALFRECARD] CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRate_Ofuscado", 0)') NOT NULL,
    [PMCONDUTI]  CHAR (10)                                                                NOT NULL,
    [PMFLUJBOM]  CHAR (10)                                                                NOT NULL,
    [NUVALORPA]  CHAR (10)                                                                NOT NULL,
    [NUVALORPV]  CHAR (10)                                                                NOT NULL,
    [NVALORPTM]  CHAR (10)                                                                NOT NULL,
    [OBSERVACI]  CHAR (400)                                                               NULL,
    CONSTRAINT [PK_HCHEMSIVI] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC, [REGREGIST] ASC),
    CONSTRAINT [FK_HCHEMSIVI_HCHEMODIA] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCHEMODIA] ([CONSECUTI])
);


GO
ALTER TABLE [dbo].[HCHEMSIVI] NOCHECK CONSTRAINT [FK_HCHEMSIVI_HCHEMODIA];

