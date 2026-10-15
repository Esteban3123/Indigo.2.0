CREATE TABLE [dbo].[HCVIAADMI] (
    [CODVIAADM] VARCHAR (20) NOT NULL,
    [DESVIAADM] CHAR (30)    NOT NULL,
    [CODHOMHV]  VARCHAR (20) NULL,
    CONSTRAINT [PK_HCVIAADMI] PRIMARY KEY CLUSTERED ([CODVIAADM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo homologo de la via de administración que pasa a smarth health', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Via de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'DESVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Via de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';

