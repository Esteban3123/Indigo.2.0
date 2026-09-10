CREATE TABLE [dbo].[FORMSMODULE] (
    [IDFORM]   VARCHAR (4) NULL,
    [IDMODULE] VARCHAR (3) NULL,
    [IDTITLE]  VARCHAR (3) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del título o encabezado del formulario (VARCHAR 3). Referencia a la sección o nombre que agrupa campos del formulario en la interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDTITLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el ID del titulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDTITLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDTITLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del módulo funcional asociado (VARCHAR 3). Referencia al área del sistema: consulta, urgencia, laboratorio, facturación, RIPS, recetas, diagnósticos u otro módulo operativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDMODULE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el ID del modulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDMODULE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDMODULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo del formulario (VARCHAR 4). Clave que vincula la estructura de formularios con módulos y títulos para composición dinámica de interfaces.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE', @level2type = N'COLUMN', @level2name = N'IDFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los formularios del sistema con los módulos y títulos (secciones) a los que pertenecen, permitiendo organizar y controlar qué formularios están disponibles en cada módulo de la aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'FORMSMODULE';
