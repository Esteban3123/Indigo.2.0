CREATE TABLE [dbo].[ADRECO3047] (
    [AUTO]      NUMERIC (18)   IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)      NOT NULL,
    [CONPARENT] INT            NOT NULL,
    [CORREOENV] VARCHAR (60)   NOT NULL,
    [ASUNTOCOR] VARCHAR (100)  NOT NULL,
    [MENSAJECO] VARCHAR (5000) NOT NULL,
    [FECHAENVI] DATETIME       NOT NULL,
    [REPRIRENV] BIT            NULL,
    [RESEGRENV] BIT            NULL,
    [FEPRIRENV] DATETIME       NULL,
    [FESEGRENV] DATETIME       NULL,
    [NUMINFORM] CHAR (4)       NOT NULL,
    [CONSINFOR] NUMERIC (18)   NOT NULL,
    [TIPOREPOR] CHAR (1)       NULL,
    CONSTRAINT [PK_ADRECO3047] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Reporte  1: Atencion Inicial Urgencias;  2:Inconsistencias;  3:Solicitud de Servicios;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'TIPOREPOR';

