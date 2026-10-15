CREATE TABLE [dbo].[FORMSMODULE] (
    [IDFORM]   VARCHAR (4) NULL,
    [IDMODULE] VARCHAR (3) NULL,
    [IDTITLE]  VARCHAR (3) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ID del titulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDTITLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ID del modulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDMODULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDFORM';

