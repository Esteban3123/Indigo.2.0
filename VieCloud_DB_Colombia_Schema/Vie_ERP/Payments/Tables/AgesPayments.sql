CREATE TABLE [Payments].[AgesPayments] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingPaymentId] INT           CONSTRAINT [DF_AgesPayments_SettingPaymentId] DEFAULT ((1010)) NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [InitialRange]     INT           NOT NULL,
    [EndRange]         INT           NOT NULL,
    [Color]            INT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_AgesPayments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgesPayments_SettingPayments] FOREIGN KEY ([SettingPaymentId]) REFERENCES [Payments].[SettingPayments] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del rango de edades de pagos (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del rango (VARCHAR 20, auditoría, identificador de operador)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del rango de edades (DATETIME, auditoría, trazabilidad)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el rango de edades de pagos (VARCHAR 20, auditoría, identificador de operador)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de color asignado para identificar visualmente el rango de edades (INT, representación hexadecimal o paleta)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color que se selecciono para identiicar el color del rango', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Color';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor final o máximo del rango de días para clasificación de pago (INT, días, límite superior inclusivo)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'EndRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor final del rango (Dias)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'EndRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'EndRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial o mínimo del rango de días; por defecto 0 para primer registro (INT, días, límite inferior inclusivo)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'InitialRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor inicial del rango, Si es el primer registro este campo sera por defecto 0 (Dias)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'InitialRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'InitialRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o etiqueta descriptiva del rango de edades de pagos (VARCHAR 100, ej: ''''0-30 días'''', ''''31-60 días'''', ''''Vencido'''')', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del rango de la edad de pagos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración de pago asociada (INT, FK → Payments.SettingPayments, valor por defecto 1010)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'SettingPaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID configuracion de pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'SettingPaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'SettingPaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del rango de edades de pagos (INT IDENTITY, clave primaria, PK_AgesPayments)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rango de edades de pagos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rangos o tramos de antigüedad de pagos (aging de cartera), donde cada fila define un intervalo de días (por ejemplo 0-30, 31-60) con su nombre, color de visualización y configuración asociada, usado para clasificar deudas o cuentas por cobrar según su vencimiento.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AgesPayments';
