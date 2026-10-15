CREATE TABLE [dbo].[INTERORDIMAGEN] (
    [ID]             INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDORDEN_IMAGEN] INT         NOT NULL,
    [CUPS]           CHAR (20)   NOT NULL,
    [TIPOORDEN]      VARCHAR (3) NOT NULL,
    [FECHACREACION]  DATETIME    NOT NULL,
    [CODUSUARIO]     CHAR (20)   NOT NULL,
    CONSTRAINT [PK_INTERORDIMAGEN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Orden:  INT - Intrahospitalario  AMB - Ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'TIPOORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo CUPS enviado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de orden de imagenes puede ser de la tabla (HCORDIMAG - AMBORDIMA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'IDORDEN_IMAGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'ID';

