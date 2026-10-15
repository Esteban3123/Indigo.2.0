CREATE TABLE [dbo].[SOLDIRECC] (
    [DIREAUTO]       TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Direccion]      VARCHAR (80) NOT NULL,
    [PROVAUTO]       NUMERIC (18) NOT NULL,
    [DirecPrincipal] BIT          NOT NULL,
    CONSTRAINT [PK_SOLDIREC] PRIMARY KEY CLUSTERED ([DIREAUTO] ASC),
    CONSTRAINT [FK_SOLDIREC_SOLPROVE] FOREIGN KEY ([PROVAUTO]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);


GO
ALTER TABLE [dbo].[SOLDIRECC] NOCHECK CONSTRAINT [FK_SOLDIREC_SOLPROVE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene si la direccion es direccion principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DirecPrincipal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene le autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'PROVAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'Direccion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDIRECC', @level2type = N'COLUMN', @level2name = N'DIREAUTO';

