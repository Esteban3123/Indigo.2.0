CREATE TABLE [dbo].[HCESCMEDPCE] (
    [CODESCMEDPCE] INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMESCMEDPCE] VARCHAR (80) NOT NULL,
    [ESTADO]       INT          NULL,
    CONSTRAINT [PK_HCESCAMEDPCE] PRIMARY KEY CLUSTERED ([CODESCMEDPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1->Activo 2->inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la escala de medición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'NOMESCMEDPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del registo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCMEDPCE', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';

