CREATE TABLE [dbo].[homifs_imp] (
    [ORIGEN_INDIGO036]           VARCHAR (50) NULL,
    [FILESHARE_PRINCIPAL]        VARCHAR (50) NULL,
    [FILESHARE_BK16FEB]          VARCHAR (50) NULL,
    [TOTAL_RECUPERADOS]          VARCHAR (50) NULL,
    [PACIENTES_ADJUNTOS_13FA12M] VARCHAR (50) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista total de carpetas ya recuperadas (comparación entre BK09FEB y BK13FEB)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'homifs_imp', @level2type = N'COLUMN', @level2name = N'TOTAL_RECUPERADOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista total de carpetas del backup del 16 de febrero <16_02_restore>', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'homifs_imp', @level2type = N'COLUMN', @level2name = N'FILESHARE_BK16FEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista total de carpetas del fileshare principal <homifs>', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'homifs_imp', @level2type = N'COLUMN', @level2name = N'FILESHARE_PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna con los datos total pacientes con documentos adjuntos en INDIGO036', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'homifs_imp', @level2type = N'COLUMN', @level2name = N'ORIGEN_INDIGO036';

