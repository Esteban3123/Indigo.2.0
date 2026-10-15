CREATE TABLE [dbo].[HCEXFISANE] (
    [ID]             INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]      CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]      CHAR (10)                                                                        NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]      DATETIME                                                                         CONSTRAINT [DF_HCEXFISANE_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ANESOSTEO]      BIT                                                                              NULL,
    [ANESCOSTEO]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESMUSCU]      BIT                                                                              NULL,
    [ANESCMUSCU]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NOT NULL,
    [ANESURINA]      BIT                                                                              NULL,
    [ANESCURINA]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESLINFATICO]  BIT                                                                              NULL,
    [ANESCLINFATICO] VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESCARDIO]     BIT                                                                              NULL,
    [ANESCCARDIO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESOTORRI]     BIT                                                                              NULL,
    [ANESCOTORRI]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESRESPIRA]    BIT                                                                              NULL,
    [ANESCRESPIRA]   VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESDIGEST]     BIT                                                                              NULL,
    [ANESCDIGEST]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESPSIQ]       BIT                                                                              NULL,
    [ANESCPSIQ]      VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESDERMA]      BIT                                                                              NULL,
    [ANESCDERMA]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESENDOCRI]    BIT                                                                              NULL,
    [ANESCENDOCRI]   VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESNEUROLO]    BIT                                                                              NULL,
    [ANESCNEUROLO]   VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESGINECO]     BIT                                                                              NULL,
    [ANESCGINECO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESINMUNO]     BIT                                                                              NULL,
    [ANESCINMUNO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESNERVI]      BIT                                                                              NULL,
    [ANESCNERVI]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESOJO]        BIT                                                                              NULL,
    [ANESCOJO]       VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESCABEZA]     BIT                                                                              NULL,
    [ANESCCABEZA]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESDENTAL]     BIT                                                                              NULL,
    [ANESCDENTAL]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESCUELLO]     BIT                                                                              NULL,
    [ANESCCUELLO]    VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESABDOM]      BIT                                                                              NULL,
    [ANESCABDOM]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESEXTRE]      BIT                                                                              NULL,
    [ANESCEXTRE]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    [ANESTORAX]      BIT                                                                              NULL,
    [ANESCTORAX]     VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')     NULL,
    CONSTRAINT [PK_HCEXFISANE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_FK_HCEXFISANE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCEXFISANE_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEXFISANE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCEXFISANE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCEXFISANE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_FK_HCEXFISANE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_ADcenaten];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_INPacient];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCEXFISANE] NOCHECK CONSTRAINT [FK_HCEXFISANE_INUNIFUNC];

