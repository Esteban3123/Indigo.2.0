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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha cuando se ejecuta la acción de no cotizar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'NotQuotedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que ejecuta la acción de no cotizar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'NotQuotedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  1 - Sin Confirmar  2 - Confirmado  3 - No Cotizado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cotización asociada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'QuotationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza la solicitud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad. Se asigna desde el ingreso. Puede ir nulo porque el grupo de atención puede ser tipo particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atención. Se asigna del ingreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla de Crystal del cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de Crystal del cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código unidad funcional', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'RequestQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la cual se realizó la solicitud del registro en las tablas de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'RequestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código centro atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es un producto o un servicio:   1 - Servicio  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa al código del servicio o al código del producto dependiendo de la tabla de Crystal de donde venga el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de admisión', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'DashboardQuoted', @level2type = N'COLUMN', @level2name = N'Id';

