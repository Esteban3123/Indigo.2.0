CREATE TABLE [dbo].[HCTLIQELI] (
    [CODLIQELI] CHAR (4)   NOT NULL,
    [DESLIQELI] CHAR (100) NOT NULL,
    [DEFTIPOLI] CHAR (1)   NOT NULL,
    CONSTRAINT [PK_HCTLIQELI] PRIMARY KEY CLUSTERED ([CODLIQELI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el tipo de liquido eliminado  0. N/A  1. Residuo Gastrico  2. Diuresis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'DEFTIPOLI';

