






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