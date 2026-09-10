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
    [EpsDaysAlreadyLiquidated]      INT             NULL,
    CONSTRAINT [PK_Novelty__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Disability_Employee_EmployeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Disability_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);






GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Novelty__EmployeeId]
    ON [Payroll].[Novelty]([EmployeeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si la fecha real marca el inicio de un ciclo de descuentos o liquidación de nómina; usado para control de períodos de descuento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IsCycleDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la fecha real es el inicio de un ciclo de descuentos de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IsCycleDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IsCycleDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que registra el instante exacto de creación, modificación o cambio de estado del registro de novedad en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de novedad; nula si no ha sido modificada desde su creación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR) del usuario que realizó la última modificación del registro; referencia a cuenta de sistema o usuario de aplicación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro de novedad en el sistema; marca el origen del documento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR) del usuario que creó el registro de novedad; trazabilidad de origen del documento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) de la novedad: 0=Normal/Pendiente, 1=Liquidada totalmente, 2=Liquidada parcialmente; controla flujo de procesamiento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la liquidacion 0 Normal  1 - Liquidada 2 - Parcialmente Liquidada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) de emisión de la resolución administrativa que autoriza o formaliza la novedad (incapacidad, licencia, sanción).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ResolutionDate', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único (VARCHAR) de la resolución administrativa que respalda la novedad; identificador legal/normativo del acto.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la resolucion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización (VARCHAR) emitido por EPS o ARP para descontar prestaciones de salud o riesgos laborales; requerido para validar derechos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'AutorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Autorizacion EPS-ARP pora Descontar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'AutorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'AutorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC) total de la novedad a liquidar o compensar; monto principal del concepto (incapacidad, sanción, licencia).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (NUMERIC) pagado por el patrono/empresa para cubrir la novedad; corresponde a obligaciones del empleador en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidEmployerValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pagado Por el Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidEmployerValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidEmployerValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (NUMERIC) pagado en nómina al empleado por concepto de novedad; descuento o abono en proceso de liquidación salarial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidPayrollValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Pagado en Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidPayrollValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'PaidPayrollValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (NUMERIC) reconocido y pagado por la entidad promotora de salud (EPS) por incapacidad o licencia de maternidad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSRecognizeValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Reconocido Por La  EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSRecognizeValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSRecognizeValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cálculo (TINYINT) de liquidación: 1=EPS 2/3 (4+ días), 2=Patrono 100%, 3=Nómina 100%, 4=90+ días progresivo, 5=2/3 todos días EPS 3er, 7=180+ sin nómina, 8=50% 3 primeros, 9=50% todos, 10=No liquida.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CalculationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Calculo: 1 - EPS (2/3 a partir de cuatro días); 2 - Patrono; 3 - Nómina (100% Todos los días); 4 - 90 días o más (90 días a 2/3, 91 o más al 50%); 5 - 2/3 Todos los días, EPS 3er día;7 - 180 Días o más, no se paga nómina ;8 - 50% Los primeros 3 dias; 9 - 50% todos los dias; 10 - No Liquida', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CalculationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'CalculationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que determina si se liquida la incapacidad o novedad en nómina: 1=Sí liquida, 0=No liquida; controla inclusión en descuentos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'NoveltyLiquidate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquida Incapacidad 1->SI 0->NO', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'NoveltyLiquidate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'NoveltyLiquidate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación (TINYINT) del tipo de riesgo laboral: 1=Accidente de trabajo, 2=Enfermedad ocupacional; determina cobertura EPS/ARP.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RiskType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Riego  1->Accidente De Trabajo 2->Enfermedad Ocupacional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RiskType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RiskType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de incapacidad/descanso (TINYINT): 1=Ambulatoria, 2=Hospitalaria, 3=Maternidad, 4=Enfermedad profesional, 5=Licencia paternidad; categoriza tipo de reposo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'InabilityClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Incapacidad:  1- Ambulatoria   2 - Hospitalaria   3 - Maternidad   4 - Enfermedad Profesional  5 - Licencia de Paternidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'InabilityClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'InabilityClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de licencia (TINYINT): 1=Remunerada, 2=No remunerada, 3=Permiso, 5=Calamidad doméstica, 6=Luto, 7=Día familia; clasifica ausencia justificada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LicenseClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase De Licencia:   1->Remunerada   2->NO Remunerada   3->Permiso  5->Calamidad Domestica  6->Licencia de Luto  7->Dia de la familia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LicenseClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LicenseClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días (TINYINT) que corre a cargo del patrono en la novedad; determina obligación económica del empleador.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployerDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días Patrono', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployerDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployerDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días (TINYINT) cubiertos por la entidad de salud (EPS) en incapacidad o maternidad; define cobertura aseguradora.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EPSDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de cálculo (INT) para liquidar la novedad; referencia a tabla/concepto base de descuento o pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidationBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base de Liquidación de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidationBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidationBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 500) del motivo o justificación de la novedad; incluye diagnóstico, causal o explicación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Reason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Reason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Reason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT) de base para cálculo: 1=IBC (Ingreso Base Cotización), 2=Salario base; determina monto a considerar en liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidatedIBCorSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion con IBC o Suledo 1->IBC 2->Sueldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidatedIBCorSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidatedIBCorSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final (DATE) de las vacaciones ajustadas/extendidas por novedad; marca término de período a eliminar si se revierte la novedad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationEndDateNovelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de la vacacion que se agrega al hacer el reajuste en vacaciones, esto se usa en casos como cuando el empleado esta en vacaciones y se tiene que hacer un cambio de fechas y su funcion principal es saber que fechas se tienen que eliminar cuando la novedad se elimina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationEndDateNovelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationEndDateNovelty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial (DATE) de las vacaciones ajustadas/extendidas por novedad; marca inicio de período a eliminar si se revierte.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationInitialDateNovelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de la vacacion que se agrega al hacer el reajuste en vacaciones, esto se usa en casos como cuando el empleado esta en vacaciones y se tiene que hacer un cambio de fechas y su funcion principal es saber que fechas se tienen que eliminar cuando la novedad se elimina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationInitialDateNovelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'VacationInitialDateNovelty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin (DATE) del período de la novedad; término del período de incapacidad, licencia o sanción.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Fin Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de días (TINYINT) que abarca la novedad; duración del evento (incapacidad, licencia, sanción).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio (DATE) de la novedad; primer día efectivo del evento (incapacidad, licencia o sanción).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RealDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RealDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'RealDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario base (NUMERIC) del empleado en el período de la novedad; monto referencial para cálculos de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeBaseSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sueldo Base Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeBaseSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeBaseSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso Base de Cotización (NUMERIC); base para aportes a seguridad social, usado en cálculo de prestaciones y descansos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso Base Cotización', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT) de prórroga: 1=Sí es prórroga/extensión, 0=No; señala si es continuación de novedad anterior.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prorroga: 1 Si - 0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de novedad (TINYINT): 1=Incapacidad (licencia médica), 2=Sanción disciplinaria, 3=Licencia (permiso/calamidad); clasifica concepto.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TypeNovelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Novedad 1- Incapacidad 2- Sancion 3 - Licencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TypeNovelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'TypeNovelty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del empleado afectado por la novedad; referencia a tabla Payroll.Employee.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del grupo/nómina al que pertenece la novedad; referencia a tabla Payroll.Group para clasificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo (INT) secuencial de la incapacidad o novedad; orden de registro dentro del empleado/período.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Incapacidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) autoincrementable de la novedad; clave primaria de la tabla para integridad referencial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedades de nómina de empleados: incapacidades, licencias, vacaciones y otros eventos que afectan la liquidación del período, incluyendo días, valores pagados por la EPS y el empleador, y bases de cálculo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de liquidación a cargo del empleador (patrón) para el cálculo de la novedad; monto sobre el cual se determina el valor que debe asumir la empresa.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidationBasePatrono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'LiquidationBasePatrono';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la incapacidad calculado sobre salario integral; aplica cuando el empleado devenga salario integral y la incapacidad se liquida con base en ese esquema salarial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IntegralSalaryDisabilityValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IntegralSalaryDisabilityValue';
