CREATE TABLE [Payments].[DeferredCausationShare] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DeferredCausationId] INT             NOT NULL,
    [PaymentMonth]        INT             NOT NULL,
    [PaymentYear]         INT             NOT NULL,
    [Value]               DECIMAL (20, 2) NOT NULL,
    [Amortized]           BIT             NOT NULL,
    [CreationUser]        VARCHAR (20)    NOT NULL,
    [CreationDate]        DATETIME        NOT NULL,
    [ModificationUser]    VARCHAR (20)    NULL,
    [ModificationDate]    DATETIME        NULL,
    CONSTRAINT [PK_DeferredCausationShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausationShare_DeferredCausation] FOREIGN KEY ([DeferredCausationId]) REFERENCES [Payments].[DeferredCausation] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro; timestamp de actualización, auditoría temporal de cambios.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha modificacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o nombre de usuario (VARCHAR 20) que modificó por última vez el registro; auditoría de cambios, identificación del usuario editor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de la cuota; timestamp de ingreso al sistema, auditoría temporal de origen.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o nombre de usuario (VARCHAR 20) que creó el registro de la cuota; auditoría de origen, identificación del usuario creador.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT: 0=No, 1=Sí) de si la cuota de causación ya fue amortizada, pagada o liquidada; estado de cancelación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Amortized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Amortizado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Amortized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Amortized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto (DECIMAL 20,2) a pagar en la cuota; cantidad económica de la causación diferida, pasivo diferido, glosa o deuda.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se va pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año (INT) en que se programó el pago de la cuota de causación diferida; define el período de pago junto con PaymentMonth.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año en que se va pagar la cuota de la causacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes (INT, 1-12) en que se program el pago de la cuota de causación diferida; componente temporal junto con PaymentYear.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes en que se va pagar la cuota de la causacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la causación diferida (FK a Payments.DeferredCausation); vincula la cuota al registro cabecera de causación diferida o pasivo diferido.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la causación diferida cabecera', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la cuota o pago diferido de la causación; clave primaria de la tabla de detalles de causación diferida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuota de la causacion diferida', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de las cuotas o participaciones mensuales de causación diferida de pagos. Cada fila representa una porción de un pago diferido que debe causarse (reconocerse contablemente) en un mes y año específicos, indicando si ya fue amortizada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare';
