CREATE TABLE [Payroll].[ElectronicPayrollConcepts] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (50)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [ConceptType]      INT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (50)  NULL,
    [ModificationDate] DATETIME      NULL,
    [State]            BIT           DEFAULT ((1)) NOT NULL,
    [InternalCode]     INT           DEFAULT ((-1)) NULL,
    CONSTRAINT [PK_ElectronicPayrollConcepts_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Concepto de Nómina Electrónica (BIT): 1=Activo, 0=Inactivo. Indica si el concepto está disponible para cálculo de nómina electrónica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Concepto de Nómina Electrónica 1- Activo 2- Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del concepto de nómina (DATETIME). Null si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del concepto (VARCHAR 50). Auditoría de cambios en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del concepto de nómina electrónica (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el concepto de nómina electrónica (VARCHAR 20). Trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Concepto de Nómina (INT): 1=Devengados (ingresos/ganancias), 2=Deducciones (descuentos), 3=No Aplica. Clasifica el concepto para cálculo de RIPS y liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Concepto: 1 = Devengados 2 = Deducciones 3 = No Aplica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del concepto de nómina electrónica (VARCHAR 100). Ej: Salario Base, AFP, EPS, Bonificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del concepto de nómina (VARCHAR 50). Referencia para integración RIPS y cálculo electrónico de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código secuencial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Code';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos de nómina electrónica: catálogo de los conceptos (devengados, deducciones y otros) que se reportan en la nómina electrónica ante la DIAN. Incluye el código, nombre, tipo de concepto y su estado activo/inactivo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del concepto de nómina electrónica (llave primaria, generada automáticamente).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo interno utilizado para empalmar con conceptos de la DIAN. El valor 0 = No Aplica (concepto no se reporta en nomina electronica).
0  = No aplica
1  = Salario Basico
2  = Auxilio de Transporte
3  = Viatico Manutencion y Alojamiento Salarial
4  = Viatico Manutencion y Alojamiento No Salarial
5  = Hora Extra Diurna
6  = Hora Extra Nocturna
7  = Hora Recargo Nocturno
8  = Hora Extra Diurna Dominical/Festivo
9  = Hora Recargo Diurno Dominical/Festivo
10 = Hora Extra Nocturna Dominical/Festivo
11 = Hora Recargo Nocturno Dominical/Festivo
12 = Vacaciones Comunes
13 = Vacaciones Compensadas / Provision
14 = Primas / Provision de Primas
15 = Cesantias / Provision
16 = Intereses de Cesantias
17 = Incapacidades (Comun, Profesional, Laboral)
18 = Licencia de Maternidad o Paternidad
19 = Licencia Remunerada
20 = Licencia No Remunerada
21 = Bonificacion Salarial (incluye Prima de Vacaciones, Incremento)
22 = Bonificacion No Salarial (Recreacion)
23 = Auxilio Salarial
24 = Auxilio No Salarial
25 = Huelga Legal
26 = Otro Concepto (Devengado)
27 = Compensacion Ordinaria
28 = Compensacion Extraordinaria
29 = Bono EPCTV (Bonos alimentacion, etc.)
30 = Comisiones
31 = Pago a Terceros (Devengado)
32 = Anticipos (Devengado)
33 = Dotacion
34 = Apoyo Sostenimiento (Contratos Aprendizaje)
35 = Teletrabajo
36 = Bonificacion por Retiro
37 = Indemnizacion
38 = Reintegro (Devengado)
39 = Aporte a Salud
40 = Aporte a Fondo de Pension
41 = Fondo de Solidaridad Pensional
42 = Fondo de Solidaridad Pensional - Subsistencia
43 = Sindicatos
44 = Sanciones
45 = Libranza
46 = Pago a Terceros (Deduccion)
47 = Anticipos (Deduccion)
48 = Otras Deducciones
49 = Aportes Voluntarios a Pension
50 = Retencion en la Fuente
51 = Aportes a Fondos AFC
52 = Cooperativa
53 = Embargo Fiscal
54 = Planes Complementarios
55 = Educacion
56 = Reintegro (Deduccion)
57 = Deuda', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'InternalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'manual_oastudillo@indigo.tech_2026-06-12', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'InternalCode';
