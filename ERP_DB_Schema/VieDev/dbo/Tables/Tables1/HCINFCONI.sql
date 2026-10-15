CREATE TABLE [dbo].[HCINFCONI] (
    [CODCONCEC]        NUMERIC (18)                                                                     NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [ABRPROMEZ]        CHAR (200)                                                                       NULL,
    [CONMEDMEZ]        NUMERIC (18, 2)                                                                  NOT NULL,
    [UNIMEDMED]        VARCHAR (20)                                                                     NOT NULL,
    [CANPROCAL]        INT                                                                              NOT NULL,
    [UNIMEDVOL]        CHAR (3)                                                                         NOT NULL,
    [UNIMEDPES]        CHAR (3)                                                                         NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CONMEDMFN]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDMFN]        CHAR (3)                                                                         NULL,
    [JUSTIFICACIONPBS] VARCHAR (2000)                                                                   NULL,
    CONSTRAINT [PK_HCINFCONI_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCINFCONI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFCONI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFCONI_HCINFLIQC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLICI] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFCONI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFCONI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCINFCONI_INUNIMEDI] FOREIGN KEY ([UNIMEDMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_HCINFLIQC];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_INPACIENT];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_INUNIMEDI];

