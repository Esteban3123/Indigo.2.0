CREATE TABLE [RegulatoryEngine].[TerminologyValueSet] (
    [Id]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (200)  NOT NULL,
    [Description]      NVARCHAR (1000) NULL,
    [JurisdictionCode] NVARCHAR (20)   NULL,
    [Version]          NVARCHAR (50)   NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [CreatedAt]        DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TerminologyValueSet_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_TerminologyValueSet_Name_Version] UNIQUE NONCLUSTERED ([Name] ASC, [Version] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TerminologyValueSet_Name]
    ON [RegulatoryEngine].[TerminologyValueSet]([Name] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de conjuntos de valores terminológicos (ValueSets) usados por las reglas del motor regulatorio. Cada ValueSet agrupa códigos de procedimientos, diagnósticos u otros conceptos clínicos bajo un nombre genérico (ej: ValidProcedureCodes, MultipleBirthDeliveryProcedures). Las reglas consultan el ValueSet por nombre, sin conocer los códigos específicos de cada país.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del ValueSet.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre único del ValueSet (ej: ValidProcedureCodes, MultipleBirthDeliveryProcedures). Es la clave de negocio que las reglas usan para referenciarlo desde RuleParameter.ParameterValue. Debe ser estable entre versiones para no romper las reglas que lo referencian.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del propósito clínico o normativo del ValueSet, indicando qué tipo de códigos agrupa y para qué reglas es relevante.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la jurisdicción a la que aplica el ValueSet (ej: CO, MX). NULL indica que el ValueSet es transversal a todas las jurisdicciones. Permite mantener listas de códigos separadas por país dentro del mismo catálogo.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet', @level2type = N'COLUMN', @level2name = N'JurisdictionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del ValueSet (ej: 2024.1). Junto con Name forma la clave única. Permite actualizar los códigos de un ValueSet creando una nueva versión sin afectar las reglas que aún referencian la versión anterior.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el ValueSet está activo. Los ValueSets inactivos no son consultados por el motor de validación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'TerminologyValueSet', @level2type = N'COLUMN', @level2name = N'CreatedAt';
