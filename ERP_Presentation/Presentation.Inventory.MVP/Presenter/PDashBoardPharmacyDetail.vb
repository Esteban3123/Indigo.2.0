'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán
' Created          : 10-02-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class PDashBoardPharmacyDetail

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IDashBoardPharmacyDetail

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IDashBoardPharmacyDetail)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Sub GetSequence()
        Using model As New MDashBoardPharmacy(Me.View.MyTag)
            Me.View.Sequence = model.GetSequense()
        End Using
    End Sub

    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer) As SettingsContractXpo
        Dim filter As String = "OperatingUnitId = " & operatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of SettingsContractXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' xpo para listar los motivos generales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListHCMOANULBXPInstant(Optional FilterType As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListHCMOANULBXPInstant(FilterType)
    End Function

    ''' <summary>
    ''' lista los ingresos en estado abierto y parcial
    ''' </summary>
    ''' <param name="FilterType"></param>
    ''' <returns></returns>
    Public Function ListAdmissions(Optional FilterType As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListGetAdmissionXpo()
    End Function

    ''' <summary>
    ''' funcion que obtiene la lista de las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListUF()
    End Function

    ''' <summary>
    ''' retorna la unidad funcional de cirugia, la primera que encuentre
    ''' </summary>
    Public Function FirstFunctionalSurgicalUnit() As INUNIFUNC
        Using model As New MDashBoardPharmacy(Me.View.MyTag)
            Return model.GetFunctionalUnitByType("19")
        End Using
    End Function
End Class
