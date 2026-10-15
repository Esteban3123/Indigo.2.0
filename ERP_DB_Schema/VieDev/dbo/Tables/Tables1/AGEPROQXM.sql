CREATE TABLE [dbo].[AGEPROQXM] (
    [AUTONUMER] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [FECHORAIN] DATETIME                                                                         NOT NULL,
    [FECHORAFI] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_AGEPROQXM] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGEPROQXM_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGEPROQXM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[AGEPROQXM] NOCHECK CONSTRAINT [FK_AGEPROQXM_ADCENATEN];


GO
ALTER TABLE [dbo].[AGEPROQXM] NOCHECK CONSTRAINT [FK_AGEPROQXM_INPACIENT];

