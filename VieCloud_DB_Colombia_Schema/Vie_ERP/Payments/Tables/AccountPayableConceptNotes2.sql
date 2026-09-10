CREATE TABLE [Payments].[AccountPayableConceptNotes2] (
    [Code]         VARCHAR (20)  NOT NULL,
    [Name]         VARCHAR (100) NOT NULL,
    [ConceptType]  TINYINT       NOT NULL,
    [IdAccount]    VARCHAR (20)  NULL,
    [AffectBudget] BIT           NOT NULL,
    [Behavior]     TINYINT       NULL,
    [Status]       BIT           NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de catálogo que almacena conceptos o rubros utilizados en notas de cuentas por pagar, identificados por un código y nombre. Cada concepto tiene un tipo, un comportamiento opcional y una indicación de si afecta el presupuesto. Puede estar asociado a una cuenta contable y habilitarse o deshabilitarse mediante el campo de estado.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'TABLE', @level1name=N'AccountPayableConceptNotes2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'TABLE', @level1name=N'AccountPayableConceptNotes2';
GO
