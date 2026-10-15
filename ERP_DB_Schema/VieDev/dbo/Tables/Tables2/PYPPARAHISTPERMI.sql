CREATE TABLE [dbo].[PYPPARAHISTPERMI] (
    [ID]             INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPYPPARAHISTO] INT                                                                           NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_PYPPARAHISTP] PRIMARY KEY CLUSTERED ([ID] ASC)
);

