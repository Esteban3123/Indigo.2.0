CREATE TABLE [dbo].[HCRADMAESTIP] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]          VARCHAR (3)   NOT NULL,
    [NOMBRE]          VARCHAR (100) NOT NULL,
    [ESTADO]          INT           NOT NULL,
    [TIPO]            INT           NOT NULL,
    [USUARIOCREACION] CHAR (20)     NOT NULL,
    [FECHACREACION]   DATETIME      NOT NULL,
    CONSTRAINT [PK_HCRADMAESTIP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Factores asociados al tratamiento   2 - Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADMAESTIP', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Activo  2 - Inactivo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADMAESTIP', @level2type = N'COLUMN', @level2name = N'ESTADO';

