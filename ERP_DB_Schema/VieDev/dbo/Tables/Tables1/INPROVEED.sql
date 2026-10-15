CREATE TABLE [dbo].[INPROVEED] (
    [CODPROVEE] CHAR (15)                                                          NOT NULL,
    [CODIGONIT] CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)') NOT NULL,
    [NOMPROVEE] CHAR (60)                                                          NOT NULL,
    [DIRPROVEE] CHAR (60)                                                          NULL,
    [TELPROVEE] CHAR (15)                                                          NULL,
    [MOVPROVEE] CHAR (15)                                                          NULL,
    [FAXPROVEE] CHAR (15)                                                          NULL,
    [DEPMUNCOD] CHAR (5)                                                           NOT NULL,
    [PLAPROVEE] TINYINT                                                            NOT NULL,
    [CUPMAXPRO] NUMERIC (15)                                                       NOT NULL,
    [CONPROVEE] CHAR (40)                                                          NOT NULL,
    [IVAPORVEE] BIT                                                                NOT NULL,
    [RETPERPRO] BIT                                                                NOT NULL,
    CONSTRAINT [PK_INPROVEED] PRIMARY KEY CLUSTERED ([CODPROVEE] ASC),
    CONSTRAINT [FK_INPROVEED_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[INPROVEED] NOCHECK CONSTRAINT [FK_INPROVEED_INMunicip];

