CREATE TABLE [dbo].[PYPPARAHISTCAMP] (
    [ID]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPYPPARAHISTO]    INT NOT NULL,
    [IDPYPPARAHISTCAMP] INT NOT NULL,
    [VISIBLE]           BIT NOT NULL,
    [OBLIGA]            BIT NOT NULL,
    CONSTRAINT [PK_PYPPARAHISTOD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que define si el campo de la historia parametrizada es obligatorio o requerido al momento del registro de información clínica; controla validación en ingreso de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'OBLIGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'si el campo es obligatorio o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'OBLIGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'OBLIGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si el campo de la historia parametrizada es visible o se muestra en la interfaz de usuario durante la consulta o ingreso de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el campo es visible o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'VISIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) que referencia el registro específico del campo en la historia parametrizada; clave secundaria para relación con campos de plantilla clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTCAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de la Historia parametrizada campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTCAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTCAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la historia parametrizada padre; FK que vincula este registro al formato o plantilla de historia clínica configurada en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la Historia parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY) autonumérica e incremental que identifica únicamente cada configuración de visibilidad y obligatoriedad de campo en historia parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de campos asociados a los historiales de parámetros de nómina o liquidación de pagos, indicando cuáles campos están visibles y cuáles son obligatorios en cada configuración histórica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP';
