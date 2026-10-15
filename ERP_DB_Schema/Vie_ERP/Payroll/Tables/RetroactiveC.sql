CREATE TABLE [Payroll].[RetroactiveC] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Status]                 TINYINT         NOT NULL,
    [PaymentType]            CHAR (1)        NOT NULL,
    [NextPayrollDate]        DATE            NULL,
    [InitialDateRetroactive] DATE            NOT NULL,
    [UsedPercentage]         DECIMAL (5, 2)  NOT NULL,
    [IdGroup]                INT             NOT NULL,
    [IdEmployee]             INT             NOT NULL,
    [IdContract]             INT             NOT NULL,
    [InitialContractNumber]  INT             NOT NULL,
    [ExecuteProcessDate]     DATE            NOT NULL,
    [TotalRetroactiveValue]  NUMERIC (18, 2) NOT NULL,
    [CreationUserId]         INT             NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ConfirmationUserId]     INT             NULL,
    [ConfirmationDate]       DATETIME        NULL,
    CONSTRAINT [PK_RetroactiveC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RetroactiveC_Contract] FOREIGN KEY ([IdContract]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_RetroactiveC_Employee] FOREIGN KEY ([IdEmployee]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_RetroactiveC_Group] FOREIGN KEY ([IdGroup]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[RetroactiveC] NOCHECK CONSTRAINT [FK_RetroactiveC_Contract];


GO
ALTER TABLE [Payroll].[RetroactiveC] NOCHECK CONSTRAINT [FK_RetroactiveC_Employee];


GO
ALTER TABLE [Payroll].[RetroactiveC] NOCHECK CONSTRAINT [FK_RetroactiveC_Group];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Usuario que Confirmó', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total del Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'TotalRetroactiveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Ejecución del Proceso de Retroactividad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ExecuteProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Contrato Inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje Utilizado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'UsedPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialDateRetroactive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En caso de que se pague con nómina, acá irá la Fecha de la Próxima Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'NextPayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Pago: 1. Nómina (N), 2. Periodo Independiente (P) ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1 - Borrador, 2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Id';

