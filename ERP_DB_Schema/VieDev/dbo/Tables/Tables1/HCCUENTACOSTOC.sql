CREATE TABLE [dbo].[HCCUENTACOSTOC] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]              CHAR (4)      NOT NULL,
    [DESCRIPCION]         VARCHAR (100) NOT NULL,
    [IDEAPBVIE]           VARCHAR (MAX) NOT NULL,
    [OBSERVACION]         VARCHAR (500) NULL,
    [FECHACONFIRMACION]   DATETIME      NOT NULL,
    [USUARIOCONFIRMACION] CHAR (20)     NOT NULL,
    [NOMBRENTIDADES]      VARCHAR (MAX) NULL,
    [TIPOCONSULTA]        INT           NULL,
    [FECHAINICIAL]        DATETIME      NULL,
    [FECHAFINAL]          DATETIME      NULL,
    CONSTRAINT [PK_HCCUENTACOSTOC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHAINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Todos los registros  2. Incidencia  3. Atenciones realizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'TIPOCONSULTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'NOMBRENTIDADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de confirmación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'USUARIOCONFIRMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación tomada desde el servidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'FECHACONFIRMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUENTACOSTOC', @level2type = N'COLUMN', @level2name = N'ID';

