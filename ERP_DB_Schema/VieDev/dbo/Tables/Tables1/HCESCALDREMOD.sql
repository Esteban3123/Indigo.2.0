CREATE TABLE [dbo].[HCESCALDREMOD] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]   CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]   DATETIME                                                                         NOT NULL,
    [ACTIVIDAD]   TINYINT                                                                          NOT NULL,
    [RESPIRACION] TINYINT                                                                          NOT NULL,
    [CIRCULACION] TINYINT                                                                          NOT NULL,
    [NIVCONCIEN]  TINYINT                                                                          NOT NULL,
    [SATUOXIGE]   TINYINT                                                                          NOT NULL,
    [APOQUIRUR]   TINYINT                                                                          NOT NULL,
    [DOLOR]       TINYINT                                                                          NOT NULL,
    [DEAMBULAC]   TINYINT                                                                          NOT NULL,
    [ALIMENTAC]   TINYINT                                                                          NOT NULL,
    [MICCION]     TINYINT                                                                          NOT NULL,
    [RESULTADO]   INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCESCALDREMOD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALDREMOD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCALDREMOD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCALDREMOD] NOCHECK CONSTRAINT [FK_HCESCALDREMOD_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCALDREMOD] NOCHECK CONSTRAINT [FK_HCESCALDREMOD_INPACIENT];

