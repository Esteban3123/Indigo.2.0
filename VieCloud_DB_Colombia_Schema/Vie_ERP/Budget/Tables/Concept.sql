CREATE TABLE [Budget].[Concept] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId] INT           NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Name]                VARCHAR (100) NULL,
    [Status]              BIT           CONSTRAINT [DF_BudgetConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_Concept_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_Concept_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Concept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Concept_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Concept__Code__BudgetaryValidityId]
    ON [Budget].[Concept]([Code] ASC, [BudgetaryValidityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP SQL Server) que registra el instante exacto de creación, modificación o cambio de estado del concepto presupuestal en el sistema.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó la última modificación del concepto presupuestal; permite auditar cambios en rúbricas o partidas.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó la última modificación del concepto; usado para auditoría y trazabilidad de cambios en el presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro del concepto presupuestal en el sistema.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó originalmente el concepto presupuestal; facilita auditoría de creación de rúbricas.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del concepto presupuestal: 1=Activo (vigente), 0=Inactivo (descontinuado); controla disponibilidad para asignaciones presupuestales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del concepto presupuestal (rúbrica, partida, glosa, concepto de factura); identifica qué se presupuesta.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del cocepto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del concepto presupuestal (rúbrica, partida, línea de gasto); referencia normalizada para reportes y RIPS.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del concepto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vigencia presupuestal asociada; llave foránea a [Budget].[BudgetaryValidity] para vincular concepto a período fiscal/anual.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Vigencia del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del concepto presupuestal; clave primaria para referencia en auditoría y relaciones presupuestales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos presupuestales asociados a una vigencia presupuestaria. Permite clasificar y nombrar los rubros o ítems del presupuesto para el control financiero de la institución.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Concept';
