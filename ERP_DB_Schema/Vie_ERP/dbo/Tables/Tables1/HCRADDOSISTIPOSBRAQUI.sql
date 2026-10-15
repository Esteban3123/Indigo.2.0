CREATE TABLE [dbo].[HCRADDOSISTIPOSBRAQUI] (
    [ID]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADDOSISBRAQUI] INT NOT NULL,
    [IDHCRADMAESTIP]     INT NOT NULL,
    [TIPO]               INT NOT NULL,
    CONSTRAINT [PK_HCRADDOSISTIPOSBRAQUI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADDOSISTIPOSBRAQUI_HCRADDOSISBRAQUI] FOREIGN KEY ([IDHCRADDOSISBRAQUI]) REFERENCES [dbo].[HCRADDOSISBRAQUI] ([ID]),
    CONSTRAINT [FK_HCRADDOSISTIPOSBRAQUI_HCRADMAESTIP] FOREIGN KEY ([IDHCRADMAESTIP]) REFERENCES [dbo].[HCRADMAESTIP] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADDOSISTIPOSBRAQUI] NOCHECK CONSTRAINT [FK_HCRADDOSISTIPOSBRAQUI_HCRADDOSISBRAQUI];


GO
ALTER TABLE [dbo].[HCRADDOSISTIPOSBRAQUI] NOCHECK CONSTRAINT [FK_HCRADDOSISTIPOSBRAQUI_HCRADMAESTIP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -  Factores Asociados al Tratamiento   2 - Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla maestro de: HCRADMAESTIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla HCRADDOSISBRAQUI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSISBRAQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';

