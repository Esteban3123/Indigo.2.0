CREATE TABLE [dbo].[HCHOJMEZD] (
    [CONSECUTI] NUMERIC (18)    NOT NULL,
    [CODPRODUC] CHAR (20)       NOT NULL,
    [CANTIUTIL] INT             NOT NULL,
    [DOSISPROD] NUMERIC (18, 2) NULL,
    [ID]        INT             IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_HCHOJMEZD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOJMEZD_HCHOJMEZC] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCHOJMEZC] ([CONSECUTI]),
    CONSTRAINT [FK_HCHOJMEZD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCHOJMEZD] NOCHECK CONSTRAINT [FK_HCHOJMEZD_HCHOJMEZC];


GO
ALTER TABLE [dbo].[HCHOJMEZD] NOCHECK CONSTRAINT [FK_HCHOJMEZD_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concetración ó Dosis del producto ordenada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Utilizada/ordenada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CANTIUTIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

