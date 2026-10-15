CREATE TABLE [dbo].[HCCENLABUD] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCENLABU] INT NOT NULL,
    [TIPSERIPS]   INT NOT NULL,
    CONSTRAINT [PK_HCCENLABUD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCENLABUD_HCCENLABU] FOREIGN KEY ([IDHCCENLABU]) REFERENCES [dbo].[HCCENLABU] ([ID])
);


GO
ALTER TABLE [dbo].[HCCENLABUD] NOCHECK CONSTRAINT [FK_HCCENLABUD_HCCENLABU];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Servicio 1: Laboratorios 2: Patologias 3: Imagenes Diagnosticas 4:   Procedimeintos no Qx 5: Procedimientos Qx 6: Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla HCCENLABU ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'IDHCCENLABU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABUD', @level2type = N'COLUMN', @level2name = N'ID';

