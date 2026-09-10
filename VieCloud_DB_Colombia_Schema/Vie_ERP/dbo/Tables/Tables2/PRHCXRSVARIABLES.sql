CREATE TABLE [dbo].[PRHCXRSVARIABLES] (
    [ID]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]    INT NOT NULL,
    [IDRSVARIABLES] INT NOT NULL,
    [OBLIGATORIO]   BIT NOT NULL,
    [ESTADO]        BIT NOT NULL,
    CONSTRAINT [PK_PRHCXRSVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXRSVARIABLES_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID]),
    CONSTRAINT [FK_PRHCXRSVARIABLES_RSVARIABLES] FOREIGN KEY ([IDRSVARIABLES]) REFERENCES [dbo].[RSVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXRSVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXRSVARIABLES_PRMODELOHC];




GO
ALTER TABLE [dbo].[PRHCXRSVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXRSVARIABLES_PRMODELOHC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la variable en el modelo de historia clínica: 1 = Activo/Habilitado, 0 = Inactivo/Deshabilitado. Controla si la variable de revisión de sistemas está disponible para captura en la HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado   1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la variable es obligatoria en la revisión de sistemas: 1 = Requerida/Obligatoria, 0 = Opcional. Define si debe ser completada antes de cerrar la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda obligatorios  1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la variable de revisión de sistemas. Referencia la tabla RSVARIABLES que contiene las variables clínicas de exploración/revisión de sistemas (síntomas, signos, antecedentes).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id variables revision de sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del modelo de historia clínica. Referencia la tabla PRMODELOHC que define la estructura y plantilla de la HC a la que pertenece esta variable de revisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) y consecutivo de la asignación de variables al modelo de HC. Clave primaria que relaciona variables de revisión de sistemas con modelos de historia clínica específicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las variables de resumen de historia clínica con los modelos de historia clínica, indicando cuáles variables son obligatorias y si la relación está activa. Permite configurar qué datos deben registrarse en cada modelo de HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES';
