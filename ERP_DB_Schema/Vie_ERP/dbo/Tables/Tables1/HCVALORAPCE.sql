CREATE TABLE [dbo].[HCVALORAPCE] (
    [CODVALORAPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMVALORAPCE] VARCHAR (100) NOT NULL,
    [ESTADO]       INT           NULL,
    CONSTRAINT [PK_HCVALORAPCE] PRIMARY KEY CLUSTERED ([CODVALORAPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1->Activo,2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la valoracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'NOMVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la valoracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALORAPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';

