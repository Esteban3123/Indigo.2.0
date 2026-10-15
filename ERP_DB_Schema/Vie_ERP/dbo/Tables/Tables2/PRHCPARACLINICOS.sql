CREATE TABLE [dbo].[PRHCPARACLINICOS] (
    [ID]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]   INT NOT NULL,
    [IDHCTABPARAC] INT NOT NULL,
    [ACTIVO]       BIT NOT NULL,
    CONSTRAINT [PK_PRHCPARACLINICOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCPARACLINICOS_HCTABPARAC] FOREIGN KEY ([IDHCTABPARAC]) REFERENCES [dbo].[HCTABPARAC] ([ID]),
    CONSTRAINT [FK_PRHCPARACLINICOS_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCPARACLINICOS] NOCHECK CONSTRAINT [FK_PRHCPARACLINICOS_HCTABPARAC];


GO
ALTER TABLE [dbo].[PRHCPARACLINICOS] NOCHECK CONSTRAINT [FK_PRHCPARACLINICOS_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado activo : 0.no 1.si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de cabecera de la tabla paraclinicos (HCTABPARAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de la historia clinica parametros (MODELOHC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ID';

