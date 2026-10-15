CREATE TABLE [dbo].[ODONTOGTRAVAL] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRDIEVAL] INT         NOT NULL,
    [CONSECTRA]   INT         NOT NULL,
    [TIPO]        VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTOGTRAVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGTRAVAL_ODONTOGDIEVAL] FOREIGN KEY ([IDCTRDIEVAL]) REFERENCES [dbo].[ODONTOGDIEVAL] ([ID]),
    CONSTRAINT [FK_ODONTOGTRAVAL_ODOPARTRA] FOREIGN KEY ([CONSECTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);


GO
ALTER TABLE [dbo].[ODONTOGTRAVAL] NOCHECK CONSTRAINT [FK_ODONTOGTRAVAL_ODONTOGDIEVAL];


GO
ALTER TABLE [dbo].[ODONTOGTRAVAL] NOCHECK CONSTRAINT [FK_ODONTOGTRAVAL_ODOPARTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el tipo tratamiento valoración NoAplica = 0, CPOCariado = 1, CPOPerdido = 2, CPOObturado = 3, CEOCariado = 4, CEOExtraido = 5,
CEOObturado = 6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumerico de los tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Id control valores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'ID';

