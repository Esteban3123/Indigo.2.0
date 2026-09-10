CREATE TABLE [Payroll].[RetroactiveD] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdRetroactiveC]              INT          NOT NULL,
    [IdConcept]                   INT          NOT NULL,
    [ValueConcept]                NUMERIC (18) NOT NULL,
    [ValueConceptWithRetroactive] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_RetroactiveD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RetroactiveD_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_RetroactiveD_RetroactiveD] FOREIGN KEY ([IdRetroactiveC]) REFERENCES [Payroll].[RetroactiveC] ([Id])
);


GO
ALTER TABLE [Payroll].[RetroactiveD] NOCHECK CONSTRAINT [FK_RetroactiveD_RetroactiveD];




GO



GO
ALTER TABLE [Payroll].[RetroactiveD] NOCHECK CONSTRAINT [FK_RetroactiveD_RetroactiveD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del concepto de nómina (salario, descuento, bonificación, etc.) después de aplicar el cálculo retroactivo; tipo NUMERIC(18), representa el importe final ajustado por retroactividad en pesos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConceptWithRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Concepto con Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConceptWithRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConceptWithRetroactive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario original del concepto de nómina (salario, descuento, bonificación, auxilio, comisión, etc.) antes de aplicar retroactividad; tipo NUMERIC(18), importe base en pesos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del concepto de nómina referenciado en tabla Payroll.Concept; vincula a rubros salariales como básico, auxilio transporte, retención, descuentos, bonificaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la cabecera/maestro de retroactividad en tabla Payroll.RetroactiveC; agrupa el conjunto de detalles de conceptos afectados por un ajuste retroactivo de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdRetroactiveC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Registro de la Cabecera de la Retroactividad (RetroactiveC)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdRetroactiveC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdRetroactiveC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del registro de detalle de retroactividad en RetroactiveD; llave primaria secuencial que identifica cada línea de concepto dentro de un proceso de retroactividad de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Registro de RetroactiveD', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos de nómina incluidos en un cálculo retroactivo. Registra el valor original de cada concepto y el valor ajustado con el retroactivo correspondiente, permitiendo auditar las diferencias generadas por reliquidaciones salariales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD';
