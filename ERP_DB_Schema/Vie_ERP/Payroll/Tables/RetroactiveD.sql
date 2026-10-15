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
ALTER TABLE [Payroll].[RetroactiveD] NOCHECK CONSTRAINT [FK_RetroactiveD_Concept];


GO
ALTER TABLE [Payroll].[RetroactiveD] NOCHECK CONSTRAINT [FK_RetroactiveD_RetroactiveD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Concepto con Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConceptWithRetroactive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'ValueConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Registro de la Cabecera de la Retroactividad (RetroactiveC)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'IdRetroactiveC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Registro de RetroactiveD', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveD', @level2type = N'COLUMN', @level2name = N'Id';

