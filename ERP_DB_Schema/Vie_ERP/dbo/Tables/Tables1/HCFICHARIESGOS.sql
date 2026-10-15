CREATE TABLE [dbo].[HCFICHARIESGOS] (
    [ID]                  INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT     NOT NULL,
    [CODRIESGOCOMPLI]     TINYINT NOT NULL,
    CONSTRAINT [PK_HCFICHARIESGOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHARIESGOS_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHARIESGOS] NOCHECK CONSTRAINT [FK_HCFICHARIESGOS_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo riesgo de Complicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHARIESGOS', @level2type = N'COLUMN', @level2name = N'CODRIESGOCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHARIESGOS', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHARIESGOS', @level2type = N'COLUMN', @level2name = N'ID';

