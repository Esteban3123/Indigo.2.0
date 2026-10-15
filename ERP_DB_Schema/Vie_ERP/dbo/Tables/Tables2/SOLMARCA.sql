CREATE TABLE [dbo].[SOLMARCA] (
    [Autonumerico] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]       VARCHAR (4)   NOT NULL,
    [Descripcion]  VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SOLMARCA] PRIMARY KEY CLUSTERED ([Autonumerico] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Descripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Autonumerico';

