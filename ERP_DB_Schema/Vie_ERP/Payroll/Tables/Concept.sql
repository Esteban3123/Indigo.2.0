CREATE TABLE [Payroll].[Concept] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (4)   NOT NULL,
    [ConceptClass]              CHAR (3)      NOT NULL,
    [ConceptType]               TINYINT       NOT NULL,
    [Name]                      VARCHAR (50)  NOT NULL,
    [AffectIBC]                 BIT           CONSTRAINT [DF_Concept_AffectIBC] DEFAULT ((0)) NULL,
    [AffectIBCRTF]              BIT           CONSTRAINT [DF_Concept_AffectIBCRTF] DEFAULT ((0)) NULL,
    [AffectIBCSENA]             BIT           CONSTRAINT [DF_Concept_AffectIBCSENA] DEFAULT ((0)) NULL,
    [AffectIBCICBF]             BIT           CONSTRAINT [DF_Concept_AffectIBCICBF] DEFAULT ((0)) NULL,
    [AffectIBCCompensationFund] BIT           CONSTRAINT [DF_Concept_AffectIBCCompensationFund] DEFAULT ((0)) NULL,
    [AffectIBCSeverance]        BIT           CONSTRAINT [DF_Concept_AffectIBCSeverance] DEFAULT ((0)) NULL,
    [AffectIBCHealth]           BIT           CONSTRAINT [DF_Concept_AffectIBCHealth] DEFAULT ((0)) NULL,
    [AffectIBCPension]          BIT           CONSTRAINT [DF_Concept_AffectIBCPension] DEFAULT ((0)) NULL,
    [AffectIBCARP]              BIT           CONSTRAINT [DF_Concept_AffectIBCARP] DEFAULT ((0)) NULL,
    [AffectIBCVacation]         BIT           CONSTRAINT [DF_Concept_AffectIBCVacation] DEFAULT ((0)) NULL,
    [AffectIBCIncentivePayment] BIT           CONSTRAINT [DF_Concept_AffectIBCIncentivePayment] DEFAULT ((0)) NULL,
    [AffectRetroactive]         BIT           CONSTRAINT [DF_Concept_AffectRetroactive] DEFAULT ((0)) NULL,
    [Formulates]                VARCHAR (MAX) NOT NULL,
    [State]                     BIT           NOT NULL,
    [CreationUser]              VARCHAR (20)  NOT NULL,
    [CreationDate]              DATETIME      NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    [IdAdjustmentConcept]       INT           NULL,
	[IdElectronicPayrollConcepts]   INT       NULL,
	[IdElectronicPayrollConceptSubtype] INT  NULL,
    [AffectLimit40Law1393]              BIT           CONSTRAINT [DF_Concept_AffectLimit40Law1393] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Concept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Concept_Concept] FOREIGN KEY ([IdAdjustmentConcept]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[Concept] NOCHECK CONSTRAINT [FK_Concept_Concept];

GO
ALTER TABLE [Payroll].[Concept] ADD CONSTRAINT [FK_Concept_ElectronicPayrollConceptSubtype] FOREIGN KEY([IdElectronicPayrollConceptSubtype]) REFERENCES [Payroll].[ElectronicPayrollConceptSubtype] ([Id]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Concept__Code]
    ON [Payroll].[Concept]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Ajuste (Se utilizará para Recargos y Horas Extras)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'IdAdjustmentConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Formulates';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Retroactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectRetroactive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC Primas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC ARP', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCARP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCPension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Affecta IBC Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCHealth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSeverance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC Caja de Compensación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCCompensationFund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC ICBF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCICBF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC SENA', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCSENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC RTF', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBCRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta IBC', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'AffectIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto 1-Devengado 2-Deducido 3-Patronal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define la Clase de Concepto:  
001 - Hora Extra Ordinaria Diurna  
002 - Primas de Servicios  
003 - Otras Primas  
004 - Bonificación por Servicios  
005 - Sueldo  
006 - Auxilio de Transporte  
007 - Indemnizaciones  
008 - Provisión Cesantías  
009 - Aporte Riesgos Profesionales  
010 - Otros Devengados  
011 - Otros Deducidos  
012 - Hora Extra Ordinaria Nocturna  
013 - Hora Extra Dominical Diurna  
014 - Pensión Empleado  
015 - Pensión Patrono  
016 - Pensión Voluntaria  
017 - Salud Empleado  
018 - Salud Patrono  
019 - Salud Voluntaria  
020 - Retención  
021 - Incapacidad Ambulatoria  
022 - Incapacidad Hospitalaria  
023 - Maternidad  
024 - Licencias  
025 - Licencia No Remunerada  
026 - Sanción  
027 - Incapacidad Riesgos Profesionales  
028 - Permisos  
030 - Vacaciones  
031 - Provisión Vacaciones  
033 - Provisión Primas  
034 - Provisión Interés de Cesantías  
035 - Parafiscal Sena  
036 - Parafiscal Caja  
037 - Parafiscal ICBF  
038 - Aporte Fondo Seguridad Pensional  
041 - Convenios  
042 - Recargo Nocturno Normal  
043 - Recargo Nocturno Festivo  
044 - Sindicato  
045 - Cuentas AFC  
046 - Bonificación por Año de Servicio  
047 - Gastos de Representación  
048 - Ajuste Retención en la Fuente  
049 - Prima de Vacaciones  
050 - Hora Extra Dominical Nocturna  
051 - Recargo Diurno Festivo  
052 - Recargo Diurno Dominical  
053 - Embargos  
054 - Viáticos  
055 - Bonificación Salarial  
056 - Provisión Bonificación Año Servicio  
057 - Provisión Incremento Vacacional  
058 - Provisión Bonificación Especial Recreación  
059 - Provisión Prima Vacaciones  
060 - Provisión Prima Navidad  
061 - Retención de Indemnización  
062 - Auxilio de Alimentos  
063 - Bonificación Especial para Recreación  
064 - Incremento Vacacional  
065 - Bonificación No Salarial  
066 - Apoyo a Sostenimiento
067 - Incapacidad Ambulatoria Patrono
068 - Incapacidad Ambulatoria ERP
069 - Incapacidad Hospitalaria Patrono
070 - Incapacidad Hospitalaria ERP
071 - Calamidad domestica
072 - Licencia Remunerada
073 - Vacaciones en Dinero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'ConceptClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Concept', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name=N'MS_Description',@value=N'id Relacion Payroll.ElectronicPayrollConcepts' ,@level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'Concept',@level2type=N'COLUMN',@level2name=N'IdElectronicPayrollConcepts';


GO
EXECUTE sp_addextendedproperty @name=N'MS_Description',@value=N'id Relacion Payroll.ElectronicPayrollConceptSubtype' ,@level0type=N'SCHEMA',@level0name=N'Payroll', @level1type=N'TABLE',@level1name=N'Concept',@level2type=N'COLUMN',@level2name=N'IdElectronicPayrollConceptSubtype';

