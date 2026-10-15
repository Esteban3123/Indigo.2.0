CREATE TABLE [dbo].[AGECONTROLPAC] (
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_AGECONTROLPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGECONTROLPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[AGECONTROLPAC] NOCHECK CONSTRAINT [FK_AGECONTROLPAC_INPACIENT];

