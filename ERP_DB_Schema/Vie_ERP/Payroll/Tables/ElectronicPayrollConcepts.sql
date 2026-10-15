CREATE TABLE [Payroll].[ElectronicPayrollConcepts] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Code]                VARCHAR (50)  NOT NULL,
    [Name]                VARCHAR (100) NOT NULL,
    [ElectronicPayrollId] VARCHAR (6)   NOT NULL,
    [ConceptType]         INT           NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (50)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [State]               BIT           DEFAULT ((1)) NOT NULL,
	[InternalCode] INT                  NULL,
    CONSTRAINT [PK_ElectronicPayrollConcepts_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Concepto de Nómina Electrónica 1- Activo 2- Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Concepto: 1 = Devengados 2 = Deducciones 3 = No Aplica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Nómina Electrónica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código secuencial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConcepts', @level2type = N'COLUMN', @level2name = N'Code';

GO
EXECUTE sys.sp_addextendedproperty @name=N'MS_Description',
@value=N'Codigo interno utilizado para empalmar con conceptos de la DIAN
1	=	Salario Básico
2	=	Auxilio de Transporte
3	=	Viático Manutención y Alojamiento Salaria
4	=	Viático Manutención y Alojamiento No Salarial / Primas
5	=	Hora Extra Diurna
6	=	Hora Extra Nocturna
7	=	Hora Recargo Nocturno
8	=	Hora Extra Diurna Dominical/Festivo
9	=	Hora Recargo Diurno Dominical/Festivo
10	=	Hora Extra Nocturna Dominical/Festivo
11	=	Hora Recargo Nocturno Dominical/Festivo
12	=	Vacaciones Comunes
13	=	Vacaciones Compensadas / Provisión
14	=	Primas / Provisión de Primas
15	=	Cesantías / Provisión
16	=	Intereses de Cesantías
17	=	Incapacidades (Común, Profesional, Laboral)
18	=	Licencia de Maternidad o Paternidad
19	=	Licencia Remunerada
20	=	Licencia No Remunerada
21	=	Bonificación Salarial (incluye Prima de Vacaciones, Incremento)
22	=	Bonificación No Salarial (Recreación)
23	=	Auxilio Salarial
24	=	Auxilio No Salarial
25	=	Huelga Legal
26	=	Otro Concepto (Devengado)
27	=	Compensación Ordinaria
28	=	Compensación Extraordinaria
29	=	Bono EPCTV (Bonos alimentación, etc.)
30	=	Comisiones
31	=	Pago a Terceros (Devengado)
32	=	Anticipos (Devengado)
33	=	Dotación
34	=	Apoyo Sostenimiento (Contratos Aprendizaje)
35	=	Teletrabajo
36	=	Bonificación por Retiro
37	=	Indemnización
38	=	Reintegro (Devengado)
39	=	Aporte a Salud
40	=	Aporte a Fondo de Pensión
41	=	Fondo de Solidaridad Pensional
42	=	Fondo de Solidaridad Pensional - Subsistencia
43	=	Sindicatos
44	=	Sanciones
45	=	Libranza
46	=	Pago terceros (Deducción)
47	=	Anticipos (Deducción)
48	=	Otras Deducciones
49	=	Aportes Voluntarios a Pensión
50	=	Retención en la Fuente
51	=	Aportes a Fondos AFC
52	=	Cooperativa
53	=	Embargo Fiscal
54	=	Planes Complementarios
55	=	Educación
56	=	Reintegro (Deducción)
57	=	Deuda
' ,
@level0type=N'SCHEMA',
@level0name=N'Payroll', 
@level1type=N'TABLE',
@level1name=N'ElectronicPayrollConcepts',
@level2type=N'COLUMN',
@level2name=N'InternalCode';