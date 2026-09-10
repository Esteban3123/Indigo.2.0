CREATE TABLE [dbo].[HCESQUEREN] (
    [ID]        INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESQUEM] NCHAR (10)  NOT NULL,
    [DESESQUEM] NCHAR (200) NOT NULL,
    [ESTADO]    TINYINT     NOT NULL,
    CONSTRAINT [PK_HCESQUEREN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del esquema renal; indica si el registro está activo, inactivo o eliminado (TINYINT: 0=Inactivo, 1=Activo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del esquema renal; nombre o detalle del protocolo, clasificación o tipo de esquema renal utilizado en atención clínica (NCHAR 200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'DESESQUEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Esquema Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'DESESQUEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'DESESQUEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del esquema renal; identificador alfanumérico único del esquema renal, protocolo de tratamiento o clasificación de enfermedad renal (NCHAR 10, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'CODESQUEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Esquema Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'CODESQUEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'CODESQUEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del esquema renal; clave primaria autonumérica que referencia internamente el registro en el sistema de historias clínicas (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del esquema renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de esquemas de prescripción o tratamiento (esquemas terapéuticos), usado para clasificar y agrupar protocolos de manejo clínico o medicamentoso. Permite definir y activar/desactivar los esquemas disponibles en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN';
