CREATE TABLE [dbo].[IHPARAMDCI] (
    [ID]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDCIMEDPADRE] VARCHAR (20) NOT NULL,
    [CODDCIMED]      VARCHAR (20) NOT NULL,
    [TIPO]           INT          NOT NULL,
    [FECHAREG]       DATETIME     NOT NULL,
    [USUARIOREG]     CHAR (20)    NOT NULL,
    CONSTRAINT [PK_IHPARAMDCI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IHPARAMDCI_IHDCIMEDI] FOREIGN KEY ([CODDCIMED]) REFERENCES [dbo].[IHDCIMEDI] ([CODDCIMED]),
    CONSTRAINT [FK_IHPARAMDCI_IHDCIMEDI1] FOREIGN KEY ([CODDCIMEDPADRE]) REFERENCES [dbo].[IHDCIMEDI] ([CODDCIMED]),
    CONSTRAINT [UK_IHPARAMDCI] UNIQUE NONCLUSTERED ([CODDCIMEDPADRE] ASC, [CODDCIMED] ASC)
);


GO
ALTER TABLE [dbo].[IHPARAMDCI] NOCHECK CONSTRAINT [FK_IHPARAMDCI_IHDCIMEDI];


GO
ALTER TABLE [dbo].[IHPARAMDCI] NOCHECK CONSTRAINT [FK_IHPARAMDCI_IHDCIMEDI1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'USUARIOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Suena Igual a  2- Se parece a  3- Suena igual y se parece a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Medicamento DCI - Detalle', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo DCI padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMEDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'ID';

