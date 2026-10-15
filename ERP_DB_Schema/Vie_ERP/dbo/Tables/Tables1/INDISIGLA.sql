CREATE TABLE [dbo].[INDISIGLA] (
    [AUTO]      NUMERIC (18)  IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [KEYSIGLAS] VARCHAR (15)  NOT NULL,
    [DESSIGLAS] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_INDISIGLA_1] PRIMARY KEY CLUSTERED ([KEYSIGLAS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Sigla o Abreviatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'DESSIGLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sigla o Abreviatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'KEYSIGLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'AUTO';

