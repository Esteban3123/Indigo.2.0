CREATE TABLE [dbo].[OthersMaterialsProgQX] (
    [ID]                  INT        IDENTITY (1, 1) NOT NULL,
    [IDAGEPROGQX]         INT        NULL,
    [MATERIALDESCRIPTION] CHAR (300) NULL,
    [QUANTITY]            INT        NULL,
    CONSTRAINT [PK__OthersMa__3214EC27060FB890] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OthersMaterials_AGEPROGQX] FOREIGN KEY ([IDAGEPROGQX]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[OthersMaterialsProgQX] NOCHECK CONSTRAINT [FK_OthersMaterials_AGEPROGQX];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del material adicional programado para la cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'QUANTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del material adicional programado para la cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'MATERIALDESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'ID';

