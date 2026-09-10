CREATE TABLE [Nursing].[Restraint] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [IdTherapeuticRestraints] INT      NOT NULL,
    [IdTypeRestraint]         INT      NOT NULL,
    [Limb]                    TINYINT  NULL,
    [OrderDate]               DATETIME NOT NULL,
    [State]                   BIT      NOT NULL,
    [SuspendedDate]           DATETIME NULL,
    CONSTRAINT [PK_Restraint] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se suspendió o levantó la restricción terapéutica (NULL si aún está activa).', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'SuspendedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de suspención', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'SuspendedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'SuspendedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo (1) o inactivo (0) de la restricción; indica si la contención está vigente o finalizada.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio o prescripción de la orden de restricción terapéutica en el paciente.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de pedido', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extremidad afectada por la restricción: superior derecha, superior izquierda, inferior derecha, inferior izquierda (TINYINT, codificado).', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Limb';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la extremidad (superior derecha, superior izquierda, inferior derecha, inferior izquierda) checked', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Limb';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Limb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tipo o clasificación de restricción terapéutica aplicada (ej: física, química, sedación).', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTypeRestraint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la tabla TypeRestraint', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTypeRestraint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTypeRestraint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tratamiento terapéutico o protocolo de restricción asociado a esta orden de contención.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la TherapeuticRestraints', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTherapeuticRestraints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (IDENTITY) de la restricción terapéutica registrada, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de restricciones físicas (sujeciones) aplicadas a pacientes durante su atención de enfermería, indicando el tipo de restricción, el miembro del cuerpo afectado, la fecha de la orden y si está activa o suspendida.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint';
