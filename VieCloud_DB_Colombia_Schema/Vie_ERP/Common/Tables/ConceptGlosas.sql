CREATE TABLE [Common].[ConceptGlosas] (
    [Id]                    INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (10)   NOT NULL,
    [NameGeneral]           VARCHAR (100)  NOT NULL,
    [Application]           VARCHAR (1000) NOT NULL,
    [Type]                  CHAR (1)       NOT NULL,
    [NameSpecific]          VARCHAR (500)  NOT NULL,
    [DiscountedMedicalFees] BIT            CONSTRAINT [DF_ConceptGlosas_DiscountedMedicalFees] DEFAULT ((0)) NOT NULL,
    [State]                 BIT            NOT NULL,
    CONSTRAINT [PK_ConceptGlosas__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_CG_ById]
    ON [Common].[ConceptGlosas]([Id] ASC)
    INCLUDE([NameSpecific], [Code]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ConceptGlosas__Code]
    ON [Common].[ConceptGlosas]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del concepto de glosa; booleano (1=activo, 0=inactivo)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aplicación de descuento en tarifas médicas o aranceles profesionales; booleano (1=sí descuenta, 0=no descuenta)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarifas médicas con descuento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre específico y detallado del concepto de glosa; descripción particular del motivo de glosa, respuesta o devolución (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameSpecific';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre específico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameSpecific';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameSpecific';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto de glosa: 1=Glosa, 2=Respuesta glosa, 3=Devolución, 4=Glosa no normativa, 5=Respuesta Glosa Injustificada, 6=Respuesta Glosa Subsanada Parcial, 7=Respuesta Glosa Subsanada Total, 8=Respuesta Glosa Aceptación Total, 9=Devolución Injustificada, 10=Devolución Justificada (CHAR 1, numérico)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo  1 - Glosa  2 - Respuesta glosa  3 - Devolución  4 - Glosa no normativa  5 - Respuesta Glosa Injustificada  6 - Respuesta Glosa Subsanada Parcial  7 - Respuesta Glosa Subsanada Total  8 - Respuesta Glosa Aceptación Total  9 - Devolución Injustificada   10 - Devolución Justificada', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la aplicación o contexto en que se utiliza el concepto de glosa; ámbito de uso en procesos de facturación, recaudos o RIPS (VARCHAR 1000)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Application';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Application';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Application';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre general o categoría del concepto de glosa; clasificación amplia del motivo de glosa o devolución en facturación (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameGeneral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre General', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameGeneral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameGeneral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único del concepto de glosa; identificador corto para procesos de facturación, auditoría y RIPS (VARCHAR 10, clave única)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable único (INT IDENTITY); clave primaria de la tabla ConceptGlosas', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de conceptos de glosas utilizados en el proceso de auditoría y facturación. Registra los tipos de glosa que pueden aplicarse a servicios de salud, indicando su nombre, aplicación, tipo y si descuenta honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas';
