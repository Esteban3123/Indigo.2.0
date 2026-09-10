CREATE TABLE [dbo].[PRHCXOTVARIABLES] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [IDMODELOHC]    INT NOT NULL,
    [IDOTVARIABLES] INT NOT NULL,
    [OBLIGATORIO]   BIT NOT NULL,
    [ESTADO]        BIT NOT NULL,
    CONSTRAINT [PK_PRHCXOTVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación (1=Activo, 0=Inactivo). Bit que controla si la variable asociada al modelo de historia clínica está habilitada o deshabilitada en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado 1 = true  = Activo      0 = false  = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de obligatoriedad (1=Sí obligatorio, 0=No obligatorio). Bit que determina si el campo o variable debe ser completado obligatoriamente en el formulario de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si es obligatorio  1 = si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de otras variables (FK). Referencia al registro de variables adicionales o complementarias asociadas a este modelo de historia clínica parametrizable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDOTVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id otras variables', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDOTVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDOTVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica (FK). Clave foránea que vincula esta variable al modelo de atención/historia clínica parametrizable del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la relacion con el modelo de Historia clinica parametrizable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, INT IDENTITY). Consecutivo autonumérico que identifica unívocamente cada relación entre variables y modelos de historia clínica en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las variables u opciones de otras tecnologías (OT) con los modelos de historia clínica, indicando cuáles son obligatorias y si están activas. Permite configurar qué variables deben diligenciarse en cada modelo de HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTVARIABLES';
