CREATE TABLE [Budget].[SettingsBudget] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]          INT          NOT NULL,
    [EnabledInterface]         BIT          CONSTRAINT [DF_SettingBudget_EnabledInterface] DEFAULT ((1)) NOT NULL,
    [CreateReserves]           BIT          CONSTRAINT [DF_SettingBudget_CreateReserves] DEFAULT ((1)) NOT NULL,
    [CreatePayableAccounts]    BIT          CONSTRAINT [DF_SettingBudget_CreatePayableAccounts] DEFAULT ((1)) NOT NULL,
    [CreateReceivableAccounts] BIT          CONSTRAINT [DF_SettingBudget_CreateReceivableAccounts] DEFAULT ((1)) NOT NULL,
    [DocumentsGroupping]       BIT          CONSTRAINT [DF_SettingBudget_DocumentsGroupping] DEFAULT ((1)) NOT NULL,
    [CreationUser]             VARCHAR (20) CONSTRAINT [DF_BudgetParameter_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]             DATETIME     CONSTRAINT [DF_BudgetParameter_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]         VARCHAR (20) NULL,
    [ModificationDate]         DATETIME     NULL,
    [TimeStamp]                ROWVERSION   NOT NULL,
    CONSTRAINT [PK_SettingBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetParameter_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Budget].[SettingsBudget] NOCHECK CONSTRAINT [FK_BudgetParameter_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agrupar documentos por tercero', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'DocumentsGroupping';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Crear cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReceivableAccounts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Crear cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreatePayableAccounts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Crear reservas', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReserves';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Habilitar interface con los otros modulos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'EnabledInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'Id';

