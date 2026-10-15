CREATE TABLE [dbo].[INTERLAID] (
    [AUTO]          INT                                                                             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]     INT                                                                             NOT NULL,
    [NUMMUESTRA]    INT                                                                             NOT NULL,
    [ANALITO]       VARCHAR (70)                                                                    NOT NULL,
    [VALOR]         VARCHAR (16) MASKED WITH (FUNCTION = 'partial(0, "Value_Ofuscado", 0)')         NOT NULL,
    [UNIDAD]        VARCHAR (30)                                                                    NOT NULL,
    [OBSERVACION]   VARCHAR (7000) MASKED WITH (FUNCTION = 'partial(0, "Observation_Ofuscado", 0)') NULL,
    [DESCRIPCION]   VARCHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)')   NULL,
    [VALORMINIMO]   VARCHAR (15)                                                                    NULL,
    [VALORMAXIMO]   VARCHAR (15)                                                                    NULL,
    [AUTOLABOR]     INT                                                                             NULL,
    [CLASIFICACION] VARCHAR (60)                                                                    NULL,
    CONSTRAINT [PK_INTERLAID] PRIMARY KEY CLUSTERED ([AUTO] ASC, [CODCONCEC] ASC)
);

