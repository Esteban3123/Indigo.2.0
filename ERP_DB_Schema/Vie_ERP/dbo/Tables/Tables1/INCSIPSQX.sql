CREATE TABLE [dbo].[INCSIPSQX] (
    [AUTO]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]  CHAR (20) NOT NULL,
    [CODSERIPS1] CHAR (20) NOT NULL,
    [CANSERIPS]  TINYINT   NOT NULL,
    [LIQISERIPS] CHAR (1)  NOT NULL,
    CONSTRAINT [PK_INCSIPSQX] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_INCSIPSQX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_INCSIPSQX_INCUPSIPS1] FOREIGN KEY ([CODSERIPS1]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[INCSIPSQX] NOCHECK CONSTRAINT [FK_INCSIPSQX_INCUPSIPS];


GO
ALTER TABLE [dbo].[INCSIPSQX] NOCHECK CONSTRAINT [FK_INCSIPSQX_INCUPSIPS1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion 1=Puntaje;2= Valor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPSQX', @level2type = N'COLUMN', @level2name = N'LIQISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPSQX', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPSQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPSQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPSQX', @level2type = N'COLUMN', @level2name = N'AUTO';

