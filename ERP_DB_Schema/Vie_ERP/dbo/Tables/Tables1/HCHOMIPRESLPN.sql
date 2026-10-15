CREATE TABLE [dbo].[HCHOMIPRESLPN] (
    [ID]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]       VARCHAR (7)    NOT NULL,
    [NOMBRECOMERCIAL] VARCHAR (60)   NOT NULL,
    [GRUPONIVEL]      VARCHAR (20)   NOT NULL,
    [FORMA]           VARCHAR (20)   NOT NULL,
    [PRESENTACION]    NUMERIC (7, 2) NOT NULL,
    [UNIDAD]          VARCHAR (3)    NOT NULL,
    [ESTADO]          BIT            NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESLPN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentación comercial de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'PRESENTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'FORMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo nivel1 de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'GRUPONIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre comercial de lista de producto nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de lista de producto nutrición mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador  de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESLPN', @level2type = N'COLUMN', @level2name = N'ID';

