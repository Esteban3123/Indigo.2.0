CREATE TABLE [dbo].[ODONTOGDIAVAL] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRDIEVAL] INT         NOT NULL,
    [CONSECDIA]   INT         NOT NULL,
    [TIPO]        VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTOGDIAVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGDIAVAL_ODONTOGDIEVAL] FOREIGN KEY ([IDCTRDIEVAL]) REFERENCES [dbo].[ODONTOGDIEVAL] ([ID]),
    CONSTRAINT [FK_ODONTOGDIAVAL_ODOPARDIA] FOREIGN KEY ([CONSECDIA]) REFERENCES [dbo].[ODOPARDIA] ([CONSECDIA])
);


GO
ALTER TABLE [dbo].[ODONTOGDIAVAL] NOCHECK CONSTRAINT [FK_ODONTOGDIAVAL_ODONTOGDIEVAL];


GO
ALTER TABLE [dbo].[ODONTOGDIAVAL] NOCHECK CONSTRAINT [FK_ODONTOGDIAVAL_ODOPARDIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de diagnóstico valoración NoAplica = 0
, Valoracion = 1, Tratamiento = 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ODOPARDIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'CONSECDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ODONTOGDIEVAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'ID';

