CREATE TABLE [dbo].[IHGRUFARM] (
    [CODGRUFAR] VARCHAR (20) NOT NULL,
    [DESGRUFAR] CHAR (100)   NOT NULL,
    CONSTRAINT [PK_IHGRUFARM] PRIMARY KEY CLUSTERED ([CODGRUFAR] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Grupo Farmacologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'DESGRUFAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo Farmacologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';

