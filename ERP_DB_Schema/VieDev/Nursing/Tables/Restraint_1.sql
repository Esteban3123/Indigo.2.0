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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de suspención', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'SuspendedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de pedido', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la extremidad (superior derecha, superior izquierda, inferior derecha, inferior izquierda) checked', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Limb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la tabla TypeRestraint', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTypeRestraint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la TherapeuticRestraints', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'IdTherapeuticRestraints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'Restraint', @level2type = N'COLUMN', @level2name = N'Id';

