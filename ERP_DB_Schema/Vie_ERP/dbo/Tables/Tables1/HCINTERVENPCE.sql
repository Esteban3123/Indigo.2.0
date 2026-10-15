CREATE TABLE [dbo].[HCINTERVENPCE] (
    [CODINTPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMINTPCE] VARCHAR (100) NOT NULL,
    [ESTADO]    INT           NULL,
    CONSTRAINT [PK_HCINTERVENPCE] PRIMARY KEY CLUSTERED ([CODINTPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1->Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'NOMINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERVENPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';

