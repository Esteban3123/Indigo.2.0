CREATE TABLE [dbo].[SOLORDCONS] (
    [ORDCONSEC] INT NOT NULL,
    CONSTRAINT [PK_SOLORDCONS] PRIMARY KEY CLUSTERED ([ORDCONSEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el consecutivo de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCONS', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';

