CREATE TABLE [dbo].[INTERCABI] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    CONSTRAINT [PK_INTERCABI] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);

