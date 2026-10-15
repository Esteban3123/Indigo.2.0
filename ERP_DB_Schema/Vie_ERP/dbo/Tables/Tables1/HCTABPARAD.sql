CREATE TABLE [dbo].[HCTABPARAD] (
    [ID]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCTABPARAC] INT          NOT NULL,
    [PARACLINICO]  VARCHAR (15) NOT NULL,
    [ORDEN]        INT          NOT NULL,
    CONSTRAINT [PK_HCTABPARAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCTABPARAD_HCTABPARAC] FOREIGN KEY ([IDHCTABPARAC]) REFERENCES [dbo].[HCTABPARAC] ([ID])
);


GO
ALTER TABLE [dbo].[HCTABPARAD] NOCHECK CONSTRAINT [FK_HCTABPARAD_HCTABPARAC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posicion para el ordenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paraclinico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'PARACLINICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCTABPARAC (Tabla Paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';

