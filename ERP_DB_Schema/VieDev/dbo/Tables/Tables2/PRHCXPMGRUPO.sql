CREATE TABLE [dbo].[PRHCXPMGRUPO] (
    [ID]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]    INT NOT NULL,
    [CODPLANMANEJO] INT NOT NULL,
    CONSTRAINT [PK_PRHCXPMGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXPMGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXPMGRUPO] NOCHECK CONSTRAINT [FK_PRHCXPMGRUPO_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '    Dieta = 1          Medicamentos = 2          InsumosDispositivos = 3          Laboratorios = 4          Patologías = 5          ImágenesDx = 6          ProcedimientosQx = 7          ProcedimientosNoQx = 8          Interconsultas = 9          Recomendaciones = 10          Hemocomponentes = 11          PAD = 12          Formulación de lentes = 13', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'CODPLANMANEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'ID';

