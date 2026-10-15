CREATE TABLE [dbo].[HCPLAOTRPROIMG] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPLAOTRPROC] INT             NOT NULL,
    [IMAGEN]         VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_HCPLAOTRPROIMG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo donde almacena imagenes adjuntas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IMAGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera tabla HCPLAOTRPROC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'IDHCPLAOTRPROC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico detalle de imagenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROIMG', @level2type = N'COLUMN', @level2name = N'ID';

