CREATE TABLE [dbo].[HCSESHEMSV] (
    [ID]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCSESHEMID]    INT             NOT NULL,
    [TIPO]          TINYINT         NOT NULL,
    [TA]            VARCHAR (7)     NOT NULL,
    [FC]            INT             NOT NULL,
    [PA]            NUMERIC (18, 1) NOT NULL,
    [SO2]           CHAR (10)       NOT NULL,
    [PV]            NUMERIC (18, 2) NOT NULL,
    [PTM]           NUMERIC (18, 2) NOT NULL,
    [FLUBOM]        NUMERIC (18, 2) NOT NULL,
    [CONDUCTIVIDAD] NUMERIC (18, 2) NOT NULL,
    [FECREGISTRO]   DATETIME        NOT NULL,
    [USUREGISTRO]   CHAR (20)       NOT NULL,
    [OBSERVACION]   VARCHAR (3000)  NULL,
    [FR]            NUMERIC (18)    NULL,
    [TEMP]          NUMERIC (18)    NULL,
    CONSTRAINT [PK_HCSESHEMSV] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSESHEMSV_HCSESHEM] FOREIGN KEY ([HCSESHEMID]) REFERENCES [dbo].[HCSESHEM] ([ID])
);


GO
ALTER TABLE [dbo].[HCSESHEMSV] NOCHECK CONSTRAINT [FK_HCSESHEMSV_HCSESHEM];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Conductividad  Unidad de medida µs/cm  de 1-40  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'CONDUCTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Flujo de Bomba   Unidad de medida ml/min   de 1-999  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FLUBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presión Transmembrana  Unidad de Medida = mmHg  de 1-999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PTM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presión Venosa  Unidad de medida = mmHg  0-28', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PV';

