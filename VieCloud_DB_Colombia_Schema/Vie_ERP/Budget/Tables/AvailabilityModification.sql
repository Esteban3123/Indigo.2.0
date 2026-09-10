CREATE TABLE [Budget].[AvailabilityModification] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]  INT           NOT NULL,
    [AvailabilityId]       INT           NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [Observations]         VARCHAR (MAX) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_AvailabilityModification_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_AvailabilityModification_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [UpTo]                 TINYINT       CONSTRAINT [DF__Availabili__UpTo__4065A6D0] DEFAULT ((3)) NOT NULL,
    [Document]             VARCHAR (100) NULL,
    [EntityId]             INT           NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityName]           VARCHAR (250) NULL,
    [AnnulmentConceptId]   INT           NULL,
    [AnnulmentDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_AvailabilityModification__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailabilityModification_Availability] FOREIGN KEY ([AvailabilityId]) REFERENCES [Budget].[Availability] ([Id]),
    CONSTRAINT [FK_AvailabilityModification_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_AvailabilityModification_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id])
);




GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AvailabilityModification__Code__BudgetaryValidityId]
    ON [Budget].[AvailabilityModification]([Code] ASC, [BudgetaryValidityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la anulación o cancelación de la modificación de disponibilidad presupuestal, motivo o justificación de por qué se anuló el documento (VARCHAR MAX, texto libre)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de anulación (razón/motivo clasificado), FK a Budget.Concept, indica la categoría o tipo de cancelación aplicada', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad, organización o unidad funcional que genera y origina el documento de modificación (VARCHAR 250)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la entidad generadora del documento, identificador alfanumérico de la organización o centro de atención (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la entidad que origina la modificación de disponibilidad (INT, FK implícito a entidad de negocio)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o referencia del documento externo o comprobante asociado a la modificación presupuestal (VARCHAR 100, ej: factura, comprobante, resolución)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Document';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Document';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Document';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o nivel de documento presupuestal hasta el cual aplica la modificación (TINYINT: 3=Disponibilidad, 4=Presupuesto, valores 0-255, default=3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'UpTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de documento hasta el cual se modifica el recurso   4 - Presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'UpTo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'UpTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo autogenerada que registra el instante exacto de creación, modificación o cambio de estado del documento (TIMESTAMP, control de concurrencia)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la anulación o cancelación del documento de modificación (DATETIME NULL, auditoría de eventos)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador o nombre del usuario que ejecutó la anulación del documento (VARCHAR 20, PII - auditoría)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la confirmación o aprobación oficial del documento de modificación (DATETIME NULL, auditoría de flujo)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que confirmó o aprobó la modificación de disponibilidad (VARCHAR 20, PII - auditoría)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio o edición del documento (DATETIME NULL, auditoría de cambios)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó la última modificación o edición (VARCHAR 20, PII - auditoría)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de modificación de disponibilidad (DATETIME, default=getdate(), auditoría)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó el documento de modificación (VARCHAR 20, PII - auditoría, default=999)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del documento (TINYINT: 1=Registrado, 2=Confirmado, 3=Anulado; indica flujo de aprobación y ciclo de vida)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, comentarios o notas adicionales sobre la modificación de disponibilidad presupuestal (VARCHAR MAX, texto libre)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha asociada a la disponibilidad, fecha de corte o fecha del evento presupuestal que se modifica (DATETIME, auditoría de negocio)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la disponibilidad presupuestal original que se está modificando, FK a Budget.Availability (INT, relación padre)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AvailabilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AvailabilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'AvailabilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vigencia presupuestal a la cual pertenece la modificación, FK a Budget.BudgetaryValidity (INT, relación de contexto temporal)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la vigencia a la cual pertenece', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'BudgetaryValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único o serial de la modificación de disponibilidad, identificador alfanumérico del movimiento presupuestal (VARCHAR 20, clave de negocio)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la modificacion de disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincremental del registro de modificación de disponibilidad (INT IDENTITY 1,1, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del la modificacion de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificaciones a disponibilidades presupuestales: registra cada documento de ajuste, adición o reducción sobre una disponibilidad de presupuesto dentro de una vigencia presupuestal, incluyendo su estado, confirmación, anulación y la entidad relacionada.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModification';
