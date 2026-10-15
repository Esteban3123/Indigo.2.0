CREATE TABLE [dbo].[HCPRESCRAEXT] (
    [ID]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPRESCRA]                INT NOT NULL,
    [NUMEROAPLICACIONINICIAL]    INT NOT NULL,
    [NUMEROAPLICACIONREPOSICION] INT NOT NULL,
    [CANTIDADREPOSICION]         INT NOT NULL,
    CONSTRAINT [PK_HCPRESCRAEXT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPRESCRAEXT_HCPRESCRA] FOREIGN KEY ([IDHCPRESCRA]) REFERENCES [dbo].[HCPRESCRA] ([ID])
);


GO
ALTER TABLE [dbo].[HCPRESCRAEXT] NOCHECK CONSTRAINT [FK_HCPRESCRAEXT_HCPRESCRA];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCPRESCRAEXT]
    ON [dbo].[HCPRESCRAEXT]([IDHCPRESCRA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda cantidad  reposición medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de aplicaciónes  en reposición de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de aplicacion inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Id de presquisión de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'IDHCPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRAEXT', @level2type = N'COLUMN', @level2name = N'ID';

