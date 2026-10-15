CREATE TABLE [dbo].[HCCTRVINS] (
    [CONSECUTI] NUMERIC (18) NOT NULL,
    [CODPRODUC] CHAR (20)    NOT NULL,
    [CANUTIPRO] INT          NOT NULL,
    [OBSERVACI] CHAR (200)   NOT NULL,
    CONSTRAINT [PK_HCCTRVINS] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCCTRVINS_HCCTRVENP] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCCTRVENP] ([CONSECUTI])
);


GO
ALTER TABLE [dbo].[HCCTRVINS] NOCHECK CONSTRAINT [FK_HCCTRVINS_HCCTRVENP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVINS', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Consumida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVINS', @level2type = N'COLUMN', @level2name = N'CANUTIPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto Gastado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVINS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Venopuncion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVINS', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

