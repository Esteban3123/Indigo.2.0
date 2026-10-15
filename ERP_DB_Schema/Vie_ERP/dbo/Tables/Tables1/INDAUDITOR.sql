CREATE TABLE [dbo].[INDAUDITOR] (
    [CODAUDITO]   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODUSUARI]   CHAR (20)     NULL,
    [FECREGISTRO] DATETIME      NULL,
    [NOMEQUIPO]   CHAR (20)     NULL,
    [MENSAJEAUD]  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_INDAUDITOR] PRIMARY KEY CLUSTERED ([CODAUDITO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INDAUDITOR_FECREGISTRO]
    ON [dbo].[INDAUDITOR]([FECREGISTRO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del mensaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'MENSAJEAUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del equipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'NOMEQUIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo auditor de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODAUDITO';

