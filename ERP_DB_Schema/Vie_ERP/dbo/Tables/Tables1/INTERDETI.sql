CREATE TABLE [dbo].[INTERDETI] (
    [AUTO]      INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT                                                                              NOT NULL,
    [AUTOLABOR] INT                                                                              NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_INTERDETI] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);

