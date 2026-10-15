CREATE TABLE [dbo].[EGRESINRESULT] (
    [CODSERIPS] CHAR (20) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    CONSTRAINT [PK_EGRESINRESULT] PRIMARY KEY CLUSTERED ([CODSERIPS] ASC, [UFUCODIGO] ASC, [CODCENATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EGRESINRESULT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EGRESINRESULT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EGRESINRESULT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';

