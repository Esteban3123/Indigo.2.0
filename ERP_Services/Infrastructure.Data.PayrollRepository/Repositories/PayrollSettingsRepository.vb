' Assembly         : Infrastructure.Data.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 27-01-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Payroll

#End Region

Public Class PayrollSettingsRepository

    Inherits GenericRepository(Of PayrollSettings)
    Implements IPayrollSettingsRepository, Inject

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtengo los Parámetros de Nómina
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns>PayrollSettings</returns>
    ''' <remarks></remarks>
    Public Function GetSettingPayroll(Optional tracking As Boolean = True) As PayrollSettings Implements IPayrollSettingsRepository.GetSettingPayroll
        Dim res As New PayrollSettings()
		If tracking Then
			res = (From d As PayrollSettings In Me._context.PayrollSettings.Include("ConceptNoveltyAdjust").Include("NoteConcepts")
				   Select d).FirstOrDefault
			If res IsNot Nothing Then
				Dim ThirdParty = (From c As ThirdParty In Me._context.ThirdParty.AsNoTracking() Where c.Id = res.PayrollChiefThirdPartyId Select c).FirstOrDefault

				If ThirdParty IsNot Nothing Then
					res.NameThirdPartyPayrollSettings = ThirdParty.Nit + " - " + ThirdParty.Name
				End If

				If res.ConceptNoveltyAdjust IsNot Nothing AndAlso res.ConceptNoveltyAdjust.Count > 0 Then
					For Each objConceptNovelty As ConceptNoveltyAdjust In res.ConceptNoveltyAdjust

						Dim ObjConceptPrincipal = (From c As Concept In Me._context.Concept.AsNoTracking() Where c.Id = objConceptNovelty.IdConcept Select c).FirstOrDefault
						objConceptNovelty.NameConceptPrincipal = ObjConceptPrincipal.Name

						Dim ObjConceptAdjust = (From c As Concept In Me._context.Concept.AsNoTracking() Where c.Id = objConceptNovelty.IdAdjustConcept Select c).FirstOrDefault
						objConceptNovelty.NameConceptAdjust = ObjConceptAdjust.Name

					Next

				End If
				Dim ExistLiquidation = (From C As Liquidation In Me._context.Liquidation.AsNoTracking() Where C.RegisterStatus = "C" Select C).FirstOrDefault

				res.CurrencyFieldEnabled = (ExistLiquidation IsNot Nothing AndAlso ExistLiquidation.Id > 0)

			End If
		Else
			res = (From d As PayrollSettings In Me._context.PayrollSettings.AsNoTracking()
				   Select d).FirstOrDefault
			'If res IsNot Nothing Then
			'    Dim ThirdParty = (From c As ThirdParty In Me._context.ThirdParty.AsNoTracking() Where c.Id = res.PayrollChiefThirdPartyId Select c).FirstOrDefault
			'    res.NameThirdPartyPayrollSettings = ThirdParty.Nit + " - " + ThirdParty.Name

			'End If
		End If
		Return res

        'End If
    End Function
End Class
