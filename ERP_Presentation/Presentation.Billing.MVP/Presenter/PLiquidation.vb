'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan F. Tamayo
' Created          : 2014-11-10
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Security.MVP

#End Region

Public Class PLiquidation

#Region "Fields"

    ''' <summary>
    ''' Referencia a la vista del frontal
    ''' </summary>
    Private _view As ILiquidation

    ''' <summary>
    ''' Valores de sesión
    ''' </summary>
    Dim _indigoSessionValues As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="iView">Referencia a la vista del frontal</param>
    Public Sub New(ByRef iView As ILiquidation)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = iView
        _indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la lista de unidades operativas
    ''' </summary>
    Public Async Function LoadListOperatingUnit() As Task
        'Using model As New Presentation.Controls.MVP.MformBase()
        '    Me._view.ListOperatingUnit = Await model.GetOperatingUnitByContainerPermission(SessionValues.Instance.IndigoContainerId)
        'End Using
        Using model = New MUsuario
            Dim _Company = UnifiedConfiguration.Instance.ListCompanies.Where(Function(c) c.Id = SessionValues.Instance.IndigoContainerId).FirstOrDefault
            Me._view.ListOperatingUnit = Await model.GetOperatingUnitByContainerPermission(SessionValues.Instance.IndigoContainerId, SessionValues.Instance.UserType, SessionValues.Instance.UserIndigo, _Company.Administrator)
        End Using
    End Function

    ''' <summary>
    ''' Carga la lista de resoluaciones de facturación
    ''' </summary>
    Public Async Sub LoadListBillingAuthorization()
        Using model As New MLiquidation()
            Me._view.ListBillingAuthorization = Await model.ListBillingAuthorizationByUserCode(SessionValues.Instance.UserIndigo)
        End Using
    End Sub

    ''' <summary>
    ''' Carga los permisos asignados al usuario en éste formulario
    ''' </summary>
    Public Sub LoadPermissionsForm()
        Using model As New MLiquidation()
            Dim permissions = model.GetPermissions(Me._view.MyTag)
            Me._view.PermissionsForm = (From a In permissions Select Action = a.TagButton, Name = [Enum].GetName(GetType(PermissionsActionsForm), a.TagButton)).ToDictionary(Function(x) x.Action, Function(y) y.Name)
        End Using
    End Sub

    Public Function GetAdmissionByNumber(number As String) As ViewAdmissionsToLiquidationConfirm
        Dim criteria As String = "AdmissionCode = " & number
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetCollection(Of ViewAdmissionsToLiquidationConfirm)(Nothing, criteria).FirstOrDefault()
    End Function

    Public Function GetOpenAdmissionByNumber(number As String) As ViewAdmissionsToLiquidation
        Dim criteria As String = "AdmissionCode = " & number
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetCollection(Of ViewAdmissionsToLiquidation)(Nothing, criteria).FirstOrDefault()
    End Function

    Public Async Function LoadFlagTaxInclude() As Task
        Using Model As New MServiceOrder("")
            Dim CompanySettings = Await Model.CompanySettings
            Me._view.FlagTaxInclude = CompanySettings.SalePriceIncludeTax
        End Using
    End Function

#End Region

End Class
