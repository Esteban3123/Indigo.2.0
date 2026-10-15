CREATE TABLE [HumanTalent].[PartialContract] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RowType]                      TINYINT       NOT NULL,
    [InitialContractNumber]        INT           NOT NULL,
    [ResolutionNumber]             VARCHAR (50)  NULL,
    [ResolutionDate]               DATE          NULL,
    [PosesionDate]                 DATE          NULL,
    [CertificateOfficeNumber]      VARCHAR (50)  NULL,
    [EmployeeId]                   INT           NOT NULL,
    [PositionId]                   INT           NOT NULL,
    [ContractTypeId]               INT           NOT NULL,
    [JobBondingDate]               DATE          NOT NULL,
    [ContractInitialDate]          DATE          NOT NULL,
    [ContractEndingDate]           DATE          NOT NULL,
    [BasicSalary]                  NUMERIC (18)  NOT NULL,
    [Status]                       TINYINT       NOT NULL,
    [RetirementReasonId]           TINYINT       NULL,
    [RetirementDate]               DATE          NULL,
    [PaymentPeriod]                TINYINT       NOT NULL,
    [PaymentType]                  TINYINT       NOT NULL,
    [TrialPeriod]                  BIT           NOT NULL,
    [TrialPeriodTime]              INT           NULL,
    [TrialPeriodSalaryPercentage]  INT           NULL,
    [ContractCreationDate]         DATETIME      NOT NULL,
    [CreationUserId]               VARCHAR (20)  NOT NULL,
    [ModificationDate]             DATETIME      NOT NULL,
    [TypeOfPensionContribution]    TINYINT       NOT NULL,
    [Valid]                        BIT           NOT NULL,
    [Notes]                        VARCHAR (255) NULL,
    [GroupId]                      INT           NOT NULL,
    [BankId]                       INT           NOT NULL,
    [BankAccountNumber]            VARCHAR (20)  NOT NULL,
    [BankAccountType]              INT           NOT NULL,
    [LiquidationPayroll]           TINYINT       NOT NULL,
    [ContractModificationReasonId] INT           NULL,
    [BaseIncome]                   DECIMAL (18)  NULL,
    [IncomeDailyBase]              INT           NULL,
    [HoursDaily]                   TINYINT       NOT NULL,
    [LastLiquidationDate]          DATE          NULL,
    [ModificationUserId]           VARCHAR (20)  NOT NULL,
    [LastModificationDate]         DATE          NOT NULL,
    [Contingency]                  TINYINT       CONSTRAINT [DF_Contract_Contingency] DEFAULT ((0)) NULL,
    [OrganizationChartPositionId]  INT           NULL,
    [PlazaPositionId]              INT           NOT NULL,
    [OrganizationalUnitId]         INT           NOT NULL,
    [WorkPlaceId]                  INT           NULL,
    [PositionsId]                  INT           NOT NULL,
    [TrialPeriodDate]              DATE          NULL,
    [ProfesionalRiskId]            INT           NULL,
    [ContractId]                   INT           NULL,
    [State]                        TINYINT       NULL,
    [HiringDocumentsId]            INT           NOT NULL,
    CONSTRAINT [PK_Contract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contract_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_Contract_ContractModificationReason] FOREIGN KEY ([ContractModificationReasonId]) REFERENCES [Payroll].[ContractModificationReason] ([Id]),
    CONSTRAINT [FK_Contract_ContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Payroll].[ContractType] ([Id]),
    CONSTRAINT [FK_Contract_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Contract_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_Contract_OrganizationChartPosition] FOREIGN KEY ([OrganizationChartPositionId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_Contract_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_Contract_RetirementReason] FOREIGN KEY ([RetirementReasonId]) REFERENCES [Payroll].[RetirementReason] ([Id]),
    CONSTRAINT [FK_PartialContract_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_PartialContract_HiringDocuments] FOREIGN KEY ([HiringDocumentsId]) REFERENCES [HumanTalent].[HiringDocuments] ([Id]),
    CONSTRAINT [FK_PartialContract_OrganizationalUnit] FOREIGN KEY ([OrganizationalUnitId]) REFERENCES [HumanTalent].[OrganizationalUnit] ([Id]),
    CONSTRAINT [FK_PartialContract_PlazaPosition] FOREIGN KEY ([PlazaPositionId]) REFERENCES [HumanTalent].[PlazaPosition] ([Id]),
    CONSTRAINT [FK_PartialContract_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id]),
    CONSTRAINT [FK_PartialContract_ProfessionalRisk] FOREIGN KEY ([ProfesionalRiskId]) REFERENCES [Payroll].[ProfessionalRisk] ([Id]),
    CONSTRAINT [FK_PartialContract_WorkPlace] FOREIGN KEY ([WorkPlaceId]) REFERENCES [HumanTalent].[WorkPlace] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_Contract_Bank];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_Contract_ContractType];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_Contract_Employee];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_Contract_Group];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_Contract_OrganizationChartPosition];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_Contract_Position];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_Contract_RetirementReason];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_PartialContract_Contract];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_PartialContract_HiringDocuments];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_PartialContract_OrganizationalUnit];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_PartialContract_PlazaPosition];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_PartialContract_Positions];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_PartialContract_ProfessionalRisk];


