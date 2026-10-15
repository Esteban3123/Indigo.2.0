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
ALTER TABLE [Payments].[DeferredCausationShare] NOCHECK CONSTRAINT [FK_DeferredCausationShare_DeferredCausation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha modificacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Amortizado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Amortized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se va pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año en que se va pagar la cuota de la causacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes en que se va pagar la cuota de la causacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'PaymentMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causación diferida cabecera', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota de la causacion diferida', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationShare', @level2type = N'COLUMN', @level2name = N'Id';

