CREATE TABLE [dbo].[PYPCAMPOHC] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (4)   NOT NULL,
    [CAMPO]       VARCHAR (100) NOT NULL,
    [OBSERVACION] VARCHAR (500) NULL,
    [FECHACREA]   DATETIME      CONSTRAINT [DF_PYPCAMPOHC_FECHACREA] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_PYPCAMPOHC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPCAMPOHC', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPCAMPOHC', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPCAMPOHC', @level2type = N'COLUMN', @level2name = N'CAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPCAMPOHC', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPCAMPOHC', @level2type = N'COLUMN', @level2name = N'ID';

