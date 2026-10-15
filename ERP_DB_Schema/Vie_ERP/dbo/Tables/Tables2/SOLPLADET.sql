CREATE TABLE [dbo].[SOLPLADET] (
    [PLADETAUT] INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PLANTAUTO] INT NOT NULL,
    [PRODAUTON] INT NOT NULL,
    [PLADETCAN] INT NOT NULL,
    CONSTRAINT [PK_SOLPLADET] PRIMARY KEY CLUSTERED ([PLADETAUT] ASC),
    CONSTRAINT [FK_SOLPLADET_SOLPLANTI] FOREIGN KEY ([PLANTAUTO]) REFERENCES [dbo].[SOLPLANTI] ([PLANTAUTO]),
    CONSTRAINT [FK_SOLPLADET_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);


GO
ALTER TABLE [dbo].[SOLPLADET] NOCHECK CONSTRAINT [FK_SOLPLADET_SOLPLANTI];


GO
ALTER TABLE [dbo].[SOLPLADET] NOCHECK CONSTRAINT [FK_SOLPLADET_SOLPRODUC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la cantidad de producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene le autonumerico de la cabecera de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETAUT';

