CREATE TABLE [dbo].[HCLISTRED] (
    [CODCONSEC]  INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC] INT        NULL,
    [CODCONSECD] NCHAR (10) NULL,
    [CODRESPES]  INT        NOT NULL,
    CONSTRAINT [PK_HCLISTRED] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCLISTRED_HCLISTRED] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[HCLISTRED] ([CODCONSEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de validación de lista de chequeo: 1=chequeada/completada, 0=no chequeada/pendiente. Tipo: INT. Uso: control de estado de verificación en procesos de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODRESPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- chequeada      0 - no chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODRESPES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODRESPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del detalle/ítem de lista de chequeo. Tipo: NCHAR(10). Referencia a componentes individuales de checklist clínico, quirúrgico o administrativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo detalle de lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de referencia a la cabecera de respuesta de lista de chequeo. Tipo: INT. FK que vincula detalles a su encabezado/agrupación principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo tabla Cabecera de Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de registro en tabla HCLISTRED. Tipo: INT PRIMARY KEY. Llave técnica para trazabilidad de listas de chequeo en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de listas de reducción o agrupación en historia clínica. Guarda las relaciones entre consecutivos de consolidación y sus respuestas o estados asociados dentro del módulo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED';
