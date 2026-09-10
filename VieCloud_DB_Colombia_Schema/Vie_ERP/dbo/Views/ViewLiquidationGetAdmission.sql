

-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 2018-04-27
-- Description:	Listado de Ingresos Abiertos
-- =============================================

CREATE View [dbo].[ViewLiquidationGetAdmission]	
As
	Select Distinct Ltrim(Rtrim(ING.NUMINGRES)) As AdmissionCode
		, Ing.IFECHAING As AdmissionDate
		, Ing.IPRNOMBRE As ResponsibleName
		, Pat.IPCODPACI As PatientCode
		, Ltrim(Rtrim(Pat.IPNOMCOMP)) As PatientName
		, Case Ing.TIPOINGRE 
			When 1 Then 'Ambulatorio' 
			When 2 Then 'Hospitalario'		   
		  End As AdmissionTypeName
		, Ltrim(Rtrim(Ing.CODICAMHO)) As BedStay
		, Ing.ILIQUIDAC As LiquidationType
		, Case Ing.IESTADOIN 
			When ' ' Then 'Abierto' 
			When 'P' Then 'Parcial' 
			When 'F' Then 'Facturado' 
			When 'C' Then 'Cerrado'
			When 'A' Then 'Anulado'
			when 'B' Then 'Bloqueado'
			Else '' 
		  End As StatusName
		, Ing.IESTADOIN As [Status]
		,RTRIM(LTRIM(Ing.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitCodeName
	From dbo.ADINGRESO As Ing With(Nolock)
	Inner Join dbo.INPACIENT As Pat With(Nolock) On Ing.IPCODPACI = Pat.IPCODPACI
	inner join dbo.INUNIFUNC fu With(Nolock) on fu.UFUCODIGO = Ing.UFUCODIGO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el listado de ingresos o admisiones de pacientes para el proceso de liquidación y facturación. Integra datos del ingreso (número de admisión, fecha, tipo —ambulatorio u hospitalario—, cama, tipo de liquidación y estado), la información del paciente (código/cédula y nombre completo) y la unidad funcional donde fue atendido. El estado del ingreso puede ser: Abierto, Parcial, Facturado, Cerrado, Anulado o Bloqueado, lo que la hace especialmente útil para gestionar la cartera de ingresos pendientes de facturar y para reportería del ciclo de liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewLiquidationGetAdmission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewLiquidationGetAdmission';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de ingresos/admisiones con datos del paciente, unidad funcional y traducción legible del tipo y estado del ingreso para apoyar procesos de liquidación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso debe tener un paciente existente en INPACIENT (INNER JOIN por IPCODPACI); Cada ingreso debe tener una unidad funcional existente en INUNIFUNC (INNER JOIN por UFUCODIGO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se muestran ingresos cuyo paciente y unidad funcional existen en sus catálogos maestros; Los códigos de admisión, paciente y unidad funcional se entregan sin espacios en blanco a los extremos (LTRIM/RTRIM); El nombre de la unidad funcional se entrega como ''CÓDIGO - DESCRIPCIÓN''; Los estados reconocidos son: Abierto, Parcial, Facturado, Cerrado, Anulado, Bloqueado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Paciente; Unidad funcional; Tipo de admisión (Ambulatorio/Hospitalario); Estado del ingreso; Liquidación; Cama (BedStay); Responsable del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve filas distintas de ingresos cruzadas con paciente y unidad funcional, traduciendo TIPOINGRE e IESTADOIN a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ing.TIPOINGRE = 1 → Se etiqueta el tipo de admisión como ''Ambulatorio'' else Si TIPOINGRE = 2 se etiqueta como ''Hospitalario''; si Ing.IESTADOIN = '' '' → Estado = ''Abierto'' else ''P''→Parcial; ''F''→Facturado; ''C''→Cerrado; ''A''→Anulado; ''B''→Bloqueado; cualquier otro valor → cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmission';
GO
