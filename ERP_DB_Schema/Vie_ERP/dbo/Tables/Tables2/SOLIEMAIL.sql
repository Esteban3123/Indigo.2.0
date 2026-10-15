CREATE TABLE [dbo].[SOLIEMAIL] (
    [Autonumerico]   TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Email]          VARCHAR (80) NOT NULL,
    [Codigo]         NUMERIC (18) NOT NULL,
    [EmailPrincipal] BIT          NOT NULL,
    CONSTRAINT [PK_SOLEMAIL] PRIMARY KEY CLUSTERED ([Autonumerico] ASC),
    CONSTRAINT [FK_SOLEMAIL_SOLPROVE] FOREIGN KEY ([Codigo]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);


GO
ALTER TABLE [dbo].[SOLIEMAIL] NOCHECK CONSTRAINT [FK_SOLEMAIL_SOLPROVE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene si el email es proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'EmailPrincipal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLIEMAIL', @level2type = N'COLUMN', @level2name = N'Autonumerico';

