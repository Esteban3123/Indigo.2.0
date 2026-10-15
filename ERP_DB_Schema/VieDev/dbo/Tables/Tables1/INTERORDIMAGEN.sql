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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Orden:  INT - Intrahospitalario  AMB - Ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'TIPOORDEN';

