CREATE TABLE [dbo].[HCPLANCENTROS] (
    [ID]             INT         IDENTITY (1, 1) NOT NULL,
    [IDHCPLANCUIENF] VARCHAR (3) NOT NULL,
    [CODCENATE]      CHAR (10)   NOT NULL,
    CONSTRAINT [PK_HCPLANCENTROS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANCENTROS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCPLANCUIENF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANCENTROS', @level2type = N'COLUMN', @level2name = N'IDHCPLANCUIENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANCENTROS', @level2type = N'COLUMN', @level2name = N'ID';

