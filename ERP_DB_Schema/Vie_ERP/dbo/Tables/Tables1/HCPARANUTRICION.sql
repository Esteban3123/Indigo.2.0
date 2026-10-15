CREATE TABLE [dbo].[HCPARANUTRICION] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]    VARCHAR (100) NULL,
    [NUTRIENTE] VARCHAR (MAX) NOT NULL,
    [RANGO]     VARCHAR (MAX) NOT NULL,
    [ESTADO]    INT           NOT NULL,
    CONSTRAINT [PK_HCPARANUTRICION] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANUTRICION', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANUTRICION', @level2type = N'COLUMN', @level2name = N'RANGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nutriente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANUTRICION', @level2type = N'COLUMN', @level2name = N'NUTRIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANUTRICION', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANUTRICION', @level2type = N'COLUMN', @level2name = N'ID';

