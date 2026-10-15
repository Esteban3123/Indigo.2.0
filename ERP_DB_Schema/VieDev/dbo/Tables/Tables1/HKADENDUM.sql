CREATE TABLE [dbo].[HKADENDUM] (
    [ID]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud]        INT           NULL,
    [codigo_interno_servicio] INT           NULL,
    [cups]                    VARCHAR (20)  NULL,
    [item]                    INT           NULL,
    [fecha]                   DATE          NULL,
    [hora]                    TIME (7)      NULL,
    [resultados]              VARCHAR (MAX) NULL,
    [codigo_radiologo]        VARCHAR (20)  NULL,
    CONSTRAINT [PK_HKADEMDUM] PRIMARY KEY CLUSTERED ([ID] ASC)
);

