CREATE TABLE [dbo].[HCANESUBC] (
    [CODCATEGO]     CHAR (2)     NOT NULL,
    [CODSUBCAT]     CHAR (2)     NOT NULL,
    [NOMSUBCAT]     VARCHAR (60) NOT NULL,
    [EDADMIN]       INT          CONSTRAINT [DF_EDADMIN] DEFAULT ((0)) NOT NULL,
    [EDADMAX]       INT          CONSTRAINT [DF_EDADMAX] DEFAULT ((120)) NOT NULL,
    [GENERO]        TINYINT      CONSTRAINT [DF_GENERO] DEFAULT ((3)) NOT NULL,
    [TIPO]          TINYINT      CONSTRAINT [DF_TIPO] DEFAULT ((1)) NOT NULL,
    [MANEJADECIMAL] BIT          NULL,
    CONSTRAINT [PK_HCANESUBC] PRIMARY KEY CLUSTERED ([CODCATEGO] ASC, [CODSUBCAT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si maneja decimal:   0 - No   1 - Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de variable:   1 - String   2 - Númerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Género:   1  - Masculino   2 - Femenino   3 - Ambos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'GENERO';

