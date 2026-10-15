CREATE TABLE [Budget].[ReimbursementResource] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]  INT           NOT NULL,
    [DocumentDate]         DATETIME      NOT NULL,
    [PaymentOrderId]       INT           NOT NULL,
    [UpTo]                 TINYINT       NOT NULL,
    [Observations]         VARCHAR (MAX) NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  CONSTRAINT [DF_ReimbursementResource_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]         DATETIME      CONSTRAINT [DF_ReimbursementResource_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [Document]             VARCHAR (100) NULL,
    [AnnulmentConceptId]   INT           NULL,
    [AnnulmentDescription] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ReimbursementResource__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReimbursementResource_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_ReimbursementResource_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id]),
    CONSTRAINT [FK_ReimbursementResource_PaymentOrder] FOREIGN KEY ([PaymentOrderId]) REFERENCES [Budget].[PaymentOrder] ([Id])
);


GO
ALTER TABLE [Budget].[ReimbursementResource] NOCHECK CONSTRAINT [FK_ReimbursementResource_BudgetaryValidity];


GO
ALTER TABLE [Budget].[ReimbursementResource] NOCHECK CONSTRAINT [FK_ReimbursementResource_Concept];


GO
ALTER TABLE [Budget].[ReimbursementResource] NOCHECK CONSTRAINT [FK_ReimbursementResource_PaymentOrder];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResource', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de documento hasta el cual se modifica el recurso   1 - Obligacion  2 - Compromiso  3 - Disponibilidad  4 - Presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'ReimbursementResource', @level2type = N'COLUMN', @level2name = N'UpTo';

