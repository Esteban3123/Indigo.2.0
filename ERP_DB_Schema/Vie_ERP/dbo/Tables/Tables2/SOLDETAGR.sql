CREATE TABLE [dbo].[SOLDETAGR] (
    [AUTO]      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTON]  INT NOT NULL,
    [AGRUPAUTO] INT NOT NULL,
    CONSTRAINT [PK_SOLDETAGR] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la Agrupacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AGRUPAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene el Autonumerico de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'COMAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AUTO';