GO
ALTER TABLE [HumanTalent].[PartialContract] NOCHECK CONSTRAINT [FK_PartialContract_WorkPlace];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidicacion de los documentos de contratación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HiringDocumentsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del contrato: 1- parcial, 2- Contrato realizado (se crea tambien en payroll), 3- anulado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de l contrato de payroll, se almacena cuando se confirma el ingreso del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del nivel de riesgo profesional del human talent.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ProfesionalRiskId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha fin del periodo de prueba al empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la posicion de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del lugar de trabajo, de humantaTalent', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'WorkPlaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad organizativa de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la plaza de talento humano', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la posición del Organigrama. Únicamente se llena, cuando el sistema está integrado con Talento Humano', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contrato de Contigencia:  0 - Ninguna  1 - Licencias  2 - Vacaciones  3 - Incapacidades', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Contingency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultima fecha de liquidacion de nomina del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de horas laborales diarias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HoursDaily';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso base cotizacion diario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'IncomeDailyBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso base cotizacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BaseIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Razones de Modificación del Contrato (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractModificationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion nomina 1-Si 0-No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LiquidationPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cuenta Ahorros - Corriente', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero cuenta bancaria empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del banco donde tiene la cuenta de nomina (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Notas o Comentarios del Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Notes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica Si un Contrato es Vigente o No. 1-Vigente 0- No Solo debe haber un contrato vigente por empleado asi tenga dos contratos temporalmente activos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Valid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cotizante pension  1 - Normal  2 - Especial  3 - Alto riesgo  4 - Servidor publico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la cual entra en vigencia la modificaion del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CreationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractCreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del sueldo a pagar durante el periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodSalaryPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de periodo de pruebas en dias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta en periodo de prueba 1 - Si 0 - No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma de pago: 1 - Cheque 2 - Efectivo 3 - Consignacion 4 - Desprendible', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo de Pago: 1 Mensual - 2 Quincenal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de retiro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Motivo de Retiro (FK). En caso de que el usuario le hayan realizado Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Contrato:1-Activo  2-Liquidado 3-Anulado 4-Reemplazado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sueldo Basico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vinculacion laboral', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'JobBondingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id del tipo de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del nivel de cargo (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Acta Posesión', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CertificateOfficeNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Posesión (Únicamente para Entidades Públicas)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PosesionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Resolución', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Resolucion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nùmero Contrato Inicial o Base - Hace referencia al id del primer contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Registro 1- Contrato Base 2- Novedad Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RowType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Id';

