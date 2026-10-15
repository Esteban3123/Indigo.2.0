CREATE TABLE [dbo].[HCPerfilFarmaD] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPERFILFARMAC] INT                                                                              NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [DOSISPROD]        NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]        VARCHAR (20)                                                                     NULL,
    [FRECUENCI]        INT                                                                              NULL,
    [UNIFRECUE]        CHAR (1)                                                                         NULL,
    [FECINIDOS]        DATETIME                                                                         NULL,
    [TIPFORMED]        CHAR (1)                                                                         NULL,
    [DURACIDOS]        CHAR (20)                                                                        NULL,
    [CANPEDPRO]        INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCPerfilFarmaD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPerfilFarmaD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPerfilFarmaD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCPerfilFarmaD] NOCHECK CONSTRAINT [FK_HCPerfilFarmaD_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPerfilFarmaD] NOCHECK CONSTRAINT [FK_HCPerfilFarmaD_INPACIENT];

