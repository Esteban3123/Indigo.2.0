CREATE TABLE [Payroll].[Novelty] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Consecutive]                   INT             NOT NULL,
    [GroupId]                       INT             NOT NULL,
    [EmployeeId]                    INT             NOT NULL,
    [TypeNovelty]                   TINYINT         NOT NULL,
    [Extension]                     TINYINT         NOT NULL,
    [IBC]                           NUMERIC (18)    NOT NULL,
    [EmployeeBaseSalary]            NUMERIC (18)    NOT NULL,
    [RealDate]                      DATE            NOT NULL,
    [Days]                          TINYINT         NOT NULL,
    [EndDate]                       DATE            NOT NULL,
    [VacationInitialDateNovelty]    DATE            NULL,
    [VacationEndDateNovelty]        DATE            NULL,
    [LiquidatedIBCorSalary]         TINYINT         NOT NULL,
    [Reason]                        VARCHAR (500)   NOT NULL,
    [LiquidationBase]               INT             NOT NULL,
    [EPSDays]                       TINYINT         NULL,
    [EmployerDays]                  TINYINT         NULL,
    [LicenseClass]                  TINYINT         NULL,
    [InabilityClass]                TINYINT         NULL,
    [RiskType]                      TINYINT         NULL,
    [NoveltyLiquidate]              BIT             NOT NULL,
    [CalculationType]               TINYINT         NULL,
    [EPSRecognizeValue]             NUMERIC (18, 2) NULL,
    [PaidPayrollValue]              NUMERIC (18, 2) NULL,
    [PaidEmployerValue]             NUMERIC (18, 2) NULL,
    [Value]                         NUMERIC (18, 2) NULL,
    [AutorizationNumber]            VARCHAR (30)    NULL,
    [ResolutionNumber]              VARCHAR (30)    NULL,
    [ResolutionDate]                DATE            NULL,
    [Status]                        TINYINT         CONSTRAINT [DF_Novelty_Status] DEFAULT ((0)) NOT NULL,
    [CreationUser]                  VARCHAR (20)    NOT NULL,
    [CreationDate]                  DATETIME        NOT NULL,
    [ModificationUser]              VARCHAR (20)    NULL,
    [ModificationDate]              DATETIME        NULL,
    [TimeStamp]                     ROWVERSION      NOT NULL,
    [IsCycleDate]                   BIT             DEFAULT ((0)) NOT NULL,
    [LiquidationBasePatrono]        DECIMAL (18, 2) CONSTRAINT [DF_Novelty_LiquidationBasePatrono] DEFAULT ((0)) NOT NULL,
    [IntegralSalaryDisabilityValue] DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Novelty__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Disability_Employee_EmployeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Disability_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[Novelty] NOCHECK CONSTRAINT [FK_Disability_Employee_EmployeId];


GO
ALTER TABLE [Payroll].[Novelty] NOCHECK CONSTRAINT [FK_Disability_Group_GroupId];


GO
CREATE NONCLUSTERED INDEX [IX_Novelty__EmployeeId]
    ON [Payroll].[Novelty]([EmployeeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la fecha real es el inicio de un ciclo de descuentos de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IsCycleDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la liquidacion 0 Normal  1 - Liquidada 2 - Parcialmente Liquidada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ResolutionDate', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la resolucion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Autorizacion EPS-ARP pora Descontar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'AutorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pagado Por el Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidEmployerValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pagado en Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidPayrollValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Reconocido Por La  EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSRecognizeValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Calculo: 1 - EPS (2/3 a partir de cuatro días); 2 - Patrono; 3 - Nómina (100% Todos los días); 4 - 90 días o más (90 días a 2/3, 91 o más al 50%); 5 - 2/3 Todos los días, EPS 3er día;7 - 180 Días o más, no se paga nómina ;8 - 50% Los primeros 3 dias; 9 - 50% todos los dias; 10 - No Liquida', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CalculationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquida Incapacidad 1->SI 0->NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'NoveltyLiquidate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Riego  1->Accidente De Trabajo 2->Enfermedad Ocupacional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RiskType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Incapacidad:  1- Ambulatoria   2 - Hospitalaria   3 - Maternidad   4 - Enfermedad Profesional  5 - Licencia de Paternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'InabilityClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase De Licencia:   1->Remunerada   2->NO Remunerada   3->Permiso  5->Calamidad Domestica  6->Licencia de Luto  7->Dia de la familia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LicenseClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployerDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base de Liquidación de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidationBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Reason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion con IBC o Suledo 1->IBC 2->Sueldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidatedIBCorSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final de la vacacion que se agrega al hacer el reajuste en vacaciones, esto se usa en casos como cuando el empleado esta en vacaciones y se tiene que hacer un cambio de fechas y su funcion principal es saber que fechas se tienen que eliminar cuando la novedad se elimina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationEndDateNovelty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial de la vacacion que se agrega al hacer el reajuste en vacaciones, esto se usa en casos como cuando el empleado esta en vacaciones y se tiene que hacer un cambio de fechas y su funcion principal es saber que fechas se tienen que eliminar cuando la novedad se elimina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationInitialDateNovelty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RealDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sueldo Base Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeBaseSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso Base Cotización', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prorroga: 1 Si - 0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Novedad 1- Incapacidad 2- Sancion 3 - Licencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TypeNovelty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Incapacidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Id';

