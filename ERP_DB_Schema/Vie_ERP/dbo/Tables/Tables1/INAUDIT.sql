CREATE TABLE [dbo].[INAUDIT] (
    [CODAUDIT]     INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIO]      NCHAR (100) NULL,
    [FECHA]        DATETIME    NULL,
    [NOMBREEQUIPO] NCHAR (100) NULL,
    [DIRECCIONIP]  NCHAR (100) NULL,
    [ACCION]       NCHAR (100) NULL,
    CONSTRAINT [PK_INAUDIT] PRIMARY KEY CLUSTERED ([CODAUDIT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'ACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'DIRECCIONIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de equipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'NOMBREEQUIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'USUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'CODAUDIT';

