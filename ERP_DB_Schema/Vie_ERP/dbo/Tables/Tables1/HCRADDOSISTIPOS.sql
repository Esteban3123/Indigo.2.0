CREATE TABLE [dbo].[HCRADDOSISTIPOS] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADDOSIS]   INT NOT NULL,
    [IDHCRADMAESTIP] INT NOT NULL,
    [TIPO]           INT NOT NULL,
    CONSTRAINT [PK_HCRADDOSISTIPOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADDOSISTIPOS_HCRADDOSIS] FOREIGN KEY ([IDHCRADDOSIS]) REFERENCES [dbo].[HCRADDOSIS] ([ID]),
    CONSTRAINT [FK_HCRADDOSISTIPOS_HCRADMAESTIP] FOREIGN KEY ([IDHCRADMAESTIP]) REFERENCES [dbo].[HCRADMAESTIP] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADDOSISTIPOS] NOCHECK CONSTRAINT [FK_HCRADDOSISTIPOS_HCRADDOSIS];


GO
ALTER TABLE [dbo].[HCRADDOSISTIPOS] NOCHECK CONSTRAINT [FK_HCRADDOSISTIPOS_HCRADMAESTIP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -  Factores Asociados al Tratamiento   2 - Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla maestro de: HCRADMAESTIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla HCRADDOSIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'ID';

