CREATE TABLE [dbo].[LogMirth] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [NumeroOrdenIndigo] INT            NOT NULL,
    [IdOrden]           INT            NOT NULL,
    [FechaRegistro]     DATETIME       NOT NULL,
    [TipoIntegracion]   TINYINT        NOT NULL,
    [TipoCanal]         TINYINT        NOT NULL,
    [MensajeError]      VARCHAR (8000) NULL,
    CONSTRAINT [PK_LogMirth] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de validacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'MensajeError';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Envio orden a tercero  2 - Recepcion Resultado orden de tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoCanal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - integracion Laboratorios  2 - integracion Patologias  3 - integracion Hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoIntegracion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'FechaRegistro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Orden  (columna AUTO de Ordenes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'IdOrden';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id orden de fallo - NumeroOrdenIndigo (INTERDETA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'NumeroOrdenIndigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'Id';

