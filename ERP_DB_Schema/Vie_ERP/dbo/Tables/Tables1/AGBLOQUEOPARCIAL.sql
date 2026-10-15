CREATE TABLE [dbo].[AGBLOQUEOPARCIAL] (
    [ID]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGENDA]         INT            NOT NULL,
    [FECHAINIBLOQUEO]  DATETIME       NOT NULL,
    [FECHAFINBLOQUEO]  DATETIME       NOT NULL,
    [ESTADO]           TINYINT        NOT NULL,
    [FECHAREGISTRO]    DATETIME       NOT NULL,
    [CODUSUARIO]       CHAR (20)      NOT NULL,
    [OBSERVACION]      VARCHAR (3000) NULL,
    [CODUSUARIODESBLO] CHAR (20)      NULL,
    [FECHADESBLO]      DATETIME       NULL,
    [BlockingReason]   INT            NULL,
    CONSTRAINT [PK_AGBLOQUEOPARCIAL] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contiene el codigo del motivo de bloque de la agenda, correspondiente a la columna CODMOTANU de la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'BlockingReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de desbloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHADESBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que desbloquea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIODESBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo del bloqueo parcial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creo el registro de bloqueo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  1 - bloqueo Activo  2 - bloqueo Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora final del bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAFINBLOQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora inicial del bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAINIBLOQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la agenda a bloquear parcialmente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'IDAGENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ID';

