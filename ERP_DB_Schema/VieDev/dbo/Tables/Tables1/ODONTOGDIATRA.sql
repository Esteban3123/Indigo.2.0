CREATE TABLE [dbo].[ODONTOGDIATRA] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRDIETRA] INT         NOT NULL,
    [CONSECDIA]   INT         NOT NULL,
    [TIPO]        VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTOGDIATRA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGDIATRA_ODONTOGDIETRA1] FOREIGN KEY ([IDCTRDIETRA]) REFERENCES [dbo].[ODONTOGDIETRA] ([ID]),
    CONSTRAINT [FK_ODONTOGDIATRA_ODOPARDIA] FOREIGN KEY ([CONSECDIA]) REFERENCES [dbo].[ODOPARDIA] ([CONSECDIA])
);


GO
ALTER TABLE [dbo].[ODONTOGDIATRA] NOCHECK CONSTRAINT [FK_ODONTOGDIATRA_ODONTOGDIETRA1];


GO
ALTER TABLE [dbo].[ODONTOGDIATRA] NOCHECK CONSTRAINT [FK_ODONTOGDIATRA_ODOPARDIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo diagnóstico de tratamiento NoAplica = 0, NivelDiente = 1, VestibularArriba = 2, Oclusal = 3, Palatino = 4, DistalIzquierda = 5, DistalDerecha = 6, MesialDerecha = 7, MesialIzquierda = 8, VestibularAbajo = 9, Lingual = 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con ODOPARDIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ODONTOGDIETRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIATRA', @level2type = N'COLUMN', @level2name = N'ID';

