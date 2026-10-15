CREATE TABLE [dbo].[HCPRODIAI] (
    [CODPRODUC] CHAR (20)                                                                     NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')  NOT NULL,
    [CODCONCEC] NUMERIC (18)                                                                  NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [MANEXTPRO] BIT                                                                           NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                  NOT NULL,
    CONSTRAINT [PK_HCPRODIAI] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC, [CODDIAGNO] ASC, [CODCONCEC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCPRODIAI_HCPRESCRC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCPRESCCI] ([CODCONCEC]),
    CONSTRAINT [FK_HCPRODIAI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCPRODIAI] NOCHECK CONSTRAINT [FK_HCPRODIAI_HCPRESCRC];


GO
ALTER TABLE [dbo].[HCPRODIAI] NOCHECK CONSTRAINT [FK_HCPRODIAI_INDIAGNOS];

