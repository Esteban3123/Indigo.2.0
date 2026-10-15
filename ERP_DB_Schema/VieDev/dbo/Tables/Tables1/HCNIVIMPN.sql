CREATE TABLE [dbo].[HCNIVIMPN] (
    [CODNIVIMP] CHAR (2)   NOT NULL,
    [DESNIVIMP] CHAR (100) NOT NULL,
    [COLNIVIMP] CHAR (50)  NOT NULL,
    [TIPO]      INT        CONSTRAINT [DF_HCNIVIMPN_TIPO] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCNIVIMPN] PRIMARY KEY CLUSTERED ([CODNIVIMP] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Enfermeria  3 - Instrumetador Quirurgico   4-Quimico Farmaceutico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'TIPO';

