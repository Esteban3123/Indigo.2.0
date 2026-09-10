CREATE TABLE [dbo].[INDISIGLA] (
    [AUTO]      NUMERIC (18)  IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [KEYSIGLAS] VARCHAR (15)  NOT NULL,
    [DESSIGLAS] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_INDISIGLA_1] PRIMARY KEY CLUSTERED ([KEYSIGLAS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción completa o significado de la sigla o abreviatura; texto expandido que explica el término abreviado utilizado en el sistema de salud (ej: diagnósticos, procedimientos, unidades funcionales, especialidades médicas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'DESSIGLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Sigla o Abreviatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'DESSIGLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'DESSIGLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave o código de la sigla/abreviatura; identificador único VARCHAR(15) que almacena el acrónimo o abreviación estándar del dominio clínico-administrativo (diagnóstico, procedimiento, especialidad, unidad funcional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'KEYSIGLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sigla o Abreviatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'KEYSIGLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'KEYSIGLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico NUMERIC(18) de la tabla; número secuencial generado automáticamente como índice interno del registro, no replicado en entornos distribuidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de siglas o abreviaturas utilizadas en el sistema, donde cada registro asocia una clave corta con su descripción completa en lenguaje natural.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDISIGLA';
