CREATE TABLE [Payroll].[ElectronicPayrollNotification] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicPayrollId] INT           NOT NULL,
    [Email]               VARCHAR (100) NOT NULL,
    [Status]              BIT           NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ShippingDate]        DATETIME      NULL,
    CONSTRAINT [PK_ElectronicPayrollNotification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollNotification_ElectronicPayroll] FOREIGN KEY ([ElectronicPayrollId]) REFERENCES [Payroll].[ElectronicPayroll] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del envío de la notificación electrónica de nómina; DATETIME NULL, registra cuándo se distribuyó efectivamente el documento al destinatario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del envío', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ShippingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de notificación; DATETIME NOT NULL, marca el momento en que se generó la intención de envío.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del envío de notificación: 0=Aún no enviado, 1=Enviado exitosamente; BIT que indica si la nómina electrónica fue distribuida al correo del destinatario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del envío      0. Aún no enviado      1. Enviado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del destinatario de la nómina; VARCHAR(100) NOT NULL, dirección de correo donde se notifica al empleado o beneficiario sobre disponibilidad del documento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico del destinatario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento de nómina electrónica relacionado; FK INT a [Payroll].[ElectronicPayroll], vincula esta notificación a su nómina de origen.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de notificación de nómina; INT IDENTITY PK, clave primaria que distingue cada intento o detalle de envío del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del envío del documento electronico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de notificaciones enviadas por correo electrónico sobre la nómina electrónica. Controla a qué destinatario se notificó, el estado del envío y las fechas de creación y despacho del mensaje.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollNotification';
