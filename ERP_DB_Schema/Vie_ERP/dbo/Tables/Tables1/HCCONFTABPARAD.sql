CREATE TABLE [dbo].[HCCONFTABPARAD] (
    [ID]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCONFTABPARAC] INT NOT NULL,
    [IDHCTABPARAC]     INT NOT NULL,
    CONSTRAINT [PK_HCCONFTABPARAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCONFTABPARAD_HCCONFTABPARAC] FOREIGN KEY ([IDHCCONFTABPARAC]) REFERENCES [dbo].[HCCONFTABPARAC] ([ID]),
    CONSTRAINT [FK_HCCONFTABPARAD_HCTABPARAC] FOREIGN KEY ([IDHCTABPARAC]) REFERENCES [dbo].[HCTABPARAC] ([ID])
);


GO
ALTER TABLE [dbo].[HCCONFTABPARAD] NOCHECK CONSTRAINT [FK_HCCONFTABPARAD_HCCONFTABPARAC];


GO
ALTER TABLE [dbo].[HCCONFTABPARAD] NOCHECK CONSTRAINT [FK_HCCONFTABPARAD_HCTABPARAC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla  HCTABPARAC = Tabla Paraclinicos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla cabecera propia HCCONFTABPARAC = Configuracion Tabla Paraclinicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';

