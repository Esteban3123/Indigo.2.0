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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Feha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Conductividad  Unidad de medida µs/cm  de 1-40  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'CONDUCTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Flujo de Bomba   Unidad de medida ml/min   de 1-999  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FLUBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presión Transmembrana  Unidad de Medida = mmHg  de 1-999', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PTM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presión Venosa  Unidad de medida = mmHg  0-28', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturación de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'SO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'PA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'FC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tensión Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Registro: 1-> Al iniciar, 2-> Monitoreo, 3-> Al Finalizar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de bolsa de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'HCSESHEMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSESHEMSV', @level2type = N'COLUMN', @level2name = N'ID';

