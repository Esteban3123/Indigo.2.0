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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del contrato  1 - Vigente  2 - Suspendido  3 - Terminado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de notificacion para cuando es control por fecha  1 - Ninguno  2 - Dias de anterioridad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de notificacion para cuando el control de terminacion es de valor  1 - Ninguna  2 - % del valor del contrato  3 - Valor fijo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'NotificationValueType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de terminacion del contrato  1- Ninguno  2 - Fecha Terminacion  3 - Valor contrato  4 - Fecha Terminacion o Valor Contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TerminationControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de impresion de los reportes  1 - Manual Tarifario  2 - CUPS   3 - Codigo RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PrintingMode';

