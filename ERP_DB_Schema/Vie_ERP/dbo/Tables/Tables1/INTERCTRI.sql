CREATE TABLE [dbo].[INTERCTRI] (
    [AUTO]         INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20)                                                                  NOT NULL,
    [NUMUESTRA]    TINYINT                                                                       NOT NULL,
    [ESTADOINT]    BIT                                                                           NOT NULL,
    [INTERPRET]    VARCHAR (MAX)                                                                 NULL,
    [AUTOLABOR]    INT                                                                           NOT NULL,
    [FECGENERA]    VARCHAR (20)                                                                  NULL,
    [FECREGIST]    DATETIME                                                                      NULL,
    [FECSERIPS]    VARCHAR (20)                                                                  NULL,
    [CODPROSAL]    CHAR (70) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_INTERCTRI] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);

