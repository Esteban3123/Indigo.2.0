CREATE TABLE [Report].[TablaResultadosLaboratorio] (
    [NUMINGRES]       CHAR (10)      NOT NULL,
    [IPCODPACI]       VARCHAR (25)   NOT NULL,
    [IDHCHISPACA]     INT            NOT NULL,
    [IDEXAGRUPO]      INT            NOT NULL,
    [NOMBRE_GRUPO]    VARCHAR (100)  NOT NULL,
    [VALOR_GRUPO]     INT            NOT NULL,
    [ID_VARIABLE]     INT            NOT NULL,
    [NOMBRE_VARIABLE] VARCHAR (150)  NOT NULL,
    [OPCION]          TINYINT        NULL,
    [VALOR]           VARCHAR (5000) NOT NULL,
    [ID]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_TablaResultadosLaboratorio] PRIMARY KEY CLUSTERED ([ID] ASC)
);

