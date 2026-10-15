CREATE TABLE [dbo].[HCINFCONCEXT] (
    [ID]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCINFCONC]                INT NOT NULL,
    [NUMEROAPLICACIONINICIAL]    INT NOT NULL,
    [NUMEROAPLICACIONREPOSICION] INT NOT NULL,
    [CANTIDADREPOSICION]         INT NOT NULL,
    CONSTRAINT [PK_HCINFLIQAEXT] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCINFCONCEXT]
    ON [dbo].[HCINFCONCEXT]([IDHCINFCONC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda cantidad de reposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número aplicación reposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número aplicación inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCINFCONC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'IDHCINFCONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONCEXT', @level2type = N'COLUMN', @level2name = N'ID';

