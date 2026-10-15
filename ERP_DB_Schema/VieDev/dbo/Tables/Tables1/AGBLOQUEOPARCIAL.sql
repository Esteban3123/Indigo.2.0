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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  1 - bloqueo Activo  2 - bloqueo Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ESTADO';

