CREATE TABLE [dbo].[HCENCPACC] (
    [AUTO]         INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCENCENF] INT                                                                              NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECENCPAC]    DATETIME                                                                         NOT NULL,
    [FECENCSIS]    DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_HCENCPACC] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCENCPACC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCENCPACC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCENCPACC] NOCHECK CONSTRAINT [FK_HCENCPACC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCENCPACC] NOCHECK CONSTRAINT [FK_HCENCPACC_INPACIENT];

