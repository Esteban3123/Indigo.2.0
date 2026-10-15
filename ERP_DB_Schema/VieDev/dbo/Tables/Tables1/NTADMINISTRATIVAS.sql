CREATE TABLE [dbo].[NTADMINISTRATIVAS] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (5)   NOT NULL,
    [NOMBRE]      VARCHAR (100) NOT NULL,
    [DESCRIPCION] VARCHAR (500) NULL,
    [ESTADO]      BIT           NOT NULL,
    [FECHACREA]   DATETIME      NOT NULL,
    [USUACREA]    CHAR (20)     NOT NULL,
    CONSTRAINT [PK_NTADMINISTRATIVAS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  Activo   Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTADMINISTRATIVAS', @level2type = N'COLUMN', @level2name = N'ESTADO';

