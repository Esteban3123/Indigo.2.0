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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP SQL Server) que registra automáticamente el instante exacto de creación, actualización o modificación del registro de configuración presupuestal, usada para control de concurrencia y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación (DATETIME) del registro de configuración presupuestal, captura cuándo se realizó el cambio en las políticas de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que realizó la última modificación de la configuración presupuestal, vinculado a auditoría y trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME) del registro de configuración presupuestal, marca el momento de instancia inicial de las políticas de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (VARCHAR 20) que creó el registro de configuración presupuestal, por defecto valor 999, usado para auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita/deshabilita la agrupación automática de documentos (facturas, glosas, RIPS, recetas) por tercero/proveedor/acreedor en la gestión presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'DocumentsGroupping';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agrupar documentos por tercero', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'DocumentsGroupping';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'DocumentsGroupping';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que activa/desactiva la creación automática de cuentas por cobrar (deudores, pacientes, aseguradoras) en el módulo de presupuesto, ligado a cartera.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReceivableAccounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Crear cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReceivableAccounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReceivableAccounts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que activa/desactiva la creación automática de cuentas por pagar (proveedores, acreedores, servicios) en el módulo de presupuesto, ligado a obligaciones.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreatePayableAccounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Crear cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreatePayableAccounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreatePayableAccounts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que habilita/deshabilita la creación automática de reservas presupuestales (provisiones, contingencias) en la gestión financiera del presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReserves';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Crear reservas', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReserves';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'CreateReserves';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, por defecto 1=activado) que habilita/deshabilita la interfaz de integración del módulo Presupuesto con otros módulos (Contabilidad, Cartera, Proveedores, RIPS, Facturación).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'EnabledInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Habilitar interface con los otros modulos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'EnabledInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'EnabledInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK a Common.OperatingUnit) que identifica la unidad operativa, centro de atención, unidad funcional o sede para la cual aplican estas configuraciones de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, clave primaria IDENTITY) del registro de configuración presupuestal de una unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración del módulo de presupuesto por unidad operativa. Define qué funcionalidades están activas: interfaz contable, creación de reservas presupuestales, cuentas por pagar, cuentas por cobrar y agrupación de documentos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'SettingsBudget';
