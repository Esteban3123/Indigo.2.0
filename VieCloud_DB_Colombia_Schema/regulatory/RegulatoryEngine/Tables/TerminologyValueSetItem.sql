CREATE TABLE [RegulatoryEngine].[TerminologyValueSetItem] (
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [TerminologyValueSetId] BIGINT         NOT NULL,
    [CodeSystem]            NVARCHAR (100) NOT NULL,
    [Code]                  NVARCHAR (100) NOT NULL,
    [DisplayName]           NVARCHAR (500) NULL,
    [JurisdictionCode]      NVARCHAR (20)  NULL,
    [IsActive]              BIT            DEFAULT ((1)) NOT NULL,
    [CreatedAt]             DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TerminologyValueSetItem_ValueSet] FOREIGN KEY ([TerminologyValueSetId]) REFERENCES [RegulatoryEngine].[TerminologyValueSet] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TerminologyValueSetItem_Code]
    ON [RegulatoryEngine].[TerminologyValueSetItem]([CodeSystem] ASC, [Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ítems individuales de un TerminologyValueSet. Cada fila es un código (ej: CUPS 730303) dentro de un sistema de codificación (CUPS, CPT, SNOMED) perteneciente a una jurisdicción. JurisdictionCode permite filtrar por país cuando un ValueSet contiene códigos de múltiples jurisdicciones. La regla no conoce qué códigos son colombianos o de otro país; solo pregunta al ValueSet.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del ítem terminológico.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al ValueSet al que pertenece este código.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'TerminologyValueSetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sistema de codificación al que pertenece el código (ej: CUPS, CPT, SNOMED-CT, ICD10, CIE10). Junto con Code identifica unívocamente un concepto clínico. El motor filtra por CodeSystem cuando se envía en el contexto de validación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'CodeSystem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código clínico o administrativo perteneciente al sistema de codificación indicado en CodeSystem (ej: 730303 en CUPS, 99213 en CPT). La búsqueda del motor es exacta y sensible a mayúsculas según el cotejamiento de la base de datos.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre legible del código (ej: "Parto por cesárea", "Office visit established patient"). Informativo; no se usa en la lógica de validación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la jurisdicción a la que aplica este ítem (ej: CO, MX). NULL indica que el código aplica a todas las jurisdicciones. Permite incluir en un mismo ValueSet códigos equivalentes de distintos países y filtrar por país en tiempo de validación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'JurisdictionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el ítem está activo. Los ítems inactivos son excluidos de las consultas del motor de validación, permitiendo retirar códigos obsoletos sin eliminar el historial.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSetItem', @level2type = N'COLUMN', @level2name = N'CreatedAt';
