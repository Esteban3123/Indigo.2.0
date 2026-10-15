CREATE TABLE [dbo].[ADFURPROU] (
    [NUMRADANT]  CHAR (10)                                                                    NULL,
    [RESPGLOSA]  CHAR (1)                                                                     NULL,
    [NUMINGRES]  CHAR (10)                                                                    NOT NULL,
    [NUMFACTUR]  CHAR (20)                                                                    NOT NULL,
    [CODIPSSEC]  CHAR (12)                                                                    NOT NULL,
    [PRIAPEVIC]  CHAR (20)                                                                    NOT NULL,
    [SEGAPEVIC]  CHAR (30)                                                                    NULL,
    [PRINOMVIC]  CHAR (20)                                                                    NOT NULL,
    [SEGNOMVIC]  CHAR (30)                                                                    NULL,
    [TIPDOCVIC]  CHAR (2)                                                                     NOT NULL,
    [NUMDOCVIC]  VARCHAR (25)                                                                 NULL,
    [FECNACVIC]  DATETIME                                                                     NOT NULL,
    [SEXOVICTI]  CHAR (1)                                                                     NOT NULL,
    [DIRRESVIC]  CHAR (40)                                                                    NOT NULL,
    [CODDEPVIC]  CHAR (2)                                                                     NOT NULL,
    [CODMUNVIC]  CHAR (3)                                                                     NOT NULL,
    [NUMTELVIC]  CHAR (10)                                                                    NOT NULL,
    [CODAFIVIC]  CHAR (1)                                                                     NOT NULL,
    [REGACTVIC]  CHAR (1)                                                                     NULL,
    [CODEPSVIC]  CHAR (6)                                                                     NULL,
    [NATUEVENT]  CHAR (2)                                                                     NOT NULL,
    [DESOTREVE]  CHAR (25)                                                                    NULL,
    [DIROCUEVE]  CHAR (40)                                                                    NOT NULL,
    [FECOCUEVE]  DATETIME                                                                     NOT NULL,
    [CODDEPEVE]  CHAR (2)                                                                     NOT NULL,
    [CODMUNEVE]  CHAR (3)                                                                     NOT NULL,
    [CODZONEVE]  CHAR (1)                                                                     NOT NULL,
    [DESBREEVEN] CHAR (255)                                                                   NOT NULL,
    [CODDIAPRI]  CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)') NOT NULL,
    [CODDIAAS1]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS2]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS3]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS4]  CHAR (4)                                                                     NOT NULL,
    [DESPROTES]  CHAR (100)                                                                   NOT NULL,
    [VALRECPRO]  CHAR (15)                                                                    NOT NULL,
    [VALADAPRO]  CHAR (15)                                                                    NOT NULL,
    [VALREAPRO]  CHAR (15)                                                                    NOT NULL,
    [VALTOTREC]  CHAR (15)                                                                    NOT NULL,
    [NUMTOTFOL]  CHAR (3)                                                                     NOT NULL,
    [FECRADFUR]  DATETIME                                                                     NOT NULL,
    CONSTRAINT [PK_ADFURPROU] PRIMARY KEY CLUSTERED ([NUMFACTUR] ASC),
    CONSTRAINT [FK_ADFURPROU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADFURPROU_INPACIENT] FOREIGN KEY ([NUMDOCVIC]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADFURPROU] NOCHECK CONSTRAINT [FK_ADFURPROU_ADINGRESO];


GO
ALTER TABLE [dbo].[ADFURPROU] NOCHECK CONSTRAINT [FK_ADFURPROU_INPACIENT];

