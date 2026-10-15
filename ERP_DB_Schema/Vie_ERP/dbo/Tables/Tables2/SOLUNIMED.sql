CREATE TABLE [dbo].[SOLUNIMED] (
    [UNIMEDAUT] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UNIMEDCOD] VARCHAR (20)  NOT NULL,
    [UNIMEDDES] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SOLUNIMED] PRIMARY KEY CLUSTERED ([UNIMEDAUT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la unidad de medida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLUNIMED', @level2type = N'COLUMN', @level2name = N'UNIMEDAUT';

