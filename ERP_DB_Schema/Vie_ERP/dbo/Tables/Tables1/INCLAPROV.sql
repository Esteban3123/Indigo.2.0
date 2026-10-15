CREATE TABLE [dbo].[INCLAPROV] (
    [CODCLAPROV] CHAR (2)  NOT NULL,
    [CODPROVEE]  CHAR (15) NOT NULL,
    CONSTRAINT [PK_INCLAP] PRIMARY KEY CLUSTERED ([CODCLAPROV] ASC, [CODPROVEE] ASC),
    CONSTRAINT [FK_INCLAPROV_INCLASESP] FOREIGN KEY ([CODCLAPROV]) REFERENCES [dbo].[INCLASESP] ([CODCLAPROV]),
    CONSTRAINT [FK_INCLAPROV_INPROVEED] FOREIGN KEY ([CODPROVEE]) REFERENCES [dbo].[INPROVEED] ([CODPROVEE])
);


GO
ALTER TABLE [dbo].[INCLAPROV] NOCHECK CONSTRAINT [FK_INCLAPROV_INCLASESP];


GO
ALTER TABLE [dbo].[INCLAPROV] NOCHECK CONSTRAINT [FK_INCLAPROV_INPROVEED];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Clase de Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODCLAPROV';

