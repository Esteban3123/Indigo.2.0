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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tarifas médicas con descuento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre específico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameSpecific';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo  1 - Glosa  2 - Respuesta glosa  3 - Devolución  4 - Glosa no normativa  5 - Respuesta Glosa Injustificada  6 - Respuesta Glosa Subsanada Parcial  7 - Respuesta Glosa Subsanada Total  8 - Respuesta Glosa Aceptación Total  9 - Devolución Injustificada   10 - Devolución Justificada', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplicacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Application';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre General', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'NameGeneral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ConceptGlosas', @level2type = N'COLUMN', @level2name = N'Id';

