CREATE TABLE [dbo].[CHTIPBLOQ] (
    [CODTIPBLO] CHAR (2)     NOT NULL,
    [DESTIPBLO] CHAR (80)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTIPBLOQ] PRIMARY KEY CLUSTERED ([CODTIPBLO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador reservado para auditoría y fines forenses (NUMERIC 18). Campo de control interno que registra si el bloqueo fue auditado o requiere seguimiento formal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de bloqueo (CHAR 80). Especifica la causa o categoría del bloqueo: administrativo, legal, judicial, auditoría, glosa, contrato, RIPS u otra restricción aplicada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'DESTIPBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de BLoqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'DESTIPBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'DESTIPBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del tipo de bloqueo (CHAR 2). Clave primaria que clasifica la razón del bloqueo de historias clínicas, pacientes o procesos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'CODTIPBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'CODTIPBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'CODTIPBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de bloqueo utilizados en historia clínica. Define las categorías o motivos por los cuales un registro clínico, atención o proceso puede ser bloqueado o restringido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ';
