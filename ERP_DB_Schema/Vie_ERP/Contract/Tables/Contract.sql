CREATE TABLE [Contract].[Contract] (
    [Id]                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractEntityId]       INT            NULL,
    [HealthAdministratorId]  INT            NOT NULL,
    [Code]                   VARCHAR (20)   NOT NULL,
    [ContractName]           VARCHAR (100)  NULL,
    [ContractNumber]         VARCHAR (30)   NULL,
    [ContractValue]          NUMERIC (18)   NOT NULL,
    [ExecuteValue]           NUMERIC (18)   NOT NULL,
    [InitialDate]            DATETIME       NULL,
    [EndDate]                DATETIME       NULL,
    [Legalized]              BIT            NULL,
    [DateLegalization]       DATETIME       NULL,
    [Observations]           VARCHAR (MAX)  NULL,
    [ContractObject]         VARCHAR (MAX)  NOT NULL,
    [PrintingMode]           TINYINT        NULL,
    [TerminationControl]     TINYINT        NULL,
    [NotificationValueType]  TINYINT        NULL,
    [PercentageNotification] NUMERIC (5, 2) NULL,
    [NotificationValue]      NUMERIC (18)   NULL,
    [NotificationTimeType]   TINYINT        NULL,
    [NotificationDays]       INT            NULL,
    [Status]                 TINYINT        NOT NULL,
    [CreationUser]           VARCHAR (20)   NOT NULL,
    [CreationDate]           DATETIME       NOT NULL,
    [ModificationUser]       VARCHAR (20)   NULL,
    [ModificationDate]       DATETIME       NULL,
    [InForceUser]            VARCHAR (20)   NULL,
    [InForceDate]            DATETIME       NULL,
    [SuspendedUser]          VARCHAR (20)   NULL,
    [SuspendedDate]          DATETIME       NULL,
    [FinishedUser]           VARCHAR (20)   NULL,
    [FinishedDate]           DATETIME       NULL,
    CONSTRAINT [PK_Contract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contract_ContractEntity] FOREIGN KEY ([ContractEntityId]) REFERENCES [Contract].[ContractEntity] ([Id]),
    CONSTRAINT [FK_Contract_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id])
);


GO
ALTER TABLE [Contract].[Contract] NOCHECK CONSTRAINT [FK_Contract_ContractEntity];


GO
ALTER TABLE [Contract].[Contract] NOCHECK CONSTRAINT [FK_Contract_HealthAdministrator];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Contract__Code]
    ON [Contract].[Contract]([Code] ASC);


GO
CREATE INDEX IX_Contract_Id_Covering
    ON Contract.Contract (Id)
    INCLUDE (Code, ContractName);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que finalizo el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que finalizo el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FinishedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se suspendio el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que suspendio el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'SuspendedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se cambio a estado vigente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que cambio a estado vigente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InForceUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del contrato  1 - Vigente  2 - Suspendido  3 - Terminado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de notificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de notificacion para cuando es control por fecha  1 - Ninguno  2 - Dias de anterioridad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Notificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notificación de porcentaje', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PercentageNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de notificacion para cuando el control de terminacion es de valor  1 - Ninguna  2 - % del valor del contrato  3 - Valor fijo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValueType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de terminacion del contrato  1- Ninguno  2 - Fecha Terminacion  3 - Valor contrato  4 - Fecha Terminacion o Valor Contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TerminationControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de impresion de los reportes  1 - Manual Tarifario  2 - CUPS   3 - Codigo RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PrintingMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objeto del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractObject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de legalizacion del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'DateLegalization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contrato esta legalizado ?', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Legalized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ejecutado del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ExecuteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad de contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Id';

