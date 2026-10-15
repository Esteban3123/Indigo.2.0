CREATE TABLE [dbo].[RIAS] (
    [ID]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPRO]            VARCHAR (4)   NOT NULL,
    [NOMBRE]            VARCHAR (100) NOT NULL,
    [EDADMINIMA]        INT           NOT NULL,
    [EDADMAXIMA]        INT           NOT NULL,
    [TIPOEDAD]          INT           NOT NULL,
    [SEXO]              INT           NOT NULL,
    [ESTADO]            INT           NOT NULL,
    [CODUSUCRE]         CHAR (20)     NOT NULL,
    [FECHACREA]         DATETIME      NOT NULL,
    [CODUSUMOD]         CHAR (20)     NULL,
    [FECHAMOD]          DATETIME      NULL,
    [FORMUCARATE]       VARCHAR (MAX) NULL,
    [MENPACNOINS]       VARCHAR (MAX) NULL,
    [MENPACINASI]       VARCHAR (MAX) NULL,
    [APLICARESTRICCION] INT           NULL,
    CONSTRAINT [PK_RIAS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo:  1 - masculino  2 - femenino  3 - ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo edad  0 dias  1 mes  2 años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIAS', @level2type = N'COLUMN', @level2name = N'TIPOEDAD';

