CREATE TABLE [dbo].[UREPPERSON] (
    [ID]          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TITULO]      VARCHAR (200)   NULL,
    [DESCRIPCION] VARCHAR (2000)  NULL,
    [DEFINICION]  VARBINARY (MAX) NULL,
    [MODULO]      CHAR (2)        NULL,
    CONSTRAINT [PK_UREPPERSON] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UREPPERSON_SEGmodulu] FOREIGN KEY ([MODULO]) REFERENCES [dbo].[SEGmodulu] ([indmodulo]),
    CONSTRAINT [FK_UREPPERSON_UREPPERSON] FOREIGN KEY ([ID]) REFERENCES [dbo].[UREPPERSON] ([ID])
);


GO
ALTER TABLE [dbo].[UREPPERSON] NOCHECK CONSTRAINT [FK_UREPPERSON_SEGmodulu];


GO
ALTER TABLE [dbo].[UREPPERSON] NOCHECK CONSTRAINT [FK_UREPPERSON_UREPPERSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Módulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'MODULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Definición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DEFINICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Titulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'TITULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'ID';

