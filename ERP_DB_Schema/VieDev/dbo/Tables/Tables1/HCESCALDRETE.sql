CREATE TABLE [dbo].[HCESCALDRETE] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]    DATETIME                                                                         NOT NULL,
    [ACTIVIDAD]    TINYINT                                                                          NOT NULL,
    [RESPIRACION]  TINYINT                                                                          NOT NULL,
    [CIRCULACION]  TINYINT                                                                          NOT NULL,
    [NIVCONSCIENC] TINYINT                                                                          NOT NULL,
    [SATURAOXIG]   TINYINT                                                                          NOT NULL,
    [RESULTADO]    INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCESCALDRETE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALDRETE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCALDRETE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCALDRETE] NOCHECK CONSTRAINT [FK_HCESCALDRETE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCALDRETE] NOCHECK CONSTRAINT [FK_HCESCALDRETE_INPACIENT];

