CREATE TABLE [dbo].[HCTIPPLAN] (
    [CODPLANIF] CHAR (2)      NOT NULL,
    [DESPLANIF] VARCHAR (100) NULL,
    [INDAUDFOR] NUMERIC (18)  NOT NULL,
    CONSTRAINT [PK_HCTIPPLAN] PRIMARY KEY CLUSTERED ([CODPLANIF] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tipo de Planificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'DESPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Planificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPPLAN', @level2type = N'COLUMN', @level2name = N'CODPLANIF';

