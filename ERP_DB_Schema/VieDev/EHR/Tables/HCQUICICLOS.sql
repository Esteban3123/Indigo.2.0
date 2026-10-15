CREATE TABLE [EHR].[HCQUICICLOS] (
    [ID]            INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCQUIORDENC] INT                                                                           NOT NULL,
    [CICLO]         INT                                                                           NOT NULL,
    [SEMANA]        INT                                                                           NOT NULL,
    [DIA]           INT                                                                           NOT NULL,
    [ESTADOCICLO]   INT                                                                           NOT NULL,
    [NUMEFOLIO]     NCHAR (10)                                                                    NOT NULL,
    [IPCODPACI]     CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HCQUICICLOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);

