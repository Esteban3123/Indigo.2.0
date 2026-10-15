CREATE TABLE [dbo].[HCNUTRICIOND] (
    [ID]          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]  INT             NOT NULL,
    [IDPARAMETRO] INT             NOT NULL,
    [APORTE]      DECIMAL (18, 2) NOT NULL,
    [VOLUMEN]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_HCNUTRICIOND] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTRICIOND_HCNUTRICIONC] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[HCNUTRICIONC] ([ID]),
    CONSTRAINT [FK_HCNUTRICIOND_HCPARANUTRICION] FOREIGN KEY ([IDPARAMETRO]) REFERENCES [dbo].[HCPARANUTRICION] ([ID])
);


GO
ALTER TABLE [dbo].[HCNUTRICIOND] NOCHECK CONSTRAINT [FK_HCNUTRICIOND_HCNUTRICIONC];


GO
ALTER TABLE [dbo].[HCNUTRICIOND] NOCHECK CONSTRAINT [FK_HCNUTRICIOND_HCPARANUTRICION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'VOLUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Aporte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'APORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id  de parametros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDPARAMETRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera; campo que me tiene la relacion con la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'ID';

