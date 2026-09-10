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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación del retroactivo por el usuario autorizado. DATETIME, auditoria de aprobación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Confirmación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que confirmó/aprobó el retroactivo. INT, FK a usuario auditor.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Usuario que Confirmó', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ConfirmationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de retroactivo. DATETIME, auditoria de origen.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó el retroactivo. INT, FK a usuario autor.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'CreationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total acumulado del retroactivo en pesos. NUMERIC(18,2), monto de liquidación compensatoria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'TotalRetroactiveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total del Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'TotalRetroactiveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'TotalRetroactiveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ejecución del proceso de cálculo y liquidación del retroactivo. DATE.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ExecuteProcessDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Ejecución del Proceso de Retroactividad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ExecuteProcessDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'ExecuteProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del contrato inicial que genera el retroactivo. INT, referencia contrato anterior.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Contrato Inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato asociado al retroactivo. INT, FK a Payroll.Contract.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado beneficiario del retroactivo. INT, FK a Payroll.Employee.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdEmployee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo/unidad funcional del empleado. INT, FK a Payroll.Group.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'IdGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aplicado sobre el cálculo retroactivo. DECIMAL(5,2), ej: 50.00 = 50%.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'UsedPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Utilizado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'UsedPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'UsedPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del período retroactivo a liquidar. DATE, desde cuándo se calcula.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialDateRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialDateRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'InitialDateRetroactive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la próxima nómina si el pago es integrado a nómina. DATE NULL, opcional según PaymentType=N.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'NextPayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En caso de que se pague con nómina, acá irá la Fecha de la Próxima Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'NextPayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'NextPayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de desembolso: N=Nómina (integrado), P=Período Independiente (cheque/transferencia). CHAR(1).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Pago: 1. Nómina (N), 2. Periodo Independiente (P) ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del retroactivo: 1=Borrador (editable), 2=Confirmado (aprobado). TINYINT, flujo de aprobación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Borrador, 2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autonumerado del registro retroactivo. INT IDENTITY(1,1), clave primaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de liquidación retroactiva de nómina: captura los ajustes salariales que deben pagarse con efecto retroactivo a empleados, incluyendo el período afectado, el porcentaje aplicado, el valor total a pagar y el estado del proceso de aprobación y ejecución.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetroactiveC';
