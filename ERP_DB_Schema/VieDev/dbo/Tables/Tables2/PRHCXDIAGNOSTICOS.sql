CREATE TABLE [dbo].[PRHCXDIAGNOSTICOS] (
    [ID]            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]    INT            NOT NULL,
    [CODDIAGNO]     CHAR (4)       NOT NULL,
    [PRINCIPAL]     BIT            NOT NULL,
    [TIPO]          VARCHAR (5)    NOT NULL,
    [CLASE]         VARCHAR (5)    NOT NULL,
    [ESTADIO1]      VARCHAR (5)    NULL,
    [ESTADIO2]      VARCHAR (5)    NULL,
    [OBSERVACIONES] VARCHAR (1000) NULL,
    CONSTRAINT [PK_PRHCXDIAGNOSTICOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXDIAGNOSTICOS_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[PRHCXDIAGNOSTICOS] NOCHECK CONSTRAINT [FK_PRHCXDIAGNOSTICOS_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda las observaciones del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el estadio de diagnóstico (0 -> 0,  A -> A, B -> B, C -> C)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el estadio de diagnóstico (0 -> 0, 1 -> I, 2 -> II, 3 -> III, 4 -> IV)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ESTADIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la clase de diagnósticos (PR -> Pre-Operatorio, PO -> Pos-Operatorio, PP -> Pre y Pos-Operatorio, HI -> Histopatológico, NA -> No Aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CLASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo del diagnóstico (I -> Impresión Diagnóstica, C -> Confirmado Nuevo, R -> Confirmado Repetido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el diagnostico principal (Checked or unchecked)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla INDIAGNOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el ID de PRMODELOHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';

