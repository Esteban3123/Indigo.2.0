CREATE TABLE [Billing].[DashboardQuoted] (
    [Id]                    INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]       VARCHAR (20)                                                                     NOT NULL,
    [ServiceCode]           VARCHAR (20)                                                                     NOT NULL,
    [Type]                  TINYINT                                                                          NOT NULL,
    [PatientCode]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CareCenterCode]        VARCHAR (20)                                                                     NOT NULL,
    [RequestDate]           DATETIME                                                                         NOT NULL,
    [RequestQuantity]       INT                                                                              NOT NULL,
    [FunctionalUnitCode]    VARCHAR (20)                                                                     NOT NULL,
    [EntityId]              INT                                                                              NULL,
    [EntityName]            VARCHAR (50)                                                                     NULL,
    [CareGroupId]           INT                                                                              NOT NULL,
    [HealthAdministratorId] INT                                                                              NULL,
    [ProfessionalCode]      VARCHAR (20)                                                                     NOT NULL,
    [QuotationId]           INT                                                                              NULL,
    [Status]                TINYINT                                                                          NOT NULL,
    [CreationUser]          VARCHAR (20)                                                                     NOT NULL,
    [CreationDate]          DATETIME                                                                         NOT NULL,
    [ModificationUser]      VARCHAR (20)                                                                     NULL,
    [ModificationDate]      DATETIME                                                                         NULL,
    [ConfirmationUser]      VARCHAR (20)                                                                     NULL,
    [ConfirmationDate]      DATETIME                                                                         NULL,
    [NotQuotedUser]         VARCHAR (20)                                                                     NULL,
    [NotQuotedDate]         DATETIME                                                                         NULL,
    [TimeStamp]             ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_DashboardQuoted] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DashboardQuoted_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_DashboardQuoted_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_DashboardQuoted_Quotation] FOREIGN KEY ([QuotationId]) REFERENCES [Billing].[Quotation] ([Id]),
    CONSTRAINT [UQ_FIELDSKEY] UNIQUE NONCLUSTERED ([AdmissionNumber] ASC, [ServiceCode] ASC, [EntityId] ASC, [EntityName] ASC)
);


GO
ALTER TABLE [Billing].[DashboardQuoted] NOCHECK CONSTRAINT [FK_DashboardQuoted_CareGroup];


GO
ALTER TABLE [Billing].[DashboardQuoted] NOCHECK CONSTRAINT [FK_DashboardQuoted_HealthAdministrator];


GO
ALTER TABLE [Billing].[DashboardQuoted] NOCHECK CONSTRAINT [FK_DashboardQuoted_Quotation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  1 - Sin Confirmar  2 - Confirmado  3 - No Cotizado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es un producto o un servicio:   1 - Servicio  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'Type';

